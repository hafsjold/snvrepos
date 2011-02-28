﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;


namespace nsPuls3060
{
    static class Program
    {
#if (DEBUG)
        //private static string m_baseUrl = @"http://hd36:8080";
        private static string m_baseUrl = @"http://localhost:8084";
        //private static string m_baseUrl = @"http://testmedlem3060.appspot.com";
#else
        private static string m_baseUrl = @"http://medlem3060.appspot.com";
#endif

#if (DEBUG)
        private static string m_sftpName = "TestHD36";
        //private static string m_sftpName = "Test";
        //private static string m_sftpName = "Produktion";
#else
        private static string m_sftpName = "Produktion";
#endif
        private static string m_Smtphost;
        private static string m_Smtpport;
        private static string m_Smtpssl;
        private static string m_Smtpuser;
        private static string m_Smtppasswd;
        private static string m_MailToName;
        private static string m_MailToAddr;
        private static string m_MailFrom;
        private static string m_MailReply;

        private static string m_path_to_lock_summasummarum_kontoplan;
        private static FileStream m_filestream_to_lock_summasummarum_kontoplan;
        private static DbDataTransSumma m_dbDataTransSumma;
        private static dsMedlem m_dsMedlemGlobal;
        private static KarMedlemmer m_KarMedlemmer;
        private static MemMedlemDictionary m_dicMedlem;
        private static MemRegnskab m_memRegnskab;
        private static MemKreditor m_memKreditor;
        private static MemInfotekst m_memInfotekst;
        private static MemAktivRegnskab m_memAktivRegnskab;
        private static MemPbsnetdir m_memPbsnetdir;
        private static MemAktivitet m_memAktivitet;
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
        private static KarFakturaer_k m_KarFakturaer_k;
        private static KarFakturastr_k m_KarFakturastr_k;
        private static KarFakturavarer_k m_KarFakturavarer_k;

        public static string AppEngName
        {
            get
            {
                try
                {
                    string[] lines = Regex.Split(m_baseUrl, "//");
                    return lines[1];
                }
                catch (Exception)
                {
                    return m_baseUrl;
                }
            }
        }
        public static string baseUrl
        {
            get
            {
                return m_baseUrl;
            }
        }
        public static string sftpName
        {
            get
            {
                return m_sftpName;
            }
        }
        public static string Smtphost
        {
            get
            {
                if (m_Smtphost == null)
                {
                    clsRest objRest = new clsRest();
                    m_Smtphost = objRest.HttpGet2(clsRest.urlBaseType.sync, "Sysinfo/SMTPHOST");
                }
                return m_Smtphost;
            }
            set
            {
                m_Smtphost = value;
            }
        }
        public static string Smtpport
        {
            get
            {
                if (m_Smtpport == null)
                {
                    clsRest objRest = new clsRest();
                    m_Smtpport = objRest.HttpGet2(clsRest.urlBaseType.sync, "Sysinfo/SMTPPORT");
                }
                return m_Smtpport;
            }
            set
            {
                m_Smtpport = value;
            }
        }
        public static string Smtpssl
        {
            get
            {
                if (m_Smtpssl == null)
                {
                    clsRest objRest = new clsRest();
                    m_Smtpssl = objRest.HttpGet2(clsRest.urlBaseType.sync, "Sysinfo/SMTPSSL");
                }
                return m_Smtpssl;
            }
            set
            {
                m_Smtpssl = value;
            }
        }
        public static string Smtpuser
        {
            get
            {
                if (m_Smtpuser == null)
                {
                    clsRest objRest = new clsRest();
                    m_Smtpuser = objRest.HttpGet2(clsRest.urlBaseType.sync, "Sysinfo/SMTPUSER");
                }
                return m_Smtpuser;
            }
            set
            {
                m_Smtpuser = value;
            }
        }
        public static string Smtppasswd
        {
            get
            {
                if (m_Smtppasswd == null)
                {
                    clsRest objRest = new clsRest();
                    m_Smtppasswd = objRest.HttpGet2(clsRest.urlBaseType.sync, "Sysinfo/SMTPPASSWD");
                }
                return m_Smtppasswd;
            }
            set
            {
                m_Smtppasswd = value;
            }
        }
        public static string MailToName
        {
            get
            {
                if (m_MailToName == null)
                {
                    clsRest objRest = new clsRest();
                    m_MailToName = objRest.HttpGet2(clsRest.urlBaseType.sync, "Sysinfo/MAILTONAME");
                }
                return m_MailToName;
            }
            set
            {
                m_MailToName = value;
            }
        }
        public static string MailToAddr
        {
            get
            {
                if (m_MailToAddr == null)
                {
                    clsRest objRest = new clsRest();
                    m_MailToAddr = objRest.HttpGet2(clsRest.urlBaseType.sync, "Sysinfo/MAILTOADDR");
                }

                return m_MailToAddr;
            }
            set
            {
                m_MailToAddr = value;
            }
        }
        public static string MailFrom
        {
            get
            {
                if (m_MailFrom == null)
                {
                    clsRest objRest = new clsRest();
                    m_MailFrom = objRest.HttpGet2(clsRest.urlBaseType.sync, "Sysinfo/MAILFROM");
                }
                return m_MailFrom;
            }
            set
            {
                m_MailFrom = value;
            }
        }
        public static string MailReply
        {
            get
            {
                if (m_MailReply == null)
                {
                    clsRest objRest = new clsRest();
                    m_MailReply = objRest.HttpGet2(clsRest.urlBaseType.sync, "Sysinfo/MAILREPLY");
                }
                return m_MailReply;
            }
            set
            {
                m_MailReply = value;
            }
        }

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
        public static DbDataTransSumma dbDataTransSumma
        {
            get
            {
                if (m_dbDataTransSumma == null)
                {
                    if (!File.Exists(global::nsPuls3060.Properties.Settings.Default.DataBasePath))
                    {
                        OpenFileDialog openFileDialog1 = new OpenFileDialog();
                        openFileDialog1.DefaultExt = "sdf";
                        openFileDialog1.FileName = global::nsPuls3060.Properties.Settings.Default.DataBasePath;
                        openFileDialog1.CheckFileExists = true;
                        openFileDialog1.CheckPathExists = true;
                        openFileDialog1.Filter = "Database files (*.sdf)|*.sdf|All files (*.*)|*.*";
                        openFileDialog1.FilterIndex = 1;
                        openFileDialog1.Multiselect = false;
                        openFileDialog1.Title = "Vælg SQL Database";

                        DialogResult res = openFileDialog1.ShowDialog();
                        if (res == DialogResult.OK)
                        {
                            global::nsPuls3060.Properties.Settings.Default.DataBasePath = openFileDialog1.FileName;
                            global::nsPuls3060.Properties.Settings.Default.Save();
                        }
                    }
                    m_dbDataTransSumma = new DbDataTransSumma(global::nsPuls3060.Properties.Settings.Default.DataBasePath);
                }
                return m_dbDataTransSumma;
            }
            set
            {
                m_dbDataTransSumma = value;
            }
        }
        public static dsMedlem dsMedlemGlobal
        {
            get
            {
                if (m_dsMedlemGlobal == null)
                {
                    m_dsMedlemGlobal = new dsMedlem();
                    if (isAppEngOnline)
                    {
                        m_dsMedlemGlobal.fillPerson();
                        m_dsMedlemGlobal.fillMedlog();
                    }
                    m_dsMedlemGlobal.filldsMedlem();
                }
                return m_dsMedlemGlobal;
            }
        }
        public static dsMedlem.tblMedlogDataTable tblMedlog
        {
            get
            {
                return dsMedlemGlobal.tblMedlog;
            }
        }
        public static dsMedlem.tblPersonDataTable tblPerson
        {
            get
            {
                return dsMedlemGlobal.tblPerson;
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
        public static MemKreditor memKreditor
        {
            get
            {
                if (m_memKreditor == null) m_memKreditor = new MemKreditor();
                return m_memKreditor;
            }
            set
            {
                m_memKreditor = value;
            }
        }
        public static MemInfotekst memInfotekst
        {
            get
            {
                if (m_memInfotekst == null) m_memInfotekst = new MemInfotekst();
                return m_memInfotekst;
            }
            set
            {
                m_memInfotekst = value;
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
        public static MemAktivitet memAktivitet
        {
            get
            {
                if (m_memAktivitet == null) m_memAktivitet = new MemAktivitet();
                return m_memAktivitet;
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
        public static IEnumerable<clsLog> qryLog()
        {

            IEnumerable<clsLog> qryLogResult = from m in Program.tblMedlog.AsEnumerable()
                                               select new clsLog
                                               {
                                                   Nr = (int?)m.Nr,
                                                   Logdato = (DateTime?)m.Logdato,
                                                   Akt_id = (int?)m.Akt_id,
                                                   Akt_dato = (DateTime?)m.Akt_dato
                                               };

            return qryLogResult;
        }

        public static FrmMain frmMain { get; set; }
        public static bool isAppEngOnline
        {
            get
            {
                clsRest objRest = new clsRest();
                string retur = objRest.HttpGet2(clsRest.urlBaseType.data, "online");
                if (retur == "Status: 200") return true;
                else return false;
            }
        }
        public static bool ValidatekBank(string Bank)
        {
            if (Bank == null) return false;
            string[] value = new string[2];
            Regex regex = new Regex("(^[0-9]*) ([0-9]*$)");
            Match m = regex.Match(Bank);
            if (m.Success)
            {
                for (int j = 1; j <= 2; j++)
                {
                    if (m.Groups[j].Success)
                    {
                        value[j - 1] = m.Groups[j].ToString().Trim();
                    }
                }
                string wRegnr = value[0];
                string wKonto = value[1];

                if ((wRegnr.Length == 4) & (wKonto.Length == 10)) return true;
                else return false;
            }
            return false;
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
