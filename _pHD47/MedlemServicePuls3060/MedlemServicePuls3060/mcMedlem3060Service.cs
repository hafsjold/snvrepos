using System;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Linq;
using NCrontab;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Pbs3060;

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
        OpdaterTransUserData
    }

    public class mcMedlem3060Service : ServiceBase
    {
        public static EventWaitHandle Service_waitStopHandle = new ManualResetEvent(false);
        static Thread _SchedulerThread;

        public mcMedlem3060Service()
        {
            this.ServiceName = "mcMedlem3060Service";
            SystemEvents.SessionEnded += new SessionEndedEventHandler(SystemEvents_SessionEnded);
            Console.WriteLine("Medlem3060Service Starter #2");
            try
            {
                UCInitializer.UnicontaLogin();
            }
            catch { }
            _SchedulerThread = new Thread(Scheduler);
            _SchedulerThread.Name = "Scheduler";
            _SchedulerThread.Start();
        }

        private void SystemEvents_SessionEnded(object sender, SessionEndedEventArgs e)
        {
            Service_waitStopHandle.Set();
            _SchedulerThread.Join();
            Console.WriteLine("Medlem3060Service SessionEnded()");
        }

        private T StringToEnum<T>(string name)
        {
            return (T)Enum.Parse(typeof(T), name);
        }

        protected override void OnStart(string[] args)
        {
            Console.WriteLine("Medlem3060Service OnStart()");
            try
            {
                UCInitializer.UnicontaLogin();
            }
            catch { }
            _SchedulerThread = new Thread(Scheduler);
            _SchedulerThread.Name = "Scheduler";
            _SchedulerThread.Start();
        }

        protected override void OnStop()
        {
            Service_waitStopHandle.Set();
            _SchedulerThread.Join();
            Console.WriteLine("Medlem3060Service OnStop()");
        }

        private void Scheduler()
        {
            //Test
            /*
            using (dbData3060DataContext m_dbData3060 = new dbData3060DataContext()) {
                clsSFTP objSFTP = new clsSFTP(m_dbData3060);
                int AntalImportFiler = objSFTP.ReadFromLocalFile(m_dbData3060, @"C:\Users\Public\Documents\D1900601.BS1");  //Læs direkte SFTP
                objSFTP.DisconnectSFtp();
                objSFTP = null;

                clsPbs602 objPbs602 = new clsPbs602();
                int Antal602Filer = objPbs602.betalinger_fra_pbs(m_dbData3060);
                if (Antal602Filer > 0)
                {
                    Console.WriteLine(string.Format("Medlem3060Service {0} begin", "Betalinger til RSMembership"));
                    puls3060_nyEntities jdb = new puls3060_nyEntities();
                    objPbs602.betalinger_til_rsmembership(m_dbData3060, jdb);
                    Console.WriteLine(string.Format("Medlem3060Service {0} end", "Betalinger til RSMembership"));
                }
                objPbs602 = null;

                clsPbs603 objPbs603 = new clsPbs603();
                int Antal603Filer = objPbs603.aftaleoplysninger_fra_pbs(m_dbData3060);
                objPbs603 = null;

                clsPbs686 objPbs686 = new clsPbs686();
                int Antal686Filer = objPbs686.aftaleoplysninger_fra_pbs(m_dbData3060);
                objPbs686 = null;
            }
            */
            //JobWorker("ReceiveFilesFromPBS", 99999);
            //JobWorker("SendEmailAdvis", 99999);
            // JobWorker("SendKontingentFileToPBS", 99999);
            // JobWorker("KontingentNyeMedlemmer", 99999);
            // JobWorker("SendEmailRykker", 99999);
            // JobWorker("SendEmailKviteringer", 99999);

            JobQMaintenance();
            LoadSchedule();

            while (true)
            {
                try
                {
                    Console.WriteLine("Medlem3060Service Scheduler() loop start");
                    int? id = null;
                    string jobname = null;
                    if (JobQueueNext(ref id, ref jobname) == 0)
                    {
                        if (Enum.IsDefined(typeof(enumTask), jobname))
                        {
                            Task<int> task = new Task<int>(() => JobWorker(jobname, id));
                            task.Start();
                            int resultat = task.Result;
                        }
                    }
                    else
                    {
                        if (Service_waitStopHandle.WaitOne(2 * 60000))
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(string.Format("Medlem3060Service Scheduler() loop failed with message: {0}", e.Message));
                    if (Service_waitStopHandle.WaitOne(5 * 60000))
                        break;
                }
            }

        }

        private int JobWorker(string jobname, int? jobid)
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
                            clsSFTP objSFTP = new clsSFTP(m_dbData3060);
                            int AntalImportFiler = objSFTP.ReadFraSFtp(m_dbData3060);  //Læs direkte SFTP
                            objSFTP.DisconnectSFtp();
                            objSFTP = null;

                            clsPbs602 objPbs602 = new clsPbs602();
                            int Antal602Filer = objPbs602.betalinger_fra_pbs(m_dbData3060);
                            if (Antal602Filer > 0)
                            {
                                Console.WriteLine(string.Format("Medlem3060Service {0} begin", "Betalinger til RSMembership"));
                                puls3060_nyEntities jdb = new puls3060_nyEntities();
                                objPbs602.betalinger_til_rsmembership(m_dbData3060, jdb);
                                Console.WriteLine(string.Format("Medlem3060Service {0} end", "Betalinger til RSMembership"));
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
                            Tuple<int, int> tresult = objPbs601a.advis_auto(m_dbData3060);
                            int AntalAdvis = tresult.Item1;
                            int lobnra = tresult.Item2;
                            if ((AntalAdvis > 0))
                                objPbs601a.advis_email_lobnr(m_dbData3060, lobnra);
                            objPbs601a = null;
                            break;

                        case enumTask.ProcessType602Files:
                            break;

                        case enumTask.ProcessType603Files:
                            break;

                        case enumTask.SendKontingentFileToPBS:
                            clsPbsHelper objPbsHelperd = new clsPbsHelper();
                            objPbsHelperd.PbsAutoKontingent(m_dbData3060);
                            objPbsHelperd = null;
                            break;

                        case enumTask.LoadSchedule:
                            LoadSchedule();
                            break;

                        case enumTask.KontingentNyeMedlemmer:
                            puls3060_nyEntities cjdb = new puls3060_nyEntities();
                            clsPbs601 objPbs601c = new clsPbs601();
                            Tuple<int, int> tresultc = objPbs601c.paypal_pending_rsmembeshhip_fakturer_auto(m_dbData3060, cjdb);
                            int AntalKontingent = tresultc.Item1;
                            int lobnrc = tresultc.Item2;
                            if ((AntalKontingent > 0))
                            {
                                //pbsType.indbetalingskort
                                objPbs601c.faktura_og_rykker_601_action_lobnr(m_dbData3060, lobnrc);
                                clsSFTP objSFTPc = new clsSFTP(m_dbData3060);
                                objSFTPc.WriteTilSFtp(m_dbData3060, lobnrc);
                                objSFTPc.DisconnectSFtp();
                                objSFTPc = null;

                                Tuple<int, int> tresultd = objPbs601c.advis_auto_lobnr(m_dbData3060, lobnrc);
                                int AntalAdvisd = tresultd.Item1;
                                int lobnrd = tresultd.Item2;
                                if ((AntalAdvisd > 0))
                                    objPbs601c.advis_email_lobnr(m_dbData3060, lobnrd);
                                objPbs601c = null;
                            }
                            break;

                        case enumTask.SendEmailRykker:
                            puls3060_nyEntities bjdb = new puls3060_nyEntities();
                            clsPbs601 objPbs601b = new clsPbs601();
                            Tuple<int, int> tresultb = objPbs601b.rykker_auto(m_dbData3060, bjdb);
                            int AntalRykker = tresultb.Item1;
                            int lobnrb = tresultb.Item2;
                            if ((AntalRykker > 0))
                                objPbs601b.rykker_email_lobnr(m_dbData3060, lobnrb);
                            objPbs601b = null;
                            break;

                        case enumTask.UpdateMedlemStatus:
                            break;

                        case enumTask.UpdateKanSlettes:
                            clsPbsHelper objPbsHelpera = new clsPbsHelper();
                            objPbsHelpera.opdaterKanSlettes();
                            break;

                        case enumTask.JobQMaintenance:
                            JobQMaintenance();
                            break;

                        case enumTask.SendEmailKviteringer:
                            puls3060_nyEntities djdb = new puls3060_nyEntities();
                            clsPbs601 objPbs601d = new clsPbs601();
                            objPbs601d.rsmembeshhip_betalinger_auto(m_dbData3060, djdb);
                            break;

                        case enumTask.OpdaterTransUserData:
                            clsPbsHelper.update_rsmembership_transactions_user_data();
                            break;

                        default:
                            break;
                    }
                }
                Console.WriteLine(string.Format("Medlem3060Service {0} end", jobname));
                JobQueueComplete((int)jobid);
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("Medlem3060Service JobWorker() for {0} failed with message: {1}", jobname, e.Message));
                JobQueueComplete((int)jobid);
                return -1;
            }
        }

        private void LoadSchedule(int days = 2)
        {
            using (dbJobQEntities db = new dbJobQEntities())
            {
                try
                {
                    var Schedules = from s in db.tblSchedule orderby s.Start select s;
                    foreach (var s in Schedules)
                    {
                        if (Enum.IsDefined(typeof(enumTask), s.Jobname))
                        {
                            var occurrence = CrontabSchedule.Parse(s.Schedule).GetNextOccurrences(DateTime.UtcNow, DateTime.UtcNow.AddDays(days)).GetEnumerator();
                            while (occurrence.MoveNext())
                                JobQueueAdd(occurrence.Current, s.Jobname, s.Id, false);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(string.Format("Medlem3060Service LoadSchedule() failed with message: {0}", e.Message));
                }
            }
        }

        private void JobQueueAdd(DateTime starttime, string jobname, int scheduleid, bool onhold)
        {
            using (dbJobQEntities db = new dbJobQEntities())
            {
                var qry = from jq in db.tblJobqueue
                          where
                          jq.Starttime == starttime
                          && jq.Jobname == jobname
                          select jq;
                if (qry.Count() == 0)
                {
                    TblJobqueue rec = new TblJobqueue
                    {
                        Scheduleid = scheduleid,
                        Starttime = starttime,
                        Jobname = jobname,
                        Onhold = onhold,
                        Selected = false,
                        Completed = false
                    };
                    db.tblJobqueue.Add(rec);
                    db.SaveChanges();
                }
            }
        }

        private int? JobQueueNext(ref int? id, ref string jobname)
        {
            var nu = DateTime.Now;
            var nuplus2 = nu.AddHours(2);
            using (dbJobQEntities db = new dbJobQEntities())
            {
                var qry1 = from jq in db.tblJobqueue
                           where
                           nu > jq.Starttime
                           && nu < nuplus2
                           && jq.Onhold == false
                           && jq.Selected == true
                           && jq.Completed == false
                           orderby jq.Starttime
                           select jq;
                if (qry1.Count() > 0)
                {
                    var rec = qry1.First();
                    id = rec.Id;
                    jobname = rec.Jobname;
                    return -id;
                }

                var qry2 = from jq in db.tblJobqueue
                           where
                           nu > jq.Starttime
                           && nu < nuplus2
                           && jq.Onhold == false
                           && jq.Selected == false
                           && jq.Completed == false
                           orderby jq.Starttime
                           select jq;
                if (qry2.Count() > 1)
                {
                    var rec = qry2.First();
                    id = rec.Id;
                    jobname = rec.Jobname;
                    rec.Selected = true;
                    db.SaveChanges();
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
        }

        private void JobQueueComplete(int id)
        {
            using (dbJobQEntities db = new dbJobQEntities())
            {
                var qry = from jq in db.tblJobqueue where jq.Id == id select jq;
                if (qry.Count() == 1)
                {
                    var rec = qry.First();
                    rec.Selected = false;
                    rec.Completed = true;
                    db.SaveChanges();
                }
            }
        }

        private void JobQMaintenanceSQL()
        {
            using (dbJobQEntities db = new dbJobQEntities())
            {
                using (var command = db.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = @"DELETE FROM dbo.tblJobqueue WHERE starttime < DATEADD(HOUR,-12,GETDATE())";
                    db.Database.OpenConnection();
                    using (var result = command.ExecuteReader())
                    {
                        // do something with result
                    }
                }
            }
        }

        private void JobQMaintenance()
        {
            using (dbJobQEntities db = new dbJobQEntities())
            {
                int count = db.Database.ExecuteSqlCommand(@"DELETE FROM dbo.tblJobqueue WHERE starttime < DATEADD(HOUR,-12,GETDATE())");
            }
        }

        private void JobQueueNext_copy()
        {
            using (dbJobQEntities db = new dbJobQEntities())
            {
                using (var command = db.Database.GetDbConnection().CreateCommand())
                {
                    var id = 0;
                    var p_id = new SqlParameter();
                    p_id.ParameterName = "@id";
                    p_id.SqlDbType = System.Data.SqlDbType.Int;
                    p_id.IsNullable = true;
                    p_id.Direction = System.Data.ParameterDirection.Output;
                    command.Parameters.Add(p_id);

                    var jobname = "";
                    var p_jobname = new SqlParameter();
                    p_jobname.ParameterName = "@jobname";
                    p_jobname.SqlDbType = System.Data.SqlDbType.NVarChar;
                    p_jobname.Size = 25;
                    p_jobname.IsNullable = true;
                    p_jobname.Direction = System.Data.ParameterDirection.Output;
                    command.Parameters.Add(p_jobname);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = @"jobqueuenext_copy";
                    db.Database.OpenConnection();
                    using (var result = command.ExecuteReader())
                    {
                        id = (int)command.Parameters["@id"].Value;
                        jobname = command.Parameters["@jobname"].Value.ToString();
                    }
                }
            }
        }

    }
}