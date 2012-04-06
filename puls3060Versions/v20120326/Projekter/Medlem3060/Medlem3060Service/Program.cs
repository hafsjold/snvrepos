using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Diagnostics;
using System.Data.SqlClient;

namespace nsMedlem3060Service
{

    static class Program
    {
        private static readonly object _locker1 = new object();
        private static dbJobQDataContext m_dbJobQ;

        public static string dbConnectionString()
        {
            string con;
            lock (_locker1)
            {
#if (DEBUG)
            con = global::nsMedlem3060Service.Properties.Settings.Default.puls3061_dk_dbConnectionString_Test + ";Password=mha733";
#else
            con = global::nsMedlem3060Service.Properties.Settings.Default.puls3061_dk_dbConnectionString_Prod + ";Password=fnyakb6c";
#endif
            var cb = new SqlConnectionStringBuilder(con);
            Trace.WriteLine(string.Format("Medlem3060Service ConnectString to SQL Database {0} on {1}", cb.InitialCatalog, cb.DataSource)); 
            }
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
            EventLog.WriteEntry("Medlem3060", "Medlem3060Service Starter", EventLogEntryType.Information, 101);


            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new mcMedlem3060Service() 
			};
            ServiceBase.Run(ServicesToRun);
        }
    }
}
