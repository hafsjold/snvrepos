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
namespace nsPuls3060
{
    class clsPbs
    {
        private DbData3060 m_dbData3060;
        private TblRegnskab m_rec_Regnskab;

        private clsPbs()
        {
        }
        public clsPbs(DbData3060 pdbData3060)
        {
            m_dbData3060 = pdbData3060;
        }

        public Boolean erMedlem(short pNr) { return erMedlem(pNr, DateTime.Now); }
        public Boolean erMedlem(short pNr, DateTime pDate)
        {
            var BetalingsFristiDageGamleMedlemmer = global::nsPuls3060.Properties.Settings.Default.BetalingsFristiDageGamleMedlemmer;
            var BetalingsFristiDageNyeMedlemmer = global::nsPuls3060.Properties.Settings.Default.BetalingsFristiDageNyeMedlemmer;
            var indmeldelsesDato = DateTime.MinValue;
            var udmeldelsesDato = DateTime.MinValue;
            var kontingentTilbageførtDato = DateTime.MinValue;
            var kontingentTilDato31 = DateTime.MinValue;
            var kontingentBetaltDato31 = DateTime.MinValue;
            var kontingentBetaltDato = DateTime.MinValue; ;
            var kontingentTilDato = DateTime.MinValue; ;
            var restanceTilDatoGamleMedlemmer = DateTime.MinValue; ;
            var opkrævningsDato = DateTime.MinValue; ;
            var restanceTilDatoNyeMedlemmer = DateTime.MinValue; ;
            var b10 = false; // Seneste Indmelses dato fundet
            var b20 = false; // Seneste PBS opkrævnings dato fundet
            var b30 = false; // Seneste Kontingent betalt til dato fundet
            var b31 = false; // Næst seneste Kontingent betalt til dato fundet
            var b40 = false; // Seneste PBS betaling tilbageført fundet
            var b50 = false; // Udmeldelses dato fundet

            //Den query skal ændres til en union !!!!!!!!!!!!
            var MedlemLogs = from c in m_dbData3060.TblMedlemLog
                             where c.Nr == pNr && c.Logdato <= pDate
                             orderby c.Logdato descending
                             select c;

            foreach (var MedlemLog in MedlemLogs)
            {
                switch (MedlemLog.Akt_id)
                {
                    case 10: // Seneste Indmelses dato
                        if (!b10)
                        {
                            b10 = true;
                            indmeldelsesDato = (DateTime)MedlemLog.Akt_dato;
                        }
                        break;

                    case 20:  // Seneste PBS opkrævnings dato
                        if (!b20)
                        {
                            b20 = true;
                            opkrævningsDato = (DateTime)MedlemLog.Akt_dato;
                        }
                        break;

                    case 30:  // Kontingent betalt til dato
                        if ((b30) && (!b31)) // Næst seneste Kontingent betalt til dato
                        {
                            b31 = true;
                            kontingentBetaltDato31 = (DateTime)MedlemLog.Logdato;
                            kontingentTilDato31 = (DateTime)MedlemLog.Akt_dato;
                        }
                        if ((!b30) && (!b31)) // Seneste Kontingent betalt til dato
                        {
                            b30 = true;
                            kontingentBetaltDato = (DateTime)MedlemLog.Logdato;
                            kontingentTilDato = (DateTime)MedlemLog.Akt_dato;
                        }
                        break;

                    case 40:  // Seneste PBS betaling tilbageført
                        if (!b40)
                        {
                            b40 = true;
                            kontingentTilbageførtDato = (DateTime)MedlemLog.Akt_dato;
                        }
                        break;

                    case 50:  // Udmeldelses dato
                        if (!b50)
                        {
                            b50 = true;
                            udmeldelsesDato = (DateTime)MedlemLog.Akt_dato;
                        }
                        break;
                }
            }

            //Undersøg vedr ind- og udmeldelse
            if (b10) //Findes der en indmeldelse
            {
                if (b50) //Findes der en udmeldelse
                {
                    if (udmeldelsesDato >= indmeldelsesDato) //Er udmeldelsen aktiv
                    {
                        if (udmeldelsesDato <= pDate) //Er udmeldelsen aktiv
                        {
                            return false;
                        }
                    }
                }
                else //Der findes ingen indmeldelse
                {
                    return false;
                }
            }

            //Find aktive betalingsrecord
            if (b40) //Findes der en kontingent tilbageført
            {
                if (kontingentTilbageførtDato >= kontingentBetaltDato) //Kontingenttilbageført er aktiv
                {
                    //''!!!Kontingent er tilbageført !!!!!!!!!
                    if (b31)
                    {
                        kontingentBetaltDato = kontingentBetaltDato31;
                        kontingentTilDato = kontingentTilDato31;
                    }
                    else
                    {
                        b30 = false;
                    }
                }
            }


            //Undersøg om der er betalt kontingent
            if (b30) //Findes der en betaling
            {
                restanceTilDatoGamleMedlemmer = kontingentTilDato.AddDays(BetalingsFristiDageGamleMedlemmer);
                if (restanceTilDatoGamleMedlemmer >= pDate) //Er kontingentTilDato aktiv
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            { //Der findes ingen betalinger. Nyt medlem?
                restanceTilDatoNyeMedlemmer = indmeldelsesDato.AddDays(BetalingsFristiDageNyeMedlemmer);
                if (restanceTilDatoNyeMedlemmer >= pDate)
                { //Er kontingentTilDato aktiv
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static int nextval(string nrserienavn, DbData3060 pdbData3060)
        {
            try
            {
                var rst = (from c in pdbData3060.Tblnrserie
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
                pdbData3060.Tblnrserie.InsertOnSubmit(rec_nrserie);
                return 0;
            }
        }

        public bool ReadRegnskaber()
        {
            string RegnskabId;
            string RegnskabMappe;
            string line;
            FileStream ts;

            string Eksportmappe = (string)Registry.CurrentUser.OpenSubKey(@"SOFTWARE\STONE'S SOFTWARE\SUMMAPRO\START").GetValue("Eksportmappe");
            string Datamappe = (string)Registry.CurrentUser.OpenSubKey(@"SOFTWARE\STONE'S SOFTWARE\SUMMAPRO\START").GetValue("Datamappe");
            DirectoryInfo dir = new DirectoryInfo(Datamappe);
            foreach (var sub_dir in dir.GetDirectories())
            {
                switch (sub_dir.Name.ToUpper())
                {
                    case "BRUGERDATA":
                    case "FORMULARER":
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
                                (from d in m_dbData3060.TblRegnskab
                                 where d.Rid.ToString() == RegnskabId
                                 select d).First();

                        }
                        catch (System.InvalidOperationException)
                        {
                            m_rec_Regnskab = new TblRegnskab
                            {
                                Rid = int.Parse(RegnskabId)
                            };
                            m_dbData3060.TblRegnskab.InsertOnSubmit(m_rec_Regnskab);
                            m_dbData3060.SubmitChanges();
                        }
                        RegnskabMappe = Datamappe + sub_dir.Name + @"\";
                        m_rec_Regnskab.Placering = RegnskabMappe;

                        m_rec_Regnskab.Eksportmappe = Eksportmappe + @"\";

                        if (m_rec_Regnskab.FraPBS == null)
                        {
                            DirectoryInfo infoEksportmappe = new DirectoryInfo(m_rec_Regnskab.Eksportmappe);
                            if (infoEksportmappe.Exists)
                            {
                                m_rec_Regnskab.FraPBS = Eksportmappe + @"\FraPBS\";
                                DirectoryInfo infoFraPBS = new DirectoryInfo(m_rec_Regnskab.FraPBS);
                                if (!infoFraPBS.Exists)
                                {
                                    infoFraPBS.Create();
                                }
                            }
                        }

                        if (m_rec_Regnskab.TilPBS == null)
                        {
                            DirectoryInfo infoEksportmappe = new DirectoryInfo(m_rec_Regnskab.Eksportmappe);
                            if (infoEksportmappe.Exists)
                            {
                                m_rec_Regnskab.TilPBS = Eksportmappe + @"\TilPBS\";
                                DirectoryInfo infoTilPBS = new DirectoryInfo(m_rec_Regnskab.TilPBS);
                                if (!infoTilPBS.Exists)
                                {
                                    infoTilPBS.Create();
                                }
                            }
                        }
                        string[] files = new string[2];
                        files[0] = RegnskabMappe + "regnskab.dat";
                        files[1] = RegnskabMappe + "status.dat";

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
            return true;
        }
    }


}
