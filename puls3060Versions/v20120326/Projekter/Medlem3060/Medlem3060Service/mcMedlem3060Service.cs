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
    public partial class mcMedlem3060Service : ServiceBase
    {
        static EventWaitHandle _waitStopHandle = new ManualResetEvent(false);

        public mcMedlem3060Service()
        {
            InitializeComponent();
/*
#if (DEBUG)
            clsSchedule.LoadSchedule();
            Thread t = new Thread(Scheduler);
            t.Name = "Scheduler";
            t.Start();
            t.Join();
#endif
*/
        }

        protected override void OnStart(string[] args)
        {
//#if (RELEASE)
            Trace.WriteLine("Medlem3060Service Starter #2");
            clsSchedule.LoadSchedule();
            Thread t = new Thread(Scheduler);
            t.Name = "Scheduler";
            t.Start();
            t.Join();
//#endif
        }

        protected override void OnStop()
        {
            _waitStopHandle.Set();
        }

        private void Scheduler()
        {
            dbJobQDataContext dbJobQ = Program.dbJobQDataContextFactory();
            while (true)
            {
                Trace.WriteLine("Medlem3060Service Starter #3");
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
                enumTask job = clsSchedule.StringToEnum<enumTask>(jobname);
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
                    
                    default:
                        break;
                }
            }
            return 0;
        }

    }
}
