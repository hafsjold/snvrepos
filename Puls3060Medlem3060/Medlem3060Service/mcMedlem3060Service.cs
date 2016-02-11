using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using NCrontab;
using System.Threading;
using System.Threading.Tasks;
using nsPbs3060;


namespace nsMedlem3060Service
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
        SendEmailKviteringer
    }

    public partial class mcMedlem3060Service : ServiceBase
    {
        static EventWaitHandle _waitStopHandle = new ManualResetEvent(false);
        static Thread _SchedulerThread;

        public mcMedlem3060Service()
        {
            InitializeComponent();
            /*
                        Program.Log("Medlem3060Service Starter #2");
                        _SchedulerThread = new Thread(Scheduler);
                        _SchedulerThread.Name = "Scheduler";
                        _SchedulerThread.Start();
                        _SchedulerThread.Join();
            */
        }

        private T StringToEnum<T>(string name)
        {
            return (T)Enum.Parse(typeof(T), name);
        }

        protected override void OnStart(string[] args)
        {
            Program.Log("Medlem3060Service OnStart()");
            _SchedulerThread = new Thread(Scheduler);
            _SchedulerThread.Name = "Scheduler";
            _SchedulerThread.Start();
        }

        protected override void OnStop()
        {
            _waitStopHandle.Set();
            _SchedulerThread.Join();
        }

        private void Scheduler()
        {
            LoadSchedule();

            while (true)
            {
                try
                {
                    Program.Log("Medlem3060Service Scheduler() loop start");
                    dbJobQDataContext dbJobQ = Program.dbJobQDataContextFactory();
                    int? id = null;
                    string jobname = null;
                    if (dbJobQ.jobqueuenext(ref id, ref jobname) == 0)
                    {
                        if (Enum.IsDefined(typeof(enumTask), jobname))
                        {
                            Task<int> task = new Task<int>(() => JobWorker(jobname));
                            task.Start();
                            int resultat = task.Result;
                        }

                        dbJobQ.jobqueuecomplete((int)id);

                    }
                    else
                    {

                        if (_waitStopHandle.WaitOne(2 * 60000))
                            break;
                    }
                }
                catch (Exception e)
                {
                    Program.Log(string.Format("Medlem3060Service Scheduler() loop failed with message: {0}", e.Message));
                    if (_waitStopHandle.WaitOne(5 * 60000))
                        break;
                }
            }

        }

        private int JobWorker(string jobname)
        {
            try
            {
                if (Enum.IsDefined(typeof(enumTask), jobname))
                {
                    Program.Log(string.Format("Medlem3060Service {0} begin", jobname));
                    dbData3060DataContext m_dbData3060 = new dbData3060DataContext(Program.dbConnectionString());
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
                                Program.Log(string.Format("Medlem3060Service {0} begin", "Betalinger til RSMembership"));
                                puls3060_dkEntities jdb = new puls3060_dkEntities();
                                objPbs602.betalinger_til_rsmembership(m_dbData3060, jdb);
                                Program.Log(string.Format("Medlem3060Service {0} end", "Betalinger til RSMembership"));
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
                                objPbs601a.advis_email(m_dbData3060, lobnra);
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
                            puls3060_dkEntities cjdb = new puls3060_dkEntities();
                            clsPbs601 objPbs601c = new clsPbs601();
                            Tuple<int, int> tresultc = objPbs601c.rsmembeshhip_fakturer_auto(m_dbData3060, cjdb);
                            int AntalKontingent = tresultc.Item1;
                            int lobnrc = tresultc.Item2;
                            if ((AntalKontingent > 0))
                            {
                                objPbs601c.faktura_og_rykker_601_action(m_dbData3060, lobnrc, fakType.fdrsmembership);
                                clsSFTP objSFTPc = new clsSFTP(m_dbData3060);
                                objSFTPc.WriteTilSFtp(m_dbData3060, lobnrc);
                                objSFTPc.DisconnectSFtp();
                                objSFTPc = null;

                                Tuple<int, int> tresultd = objPbs601c.advis_autoxxx(m_dbData3060, lobnrc);
                                int AntalAdvisd = tresultd.Item1;
                                int lobnrd = tresultd.Item2;
                                if ((AntalAdvisd > 0))
                                    objPbs601c.advis_email(m_dbData3060, lobnrd);
                                objPbs601c = null;
                            }
                            break;

                        case enumTask.SendEmailRykker:
                            puls3060_dkEntities bjdb = new puls3060_dkEntities();
                            clsPbs601 objPbs601b = new clsPbs601();
                            Tuple<int, int> tresultb = objPbs601b.rykker_auto(m_dbData3060, bjdb);
                            int AntalRykker = tresultb.Item1;
                            int lobnrb = tresultb.Item2;
                            if ((AntalRykker > 0))
                                objPbs601b.rykker_email(m_dbData3060, lobnrb);
                            objPbs601b = null;
                            break;

                        case enumTask.UpdateMedlemStatus:
                            m_dbData3060.UpdateMedlemStatus();
                            break;

                        case enumTask.UpdateKanSlettes:
                            clsPbsHelper objPbsHelpera = new clsPbsHelper();
                            objPbsHelpera.opdaterKanSlettes();
                            break;

                        case enumTask.JobQMaintenance:
                            clsPbsHelper objPbsHelperb = new clsPbsHelper();
                            objPbsHelperb.JobQMaintenance(m_dbData3060);
                            break;

                        case enumTask.SendEmailKviteringer:
                            puls3060_dkEntities djdb = new puls3060_dkEntities();
                            clsPbs601 objPbs601d = new clsPbs601();
                            objPbs601d.rsmembeshhip_betalinger_auto(m_dbData3060, djdb);
                            break;

                        default:
                            break;
                    }
                }
                Program.Log(string.Format("Medlem3060Service {0} end", jobname));
                return 0;
            }
            catch (Exception e)
            {
                Program.Log(string.Format("Medlem3060Service JobWorker() for {0} failed with message: {1}", jobname, e.Message));
                return -1;
            }
        }

        private void LoadSchedule(int days = 2)
        {
            try
            {
                dbJobQDataContext dbJobQ = Program.dbJobQDataContextFactory();
                var Schedules = from s in dbJobQ.tblSchedules orderby s.start select s;
                foreach (var s in Schedules)
                {
                    if (Enum.IsDefined(typeof(enumTask), s.jobname))
                    {
                        var occurrence = CrontabSchedule.Parse(s.schedule).GetNextOccurrences(DateTime.UtcNow, DateTime.UtcNow.AddDays(days)).GetEnumerator();
                        while (occurrence.MoveNext())
                            dbJobQ.jobqueueadd(occurrence.Current, s.jobname, s.id, false);
                    }
                }
            }
            catch (Exception e)
            {
                Program.Log(string.Format("Medlem3060Service LoadSchedule() failed with message: {0}", e.Message));
            }
        }

    }
}
