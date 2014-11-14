using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nsInfo3060
{
    static class Program
    {
        private static dbData3060DataContext m_dbPuls3060MedlemEntities;
        public static FrmMain frmMain { get; set; }
        private static string m_ConnectStringWithoutPassword;
        private static string m_Password;
        static byte[] s_aditionalEntropy = { 9, 8, 7, 6, 5 };

        public static string ConnectStringWithoutPassword
        {
            get
            {
                if (m_ConnectStringWithoutPassword == null)
                {
#if (DEBUG)
                    m_ConnectStringWithoutPassword = global::nsInfo3060.Properties.Settings.Default.puls3061_dk_dbConnectionString_Test;
                    //m_ConnectStringWithoutPassword = global::nsPuls3060.Properties.Settings.Default.test_dbConnectionString_Test;
#else
                    m_ConnectStringWithoutPassword = global::nsPuls3060.Properties.Settings.Default.puls3061_dk_dbConnectionString_Prod;
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
            m_Password = global::nsInfo3060.Properties.Settings.Default.UserPassword;
            if (Unprotect(m_Password) == null)
                res = (new FrmPassword()).ShowDialog();
            if (res != DialogResult.OK) return null;
            //Program.Log(ConnectStringWithoutPassword, "ConnectString");
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
                if (m_dbPuls3060MedlemEntities == null)
                {
                    m_dbPuls3060MedlemEntities = dbData3060DataContextFactory();

                }
                return m_dbPuls3060MedlemEntities;
            }
            set
            {
                m_dbPuls3060MedlemEntities = value;
            }
        }

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

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}
