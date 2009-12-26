using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace nsPuls3060
{
    static class Program
    {
        private static FrmMedlemmer m_frmMedlemmer;
        private static DbData3060 m_dbData3060;
        private static KarMedlemmer m_KarMedlemmer;
        private static MemMedlemDictionary m_dicMedlem;
        private static KarDkkonti m_KarDkkonti;
        private static KarFakturaer_s m_KarFakturaer_s;
        private static KarFakturastr_s m_KarFakturastr_s;
        private static KarFakturavarer_s m_KarFakturavarer_s;
        private static KarKortnr m_KarKortnr;
        private static KarRegnskab m_KarRegnskab;
        private static KarStatus m_KarStatus;

        public static FrmMedlemmer frmMedlemmer
        {
            get
            {
                if (m_frmMedlemmer == null)
                {
                    m_frmMedlemmer = new FrmMedlemmer();
                    Program.frmMedlemmer.Show();
                }
                else
                {
                    try
                    {
                        m_frmMedlemmer.Show();

                    }
                    catch (ObjectDisposedException)
                    {
                        m_frmMedlemmer = new FrmMedlemmer();
                        m_frmMedlemmer.Show();
                    }
                }

                return m_frmMedlemmer;
            }
            set
            {
                m_frmMedlemmer = value;
            }
        }
        public static DbData3060 dbData3060 
        {
            get 
            {
                if (m_dbData3060 == null) m_dbData3060 = new DbData3060(global::nsPuls3060.Properties.Settings.Default.DataBasePath);
                return m_dbData3060;
            }
            set 
            {
                m_dbData3060 = value;
            }        
		}
        public static KarMedlemmer karMedlemmer
        {
            get
            {
                if (m_KarMedlemmer == null) m_KarMedlemmer = new KarMedlemmer();
                return m_KarMedlemmer;
            }
            set 
            {
                m_KarMedlemmer = value;
            }
        }
        public static MemMedlemDictionary memMedlemDictionary
        {
            get
            {
                if (m_dicMedlem == null) m_dicMedlem = new MemMedlemDictionary();
                return m_dicMedlem;
            }
            set 
            {
                m_dicMedlem = value;
            }
        }
        public static KarDkkonti karDkkonti
        {
            get
            {
                if (m_KarDkkonti == null) m_KarDkkonti = new KarDkkonti();
                return m_KarDkkonti;
            }
            set
            {
                m_KarDkkonti = value;
            }
        }
        public static KarFakturaer_s karFakturaer_s
        {
            get
            {
                if (m_KarFakturaer_s == null) m_KarFakturaer_s = new KarFakturaer_s();
                return m_KarFakturaer_s;
            }
            set
            {
                m_KarFakturaer_s = value;
            }
        }
        public static KarFakturastr_s karFakturastr_s
        {
            get
            {
                if (m_KarFakturastr_s == null) m_KarFakturastr_s = new KarFakturastr_s();
                return m_KarFakturastr_s;
            }
            set
            {
                m_KarFakturastr_s = value;
            }
        }
        public static KarFakturavarer_s karFakturavarer_s
        {
            get
            {
                if (m_KarFakturavarer_s == null) m_KarFakturavarer_s = new KarFakturavarer_s();
                return m_KarFakturavarer_s;
            }
            set
            {
                m_KarFakturavarer_s = value;
            }
        }
        public static KarKortnr karKortnr
        {
            get
            {
                if (m_KarKortnr == null) m_KarKortnr = new KarKortnr();
                return m_KarKortnr;
            }
            set
            {
                m_KarKortnr = value;
            }
        }
        public static KarRegnskab karRegnskab
        {
            get
            {
                if (m_KarRegnskab == null) m_KarRegnskab = new KarRegnskab();
                return m_KarRegnskab;
            }
            set
            {
                m_KarRegnskab = value;
            }
        }
        public static KarStatus karStatus
        {
            get
            {
                if (m_KarStatus == null) m_KarStatus = new KarStatus();
                return m_KarStatus;
            }
            set
            {
                m_KarStatus = value;
            }
        }
						
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Diagnostics.Process[] p = System.Diagnostics.Process.GetProcessesByName("Medlem3060");
            if (p.Length > 1)
            {
                clsUtil.SetForegroundWindow(p[0].MainWindowHandle);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FrmMain());
            }
        }
    }
}
