using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;


namespace Trans2SummaHDC
{
    public class recBankafstemning
    {
        public recBankafstemning() { }

        public int? bid { get; set; }
        public decimal? bsaldo { get; set; }
        public bool? bskjul { get; set; }
        public DateTime? bdato { get; set; }
        public string btekst { get; set; }
        public decimal? bbeløb { get; set; }
        public int? rid { get; set; }
        public int? tid { get; set; }
        public int? tnr { get; set; }
    }

    public class KarBankafstemning : List<recBankafstemning>
    {
        private string m_path { get; set; }

        public KarBankafstemning()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            m_path = rec_regnskab.Placering + "kladder.dat";
            m_path = @"F:\summatest\vBankafstemningEksport.txt";
            open();
        }

        public void open()
        {
            FileStream ts = new FileStream(m_path, FileMode.Open, FileAccess.Read, FileShare.None);
            string ln = null;
            recBankafstemning rec;
            Regex regexKontoplan = new Regex(@"""(.*?)"";|([^;]*);|(.*)$");
            using (StreamReader sr = new StreamReader(ts, Encoding.Default))
            {
                while ((ln = sr.ReadLine()) != null)
                {
                    int i = 0;
                    int iMax = 11;
                    string[] value = new string[iMax];
                    foreach (Match m in regexKontoplan.Matches(ln))
                    {
                        for (int j = 1; j <= 3; j++)
                        {
                            if (m.Groups[j].Success)
                            {
                                if (i < iMax)
                                {
                                    value[i++] = m.Groups[j].ToString();
                                    break;
                                }
                            }
                        }
                    }

                    rec = new recBankafstemning
                    {
                        bid = Microsoft.VisualBasic.Information.IsNumeric(value[0]) ? int.Parse(value[0]) : (int?)null,
                        bsaldo = Microsoft.VisualBasic.Information.IsNumeric(value[1]) ? decimal.Parse(value[1]) : (decimal?)null,
                        bskjul = Microsoft.VisualBasic.Information.IsNumeric(value[2]) ? (int.Parse(value[2]) != 0) : (bool?)null,
                        bdato = Microsoft.VisualBasic.Information.IsDate(value[3]) ? DateTime.Parse(value[3]) : (DateTime?)null,
                        btekst = value[4],
                        bbeløb = Microsoft.VisualBasic.Information.IsNumeric(value[5]) ? decimal.Parse(value[5]) : (decimal?)null,
                        rid = Microsoft.VisualBasic.Information.IsNumeric(value[6]) ? int.Parse(value[6]) : (int?)null,
                        tid = Microsoft.VisualBasic.Information.IsNumeric(value[7]) ? int.Parse(value[7]) : (int?)null,
                        tnr = Microsoft.VisualBasic.Information.IsNumeric(value[8]) ? int.Parse(value[8]) : (int?)null
                    };
                    this.Add(rec);
                }
            }
            ts.Close();
        }

        public void load()
        {
            int? lastBid = 0;
            var qry = from b in this orderby b.bid select b;
            int antal = qry.Count();
            foreach (var b in qry)
            {
                if (lastBid != b.bid)
                {
                    tblbankkonto recBankkonto = new tblbankkonto
                    {
                        pid = (int)b.bid,
                        saldo = b.bsaldo,
                        skjul = b.bskjul,
                        dato = b.bdato,
                        tekst = b.btekst,
                        belob = b.bbeløb
                    };
                    Program.dbDataTransSumma.tblbankkontos.InsertOnSubmit(recBankkonto);
                }

                if ((b.rid != null) && (b.tid != null) && (b.tnr != null))
                {
                    tblbankafsteminit recBankafsteminit = new tblbankafsteminit
                    {
                        bid = (int)b.bid,
                        rid = (int)b.rid,
                        tid = (int)b.tid,
                        tnr = (int)b.tnr
                    };
                    Program.dbDataTransSumma.tblbankafsteminits.InsertOnSubmit(recBankafsteminit);
                }

                lastBid = b.bid;
            }
            Program.dbDataTransSumma.SubmitChanges();
        }
    }
}
