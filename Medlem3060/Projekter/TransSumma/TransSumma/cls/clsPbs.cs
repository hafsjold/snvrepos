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

namespace nsPuls3060
{
    class clsPbs
    {
        private recMemRegnskab m_rec_Regnskab;

        public clsPbs() { }

        public static int nextval(string nrserienavn)
        {
            try
            {
                var rst = (from c in Program.dbDataTransSumma.Tblnrserie
                           where c.Nrserienavn == nrserienavn
                           select c).First();

                if (rst.Sidstbrugtenr != null)
                {
                    rst.Sidstbrugtenr += 1;
                    return rst.Sidstbrugtenr.Value;
                }
                else
                {
                    rst.Sidstbrugtenr = 0;
                    return rst.Sidstbrugtenr.Value;
                }
            }
            catch (System.InvalidOperationException)
            {
                Tblnrserie rec_nrserie = new Tblnrserie
                {
                    Nrserienavn = nrserienavn,
                    Sidstbrugtenr = 0
                };
                Program.dbDataTransSumma.Tblnrserie.InsertOnSubmit(rec_nrserie);
                Program.dbDataTransSumma.SubmitChanges();

                return 0;
            }
        }

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
                                m_rec_Regnskab =
                                    (from d in Program.memRegnskab
                                     where d.Rid.ToString() == RegnskabId
                                     select d).First();

                            }
                            catch (System.InvalidOperationException)
                            {
                                m_rec_Regnskab = new recMemRegnskab()
                                {
                                    Rid = int.Parse(RegnskabId)
                                };
                                Program.memRegnskab.Add(m_rec_Regnskab);
                            }
                            RegnskabMappe = Datamappe + sub_dir.Name + @"\";
                            m_rec_Regnskab.Placering = RegnskabMappe;

                            m_rec_Regnskab.Eksportmappe = Eksportmappe + @"\";

                            string[] files = new string[2];
                            files[0] = RegnskabMappe + "regnskab.dat";
                            files[1] = RegnskabMappe + "status.dat";
                            m_rec_Regnskab.Afsluttet = false;
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
                                                    m_rec_Regnskab.Navn = X[1];
                                                    break;
                                                case "Oprettet":
                                                    m_rec_Regnskab.Oprettet = clsUtil.MSSerial2DateTime(double.Parse(X[1]));
                                                    break;
                                                case "Start":
                                                    m_rec_Regnskab.Start = clsUtil.MSSerial2DateTime(double.Parse(X[1]));
                                                    break;
                                                case "Slut":
                                                    m_rec_Regnskab.Slut = clsUtil.MSSerial2DateTime(double.Parse(X[1]));
                                                    break;
                                                case "DatoLaas":
                                                    m_rec_Regnskab.DatoLaas = clsUtil.MSSerial2DateTime(double.Parse(X[1]));
                                                    break;
                                                case "Afsluttet":
                                                    m_rec_Regnskab.Afsluttet = (X[1] == "1") ? true : false;
                                                    break;
                                                case "Firmanavn":
                                                    m_rec_Regnskab.Firmanavn = X[1];
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

    }
}
