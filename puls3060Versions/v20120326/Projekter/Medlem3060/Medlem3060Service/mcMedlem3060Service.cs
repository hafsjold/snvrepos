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
        LoadSchedule
    }

    public partial class mcMedlem3060Service : ServiceBase
    {
        static EventWaitHandle _waitStopHandle = new ManualResetEvent(false);
        static Thread _SchedulerThread;

        public mcMedlem3060Service()
        {
            InitializeComponent();
/*
//#if (DEBUG)            
            Trace.WriteLine("Medlem3060Service Starter #2");
            _SchedulerThread = new Thread(Scheduler);
            _SchedulerThread.Name = "Scheduler";
            _SchedulerThread.Start();
            _SchedulerThread.Join();
//#endif
*/
        }

        private T StringToEnum<T>(string name)
        {
            return (T)Enum.Parse(typeof(T), name);
        }

        protected override void OnStart(string[] args)
        {
            Trace.WriteLine("Medlem3060Service OnStart()");
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
            dbJobQDataContext dbJobQ = Program.dbJobQDataContextFactory();            
             while (true)
            {
                Trace.WriteLine("Medlem3060Service Scheduler() loop start");
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
                    if (_waitStopHandle.WaitOne(60000))
                        break;
                }
            }

        }

        private int JobWorker(string jobname)
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
                    
                    default:
                        break;
                }
            }
            return 0;
        }

        private void LoadSchedule(int days = 2)
        {
            Trace.WriteLine("Medlem3060Service LoadSchedule()");
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
            catch (Exception)
            {
                Trace.WriteLine("Medlem3060Service LoadSchedule() failed");
            }
        }

    }
}
