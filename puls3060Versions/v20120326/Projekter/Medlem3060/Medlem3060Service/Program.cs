using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Diagnostics;

namespace nsMedlem3060Service
{

    static class Program
    {
        private static dbJobQDataContext m_dbJobQ;

        public static string dbConnectionString()
        {

#if (DEBUG)
            string con = global::nsMedlem3060Service.Properties.Settings.Default.puls3061_dk_dbConnectionString + ";Password=mha733";
#else
            string con = global::nsMedlem3060Service.Properties.Settings.Default.puls3061_dk_dbConnectionString + ";Password=mha733";   
#endif
            return con;
        }

        public static dbJobQDataContext dbJobQDataContextFactory()
        {
            return new dbJobQDataContext(dbConnectionString());
        }
        
        public static dbJobQDataContext dbJobQ
        {
            get
            {
                if (m_dbJobQ == null)
                {
                    m_dbJobQ = dbJobQDataContextFactory();
                }
                return m_dbJobQ;
            }
            set
            {
                m_dbJobQ = value;
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Trace.Listeners.RemoveAt(0);
            DefaultTraceListener defaultListener;
            defaultListener = new DefaultTraceListener();
            Trace.Listeners.Add(defaultListener);
            
            if (!EventLog.SourceExists("Medlem3060"))
                EventLog.CreateEventSource("Medlem3060", "Application");
            
            Trace.Listeners.Add(new EventLogTraceListener("Medlem3060"));
            Trace.WriteLine("Medlem3060Service Starter");
            EventLog.WriteEntry("Medlem3060", "message string", EventLogEntryType.Information, 101);


            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new mcMedlem3060Service() 
			};
            ServiceBase.Run(ServicesToRun);
        }
    }
}
