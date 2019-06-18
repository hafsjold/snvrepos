using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using NCrontab;
using Pbs3060;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Uniconta.API.System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace MedlemServicePuls3060
{
    public enum enumTask
    {
        ReceiveFilesFromPBS = 1,
        ProcessType602Files,
        ProcessType603Files,
        SendKontingentFileToPBS,
        LoadSchedule,
        KontingentNyeMedlemmer,
        SendEmailRykker,
        UpdateMedlemStatus,
        SendEmailAdvis,
        UpdateKanSlettes,
        JobQMaintenance,
        SendEmailKviteringer,
        OpdaterTransUserData,
        ImportEmailBilag,
    }

    public class clsAppQueue
    {
        const string ServiceBusConnectionString = "Endpoint=sb://medlemservicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=CfRUJ/uGHdBsYKU0psjtjopHaEv15SvT40bi2rpPeoQ=";
        const string QueueName = "medlemqueue";
        public static IQueueClient queueClient;
        public static DateTime ScheduledEnqueueTimeUtc;
        public static string DTFormat = "dd/MM/yyyy HH:mm:ss zz";
        public Logger Log;

        //*************************************************************************************************************
        //*************************************************************************************************************
        public clsAppQueue()
        {
            Log = LogManager.GetLogger("databaseLogger");
            Log.Log(LogLevel.Info, "Medlem3060Service start");

            try
            {
                UCInitializer.UnicontaLogin();
            }
            catch { }

            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);
            Schedule(); 

            var cts = new CancellationTokenSource();
            this.ReceiveMessagesAsync(ServiceBusConnectionString, QueueName, cts.Token).Wait();
        }

        //*************************************************************************************************************
        //*************************************************************************************************************
        async Task ReceiveMessagesAsync(string connectionString, string queueName, CancellationToken cancellationToken)
        {
            var receiver = new MessageReceiver(connectionString, queueName, ReceiveMode.PeekLock);
            cancellationToken.Register(() => receiver.CloseAsync());
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    // ask for the next message "forever" or until the cancellation token is triggered
                    lock (Console.Out) Console.WriteLine("  {0} - Receiving message from Queue...", DateTime.Now.ToString(DTFormat));

                    var message = await receiver.ReceiveAsync();


                    if (message != null)
                    {
                        ScheduledEnqueueTimeUtc = message.ScheduledEnqueueTimeUtc;

                        if (Enum.IsDefined(typeof(enumTask), message.Label))
                        {
                            await receiver.CompleteAsync(message.SystemProperties.LockToken);
                            lock (Console.Out) Console.WriteLine("{0} - Start Job: {1}", ScheduledEnqueueTimeUtc.ToString(DTFormat), message.Label);
                            JobWorker(message.Label);
                        }
                        else
                        {
                            lock (Console.Out) Console.WriteLine("{0} - Error Job: {1}", ScheduledEnqueueTimeUtc.ToString(DTFormat), message.Label);
                        }
                    }
                }
                catch (ServiceBusException e)
                {
                    if (!e.IsTransient)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            await receiver.CloseAsync();
        }

        //*************************************************************************************************************
        //*************************************************************************************************************
        private void JobWorker(string jobname)
        {
            try
            {
                if (Enum.IsDefined(typeof(enumTask), jobname))
                {
                    Console.WriteLine(string.Format("Medlem3060Service {0} begin", jobname));
                    dbData3060DataContext m_dbData3060 = new dbData3060DataContext();
                    enumTask job = StringToEnum<enumTask>(jobname);
                    switch (job)
                    {
                        case enumTask.ReceiveFilesFromPBS:
                            CrudAPI api = UCInitializer.GetBaseAPI;
                            clsSFTP objSFTP = new clsSFTP(m_dbData3060);
                            int AntalImportFiler = objSFTP.ReadFraSFtp(m_dbData3060);  //Læs direkte SFTP
                            objSFTP.DisconnectSFtp();
                            objSFTP = null;

                            clsPbs602 objPbs602 = new clsPbs602();
                            int Antal602Filer = objPbs602.betalinger_fra_pbs(m_dbData3060);
                            //if (Antal602Filer > 0)  // <<----------------------------
                            {
                                Console.WriteLine(string.Format("Medlem3060Service {0} begin", "Betalinger til RSMembership"));
                                objPbs602.betalinger_opdate_uniconta(m_dbData3060, api);
                                Console.WriteLine(string.Format("Medlem3060Service {0} end", "Betalinger til RSMembership"));

                                clsUniconta objSumma = new clsUniconta(m_dbData3060, api);
                                int AntalBetalinger = objSumma.BogforIndBetalinger();
                            }
                            objPbs602 = null;

                            clsPbs603 objPbs603 = new clsPbs603();
                            int Antal603Filer = objPbs603.aftaleoplysninger_fra_pbs(m_dbData3060);
                            objPbs603 = null;

                            clsPbs686 objPbs686 = new clsPbs686();
                            int Antal686Filer = objPbs686.aftaleoplysninger_fra_pbs(m_dbData3060);
                            objPbs686 = null;
                            break;

                        case enumTask.SendEmailAdvis:
                            clsPbs601 objPbs601a = new clsPbs601();
                            CrudAPI aapi = UCInitializer.GetBaseAPI;
                            Tuple<int, int> tresult = objPbs601a.advis_auto(m_dbData3060);
                            int AntalAdvis = tresult.Item1;
                            int lobnra = tresult.Item2;
                            if ((AntalAdvis > 0))
                                objPbs601a.advis_email_lobnr(m_dbData3060, lobnra, aapi);
                            objPbs601a = null;
                            break;

                        case enumTask.ProcessType602Files:
                            break;

                        case enumTask.ProcessType603Files:
                            break;

                        case enumTask.SendKontingentFileToPBS:
                            CrudAPI dapi = UCInitializer.GetBaseAPI;
                            clsPbsHelper objPbsHelperd = new clsPbsHelper();
                            objPbsHelperd.PbsAutoKontingent(m_dbData3060, dapi);
                            objPbsHelperd = null;
                            break;

                        case enumTask.LoadSchedule:
                            Schedule();
                            break;

                        case enumTask.KontingentNyeMedlemmer:
                            CrudAPI capi = UCInitializer.GetBaseAPI;
                            clsPbs601 objPbs601c = new clsPbs601();
                            Tuple<int, int> tresultc = objPbs601c.pending_rsform_indmeldelser(m_dbData3060, capi);
                            int AntalKontingent = tresultc.Item1;
                            int lobnrc = tresultc.Item2;
                            if ((AntalKontingent > 0))
                            {
                                //pbsType.indbetalingskort
                                objPbs601c.faktura_og_rykker_601_action_lobnr(m_dbData3060, lobnrc, capi);
                                clsSFTP objSFTPc = new clsSFTP(m_dbData3060);
                                objSFTPc.WriteTilSFtp(m_dbData3060, lobnrc);
                                objSFTPc.DisconnectSFtp();
                                objSFTPc = null;

                                Tuple<int, int> tresultd = objPbs601c.advis_auto_lobnr(m_dbData3060, lobnrc);
                                int AntalAdvisd = tresultd.Item1;
                                int lobnrd = tresultd.Item2;
                                if ((AntalAdvisd > 0))
                                    objPbs601c.advis_email_lobnr(m_dbData3060, lobnrd, capi);
                                objPbs601c = null;
                            }
                            break;

                        case enumTask.SendEmailRykker:
                            clsPbs601 objPbs601b = new clsPbs601();
                            CrudAPI bapi = UCInitializer.GetBaseAPI;
                            Tuple<int, int> tresultb = objPbs601b.rykker_auto(m_dbData3060, bapi);
                            int AntalRykker = tresultb.Item1;
                            int lobnrb = tresultb.Item2;
                            if ((AntalRykker > 0))
                                objPbs601b.rykker_email_lobnr(m_dbData3060, lobnrb, bapi);
                            objPbs601b = null;
                            break;

                        case enumTask.UpdateMedlemStatus:
                            break;

                        case enumTask.JobQMaintenance:
                            JobQMaintenance();
                            break;

                        case enumTask.SendEmailKviteringer:
                            clsPbs601 objPbs601d = new clsPbs601();
                            CrudAPI apif = UCInitializer.GetBaseAPI;
                            Tuple<int, int> tresultf = objPbs601d.kvitering_auto(m_dbData3060);
                            int AntalKvit = tresultf.Item1;
                            int lobnrf = tresultf.Item2;
                            if ((AntalKvit > 0))
                                objPbs601d.kvitering_email_lobnr(m_dbData3060, lobnrf, apif);
                            objPbs601d = null;
                            break;

                        case enumTask.ImportEmailBilag:
                            CrudAPI apig = UCInitializer.GetBaseAPI;
                            clsUnicontaHelp objUnicontaHelp = new clsUnicontaHelp(apig);
                            objUnicontaHelp.ImportEmailBilag();
                            objUnicontaHelp = null;
                            break;

                        default:
                            break;
                    }
                }
                Console.WriteLine(string.Format("Medlem3060Service {0} end", jobname));
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("Medlem3060Service JobWorker() for {0} failed with message: {1}", jobname, e.Message));
            }
        }

        private T StringToEnum<T>(string name)
        {
            return (T)Enum.Parse(typeof(T), name);
        }
        
        //*************************************************************************************************************
        //*************************************************************************************************************
        private void JobQMaintenance()
        {
            using (dbJobQEntities db = new dbJobQEntities())
            {
                int count = db.Database.ExecuteSqlCommand(@"DELETE FROM dbo.tblJobqueue WHERE starttime < DATEADD(HOUR,-12,GETDATE())");
            }
        }
        
        //*************************************************************************************************************
        //*************************************************************************************************************
        public void Schedule()
        {
            List<localSchedul> ls = new List<localSchedul>();
            ls.Add(new localSchedul() { Id = 1, Schedule = "15,45 * * * *", Jobname = enumTask.ReceiveFilesFromPBS.ToString() });
            ls.Add(new localSchedul() { Id = 2, Schedule = "01 * * * *", Jobname = enumTask.LoadSchedule.ToString() });
            ls.Add(new localSchedul() { Id = 5, Schedule = "01 06 * * *", Jobname = enumTask.SendEmailRykker.ToString() });
            ls.Add(new localSchedul() { Id = 6, Schedule = "01 18 * * *", Jobname = enumTask.SendEmailAdvis.ToString() });
            ls.Add(new localSchedul() { Id = 7, Schedule = "01,16,31,46 * * * *", Jobname = enumTask.KontingentNyeMedlemmer.ToString() });
            ls.Add(new localSchedul() { Id = 8, Schedule = "01 21 * * *", Jobname = enumTask.SendEmailAdvis.ToString() });
            ls.Add(new localSchedul() { Id = 9, Schedule = "05 07 * * *", Jobname = enumTask.UpdateKanSlettes.ToString() });
            ls.Add(new localSchedul() { Id = 10, Schedule = "07 07 * * *", Jobname = enumTask.JobQMaintenance.ToString() });
            ls.Add(new localSchedul() { Id = 11, Schedule = "03,18,33,48 * * * *", Jobname = enumTask.SendEmailKviteringer.ToString() });
            ls.Add(new localSchedul() { Id = 14, Schedule = "03 01,07,13,19 * * *", Jobname = enumTask.OpdaterTransUserData.ToString() });
            ls.Add(new localSchedul() { Id = 15, Schedule = "01 21 12 * *", Jobname = enumTask.SendKontingentFileToPBS.ToString() });

            using (dbJobQEntities db = new dbJobQEntities())
            {
                foreach (var s in ls)
                {
                    var occurrence = CrontabSchedule.Parse(s.Schedule).GetNextOccurrences(DateTime.UtcNow, DateTime.UtcNow.AddHours(48)).GetEnumerator();
                    while (occurrence.MoveNext())
                    {
                        try
                        {
                            var recJobqueue = db.tblJobqueue.Where(j => j.Starttime == occurrence.Current).Where(j => j.Jobname == s.Jobname).Single();
                        }
                        catch
                        {

                            var message = new Microsoft.Azure.ServiceBus.Message(Encoding.UTF8.GetBytes(s.Jobname))
                            {
                                Label = s.Jobname,
                                ContentType = s.Jobname.GetType().ToString(),
                                ScheduledEnqueueTimeUtc = occurrence.Current
                            };
                            // Send the message to the queue.
                            queueClient.SendAsync(message).Wait();

                            var recJobqueue = new TblJobqueue()
                            {
                                 Scheduleid = s.Id,
                                 Starttime = occurrence.Current,
                                 Jobname = s.Jobname,
                                 Onhold = false,
                                 Selected = false,
                                 Completed = false
                            };
                            db.tblJobqueue.Local.Add(recJobqueue);
                            try
                            {
                                db.SaveChanges();

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(string.Format("Error {0}", e.Message));
                            }
                        }
                        
                    }
                }
            }
        }
        private class localSchedul
        {
            public int Id { get; set; }
            public string Jobname { get; set; }
            public string Schedule { get; set; }
        }
    }
}
