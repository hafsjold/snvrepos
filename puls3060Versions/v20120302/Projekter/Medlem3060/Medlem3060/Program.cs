﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;



namespace nsPuls3060
{
    static class Program
    {
        static byte[] s_aditionalEntropy = { 9, 8, 7, 6, 5 };
        private static string m_Smtphost;
        private static string m_Smtpport;
        private static string m_Smtpssl;
        private static string m_Smtpuser;
        private static string m_Smtppasswd;
        private static string m_MailToName;
        private static string m_MailToAddr;
        private static string m_MailFrom;
        private static string m_MailReply;
        private static string m_Password;

        private static string m_path_to_lock_summasummarum_kontoplan;
        private static FileStream m_filestream_to_lock_summasummarum_kontoplan;
        private static DbData3060 m_dbData3060;
        private static dsMedlem m_dsMedlemImport;
        private static KarMedlemmer m_KarMedlemmer;
        private static MemMedlemDictionary m_dicMedlem;
        private static MemAktivRegnskab m_memAktivRegnskab;
        private static MemRegnskaber m_memRegnskaber;
        private static MemPbsnetdir m_memPbsnetdir;
        private static KarDkkonti m_KarDkkonti;
        private static KarFakturaer_s m_KarFakturaer_s;
        private static KarFakturastr_s m_KarFakturastr_s;
        private static KarFakturavarer_s m_KarFakturavarer_s;
        private static KarKortnr m_KarKortnr;
        private static KarRegnskab m_KarRegnskab;
        private static KarStatus m_KarStatus;
        private static KarKladde m_KarKladde;
        private static KarPosteringsjournal m_KarPosteringsjournal;
        private static KarKontoplan m_KarKontoplan;
        private static KarPosteringer m_KarPosteringer;
        private static KarFakturaer_k m_KarFakturaer_k;
        private static KarFakturastr_k m_KarFakturastr_k;
        private static KarFakturavarer_k m_KarFakturavarer_k;

        public static string Smtphost
        {
            get
            {
                if (m_Smtphost == null) m_Smtphost = (Program.dbData3060.ExecuteQuery<clsPbs.clsSysinfo>("SELECT * FROM [tblSysinfo] WHERE [vkey] = N'SMTPHOST';")).First().val;
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
                if (m_Smtpport == null) m_Smtpport = (Program.dbData3060.ExecuteQuery<clsPbs.clsSysinfo>("SELECT * FROM [tblSysinfo] WHERE [vkey] = N'SMTPPORT';")).First().val;
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
                if (m_Smtpssl == null) m_Smtpssl = (Program.dbData3060.ExecuteQuery<clsPbs.clsSysinfo>("SELECT * FROM [tblSysinfo] WHERE [vkey] = N'Smtpssl';")).First().val;
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
                if (m_Smtpuser == null) m_Smtpuser = (Program.dbData3060.ExecuteQuery<clsPbs.clsSysinfo>("SELECT * FROM [tblSysinfo] WHERE [vkey] = N'Smtpuser';")).First().val;
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
                if (m_Smtppasswd == null) m_Smtppasswd = (Program.dbData3060.ExecuteQuery<clsPbs.clsSysinfo>("SELECT * FROM [tblSysinfo] WHERE [vkey] = N'Smtppasswd';")).First().val;
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
                if (m_MailToName == null) m_MailToName = (Program.dbData3060.ExecuteQuery<clsPbs.clsSysinfo>("SELECT * FROM [tblSysinfo] WHERE [vkey] = N'MailToName';")).First().val;
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
                if (m_MailToAddr == null) m_MailToAddr = (Program.dbData3060.ExecuteQuery<clsPbs.clsSysinfo>("SELECT * FROM [tblSysinfo] WHERE [vkey] = N'MailToAddr';")).First().val;
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
                if (m_MailFrom == null) m_MailFrom = (Program.dbData3060.ExecuteQuery<clsPbs.clsSysinfo>("SELECT * FROM [tblSysinfo] WHERE [vkey] = N'MailFrom';")).First().val;
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
                if (m_MailReply == null) m_MailReply = (Program.dbData3060.ExecuteQuery<clsPbs.clsSysinfo>("SELECT * FROM [tblSysinfo] WHERE [vkey] = N'MailReply';")).First().val;
                return m_MailReply;
            }
            set
            {
                m_MailReply = value;
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
        public static DbData3060 dbData3060
        {
            get
            {
                if (m_dbData3060 == null)
                {
                    DialogResult res = DialogResult.OK;
                    m_Password = global::nsPuls3060.Properties.Settings.Default.UserPassword;
                    if (Unprotect(m_Password) == null)
                        res = (new FrmPassword()).ShowDialog();
                    if (res != DialogResult.OK) return null;
                    m_dbData3060 = new DbData3060(global::nsPuls3060.Properties.Settings.Default.puls3061_dk_dbConnectionString_Prod + ";Password=" + Unprotect(m_Password));    
                }
                return m_dbData3060;
            }
            set
            {
                m_dbData3060 = value;
            }
        }
        public static dsMedlem dsMedlemImport
        {
            get
            {
                if (m_dsMedlemImport == null)
                {
                    m_dsMedlemImport = new dsMedlem();
                    m_dsMedlemImport.filldsMedlem();
                }
                return m_dsMedlemImport;
            }
            set
            {
                m_dsMedlemImport = value;
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
        public static MemRegnskaber memRegnskaber
        {
            get
            {
                if (m_memRegnskaber == null) m_memRegnskaber = new MemRegnskaber();
                return m_memRegnskaber;
            }
            set
            {
                m_memRegnskaber = value;
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
        public static KarPosteringsjournal karPosteringsjournal
        {
            get
            {
                if (m_KarPosteringsjournal == null) m_KarPosteringsjournal = new KarPosteringsjournal();
                return m_KarPosteringsjournal;
            }
            set
            {
                m_KarPosteringsjournal = value;
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

        public static recRegnskaber qryAktivRegnskab()
        {
            try
            {
                return (from a in Program.memAktivRegnskab
                        join r in Program.memRegnskaber on a.Rid equals r.rid
                        select r).First();

            }
            catch (System.InvalidOperationException)
            {
                return new recRegnskaber
                {
                    rid = 999,
                    Navn = "Vælg et eksisterende regnskab"
                };
            }
        }
        public static IQueryable<clsLog> qryLog()
        {
            var qryMedlemLog = from m in Program.dbData3060.tblMedlemLogs
                               select new clsLog
                               {
                                   Id = (int?)m.id,
                                   Nr = (int?)m.Nr,
                                   Logdato = (DateTime?)m.logdato,
                                   Akt_id = (int?)m.akt_id,
                                   Akt_dato = (DateTime?)m.akt_dato
                               };
            var qryFak = from f in Program.dbData3060.tblfaks
                         join p in Program.dbData3060.tbltilpbs on f.tilpbsid equals p.id
                         select new clsLog
                         {
                             Id = (int?)f.id,
                             Nr = (int?)f.Nr,
                             Logdato = (DateTime)p.bilagdato,
                             Akt_id = (int?)20,
                             Akt_dato = (DateTime?)f.betalingsdato
                         };

            var qryBetlin = from b in Program.dbData3060.tblbetlins
                            join f in Program.dbData3060.tblfaks on b.faknr equals f.faknr
                            where b.pbstranskode == "0236" || b.pbstranskode == "0297"
                            select new clsLog
                            {
                                Id = (int?)b.id,
                                Nr = (int?)b.Nr,
                                Logdato = (DateTime?)b.indbetalingsdato,
                                Akt_id = (int?)30,
                                Akt_dato = (DateTime?)f.tildato
                            };

            var qryBetlin40 = from b in Program.dbData3060.tblbetlins
                              where b.pbstranskode == "0237"
                              select new clsLog
                              {
                                  Id = (int?)b.id,
                                  Nr = (int?)b.Nr,
                                  Logdato = (DateTime?)(((DateTime)b.betalingsdato).AddSeconds(-30)),  //Workaround for problem med samme felt (b.Betalingsdato) 2 gange
                                  Akt_id = (int?)40,
                                  Akt_dato = (DateTime?)b.betalingsdato
                              };


            var qryLogResult = qryMedlemLog.Union(qryFak)
                                           .Union(qryBetlin)
                                           .Union(qryBetlin40);

            return qryLogResult;
        }

        public static FrmMain frmMain { get; set; }

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
        public static string Protect(string secret)
        {
            try
            {
                var data = Encoding.Unicode.GetBytes(secret);
                byte[] encrypted = ProtectedData.Protect(data, s_aditionalEntropy, DataProtectionScope.CurrentUser);
                return Convert.ToBase64String(encrypted); 
            }
            catch (CryptographicException e)
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
            catch (CryptographicException e)
            {
                return null;
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
