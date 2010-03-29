using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace nsHafsjoldData
{
    static class Program
    {
        private static string m_path_to_lock_summasummarum_kontoplan;
        private static FileStream m_filestream_to_lock_summasummarum_kontoplan;
        private static DbHafsjoldData m_dbHafsjoldData;
        private static MemAktivRegnskab m_memAktivRegnskab;
        private static KarRegnskab m_KarRegnskab;
        private static KarStatus m_KarStatus;
        private static KarKladde m_KarKladde;
        private static KarKontoplan m_KarKontoplan;
        private static KarKartotek m_KarKartotek;
        private static KarDanskeErhverv m_KarDanskeErhverv;

        public static string path_to_lock_summasummarum_kontoplan
        {
            get
            {
                return m_path_to_lock_summasummarum_kontoplan;
            }
            set
            {
                m_path_to_lock_summasummarum_kontoplan = value;
            }
        }
        public static FileStream filestream_to_lock_summasummarum_kontoplan
        {
            get
            {
                return m_filestream_to_lock_summasummarum_kontoplan;
            }
            set
            {
                m_filestream_to_lock_summasummarum_kontoplan = value;
            }
        }
        public static DbHafsjoldData dbHafsjoldData
        {
            get
            {
                if (m_dbHafsjoldData == null)
                {
                    if (!File.Exists(global::nsHafsjoldData.Properties.Settings.Default.DataBasePath))
                    {
                        OpenFileDialog openFileDialog1 = new OpenFileDialog();
                        openFileDialog1.DefaultExt = "sdf";
                        openFileDialog1.FileName = global::nsHafsjoldData.Properties.Settings.Default.DataBasePath;
                        openFileDialog1.CheckFileExists = true;
                        openFileDialog1.CheckPathExists = true;
                        openFileDialog1.Filter = "Database files (*.sdf)|*.sdf|All files (*.*)|*.*";
                        openFileDialog1.FilterIndex = 1;
                        openFileDialog1.Multiselect = false;
                        openFileDialog1.Title = "Vælg SQL Database";

                        DialogResult res = openFileDialog1.ShowDialog();
                        if (res == DialogResult.OK)
                        {
                            global::nsHafsjoldData.Properties.Settings.Default.DataBasePath = openFileDialog1.FileName;
                            global::nsHafsjoldData.Properties.Settings.Default.Save();
                        }
                    }
                    m_dbHafsjoldData = new DbHafsjoldData(global::nsHafsjoldData.Properties.Settings.Default.DataBasePath);
                }
                return m_dbHafsjoldData;
            }
            set
            {
                m_dbHafsjoldData = value;
            }
        }

        public static MemAktivRegnskab memAktivRegnskab
        {
            get
            {
                if (m_memAktivRegnskab == null) m_memAktivRegnskab = new MemAktivRegnskab();
                return m_memAktivRegnskab;
            }
            set
            {
                m_memAktivRegnskab = value;
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
        public static KarKontoplan karKontoplan
        {
            get
            {
                if (m_KarKontoplan == null) m_KarKontoplan = new KarKontoplan();
                return m_KarKontoplan;
            }
            set
            {
                m_KarKontoplan = value;
            }
        }
        public static KarKartotek karKartotek
        {
            get
            {
                if (m_KarKartotek == null) m_KarKartotek = new KarKartotek();
                return m_KarKartotek;
            }
            set
            {
                m_KarKartotek = value;
            }
        }
        public static KarDanskeErhverv karDanskeErhverv
        {
            get
            {
                if (m_KarDanskeErhverv == null) m_KarDanskeErhverv = new KarDanskeErhverv();
                return m_KarDanskeErhverv;
            }
            set
            {
                m_KarDanskeErhverv = value;
            }
        }
        public static TblRegnskab qryAktivRegnskab()
        {
            try
            {
                return (from a in Program.memAktivRegnskab
                        join r in Program.dbHafsjoldData.TblRegnskab on a.Rid equals r.Rid
                        select r).First();

            }
            catch (System.InvalidOperationException)
            {
                return new TblRegnskab
                {
                    Rid = 999,
                    Navn = "Vælg et eksisterende regnskab"
                };
            }
        }
 
        public static FrmMain frmMain { get; set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Diagnostics.Process[] p = System.Diagnostics.Process.GetProcessesByName("HafsjoldData");
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
