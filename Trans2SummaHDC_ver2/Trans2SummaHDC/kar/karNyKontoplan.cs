using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using Uniconta.Common;
using Uniconta.ClientTools.DataModel;

namespace Trans2SummaHDC
{
    public class recNyKontoplan
    {
        public recNyKontoplan() { }

        public int? Kontonr { get; set; }
        public string NytKontonr { get; set; }
        public string Kontonavn { get; set; }
        public Boolean SkalOprettes { get; set; }

    }

    public class KarNyKontoplan : List<recNyKontoplan>
    {
        private string m_path { get; set; }

        public KarNyKontoplan()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            m_path = rec_regnskab.Placering + "nykontoplan.csv";
            open();
        }

        public void open()
        {
            var api = UCInitializer.GetBaseAPI;
            recNyKontoplan rec;
            FileStream ts = new FileStream(m_path, FileMode.Open, FileAccess.Read, FileShare.None);
            string ln = null;
            Regex regexKontoplan = new Regex(@"""(.*?)"";|([^;]*);|(.*)$");
            using (StreamReader sr = new StreamReader(ts, Encoding.Default))
            {
                while ((ln = sr.ReadLine()) != null)
                {
                    int i = 0;
                    int iMax = 4;
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

                    Boolean wSkalOprettes = false;
                 
                    if (value[1] != "x")
                    {
                        var crit = new List<PropValuePair>();
                        var pair = PropValuePair.GenereteWhereElements("Account", typeof(String), value[1]);
                        crit.Add(pair);
                        var task =  api.Query<GLAccountClient>(crit);
                        task.Wait();
                        var col = task.Result;
                        if (col.Count() == 0)
                            wSkalOprettes = true;
                    }
                  
                    rec = new recNyKontoplan
                    {
                        Kontonr = Microsoft.VisualBasic.Information.IsNumeric(value[0]) ? int.Parse(value[0]) : (int?)null,
                        NytKontonr = value[1],
                        Kontonavn = value[2],
                        SkalOprettes = wSkalOprettes
                    };
                    this.Add(rec);

                }
            }
        }

        public static string NytKontonr(int? kontonr)
        {
            try
            {
                return (from m in Program.karNyKontoplan where m.Kontonr == kontonr select m.NytKontonr).First();
            }
            catch
            {
                return "";
            }
        }

        public static string NytKontonr(string AfstemningsKonto)
        {
            try
            {
                string strAfstemKonti = (from s in Program.karRegnskab where s.key == "AfstemKonti" select s.value).First();
                string[] strarrAfstemKonti = strAfstemKonti.Split(',');
                int?[] intarrAfstemKonti = new int?[strarrAfstemKonti.Length];
                for (int i = 0; i < strarrAfstemKonti.Length; i++)
                {
                    intarrAfstemKonti[i] = int.Parse(strarrAfstemKonti[i]);
                }
                var kontonr = (from k in Program.karKontoplan
                               where intarrAfstemKonti.Contains(k.Kontonr) && k.Kontonavn == AfstemningsKonto
                               select k.Kontonr).First();

                return (from m in Program.karNyKontoplan where m.Kontonr == kontonr select m.NytKontonr).First();
            }
            catch
            {
                return "";
            }
        }

        public void update()
        {
            var qry = from p in Program.karPosteringer
                      join k in Program.karKontoplan on p.Konto equals k.Kontonr
                      orderby p.Konto, p.Nr, p.Id
                      select new
                      {
                          p.Konto,
                          k.Kontonavn
                      };

            FileStream ts = new FileStream(m_path, FileMode.Truncate, FileAccess.Write, FileShare.None);
            using (StreamWriter sr = new StreamWriter(ts, Encoding.Default))
            {
                int last_Konto = 0;

                foreach (var p in qry)
                {
                    if (p.Konto != last_Konto)
                    {
                        last_Konto = p.Konto;
                        string line;
                        try
                        {
                            var rec = (from x in this where x.Kontonr == p.Konto && x.NytKontonr != "x" select x).First();
                            line = string.Format("{0};{1};{2};{3}", rec.Kontonr, rec.NytKontonr, rec.Kontonavn,rec.SkalOprettes);
                        }
                        catch
                        {
                            line = string.Format("{0};{1};{2};{3}", p.Konto, "x", p.Kontonavn, false);
                        }
                        sr.WriteLine(line);
                    }

                }
            }
        }

        public void save()
        {
            FileStream ts = new FileStream(m_path, FileMode.Truncate, FileAccess.Write, FileShare.None);
            using (StreamWriter sr = new StreamWriter(ts, Encoding.Default))
            {
                var qry = from x in this select x;
                foreach (var p in qry)
                {
                    string line = string.Format("{0};{1};{2};{3}", p.Kontonr, p.NytKontonr, p.Kontonavn,p.SkalOprettes);
                    sr.WriteLine(line);
                }
            }
        }
    }
}
