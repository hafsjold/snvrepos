﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;


namespace Trans2Summa3060
{
    public class recBankkontoudtogMobilePay
    {
        public recBankkontoudtogMobilePay() { }

        public string bnavn { get; set; }
        public string bmobilnummer { get; set; }
        public decimal? bbeløb { get; set; }
        public DateTime? bdato { get; set; }
        public DateTime? btid { get; set; }
        public string bid { get; set; }
        public string btekst { get; set; }
        public int? bbankkontoid { get; set; }
    }


    public class KarBankkontoudtogMobilePay : List<recBankkontoudtogMobilePay>
    {
        private int m_bankkontoid { get; set; }
        private string m_path { get; set; }

        public KarBankkontoudtogMobilePay(int bankkontoid)
        {
            m_bankkontoid = bankkontoid;
            string csvfile;
            try
            {
                csvfile = (from w in Program.dbDataTransSumma.tblkontoudtogs where w.pid == m_bankkontoid select w).First().savefile;
            }
            catch
            {
                csvfile = "NoFile";
            }

            var rec_regnskab = Program.qryAktivRegnskab();
            m_path = rec_regnskab.Eksportmappe + csvfile;
            open();
        }

        public void open()
        {
            FileStream ts = new FileStream(m_path, FileMode.Open, FileAccess.Read, FileShare.None);
            string ln = null;
            recBankkontoudtogMobilePay rec;
            Regex regexKontoplan = new Regex(@"""(.*?)"";|([^;]*);|(.*)$");
            using (StreamReader sr = new StreamReader(ts, Encoding.Default))
            {
                while ((ln = sr.ReadLine()) != null)
                {
                    int i = 0;
                    int iMax = 9;
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

                    DateTime? wdatotid = null;
                    DateTime? wdato = Microsoft.VisualBasic.Information.IsDate(value[3]) ? DateTime.Parse(value[3]) : (DateTime?)null;
                    DateTime? wtid = Microsoft.VisualBasic.Information.IsDate(value[4]) ? DateTime.Parse(value[4]) : (DateTime?)null;
                    if ((wtid != null) && (wdato != null))
                    {
                        wdatotid = new DateTime(wdato.Value.Year, wdato.Value.Month, wdato.Value.Day, wtid.Value.Hour, wtid.Value.Minute, wtid.Value.Second);
                    }
                    if (wdatotid != null)
                    {
                        rec = new recBankkontoudtogMobilePay
                        {
                            bnavn = value[0],
                            bmobilnummer = value[1],
                            bbeløb = Microsoft.VisualBasic.Information.IsNumeric(value[2]) ? decimal.Parse(value[2]) : (decimal?)null,
                            bdato = wdatotid,
                            bid = value[5], 
                            btekst = value[6],
                            bbankkontoid = m_bankkontoid
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
                      join b in Program.dbDataTransSumma.tblmobilepays on w.bid equals b.mobilepay_id into bankkonto
                      from b in bankkonto.DefaultIfEmpty(new tblmobilepay { pid = 0, belob = null })
                      where b.belob == null
                      orderby w.bdato
                      select w;


            int antal = qry.Count();
            foreach (var b in qry)
            {
                tblmobilepay recBankkonto = new tblmobilepay
                {
                    navn = b.bnavn,
                    mobilnummer = b.bmobilnummer,
                    belob = b.bbeløb,
                    dato = b.bdato,
                    mobilepay_id = b.bid,
                    tekst = b.btekst,
                };
                Program.dbDataTransSumma.tblmobilepays.InsertOnSubmit(recBankkonto);

            }
            Program.dbDataTransSumma.SubmitChanges();
        }
    }
}
