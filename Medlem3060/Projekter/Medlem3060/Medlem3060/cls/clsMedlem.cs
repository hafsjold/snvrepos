using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.Linq.Mapping;

namespace nsPuls3060
{
    public partial class clsMedlem
    {
        private int? m_Nr;
        private String m_Navn;
        private String m_Kaldenavn;
        private String m_Adresse;
        private String m_Postnr;
        private String m_Bynavn;
        private String m_Email;
        private String m_Telefon;
        private String m_Bank;
        private String m_Debgrup;
        private String m_Krdgrup;
        private String m_Debktonr;
        private String m_Krdktonr;
        private String m_Erdebitor;
        private String m_Erkreditor;
        private String m_CsvString;
        private clsCsv m_Csv;

        public int? Nr
        {
            get
            {
                return m_Nr;
            }
            set
            {
                m_Nr = value;
            }
        }

        public String Navn
        {
            get
            {
                return m_Navn;
            }
            set
            {
                m_Navn = value;
            }
        }

        public String Kaldenavn
        {
            get
            {
                return m_Kaldenavn;
            }
            set
            {
                m_Kaldenavn = value;
            }
        }

        public String Adresse
        {
            get
            {
                return m_Adresse;
            }
            set
            {
                m_Adresse = value;
            }
        }

        public String Postnr
        {
            get
            {
                return m_Postnr;
            }
            set
            {
                m_Postnr = value;
            }
        }

        public String Bynavn
        {
            get
            {
                return m_Bynavn;
            }
            set
            {
                m_Bynavn = value;
            }
        }

        public String Email
        {
            get
            {
                return m_Email;
            }
            set
            {
                m_Email = value;
            }
        }

        public String Telefon
        {
            get
            {
                return m_Telefon;
            }
            set
            {
                m_Telefon = value;
            }
        }

        public String Bank
        {
            get
            {
                return m_Bank;
            }
            set
            {
                m_Bank = value;
            }
        }
        public String Debgrup //11
        {
            get
            {
                return m_Debgrup;
            }
            set
            {
                m_Debgrup = value;
            }
        }
        public String Krdgrup //12
        {
            get
            {
                return m_Krdgrup;
            }
            set
            {
                m_Krdgrup = value;
            }
        }
        public String Debktonr //20
        {
            get
            {
                return m_Debktonr;
            }
            set
            {
                m_Debktonr = value;
            }
        }
        public String Krdktonr //21
        {
            get
            {
                return m_Krdktonr;
            }
            set
            {
                m_Krdktonr = value;
            }
        }
        public String Erdebitor //22
        {
            get
            {
                return m_Erdebitor;
            }
            set
            {
                m_Erdebitor = value;
            }
        }
        public String Erkreditor //23
        {
            get
            {
                return m_Erkreditor;
            }
            set
            {
                m_Erkreditor = value;
            }
        }
        public String CsvString
        {
            get
            {
                return m_CsvString;
            }
        }

        public clsMedlem() { }

        public clsMedlem(string pCsvString)
        {
            m_CsvString = pCsvString;
            m_Csv = new clsCsv();
            m_Csv.ln = CsvString;
            m_Csv.todo = CsvToDoType.fdUpdateClsMedlem;

            clsCsv.MedlemFieldUpdate += new nsPuls3060.clsCsv.MedlemFieldUpdateHandler(On_MedlemFieldUpdate);
            ParseCsvString(ref m_Csv);
            clsCsv.MedlemFieldUpdate -= new nsPuls3060.clsCsv.MedlemFieldUpdateHandler(On_MedlemFieldUpdate);
        }

        public void setKreditor()
        {
            if (this.Erkreditor != "1")
            {
                if (Program.ValidatekBank(Bank))
                {
                    Erkreditor = "1";
                    if (Krdktonr.Length == 0)
                    {
                        Krdgrup = "Standard";
                        Krdktonr = KarDkkonti.nextval().ToString();
                    }
                }
            }
        }

        public string getNewCvsString()
        {
            m_Csv = new clsCsv();
            m_Csv.ln = Program.memMedlemDictionary.NewMedlemCsvString(this.Nr); //Ny tom string
            m_Csv.todo = CsvToDoType.fdUpdateCsvString;

            clsCsv.MedlemCsvStringUpdate += new nsPuls3060.clsCsv.MedlemFieldUpdateHandler(On_MedlemCsvStringNew);
            ParseCsvString(ref m_Csv);
            clsCsv.MedlemCsvStringUpdate -= new nsPuls3060.clsCsv.MedlemFieldUpdateHandler(On_MedlemCsvStringNew);
            m_CsvString = m_Csv.ln_raw;
            return m_CsvString;
        }

        public string getUpdatedCvsString()
        {
            m_Csv = new clsCsv();
            m_Csv.ln = m_CsvString;
            m_Csv.todo = CsvToDoType.fdUpdateCsvString;

            clsCsv.MedlemCsvStringUpdate += new nsPuls3060.clsCsv.MedlemFieldUpdateHandler(On_MedlemCsvStringUpdate);
            ParseCsvString(ref m_Csv);
            clsCsv.MedlemCsvStringUpdate -= new nsPuls3060.clsCsv.MedlemFieldUpdateHandler(On_MedlemCsvStringUpdate);
            m_CsvString = m_Csv.ln_raw;
            return m_CsvString;
        }

        private void On_MedlemCsvStringNew()
        {
            int? w_nr = m_Csv.nr;
            int? w_subnr = 0;
            if (m_Csv.niveau > 0)
            {
                w_nr = m_Csv.stack[(int)m_Csv.niveau - 1].nr;
                w_subnr = m_Csv.nr;
            }
            string fieldKey = clsField.buildKey(w_nr, w_subnr);


            switch (fieldKey)
            {
                case "2":
                    m_Csv.value_new = Navn != null ? Navn : "";
                    break;

                case "3":
                    break;

                case "3,1":
                    m_Csv.value_new = Adresse != null ? Adresse : "";
                    break;

                case "4":
                    m_Csv.value_new = Postnr != null ? Postnr : "";
                    break;

                case "5":
                    m_Csv.value_new = Bynavn != null ? Bynavn : "";
                    break;

                case "8":
                    m_Csv.value_new = Kaldenavn != null ? Kaldenavn : "";
                    break;

                case "9":
                    m_Csv.value_new = Email != null ? Email : "";
                    break;

                case "13":
                    m_Csv.value_new = Telefon != null ? Telefon : "";
                    break;

                case "16":
                    m_Csv.value_new = Bank != null ? Bank : "";
                    break;

                default:
                    m_Csv.value_new = m_Csv.value;
                    break;
            }
            if (m_Csv.value != m_Csv.value_new)
            {
                string part1 = m_Csv.ln_raw.Substring(0, m_Csv.comma_start + 1);
                string part2 = m_Csv.ln_raw.Substring(m_Csv.comma_start + 1, (m_Csv.comma_end - 1) - (m_Csv.comma_start + 1) + 1);
                string part3 = m_Csv.ln_raw.Substring(m_Csv.comma_end);
                string part2_new = quote(m_Csv.value_new, m_Csv.niveau);
                m_Csv.ln_updated = part1 + part2_new + part3;
                m_Csv.ln = part1 + part2_new + part3;

                int n_delta = part2_new.Length - part2.Length;
                m_Csv.n += n_delta;
                m_Csv.fld_end += n_delta;
                m_Csv.comma_end += n_delta;
            }

            if (m_Csv.niveau > 0)
            {
                m_Csv.stack[m_Csv.niveau - 1].value_new = m_Csv.ln_raw;
            }
        }

        private void On_MedlemCsvStringUpdate()
        {
            int? w_nr = m_Csv.nr;
            int? w_subnr = 0;
            if (m_Csv.niveau > 0)
            {
                w_nr = m_Csv.stack[(int)m_Csv.niveau - 1].nr;
                w_subnr = m_Csv.nr;
            }
            string fieldKey = clsField.buildKey(w_nr, w_subnr);


            switch (fieldKey)
            {
                case "2":
                    m_Csv.value_new = Navn != null ? Navn : "";
                    break;

                case "3":
                    break;

                case "3,1":
                    m_Csv.value_new = Adresse != null ? Adresse : "";
                    break;

                case "4":
                    m_Csv.value_new = Postnr != null ? Postnr : "";
                    break;

                case "5":
                    m_Csv.value_new = Bynavn != null ? Bynavn : "";
                    break;

                case "8":
                    m_Csv.value_new = Kaldenavn != null ? Kaldenavn : "";
                    break;

                case "9":
                    m_Csv.value_new = Email != null ? Email : "";
                    break;

                case "11":
                    m_Csv.value_new = Debgrup != null ? Debgrup : "";
                    break;

                case "12":
                    m_Csv.value_new = Krdgrup != null ? Krdgrup : "";
                    break;

                case "13":
                    m_Csv.value_new = Telefon != null ? Telefon : "";
                    break;

                case "16":
                    m_Csv.value_new = Bank != null ? Bank : "";
                    break;

                case "20":
                    m_Csv.value_new = Debktonr != null ? Debktonr : "";
                    break;

                case "21":
                    m_Csv.value_new = Krdktonr != null ? Krdktonr : "";
                    break;

                case "22":
                    m_Csv.value_new = Erdebitor != null ? Erdebitor : "";
                    break;

                case "23":
                    m_Csv.value_new = Erkreditor != null ? Erkreditor : "";
                    break;


                default:
                    m_Csv.value_new = m_Csv.value;
                    break;
            }
            if (m_Csv.value != m_Csv.value_new)
            {
                string part1 = m_Csv.ln_raw.Substring(0, m_Csv.comma_start + 1);
                string part2 = m_Csv.ln_raw.Substring(m_Csv.comma_start + 1, (m_Csv.comma_end - 1) - (m_Csv.comma_start + 1) + 1);
                string part3 = m_Csv.ln_raw.Substring(m_Csv.comma_end);
                string part2_new = quote(m_Csv.value_new, m_Csv.niveau);
                m_Csv.ln_updated = part1 + part2_new + part3;
                m_Csv.ln = part1 + part2_new + part3;

                int n_delta = part2_new.Length - part2.Length;
                m_Csv.n += n_delta;
                m_Csv.fld_end += n_delta;
                m_Csv.comma_end += n_delta;
            }

            if (m_Csv.niveau > 0)
            {
                m_Csv.stack[m_Csv.niveau - 1].value_new = m_Csv.ln_raw;
            }
        }

        private void On_MedlemFieldUpdate()
        {
            int w_nr = m_Csv.nr;
            int w_subnr = 0;
            if (m_Csv.niveau > 0)
            {
                w_nr = m_Csv.stack[m_Csv.niveau - 1].nr;
                w_subnr = m_Csv.nr;
            }
            string fieldKey = clsField.buildKey(w_nr, w_subnr);

            switch (fieldKey)
            {
                case "1":
                    Nr = int.Parse(m_Csv.value);
                    break;

                case "2":
                    Navn = m_Csv.value;
                    break;

                case "3,1":
                    Adresse = m_Csv.value;
                    break;

                case "4":
                    Postnr = m_Csv.value;
                    break;

                case "5":
                    Bynavn = m_Csv.value;
                    break;

                case "8":
                    Kaldenavn = m_Csv.value;
                    break;

                case "9":
                    Email = m_Csv.value;
                    break;

                case "11":
                    Debgrup = m_Csv.value;
                    break;

                case "12":
                    Krdgrup = m_Csv.value;
                    break;

                case "13":
                    Telefon = m_Csv.value;
                    break;

                case "16":
                    Bank = m_Csv.value;
                    break;

                case "20":
                    Debktonr = m_Csv.value;
                    break;

                case "21":
                    Krdktonr = m_Csv.value;
                    break;

                case "22":
                    Erdebitor = m_Csv.value;
                    break;

                case "23":
                    Erkreditor = m_Csv.value;
                    break;

                default:
                    break;
            }
            return;
        }

        private void ParseCsvString(ref clsCsv Csv)
        {
            ParseStatus stat;
            FoundChars found = FoundChars.fdNoneFound;

            Csv.nr = 0;
            Csv.comma_end = -1;
            stat = ParseStatus.fdParserOutsideField;

            for (Csv.n = 0; Csv.n < Csv.lnLen; Csv.n++)
            {
                switch (stat)
                {
                    case ParseStatus.fdParserInField:
                        switch (Csv.ln.Substring(Csv.n, 1))
                        {
                            case ",":
                                Csv.comma_end = Csv.n;
                                Csv.fld_end = Csv.n - 1;
                                Csv.nr++;
                                Csv.value = Csv.ln.Substring(Csv.fld_start, Csv.fld_end - Csv.fld_start + 1);
                                if (Csv.multifld == true)
                                {
                                    Csv.Push();
                                    ParseCsvString(ref Csv);
                                    Csv.Pop();
                                }
                                Csv.todoAction();
                                stat = ParseStatus.fdParserOutsideField;
                                break;

                            default:
                                if (Csv.n == Csv.lnLen - 1) // End of line condition
                                {
                                    Csv.comma_end = Csv.n + 1;
                                    Csv.fld_end = Csv.n;
                                    Csv.nr++;
                                    Csv.value = Csv.ln.Substring(Csv.fld_start, Csv.fld_end - Csv.fld_start + 1);
                                    if (Csv.multifld == true)
                                    {
                                        Csv.Push();
                                        ParseCsvString(ref Csv);
                                        Csv.Pop();
                                    }
                                    Csv.todoAction();
                                    stat = ParseStatus.fdParserOutsideField;
                                }
                                break;

                        }
                        break;

                    case ParseStatus.fdParserInFieldQuoted:
                        switch (Csv.ln.Substring(Csv.n, 2))
                        {
                            case "\"\"":   //Double quote
                                if ((found & FoundChars.fdDoubleQuoteFound) != FoundChars.fdDoubleQuoteFound) found = found | FoundChars.fdDoubleQuoteFound;
                                Csv.n++;
                                break;

                            case "\",":    //Quote + comma
                                Csv.comma_end = Csv.n + 1;
                                Csv.fld_end = Csv.n - 1;
                                Csv.nr++;
                                Csv.value = Csv.ln.Substring(Csv.fld_start, Csv.fld_end - Csv.fld_start + 1);
                                if (Csv.multifld)
                                {
                                    Csv.Push();
                                    ParseCsvString(ref Csv);
                                    Csv.Pop();
                                }
                                Csv.todoAction();
                                Csv.n++;
                                stat = ParseStatus.fdParserOutsideField;
                                break;

                            #region Found chars
                            default:
                                switch (Csv.ln.Substring(Csv.n, 1))
                                {
                                    case " ":
                                        if ((found & FoundChars.fdBlankFound) == FoundChars.fdBlankFound) found = found | FoundChars.fdBlankFound;
                                        break;

                                    case ",":
                                        if ((found & FoundChars.fdCommaFound) == FoundChars.fdCommaFound)
                                        {
                                            if ((found & FoundChars.fdMoreCommasFound) != FoundChars.fdMoreCommasFound) found = found | FoundChars.fdMoreCommasFound;
                                        }
                                        else
                                        {
                                            found = found | FoundChars.fdCommaFound;
                                        }
                                        break;

                                    case "\"":
                                        if ((found & FoundChars.fdQuoteFound) != FoundChars.fdQuoteFound) found = found | FoundChars.fdQuoteFound;
                                        break;

                                    case "0":
                                    case "1":
                                    case "2":
                                    case "3":
                                    case "4":
                                    case "5":
                                    case "6":
                                    case "7":
                                    case "8":
                                    case "9":
                                        if ((found & FoundChars.fdNumberFound) != FoundChars.fdNumberFound) found = found | FoundChars.fdNumberFound;
                                        break;

                                    case "+":
                                    case "-":
                                        if ((found & FoundChars.fdSignFound) != FoundChars.fdSignFound) found = found | FoundChars.fdSignFound;
                                        break;

                                    case ".":
                                        if ((found & FoundChars.fdDotFound) != FoundChars.fdDotFound) found = found | FoundChars.fdDotFound;
                                        break;


                                    default:
                                        if ((found & FoundChars.fdAlfaFound) != FoundChars.fdAlfaFound) found = found | FoundChars.fdAlfaFound;
                                        break;
                                }
                                break;
                            #endregion
                        }
                        break;

                    case ParseStatus.fdParserInFieldDoubleQuoted:
                        switch (Csv.ln.Substring(Csv.n, 3))
                        {
                            case "\"\",":  //Double Quote + comma
                                Csv.comma_end = Csv.n + 2;
                                Csv.fld_end = Csv.n - 1;
                                Csv.nr++;
                                Csv.value = Csv.ln.Substring(Csv.fld_start, Csv.fld_end - Csv.fld_start + 1);
                                Csv.todoAction();
                                Csv.n += 2;
                                stat = ParseStatus.fdParserOutsideField;
                                break;

                            #region Found chars
                            default:
                                switch (Csv.ln.Substring(Csv.n, 1))
                                {
                                    case " ":
                                        if ((found & FoundChars.fdBlankFound) == FoundChars.fdBlankFound) found = found | FoundChars.fdBlankFound;
                                        break;

                                    case ",":
                                        if ((found & FoundChars.fdCommaFound) == FoundChars.fdCommaFound)
                                        {
                                            if ((found & FoundChars.fdMoreCommasFound) != FoundChars.fdMoreCommasFound) found = found | FoundChars.fdMoreCommasFound;
                                        }
                                        else
                                        {
                                            found = found | FoundChars.fdCommaFound;
                                        }
                                        break;

                                    case "\"":
                                        if ((found & FoundChars.fdQuoteFound) != FoundChars.fdQuoteFound) found = found | FoundChars.fdQuoteFound;
                                        break;

                                    case "0":
                                    case "1":
                                    case "2":
                                    case "3":
                                    case "4":
                                    case "5":
                                    case "6":
                                    case "7":
                                    case "8":
                                    case "9":
                                        if ((found & FoundChars.fdNumberFound) != FoundChars.fdNumberFound) found = found | FoundChars.fdNumberFound;
                                        break;

                                    case "+":
                                    case "-":
                                        if ((found & FoundChars.fdSignFound) != FoundChars.fdSignFound) found = found | FoundChars.fdSignFound;
                                        break;

                                    case ".":
                                        if ((found & FoundChars.fdDotFound) != FoundChars.fdDotFound) found = found | FoundChars.fdDotFound;
                                        break;

                                    default:
                                        if ((found & FoundChars.fdAlfaFound) != FoundChars.fdAlfaFound) found = found | FoundChars.fdAlfaFound;
                                        break;
                                }
                                break;
                            #endregion
                        }
                        break;

                    case ParseStatus.fdParserOutsideField:
                        switch (Csv.ln.Substring(Csv.n, 1))
                        {
                            case ",":
                                stat = ParseStatus.fdParserOutsideField;
                                Csv.nr++;
                                Csv.comma_start = Csv.comma_end;
                                Csv.comma_end = Csv.n;
                                Csv.fld_start = Csv.n;
                                Csv.fld_end = Csv.n - 1;
                                Csv.value = Csv.ln.Substring(Csv.fld_start, Csv.fld_end - Csv.fld_start + 1);
                                if (Csv.multifld)
                                {
                                    Csv.Push();
                                    ParseCsvString(ref Csv);
                                    Csv.Pop();
                                }
                                Csv.todoAction();
                                break;

                            case "\"":
                                if (Csv.ln.Substring(Csv.n, 3) == "\"\"\"") //Trible quote
                                {
                                    stat = ParseStatus.fdParserInFieldQuoted;
                                    found = FoundChars.fdNoneFound;
                                    Csv.comma_start = Csv.comma_end;
                                    Csv.fld_start = Csv.n + 1;
                                }
                                else if (Csv.ln.Substring(Csv.n, 2) == "\"\"") //Double quote
                                {
                                    stat = ParseStatus.fdParserInFieldDoubleQuoted;
                                    Csv.comma_start = Csv.comma_end;
                                    found = FoundChars.fdNoneFound;
                                    Csv.fld_start = Csv.n + 2;
                                    Csv.n++;
                                }
                                else //Single quote
                                {
                                    stat = ParseStatus.fdParserInFieldQuoted;
                                    found = FoundChars.fdNoneFound;
                                    Csv.comma_start = Csv.comma_end;
                                    Csv.fld_start = Csv.n + 1;
                                }
                                break;

                            default:
                                stat = ParseStatus.fdParserInField;
                                Csv.comma_start = Csv.comma_end;
                                Csv.fld_start = Csv.n;
                                break;
                        }
                        break;
                }
            }
        }

        private string quote(string val, int niveau)
        {
            string s;
            s = val;
            s = s.Trim();
            if (s.IndexOf(' ') == -1 && s.IndexOf(',') == -1 && s.IndexOf('"') == -1)
            {
                return s;
            }
            else
            {
                if (niveau == 0)
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
    public class clsMedlemInternAll
    {
        [Fieldattr(Heading = "Nr")]
        public int? Nr { get; set; }
        [Fieldattr(Heading = "Navn")]
        public string Navn { get; set; }
        [Fieldattr(Heading = "Kaldenavn")]
        public string Kaldenavn { get; set; }
        [Fieldattr(Heading = "Adresse")]
        public string Adresse { get; set; }
        [Fieldattr(Heading = "Postnr")]
        public string Postnr { get; set; }
        [Fieldattr(Heading = "By")]
        public string Bynavn { get; set; }
        [Fieldattr(Heading = "Email")]
        public string Email { get; set; }
        [Fieldattr(Heading = "Telefon")]
        public string Telefon { get; set; }
        [Fieldattr(Heading = "Kom")]
        public int? Knr { get; set; }
        [Fieldattr(Heading = "Køn")]
        public string Kon { get; set; }
        [Fieldattr(Heading = "Født")]
        public DateTime? FodtDato { get; set; }
        [Fieldattr(Heading = "Bank")]
        public string Bank { get; set; }
        [Fieldattr(Heading = "erMedlem")]
        public int? erMedlem { get; set; }
        [Fieldattr(Heading = "Kontingent")]
        public DateTime? kontingentBetaltTilDato { get; set; }
        [Fieldattr(Heading = "Indmeldt")]
        public DateTime? indmeldelsesDato { get; set; }
        [Fieldattr(Heading = "Udmeldt")]
        public DateTime? udmeldelsesDato { get; set; }
        [Fieldattr(Heading = "Opkrævning")]
        public DateTime? opkrævningsDato { get; set; }
        [Fieldattr(Heading = "Tilbageført")]
        public DateTime? kontingentTilbageførtDato { get; set; }
    }

    public class clsMedlemExternAll
    {
        [Fieldattr(Heading = "Nr")]
        public int? Nr { get; set; }
        [Fieldattr(Heading = "Navn")]
        public string Navn { get; set; }
        [Fieldattr(Heading = "Kaldenavn")]
        public string Kaldenavn { get; set; }
        [Fieldattr(Heading = "Adresse")]
        public string Adresse { get; set; }
        [Fieldattr(Heading = "Postnr")]
        public string Postnr { get; set; }
        [Fieldattr(Heading = "By")]
        public string Bynavn { get; set; }
        [Fieldattr(Heading = "Email")]
        public string Email { get; set; }
        [Fieldattr(Heading = "Telefon")]
        public string Telefon { get; set; }
        [Fieldattr(Heading = "Køn")]
        public string Kon { get; set; }
        [Fieldattr(Heading = "Født")]
        public DateTime? FodtDato { get; set; }
        [Fieldattr(Heading = "erMedlem")]
        public int? erMedlem { get; set; }
    }

    public class clsJournalposter
    {
        [Fieldattr(Heading = "ds")]
        public string ds { get; set; }
        [Fieldattr(Heading = "k")]
        public string k { get; set; }
        [Fieldattr(Heading = "Konto")]
        public string Konto { get; set; }
        [Fieldattr(Heading = "Dato")]
        public DateTime? Dato { get; set; }
        [Fieldattr(Heading = "Bilag")]
        public int? Bilag { get; set; }
        [Fieldattr(Heading = "Nr")]
        public int? Nr { get; set; }
        [Fieldattr(Heading = "Id")]
        public int? Id { get; set; }
        [Fieldattr(Heading = "Tekst")]
        public string Tekst { get; set; }
        [Fieldattr(Heading = "Beløb")]
        public decimal? Beløb { get; set; }
    }

}
