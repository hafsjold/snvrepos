using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;


namespace nsPuls3060
{
    public class recPosteringsjournal
    {
        public recPosteringsjournal() { }
        public int? Rid { get; set; }
        public DateTime? Dato { get; set; }
        public int? Bilag { get; set; }
        public string Tekst { get; set; }
        public int? Konto { get; set; }
        public string Kontonavn { get; set; }
        public decimal? Bruttobeløb { get; set; }
        public decimal? Moms { get; set; }
        public decimal? Belob { get; set; }
        public int? Nr { get; set; }
        public int? Id { get; set; }
        public int? Sag { get; set; }
    }

    public class KarPosteringsjournal : List<recPosteringsjournal>
    {
        private string m_path { get; set; }
        private int m_rid;

        public KarPosteringsjournal()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            m_path = rec_regnskab.Eksportmappe + "Posteringsjournal.txt";
            m_rid = rec_regnskab.rid;
            open();
        }

        public void open()
        {
            FileStream ts = new FileStream(m_path, FileMode.Open, FileAccess.Read, FileShare.None);
            string ln = null;
            int lnr = 0;
            int Posteringsjournal_Format = 0;
            recPosteringsjournal rec;
            Regex regexPosteringsjournal = new Regex(@"""(.*?)"",|([^,]*),|(.*)$");
            using (StreamReader sr = new StreamReader(ts, Encoding.Default))
            {
                while ((ln = sr.ReadLine()) != null)
                {
                    lnr++;
                    int i = 0;
                    int iMax = 11;
                    string[] value = new string[iMax];
                    foreach (Match m in regexPosteringsjournal.Matches(ln))
                    {
                        for (int j = 1; j <= 3; j++)
                        {
                            if (m.Groups[j].Success)
                            {
                                if (i < iMax)
                                {
                                    value[i++] = m.Groups[j].ToString();
                                }
                                j = 4; //Break loop
                            }
                        }
                    }
                    
                    if (lnr == 1)
                    {
                        if ((value[5] == "Moms") && (value[6] == "Debet")) { Posteringsjournal_Format = 1; }
                        else if ((value[5] == "Bruttobeløb") && (value[6] == "Moms")) { Posteringsjournal_Format = 2; }
                        else { Posteringsjournal_Format = 9; }
                    }
                    if ((lnr > 1) && (Posteringsjournal_Format == 1))
                    {
                        //Dato,Bilag,Tekst,Konto,Kontonavn,Moms,Debet,Kredit,Nr.,Id,Sag
                        decimal wMoms = Microsoft.VisualBasic.Information.IsNumeric(value[5]) ? decimal.Parse(value[5]) : (decimal)0;
                        decimal wDebet = Microsoft.VisualBasic.Information.IsNumeric(value[6]) ? decimal.Parse(value[6]) : (decimal)0;
                        decimal wKredit = Microsoft.VisualBasic.Information.IsNumeric(value[7]) ? decimal.Parse(value[7]) : (decimal)0;

                        rec = new recPosteringsjournal
                        {
                            Rid = m_rid,
                            Dato = Microsoft.VisualBasic.Information.IsDate(value[0]) ? DateTime.Parse(value[0]) : (DateTime?)null,
                            Bilag = Microsoft.VisualBasic.Information.IsNumeric(value[1]) ? int.Parse(value[1]) : (int?)null,
                            Tekst = value[2],
                            Konto = Microsoft.VisualBasic.Information.IsNumeric(value[3]) ? int.Parse(value[3]) : (int?)null,
                            Kontonavn = value[4],
                            Bruttobeløb = wDebet - wKredit,
                            Moms = wMoms,
                            Belob = wDebet - wKredit - wMoms,
                            Nr = Microsoft.VisualBasic.Information.IsNumeric(value[8]) ? int.Parse(value[8]) : (int?)null,
                            Id = Microsoft.VisualBasic.Information.IsNumeric(value[9]) ? int.Parse(value[9]) : (int?)null,
                            Sag = Microsoft.VisualBasic.Information.IsNumeric(value[10]) ? int.Parse(value[10]) : (int?)null
                        };
                        this.Add(rec);
                    }

                    if ((lnr > 1) && (Posteringsjournal_Format == 2))
                    {
                        //Dato,Bilag,Tekst,Konto,Kontonavn,Bruttobeløb,Moms,Beløb,Nr.,Id,Sag
                        rec = new recPosteringsjournal
                        {
                            Rid = m_rid,
                            Dato = Microsoft.VisualBasic.Information.IsDate(value[0]) ? DateTime.Parse(value[0]) : (DateTime?)null,
                            Bilag = Microsoft.VisualBasic.Information.IsNumeric(value[1]) ? int.Parse(value[1]) : (int?)null,
                            Tekst = value[2],
                            Konto = Microsoft.VisualBasic.Information.IsNumeric(value[3]) ? int.Parse(value[3]) : (int?)null,
                            Kontonavn = value[4],
                            Bruttobeløb = Microsoft.VisualBasic.Information.IsNumeric(value[5]) ? decimal.Parse(value[5]) : (decimal?)null,
                            Moms = Microsoft.VisualBasic.Information.IsNumeric(value[6]) ? decimal.Parse(value[6]) : (decimal?)null,
                            Belob = Microsoft.VisualBasic.Information.IsNumeric(value[7]) ? decimal.Parse(value[7]) : (decimal?)null,
                            Nr = Microsoft.VisualBasic.Information.IsNumeric(value[8]) ? int.Parse(value[8]) : (int?)null,
                            Id = Microsoft.VisualBasic.Information.IsNumeric(value[9]) ? int.Parse(value[9]) : (int?)null,
                            Sag = Microsoft.VisualBasic.Information.IsNumeric(value[10]) ? int.Parse(value[10]) : (int?)null
                        };
                        this.Add(rec);
                    }
                }
            }
        }

    }
}
