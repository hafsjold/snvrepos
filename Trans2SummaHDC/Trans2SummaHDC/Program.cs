using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;


namespace Trans2SummaHDC
{
    static class Program
    {
        public static string ConnectStringWithoutPassword
        {
            get
            {
                if (m_ConnectStringWithoutPassword == null)
                {
#if (DEBUG)
                    m_ConnectStringWithoutPassword = global::Trans2SummaHDC.Properties.Settings.Default.dbDataTransSummaConnectionString;
#else
                    m_ConnectStringWithoutPassword = global::Trans2SummaHDC.Properties.Settings.Default.dbDataTransSummaConnectionString;
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
            return ConnectStringWithoutPassword;
        }

        public static DbDataTransSumma dbDataTransSumma
        {
            get
            {
                if (m_dbDataTransSumma == null)
                {
                    m_dbDataTransSumma = new DbDataTransSumma(dbConnectionString());
                }
                return m_dbDataTransSumma;
            }
            set
            {
                m_dbDataTransSumma = value;
            }
        }
        private static string m_ConnectStringWithoutPassword;
        private static string m_path_to_lock_summasummarum_kontoplan;
        private static FileStream m_filestream_to_lock_summasummarum_kontoplan;
        private static DbDataTransSumma m_dbDataTransSumma;
        private static MemRegnskab m_memRegnskab;
        private static MemAktivRegnskab m_memAktivRegnskab;
        private static KarDkkonti m_KarDkkonti;
        private static KarFakturaer_s m_KarFakturaer_s;
        private static KarFakturastr_s m_KarFakturastr_s;
        private static KarFakturavarer_s m_KarFakturavarer_s;
        private static KarKortnr m_KarKortnr;
        private static KarRegnskab m_KarRegnskab;
        private static KarStatus m_KarStatus;
        private static KarKladde m_KarKladde;
        private static KarKontoplan m_KarKontoplan;
        private static KarPosteringer m_KarPosteringer;
        private static KarKladder m_KarKladder;
        private static KarFakturaer_k m_KarFakturaer_k;
        private static KarFakturastr_k m_KarFakturastr_k;
        private static KarFakturavarer_k m_KarFakturavarer_k;
        private static KarAfstemningskonti m_KarAfstemningskonti;
        private static KarMoms m_KarMoms;
        private static KarKartotek m_KarKartotek;
        private static KarVarer m_KarVarer;
        private static KarTrans2Summa m_KarTrans2Summa;

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

        public static MemRegnskab memRegnskab
        {
            get
            {
                if (m_memRegnskab == null) m_memRegnskab = new MemRegnskab();
                return m_memRegnskab;
            }
            set
            {
                m_memRegnskab = value;
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
        public static KarPosteringer karPosteringer
        {
            get
            {
                if (m_KarPosteringer == null) m_KarPosteringer = new KarPosteringer();
                return m_KarPosteringer;
            }
            set
            {
                m_KarPosteringer = value;
            }
        }
        public static KarKladder karKladder
        {
            get
            {
                if (m_KarKladder == null) m_KarKladder = new KarKladder();
                return m_KarKladder;
            }
            set
            {
                m_KarKladder = value;
            }
        }
        public static KarMoms karMoms
        {
            get
            {
                if (m_KarMoms == null) m_KarMoms = new KarMoms();
                return m_KarMoms;
            }
            set
            {
                m_KarMoms = value;
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
        public static KarVarer karVarer
        {
            get
            {
                if (m_KarVarer == null) m_KarVarer = new KarVarer();
                return m_KarVarer;
            }
            set
            {
                m_KarVarer = value;
            }
        }
        public static KarTrans2Summa karTrans2Summa
        {
            get
            {
                if (m_KarTrans2Summa == null) m_KarTrans2Summa = new KarTrans2Summa();
                return m_KarTrans2Summa;
            }
            set
            {
                m_KarTrans2Summa = value;
            }
        }
        public static KarFakturaer_k karFakturaer_k
        {
            get
            {
                if (m_KarFakturaer_k == null) m_KarFakturaer_k = new KarFakturaer_k();
                return m_KarFakturaer_k;
            }
            set
            {
                m_KarFakturaer_k = value;
            }
        }
        public static KarFakturastr_k karFakturastr_k
        {
            get
            {
                if (m_KarFakturastr_k == null) m_KarFakturastr_k = new KarFakturastr_k();
                return m_KarFakturastr_k;
            }
            set
            {
                m_KarFakturastr_k = value;
            }
        }
        public static KarFakturavarer_k karFakturavarer_k
        {
            get
            {
                if (m_KarFakturavarer_k == null) m_KarFakturavarer_k = new KarFakturavarer_k();
                return m_KarFakturavarer_k;
            }
            set
            {
                m_KarFakturavarer_k = value;
            }
        }
        public static KarAfstemningskonti karAfstemningskonti
        {
            get
            {
                if (m_KarAfstemningskonti == null) m_KarAfstemningskonti = new KarAfstemningskonti();
                return m_KarAfstemningskonti;
            }
            set
            {
                m_KarAfstemningskonti = value;
            }
        }
        public static recMemRegnskab qryAktivRegnskab()
        {
            try
            {
                return (from a in Program.memAktivRegnskab
                        join r in Program.memRegnskab on a.Rid equals r.Rid
                        select r).First();

            }
            catch (System.InvalidOperationException)
            {
                return new recMemRegnskab
                {
                    Rid = 999,
                    Navn = "Vælg et eksisterende regnskab"
                };
            }
        }
        public static bool HafsjoldDataApS
        {
            get
            {
                recMemRegnskab AktivRegnskab = Program.qryAktivRegnskab();
                string Firma = AktivRegnskab.Firmanavn;
                if (Firma == @"Hafsjold Data ApS")
                    return true;
                else
                    return false;
            }
        }

        public static bool Puls3060
        {
            get
            {
                recMemRegnskab AktivRegnskab = Program.qryAktivRegnskab();
                string Firma = AktivRegnskab.Firmanavn;
                if (Firma == @"Løbeklubben PULS 3060")
                    return true;
                else
                    return false;
            }
        }

        public static FrmMain frmMain { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Diagnostics.Process[] p = System.Diagnostics.Process.GetProcessesByName("Trans2SummaHDC");
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
