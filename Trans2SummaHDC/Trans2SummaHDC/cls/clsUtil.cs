using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.Threading;
using System.Globalization;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.Win32;

namespace Trans2SummaHDC
{
    class clsPbs
    {
        private recMemRegnskab m_rec_Regnskab;

        public clsPbs() { }

        public static int nextval(string nrserienavn)
        {
            try
            {
                var rst = (from c in Program.dbDataTransSumma.tblnrseries
                           where c.nrserienavn == nrserienavn
                           select c).First();

                if (rst.sidstbrugtenr != null)
                {
                    rst.sidstbrugtenr += 1;
                    return rst.sidstbrugtenr.Value;
                }
                else
                {
                    rst.sidstbrugtenr = 0;
                    return rst.sidstbrugtenr.Value;
                }
            }
            catch (System.InvalidOperationException)
            {
                tblnrserie rec_nrserie = new tblnrserie
                {
                    nrserienavn = nrserienavn,
                    sidstbrugtenr = 0
                };
                Program.dbDataTransSumma.tblnrseries.InsertOnSubmit(rec_nrserie);
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

    public class ColumnSorter : IComparer
    {
        private int m_CurrentColumn;
        private SortOrder m_CurrentSortOrder;
        public int CurrentColumn
        {
            get
            {
                return m_CurrentColumn;
            }
            set
            {
                if (m_CurrentColumn == value)
                {
                    switch (m_CurrentSortOrder)
                    {
                        case SortOrder.Ascending:
                            m_CurrentSortOrder = SortOrder.Descending;
                            break;
                        case SortOrder.Descending:
                            m_CurrentSortOrder = SortOrder.Ascending;
                            break;
                        case SortOrder.None:
                            m_CurrentSortOrder = SortOrder.Ascending;
                            break;
                        default:
                            m_CurrentSortOrder = SortOrder.Ascending;
                            break;
                    }
                }
                else m_CurrentSortOrder = SortOrder.Ascending;

                m_CurrentColumn = value;
            }
        }

        public int Compare(object x, object y)
        {
            ListViewItem rowA = (ListViewItem)x;
            ListViewItem rowB = (ListViewItem)y;
            try
            {
                string sA = rowA.SubItems[m_CurrentColumn].Text;
                string sB = rowB.SubItems[m_CurrentColumn].Text;
                if (m_CurrentColumn == 1)
                {
                    sA = lpad(sA, 5, ' ');
                    sB = lpad(sB, 5, ' ');
                }
                if (m_CurrentSortOrder == SortOrder.Ascending)
                {
                    return String.Compare(sA, sB);
                }
                else
                {
                    return String.Compare(sB, sA);
                }

            }
            catch (ArgumentOutOfRangeException)
            {

                return 0;
            }
        }

        public ColumnSorter()
        {
            m_CurrentColumn = 0;
            m_CurrentSortOrder = SortOrder.Descending;

        }

        public string lpad(Object oVal, int Length, char PadChar)
        {
            string Val = oVal.ToString();
            return Val.PadLeft(Length, PadChar);
        }

    }

    public static class clsUtil
    {
        const double J_correction_01_01_4712BC = 1721119.5;
        const double J_correction_01_01_1900 = -693899;

        public static DateTime bankdageplus(DateTime pfradato, int antdage)
        {
            DateTime fradato = new DateTime(pfradato.Year, pfradato.Month, pfradato.Day);
            DateTime dato;
            int antal = 0;
            bool fridag;
            bool negativ;
            DateTime[] paaskedag = {   new DateTime(2009, 4, 12)
                                     , new DateTime(2010, 4, 4) 
                                     , new DateTime(2011, 4, 24) 
                                     , new DateTime(2012, 4, 8) 
                                     , new DateTime(2013, 3, 31) 
                                     , new DateTime(2014, 4, 20) 
                                     , new DateTime(2015, 4, 5) 
                                     , new DateTime(2016, 3, 27) 
                                     , new DateTime(2017, 4, 16) 
                                     , new DateTime(2018, 4, 1) 
                                     , new DateTime(2019, 4, 21) 
                                     , new DateTime(2020, 4, 12) 
                                   };
            if (antdage < 0)
            {
                negativ = true;
                dato = fradato.AddDays(1);
            }
            else
            {
                negativ = false;
                dato = fradato.AddDays(-1);
            }

            while (antal <= Math.Abs(antdage))
            {
                if (negativ) dato = dato.AddDays(-1);
                else dato = dato.AddDays(1);

                if (dato.DayOfWeek == DayOfWeek.Saturday) fridag = true; //lørdag
                else if (dato.DayOfWeek == DayOfWeek.Sunday) fridag = true; //søndag
                else if ((dato.Month == 1) && (dato.Day == 1)) fridag = true; //1. nytårsdag
                else if ((from p in paaskedag select p).Contains(dato.AddDays(3))) fridag = true; //skærtorsdag
                else if ((from p in paaskedag select p).Contains(dato.AddDays(2))) fridag = true; //langfredag
                else if ((from p in paaskedag select p).Contains(dato.AddDays(-1))) fridag = true; //2. påskedag
                else if ((from p in paaskedag select p).Contains(dato.AddDays(-26))) fridag = true; //st. bededag (fredag)
                else if ((from p in paaskedag select p).Contains(dato.AddDays(-39))) fridag = true; //kristi himmelfartsdag (torsdag)
                else if ((from p in paaskedag select p).Contains(dato.AddDays(-40))) fridag = true; //fredag efter kristi himmelfartsdag
                else if ((from p in paaskedag select p).Contains(dato.AddDays(-50))) fridag = true; //2. pinsedag
                else if ((dato.Month == 6) && (dato.Day == 5)) fridag = true; //grundlovsdag
                else if ((dato.Month == 12) && (dato.Day == 24)) fridag = true; //juleaften
                else if ((dato.Month == 12) && (dato.Day == 25)) fridag = true; //1. juledag
                else if ((dato.Month == 12) && (dato.Day == 26)) fridag = true; //2. juledag
                else if ((dato.Month == 12) && (dato.Day == 31)) fridag = true; //nytårsaftens dag
                else fridag = false;
                if (!fridag) ++antal;
            }

            return dato;
        }

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public static bool IsProcessOpen(string name)
        {
            foreach (Process p in Process.GetProcesses())
            {
                if (p.ProcessName.Contains(name))
                {
                    if (!p.ProcessName.Contains("Trans2SummaHDC")) return true;
                }
            }
            return false;
        }

        public static int SummaDateTime2Serial(DateTime dt)
        {
            double sumStartDate = 1088647360;
            DateTime MSStartDate = new DateTime(2009, 1, 1);
            double days = (double)DateAndTime.DateDiff("d", MSStartDate, dt, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays);
            return (int)(sumStartDate + days * 32);
        }

        public static DateTime SummaSerial2DateTime(double J)
        {
            double sumStartDate = 1088647360;
            DateTime MSStartDate = new DateTime(2009, 1, 1);
            double days = (J - sumStartDate) / 32;
            if (days > -7000)
            {
                return MSStartDate.AddDays(days);
            }
            else
            {
                return new DateTime(1900, 1, 1);
            }
        }

        public static double MSDateTime2Serial(DateTime dt)
        {
            return GregorianDate2JulianDayNumber(dt, J_correction_01_01_1900);
        }

        public static DateTime MSSerial2DateTime(double J)
        {
            return JulianDayNumber2GregorianDate(J, J_correction_01_01_1900);
        }

        private static double GregorianDate2JulianDayNumber(DateTime dt, double pJ0)
        {
            // formel from website: http://www.astro.uu.nl/~strous/AA/en/reken/juliaansedag.html

            int j = dt.Year;
            int m = dt.Month;
            double d = (double)dt.Day + (double)dt.Hour / 24 + (double)dt.Minute / (24 * 60) + (double)dt.Second / (24 * 3600);
            //If m < 3, then replace m by m + 12 and j by j − 1
            if (m < 3)
            {
                m += 12;
                j--;
            }

            //(Eq. 1) c = ⌊j/100⌋
            double c = Math.Floor((double)j / 100);

            //(Eq. 2) x = j − 100*c = j mod 100
            double x = j - 100 * c;

            //(Eq. 3) J₀ = 1721119.5
            double J0 = pJ0;

            //(Eq. 4) J₁ = ⌊146097*c/4⌋
            double J1 = Math.Floor(146097 * c / 4);

            //(Eq. 5) J₂ = ⌊36525*x/100⌋
            double J2 = Math.Floor(36525 * x / 100);

            //(Eq. 6) J₃ = ⌊(153*m − 457)/5⌋
            double J3 = Math.Floor((153 * (double)m - 457) / 5);

            //(Eq. 7) J = J₁ + J₂ + J₃ + d − 1 + J₀ = ⌊146097*c/4⌋ + ⌊36525*x/100⌋ + ⌊(153*m − 457)/5⌋ + d − 1 + 1721119.5
            double J = J1 + J2 + J3 + d - 1 + J0;

            return J;
        }

        private static DateTime JulianDayNumber2GregorianDate(double J, double pJ0)
        {
            // formel from website: http://www.astro.uu.nl/~strous/AA/en/reken/juliaansedag.html

            double J0 = pJ0;

            //1. (Eq. 8) x₂ = J − 1721119.5
            double x2 = J - J0;

            //2. (Eq. 9) c₂ = ⌊(8*x₂ + 7)/292194⌋
            double c2 = Math.Floor((8 * x2 + 7) / 292194);

            //3. (Eq. 10) x₁ = x₂ − ⌊146097*c₂/4⌋
            double x1 = x2 - Math.Floor(146097 * c2 / 4);

            //4. (Eq. 11) c₁ = ⌊(200*x₁ + 199)/73050⌋
            double c1 = Math.Floor((200 * x1 + 199) / 73050);

            //5. (Eq. 12) x₀ = x₁ − ⌊36525*c₁/100⌋
            double x0 = x1 - Math.Floor(36525 * c1 / 100);

            //6. (Eq. 13) j = 100*c₂ + c₁
            double j = 100 * c2 + c1;

            //7. (Eq. 14) m = ⌊(10*x₀ + 923)/306⌋
            double m = Math.Floor((10 * x0 + 923) / 306);

            //8. (Eq. 15) d = x₀ − ⌊(153*m − 457)/5⌋ + 1
            double d = x0 - Math.Floor((153 * m - 457) / 5) + 1;

            //9. If m > 12, then replace m by m − 12 and j by j + 1
            if (m > 12)
            {
                j++;
                m -= 12;
            }

            double ts = d - Math.Floor(d);
            double h = Math.Floor(ts * 24);
            ts = ts - h / 24;
            double min = Math.Floor(ts * 24 * 60);
            ts = ts - min / (24 * 60);
            double s = Math.Round(ts * 24 * 60 * 60);
            ts = ts - s / (24 * 60 * 60);
            DateTime dt;
            if (d < 1)
            {
                dt = new DateTime((int)j, (int)m, 1, (int)h, (int)min, (int)s);
                return dt.AddDays(-1);
            }
            else
            {
                return new DateTime((int)j, (int)m, (int)d, (int)h, (int)min, (int)s);
            }

        }
        public static void INI_File()
        {
            string data = @"[WindowSettings]
Window X Pos=0
Window Y Pos=0
Window Maximized=false
Window Name=Jabberwocky

[Logging]
Directory=C:\Rosetta Stone\Logs
";
            string pattern = @"
^                           # Beginning of the line
((?:\[)                     # Section Start
     (?<Section>[^\]]*)     # Actual Section text into Section Group
 (?:\])                     # Section End then EOL/EOB
 (?:[\r\n]{0,}|\Z))         # Match but don't capture the CRLF or EOB
 (                          # Begin capture groups (Key Value Pairs)
  (?!\[)                    # Stop capture groups if a [ is found; new section
  (?<Key>[^=]*?)            # Any text before the =, matched few as possible
  (?:=)                     # Get the = now
  (?<Value>[^\r\n]*)        # Get everything that is not an Line Changes
  (?:[\r\n]{0,4})           # MBDC \r\n
  )+                        # End Capture groups";

            Dictionary<string, Dictionary<string, string>> InIFile
            = (from Match m in Regex.Matches(data, pattern, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline)
               select new
               {
                   Section = m.Groups["Section"].Value,

                   kvps = (from cpKey in m.Groups["Key"].Captures.Cast<Capture>().Select((a, i) => new { a.Value, i })
                           join cpValue in m.Groups["Value"].Captures.Cast<Capture>().Select((b, i) => new { b.Value, i }) on cpKey.i equals cpValue.i
                           select new KeyValuePair<string, string>(cpKey.Value, cpValue.Value)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value)

               }).ToDictionary(itm => itm.Section, itm => itm.kvps);

            Console.WriteLine(InIFile["WindowSettings"]["Window Name"]); // Jabberwocky
            Console.WriteLine(InIFile["Logging"]["Directory"]);          // C:\Rosetta Stone\Logs

        }
    }

    public class clsNavnAdresse
    {
        public string Navn { get; set; }
        public string[] Adresse { get; set; }
        public string Postnr { get; set; }
        public string Bynavn { get; set; }
        public string Land { get; set; }

        public string getCsv()
        {
            string NvnAdr = "";
            if ((Navn != null) && (Navn.Length > 0))
            {
                if (NvnAdr.Length > 0)
                    NvnAdr += "," + quote(Navn, 2);
                else
                    NvnAdr = quote(Navn, 2);
            }
            if ((Adresse != null) && (Adresse.Count() > 0))
            {
                for (int i = 0; i < Adresse.Count(); i++)
                {
                    if ((Adresse[i] != null) && (Adresse[i].Length > 0))
                    {
                        if (NvnAdr.Length > 0)
                            NvnAdr += "," + quote(Adresse[i], 2);
                        else
                            NvnAdr = quote(Adresse[i], 2);
                    }
                }
            }

            string PostnrBynavn = Postnr;
            if ((PostnrBynavn != null) && (PostnrBynavn.Length > 0))
                PostnrBynavn += " " + Bynavn;
            else
                PostnrBynavn = Bynavn;
            if ((PostnrBynavn != null) && (PostnrBynavn.Length > 0))
            {
                if (NvnAdr.Length > 0)
                    NvnAdr += "," + quote(PostnrBynavn, 2);
                else
                    NvnAdr = quote(PostnrBynavn, 2);
            }
            if ((Land != null) && (Land.Length > 0))
            {
                if (NvnAdr.Length > 0)
                    NvnAdr += "," + quote(Land, 2);
                else
                    NvnAdr = quote(Land, 2);
            }
            return NvnAdr;
        }

        private string quote(string val, int antalQuotes)
        {
            string s;
            if (val == null)
                s = "";
            else
                s = val;

            s = s.Trim();
            if (s.IndexOf(' ') == -1 && s.IndexOf(',') == -1 && s.IndexOf('"') == -1)
            {
                return s;
            }
            else
            {
                if (antalQuotes == 1)
                {
                    return "\"" + s + "\"";
                }
                else
                {
                    return "\"\"" + s + "\"\"";
                }
            }
        }


    }
}
