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
    public class clsLog
    {
        public int? Nr;
        public DateTime? Logdato;
        public int? Akt_id;
        public DateTime? Akt_dato;
    }

    public partial class clsMedlem
    {
        private DateTime? m_indmeldelsesDato = null;
        private DateTime? m_udmeldelsesDato = null;
        private DateTime? m_kontingentTilbageførtDato = null;
        private DateTime? m_kontingentBetalingsDato = null;
        private DateTime? m_kontingentBetaltTilDato = null;
        private DateTime? m_opkrævningsDato = null;
        private int m_BetalingsFristiDageGamleMedlemmer = global::nsPuls3060.Properties.Settings.Default.BetalingsFristiDageGamleMedlemmer;
        private int m_BetalingsFristiDageNyeMedlemmer = global::nsPuls3060.Properties.Settings.Default.BetalingsFristiDageNyeMedlemmer;
        private DateTime m_kontingentTilDato31 = DateTime.MinValue;
        private DateTime m_kontingentBetaltDato31 = DateTime.MinValue;
        private DateTime m_restanceTilDatoGamleMedlemmer = DateTime.MinValue;
        private DateTime restanceTilDatoNyeMedlemmer = DateTime.MinValue;
        private bool m_b10 = false; // Seneste Indmelses dato fundet
        private bool m_b20 = false; // Seneste PBS opkrævnings dato fundet
        private bool m_b30 = false; // Seneste Kontingent betalt til dato fundet
        private bool m_b31 = false; // Næst seneste Kontingent betalt til dato fundet
        private bool m_b40 = false; // Seneste PBS betaling tilbageført fundet
        private bool m_b50 = false; // Udmeldelses dato fundet

        public DateTime? indmeldelsesDato
        {
            get
            {
                return m_indmeldelsesDato;
            }
            set
            {
                m_indmeldelsesDato = value;
            }
        }
        public DateTime? udmeldelsesDato
        {
            get
            {
                return m_udmeldelsesDato;
            }
            set
            {
                m_udmeldelsesDato = value;
            }
        }
        public DateTime? kontingentTilbageførtDato
        {
            get
            {
                return m_kontingentTilbageførtDato;
            }
            set
            {
                m_kontingentTilbageførtDato = value;
            }
        }
        public DateTime? kontingentBetalingsDato
        {
            get
            {
                return m_kontingentBetalingsDato;
            }
            set
            {
                m_kontingentBetalingsDato = value;
            }
        }
        public DateTime? kontingentBetaltTilDato
        {
            get
            {
                return m_kontingentBetaltTilDato;
            }
            set
            {
                m_kontingentBetaltTilDato = value;
            }
        }
        public DateTime? opkrævningsDato
        {
            get
            {
                return m_opkrævningsDato;
            }
            set
            {
                m_opkrævningsDato = value;
            }
        }
        public Boolean erMedlem() { return erMedlem(DateTime.Now); }
        public Boolean erMedlem(DateTime pDate)
        {


            var qrylog = Program.qryLog()
                                .Where(u => u.Nr == m_Nr)
                                .Where(u => u.Logdato <= pDate)
                                .OrderByDescending(u => u.Logdato);

            foreach (var MedlemLog in qrylog)
            {
                switch (MedlemLog.Akt_id)
                {
                    case 10: // Seneste Indmelses dato
                        if (!m_b10)
                        {
                            m_b10 = true;
                            m_indmeldelsesDato = (DateTime)MedlemLog.Akt_dato;
                        }
                        break;

                    case 20:  // Seneste PBS opkrævnings dato
                        if (!m_b20)
                        {
                            m_b20 = true;
                            m_opkrævningsDato = (DateTime)MedlemLog.Akt_dato;
                        }
                        break;

                    case 30:  // Kontingent betalt til dato
                        if ((m_b30) && (!m_b31)) // Næst seneste Kontingent betalt til dato
                        {
                            m_b31 = true;
                            m_kontingentBetaltDato31 = (DateTime)MedlemLog.Logdato;
                            m_kontingentTilDato31 = (DateTime)MedlemLog.Akt_dato;
                        }
                        if ((!m_b30) && (!m_b31)) // Seneste Kontingent betalt til dato
                        {
                            m_b30 = true;
                            m_kontingentBetalingsDato = (DateTime)MedlemLog.Logdato;
                            m_kontingentBetaltTilDato = (DateTime)MedlemLog.Akt_dato;
                        }
                        break;

                    case 40:  // Seneste PBS betaling tilbageført
                        if (!m_b40)
                        {
                            m_b40 = true;
                            m_kontingentTilbageførtDato = (DateTime)MedlemLog.Akt_dato;
                        }
                        break;

                    case 50:  // Udmeldelses dato
                        if (!m_b50)
                        {
                            m_b50 = true;
                            m_udmeldelsesDato = (DateTime)MedlemLog.Akt_dato;
                        }
                        break;
                }
            }

            //Undersøg vedr ind- og udmeldelse
            if (m_b10) //Findes der en indmeldelse
            {
                if (m_b50) //Findes der en udmeldelse
                {
                    if (m_udmeldelsesDato >= m_indmeldelsesDato) //Er udmeldelsen aktiv
                    {
                        if (m_udmeldelsesDato <= pDate) //Er udmeldelsen aktiv
                        {
                            return false;
                        }
                    }
                }
            }
            else //Der findes ingen indmeldelse
            {
                return false;
            }


            //Find aktive betalingsrecord
            if (m_b40) //Findes der en kontingent tilbageført
            {
                if (m_kontingentTilbageførtDato >= m_kontingentBetalingsDato) //Kontingenttilbageført er aktiv
                {
                    //''!!!Kontingent er tilbageført !!!!!!!!!
                    if (m_b31)
                    {
                        m_kontingentBetalingsDato = m_kontingentBetaltDato31;
                        m_kontingentBetaltTilDato = m_kontingentTilDato31;
                    }
                    else
                    {
                        m_b30 = false;
                    }
                }
            }


            //Undersøg om der er betalt kontingent
            if ((m_b30) && (m_kontingentBetaltTilDato > m_indmeldelsesDato))//Findes der en betaling efter indmelsesdato
            {
                m_restanceTilDatoGamleMedlemmer = ((DateTime)m_kontingentBetaltTilDato).AddDays(m_BetalingsFristiDageGamleMedlemmer);
                if (m_restanceTilDatoGamleMedlemmer >= pDate) //Er kontingentTilDato aktiv
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
                restanceTilDatoNyeMedlemmer = ((DateTime)m_indmeldelsesDato).AddDays(m_BetalingsFristiDageNyeMedlemmer);
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
    }

    class clsPbs
    {
        private recMemRegnskab m_rec_Regnskab;

        public clsPbs() { }

        public static int nextval(string nrserienavn)
        {
            clsRest objRest = new clsRest();
            string strNextnr = objRest.HttpGet2(clsRest.urlBaseType.sync, "NrSerie/" + nrserienavn);
            try
            {
                return int.Parse(strNextnr);
            }
            catch (System.InvalidOperationException)
            {
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
