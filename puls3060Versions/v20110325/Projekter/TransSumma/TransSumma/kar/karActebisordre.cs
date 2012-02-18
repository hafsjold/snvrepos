using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace nsPuls3060
{
    public class recActebisordre
    {

        public recActebisordre() { }

        public DateTime? Ordredato { get; set; }
        public long? Ordrenr { get; set; }
        public string Ordreref { get; set; }
        public decimal? Ordrebelob { get; set; }
        public string Ordrestatus { get; set; }
        public string Leveringsadresse { get; set; }
        public int? Pos { get; set; }
        public int? Antal { get; set; }
        public int? Varenr { get; set; }
        public string Sku { get; set; }
        public string Beskrivelse { get; set; }
        public string Ordrerefpos { get; set; }
        public decimal? Stkpris { get; set; }
        public int? Fakturanr { get; set; }
        public long? Leveringsnr { get; set; }
        public string Serienr { get; set; }
        public string Ordrestatuspos { get; set; }
        public string Producent { get; set; }
    }

    class KarActebisordre : List<recActebisordre>
    {
        private string m_path { get; set; }

        public KarActebisordre()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            m_path = rec_regnskab.Eksportmappe + "actebis_ordre.csv";
            open();
        }

        private void open()
        {
            FileStream ts = new FileStream(m_path, FileMode.Open, FileAccess.Read, FileShare.None);
            string ln = null;
            recActebisordre rec;
            Regex regexKontoplan = new Regex(@"""(.*?)"";|([^;]*);|(.*)$");
            using (StreamReader sr = new StreamReader(ts, Encoding.Default))
            {
                while ((ln = sr.ReadLine()) != null)
                {
                    int i = 0;
                    int iMax = 20;
                    string[] value = new string[iMax];
                    foreach (Match m in regexKontoplan.Matches(ln + ";"))
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

                    DateTime wdato;
                    if (DateTime.TryParse(value[0], out wdato))
                    {
                        rec = new recActebisordre
                        {
                            Ordredato = wdato,
                            Ordrenr = Microsoft.VisualBasic.Information.IsNumeric(value[1]) ? long.Parse(value[1]) : (long?)null,
                            Ordreref = value[2],
                            Ordrebelob = Microsoft.VisualBasic.Information.IsNumeric(value[3]) ? decimal.Parse(value[3]) : (decimal?)null,
                            Ordrestatus = value[4],
                            Leveringsadresse = value[5],
                            Pos = Microsoft.VisualBasic.Information.IsNumeric(value[6]) ? int.Parse(value[6]) : (int?)null,
                            Antal = Microsoft.VisualBasic.Information.IsNumeric(value[7]) ? int.Parse(value[7]) : (int?)null,
                            Varenr = Microsoft.VisualBasic.Information.IsNumeric(value[8]) ? int.Parse(value[8]) : (int?)null,
                            Sku = value[9],
                            Beskrivelse = value[10],
                            Ordrerefpos = value[11],
                            Stkpris = Microsoft.VisualBasic.Information.IsNumeric(value[12]) ? decimal.Parse(value[12]) : (decimal?)null,
                            Fakturanr = Microsoft.VisualBasic.Information.IsNumeric(value[13]) ? int.Parse(value[13]) : (int?)null,
                            Leveringsnr = Microsoft.VisualBasic.Information.IsNumeric(value[14]) ? long.Parse(value[14]) : (long?)null,
                            Serienr = value[15],
                            Ordrestatuspos = value[16],
                            Producent = value[17]
                        };
                        this.Add(rec);
                    }
                }
            }
            ts.Close();
        }

        public void load()
        {
            var qry = from w in this
                      where w.Ordrestatus == "lukket"
                      join a in Program.dbDataTransSumma.Tblactebisordre
                      on new
                      {
                          w.Ordrenr,
                          w.Pos,
                      }
                      equals new
                      {
                          a.Ordrenr,
                          a.Pos,
                      }
                      into actebisordre
                      from a in actebisordre.DefaultIfEmpty(new Tblactebisordre { Pid = 0, Ordrenr = null })
                      where a.Ordrenr == null
                      orderby w.Fakturanr, w.Pos
                      select w;


            int antal = qry.Count();
            int? lastFakturanr = null;
            Tblactebisfaktura recActebisfaktura = null;
            foreach (var w in qry)
            {
                if ((w.Fakturanr != lastFakturanr) && (w.Fakturanr != null))
                {
                    try
                    {
                        recActebisfaktura = (from f in Program.dbDataTransSumma.Tblactebisfaktura where f.Fakturanr == w.Fakturanr select f).First();
                    }
                    catch
                    {
                        recActebisfaktura = new Tblactebisfaktura
                        {
                            Import = true,
                            Ordredato = w.Ordredato,
                            Fakturanr = w.Fakturanr,
                            Ordrenr = w.Ordrenr,
                            Ordreref = w.Ordreref,
                            Ordrebelob = w.Ordrebelob,
                            Ordrestatus = w.Ordrestatus,
                            Leveringsadresse = w.Leveringsadresse,
                        };
                        Program.dbDataTransSumma.Tblactebisfaktura.InsertOnSubmit(recActebisfaktura);
                    }
                }

                Tblactebisordre recActebisordre = new Tblactebisordre
                {
                    Ordrenr = w.Ordrenr,
                    Pos = w.Pos,
                    Antal = w.Antal,
                    Varenr = w.Varenr,
                    Sku = w.Sku,
                    Beskrivelse = w.Beskrivelse,
                    Ordrerefpos = w.Ordrerefpos,
                    Stkpris = w.Stkpris,
                    Leveringsnr = w.Leveringsnr,
                    Serienr = w.Serienr,
                    Ordrestatuspos = w.Ordrestatuspos,
                    Producent = w.Producent,
                };
                recActebisfaktura.Tblactebisordre.Add(recActebisordre);
                lastFakturanr = w.Fakturanr;
            }
            Program.dbDataTransSumma.SubmitChanges();
        }
    }
}
