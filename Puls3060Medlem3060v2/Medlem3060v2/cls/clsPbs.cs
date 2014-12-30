using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using Microsoft.VisualBasic;
using System.Transactions;
using System.Text.RegularExpressions;

namespace nsPuls3060v2
{
    public class clsLog
    {
        public int? Id;
        public int? Nr;
        public DateTime? Logdato;
        public int? Akt_id;
        public DateTime? Akt_dato;
    }

    class clsPbs
    {
        private recRegnskaber m_rec_Regnskaber;

        public clsPbs() { }

        public bool ReadRegnskaber()
        {
            string RegnskabId;
            string RegnskabMappe;
            string line;
            FileStream ts;
            string Eksportmappe;
            string Datamappe;

            try
            {
                Eksportmappe = (string)Registry.CurrentUser.OpenSubKey(@"SOFTWARE\STONE'S SOFTWARE\SUMMAPRO\START").GetValue("Eksportmappe");
                Datamappe = (string)Registry.CurrentUser.OpenSubKey(@"SOFTWARE\STONE'S SOFTWARE\SUMMAPRO\START").GetValue("Datamappe");
            }
            catch (System.NullReferenceException)
            {
                return false;
            }
            DirectoryInfo dir = new DirectoryInfo(Datamappe);
            foreach (var sub_dir in dir.GetDirectories())
            {
                if (Information.IsNumeric(sub_dir.Name))
                {
                    switch (sub_dir.Name.ToUpper())
                    {
                        case "-2":
                        case "-1":
                        case "0":
                            break;

                        default:
                            //do somthing here
                            RegnskabId = sub_dir.Name;

                            try
                            {
                                m_rec_Regnskaber =
                                    (from d in Program.memRegnskaber
                                     where d.rid.ToString() == RegnskabId
                                     select d).First();

                            }
                            catch (System.InvalidOperationException)
                            {
                                m_rec_Regnskaber = new recRegnskaber
                                {
                                    rid = int.Parse(RegnskabId)
                                };
                                Program.memRegnskaber.Add(m_rec_Regnskaber);
                            }
                            RegnskabMappe = Datamappe + sub_dir.Name + @"\";
                            m_rec_Regnskaber.Placering = RegnskabMappe;

                            m_rec_Regnskaber.Eksportmappe = Eksportmappe + @"\";

                            if (m_rec_Regnskaber.FraPBS == null)
                            {
                                DirectoryInfo infoEksportmappe = new DirectoryInfo(m_rec_Regnskaber.Eksportmappe);
                                if (infoEksportmappe.Exists)
                                {
                                    m_rec_Regnskaber.FraPBS = Eksportmappe + @"\FraPBS\";
                                    DirectoryInfo infoFraPBS = new DirectoryInfo(m_rec_Regnskaber.FraPBS);
                                    if (!infoFraPBS.Exists)
                                    {
                                        infoFraPBS.Create();
                                    }
                                }
                            }

                            if (m_rec_Regnskaber.TilPBS == null)
                            {
                                DirectoryInfo infoEksportmappe = new DirectoryInfo(m_rec_Regnskaber.Eksportmappe);
                                if (infoEksportmappe.Exists)
                                {
                                    m_rec_Regnskaber.TilPBS = Eksportmappe + @"\TilPBS\";
                                    DirectoryInfo infoTilPBS = new DirectoryInfo(m_rec_Regnskaber.TilPBS);
                                    if (!infoTilPBS.Exists)
                                    {
                                        infoTilPBS.Create();
                                    }
                                }
                            }
                            string[] files = new string[2];
                            files[0] = RegnskabMappe + "regnskab.dat";
                            files[1] = RegnskabMappe + "status.dat";
                            m_rec_Regnskaber.Afsluttet = false;
                            foreach (var file in files)
                            {
                                ts = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.None);
                                using (StreamReader sr = new StreamReader(ts, Encoding.Default))
                                {
                                    while ((line = sr.ReadLine()) != null)
                                    {
                                        if (line.Length > 0)
                                        {
                                            string[] X = line.Split('=');
                                            switch (X[0])
                                            {
                                                case "Navn":
                                                    m_rec_Regnskaber.Navn = X[1];
                                                    break;
                                                case "Oprettet":
                                                    m_rec_Regnskaber.Oprettet = clsUtil.MSSerial2DateTime(double.Parse(X[1]));
                                                    break;
                                                case "Start":
                                                    m_rec_Regnskaber.Start = clsUtil.MSSerial2DateTime(double.Parse(X[1]));
                                                    break;
                                                case "Slut":
                                                    m_rec_Regnskaber.Slut = clsUtil.MSSerial2DateTime(double.Parse(X[1]));
                                                    break;
                                                case "DatoLaas":
                                                    m_rec_Regnskaber.DatoLaas = clsUtil.MSSerial2DateTime(double.Parse(X[1]));
                                                    break;
                                                case "Afsluttet":
                                                    m_rec_Regnskaber.Afsluttet = (X[1] == "1") ? true : false;
                                                    break;
                                                case "Firmanavn":
                                                    m_rec_Regnskaber.Firmanavn = X[1];
                                                    break;
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }
            }
            return true;
        }

        public class clsSysinfo
        {
            public string vkey = "";
            public string val = "";
        }

    }
}
