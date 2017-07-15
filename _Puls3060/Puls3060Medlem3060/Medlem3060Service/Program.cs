using System;
using System.ServiceProcess;
using System.Diagnostics;
using System.Data.SqlClient;
using nsPbs3060;

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
            con = global::nsMedlem3060Service.Properties.Settings.Default.puls3061_dk_dbConnectionString_Test + ";Password=" + clsApp.dbPuls3060MedlemPW;
#else
            con = global::nsMedlem3060Service.Properties.Settings.Default.puls3061_dk_dbConnectionString_Prod + ";Password=" + clsApp.dbPuls3060MedlemPW;
#endif
            var cb = new SqlConnectionStringBuilder(con);
            Program.Log(string.Format("Medlem3060Service ConnectString to SQL Database {0} on {1}", cb.InitialCatalog, cb.DataSource)); 
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

        public static void Log(string message)
        {
            string msg = DateTime.Now.ToString() + "||" + message;
            Trace.WriteLine(msg);
        }

        public static void Log(string message, string category)
        {
            string msg = DateTime.Now.ToString() + "|" + category + "|" + message;
            Trace.WriteLine(msg);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Trace.Listeners.RemoveAt(0);
            Trace.AutoFlush = true;
            DefaultTraceListener defaultListener;
            defaultListener = new DefaultTraceListener();
            defaultListener.LogFileName = "Application.log";
            Trace.Listeners.Add(defaultListener);
            
            if (!EventLog.SourceExists("Medlem3060"))
                EventLog.CreateEventSource("Medlem3060", "Application");
            
            //Trace.Listeners.Add(new EventLogTraceListener("Medlem3060"));

            Program.Log("Medlem3060Service Starter");
            EventLog.WriteEntry("Medlem3060", "Medlem3060Service Starter", EventLogEntryType.Information, 101);

            Uniconta.ClientTools.Localization.SetLocalizationStrings(System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
            Uniconta.WindowsAPI.Startup.OnLoad();
            UCInitializer.InitUniconta();

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new mcMedlem3060Service() 
			};
            ServiceBase.Run(ServicesToRun);
            Trace.Flush();
        }
    }
}
