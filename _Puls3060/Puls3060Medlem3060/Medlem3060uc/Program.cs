using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;
using nsPbs3060;
using System.Diagnostics;



namespace Medlem3060uc
{
    static class Program
    {
        private static dbData3060DataContext m_dbData3060DataContext;
        public static string ConnectStringWithoutPassword
        {
            get
            {
                if (m_ConnectStringWithoutPassword == null)
                {
#if (DEBUG)
                    m_ConnectStringWithoutPassword = global::Medlem3060uc.Properties.Settings.Default.puls3061_dk_dbConnectionString_Prod;
                    //m_ConnectStringWithoutPassword = global::Medlem3060uc.Properties.Settings.Default.puls3061_dk_dbConnectionString_Test;
                    //m_ConnectStringWithoutPassword = global::Medlem3060uc.Properties.Settings.Default.test_dbConnectionString_Test;
#else
                    m_ConnectStringWithoutPassword = global::Medlem3060uc.Properties.Settings.Default.puls3061_dk_dbConnectionString_Prod;
#endif
                }
                return m_ConnectStringWithoutPassword;
            }
            set
            {
                m_ConnectStringWithoutPassword = value;
            }
        }
        public static string dbConnectionString()
        {
            DialogResult res = DialogResult.OK;
            m_Password = global::Medlem3060uc.Properties.Settings.Default.UserPassword;
            if (Unprotect(m_Password) == null)
                res = (new FrmPassword()).ShowDialog();
            if (res != DialogResult.OK) return null;
            Program.Log(ConnectStringWithoutPassword, "ConnectString");
            return ConnectStringWithoutPassword + ";Password=" + Unprotect(m_Password);
        }
        public static dbData3060DataContext dbData3060DataContextFactory()
        {
            return new dbData3060DataContext(dbConnectionString());
        }
        public static dbData3060DataContext dbData3060
        {
            get
            {
                if (m_dbData3060DataContext == null)
                {
                    m_dbData3060DataContext = dbData3060DataContextFactory();
                }
                return m_dbData3060DataContext;
            }
            set
            {
                m_dbData3060DataContext = value;
            }
        }

        static byte[] s_aditionalEntropy = { 9, 8, 7, 6, 5 };
        private static string m_ConnectStringWithoutPassword;
        private static string m_Password;
        private static MemPbsnetdir m_memPbsnetdir;
        private static KarKladde m_KarKladde;

        public static string Password
        {
            get
            {
                return m_Password;
            }
            set
            {
                m_Password = value;
            }
        }
        public static MemPbsnetdir memPbsnetdir
        {
            get
            {
                if (m_memPbsnetdir == null) m_memPbsnetdir = new MemPbsnetdir();
                return m_memPbsnetdir;
            }
            set
            {
                m_memPbsnetdir = value;
            }
        }

        public static KarKladde karKladde
        {
            get
            {
                if (m_KarKladde == null) m_KarKladde = new KarKladde();
                return m_KarKladde;
            }
            set
            {
                m_KarKladde = value;
            }
        }

        public static FrmMain frmMain { get; set; }

        public static string Protect(string secret)
        {
            try
            {
                var data = Encoding.Unicode.GetBytes(secret);
                byte[] encrypted = ProtectedData.Protect(data, s_aditionalEntropy, DataProtectionScope.CurrentUser);
                return Convert.ToBase64String(encrypted);
            }
            catch (CryptographicException)
            {
                return null;
            }
        }
        public static string Unprotect(string cipher)
        {
            try
            {
                byte[] data = Convert.FromBase64String(cipher);
                byte[] decrypted = ProtectedData.Unprotect(data, s_aditionalEntropy, DataProtectionScope.CurrentUser);
                return Encoding.Unicode.GetString(decrypted);

            }
            catch (CryptographicException)
            {
                return null;
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
        [STAThread]
        static void Main()
        {
            Trace.Listeners.RemoveAt(0);
            DefaultTraceListener defaultListener;
            defaultListener = new DefaultTraceListener();
            defaultListener.LogFileName = "Application.log";

            Trace.Listeners.Add(defaultListener);
            if (!EventLog.SourceExists("Medlem3060uc"))
                EventLog.CreateEventSource("Medlem3060uc", "Application");
            //Trace.Listeners.Add(new EventLogTraceListener("Medlem3060uc"));

            Program.Log("Starter Medlem3060uc");
            System.Diagnostics.Process[] p = System.Diagnostics.Process.GetProcessesByName("Medlem3060uc");
            if (p.Length > 1)
            {
                clsUtil.SetForegroundWindow(p[0].MainWindowHandle);
            }
            else
            {
                Uniconta.ClientTools.Localization.SetLocalizationStrings(System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
                Uniconta.WindowsAPI.Startup.OnLoad();
                UCInitializer.InitUniconta();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FrmMain());
            }
        }
    }
}
