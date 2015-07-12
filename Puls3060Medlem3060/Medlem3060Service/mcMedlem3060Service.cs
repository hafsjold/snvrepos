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
        SendFilesToPBS,
        LoadSchedule,
        KontingentNyeMedlemmer,
        SendEmailRykker,
        UpdateMedlemStatus,
        SendEmailAdvis
    }

    public partial class mcMedlem3060Service : ServiceBase
    {
        static EventWaitHandle _waitStopHandle = new ManualResetEvent(false);
        static Thread _SchedulerThread;

        public mcMedlem3060Service()
        {
            InitializeComponent();
            ///*
            //#if (DEBUG)            
                        Program.Log("Medlem3060Service Starter #2");
                        _SchedulerThread = new Thread(Scheduler);
                        _SchedulerThread.Name = "Scheduler";
                        _SchedulerThread.Start();
                        _SchedulerThread.Join();
            //#endif
            //*/
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

                        case enumTask.SendFilesToPBS:
                            break;

                        case enumTask.LoadSchedule:
                            LoadSchedule();
                            break;

                        case enumTask.KontingentNyeMedlemmer:
                            Program.Log("Medlem3060Service KontingentNyeMedlemmer begin");
                            puls3060_dkEntities jdb = new puls3060_dkEntities();
                            clsPbs601 objPbs601c = new clsPbs601();
                            Tuple<int, int> tresultc = objPbs601c.rsmembeshhip_fakturer_auto(m_dbData3060, jdb);
                            int AntalKontingent = tresultc.Item1;
                            int lobnrc = tresultc.Item2;
                            if ((AntalKontingent > 0))
                            {
                                objPbs601c.faktura_og_rykker_601_action(m_dbData3060, lobnrc, fakType.fdrsmembership);
                                clsSFTP objSFTPc = new clsSFTP(m_dbData3060);
                                objSFTPc.WriteTilSFtp(m_dbData3060, lobnrc);
                                objSFTPc.DisconnectSFtp();
                                objSFTPc = null;
                            }
                            Program.Log("Medlem3060Service KontingentNyeMedlemmer end");
                            break;

                        case enumTask.SendEmailRykker:
                            clsPbs601 objPbs601b = new clsPbs601();
                            Tuple<int, int> tresultb = objPbs601b.rykker_auto(m_dbData3060);
                            int AntalRykker = tresultb.Item1;
                            int lobnrb = tresultb.Item2;
                            if ((AntalRykker > 0))
                                objPbs601b.rykker_email(m_dbData3060, lobnrb);
                            objPbs601b = null;
                            break;

                        case enumTask.UpdateMedlemStatus:
                            m_dbData3060.UpdateMedlemStatus();
                            break;

                        default:
                            break;
                    }
                }
                return 0;
            }
            catch (Exception e)
            {
                Program.Log(string.Format("Medlem3060Service JobWorker() failed with message: {0}", e.Message));
                return -1;
            }
        }

        private void LoadSchedule(int days = 2)
        {
            Program.Log("Medlem3060Service LoadSchedule()");
            try
            {
                dbJobQDataContext dbJobQ = Program.dbJobQDataContextFactory();
                var Schedules = from s in dbJobQ.tblSchedules orderby s.start select s;
                foreach (var s in Schedules)
                {
                    if (Enum.IsDefined(typeof(enumTask), s.jobname))
                    {
                        var occurrence = CrontabSchedule.Parse(s.schedule).GetNextOccurrences(DateTime.Now, DateTime.Now.AddDays(days)).GetEnumerator();
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
