using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trans2SummaHDC
{
    public class clsParam
    {
        public string Delsystem { get; set; }
        public string Kontotype { get; set; }
        public string Konto { get; set; }
        public string Moms_Konto { get; set; }
        public string Modkontotype { get; set; }
        public string Modkonto { get; set; }
        public string Moms_Modkonto { get; set; }
        public double? Debit { get; set; }
        public double? Kredit { get; set; }

        public int? Faktura_Nr  { get; set; }
        public string Lin0_Tekst { get; set; }
        public double Lin0_Antal { get; set; }
        public double Lin0_Pris { get; set; }
        public string Lin0_Konto { get; set; }
        public string Lin0_Moms { get; set; }


        public clsParam() { }
        public clsParam(string[] arrParam)
        {
            char[] arrsplit = { ':'};
            foreach (var s in arrParam)
            {
                var p = s.Split(arrsplit);
                if ((p.Count() == 2) && !string.IsNullOrWhiteSpace(p[1]))
                {
                    try
                    {
                        switch (p[0].ToLower().Trim())
                        {
                            case "#delsystem":
                                Delsystem = p[1].Trim();
                                break;

                            case "#kontotype":
                                Kontotype = p[1].Trim();
                                break;

                            case "#konto":
                                Konto = p[1].Trim();
                                break;

                            case "#moms_konto":
                                Moms_Konto = p[1].Trim();
                                break;

                            case "#modkontotype":
                                Modkontotype = p[1].Trim();
                                break;

                            case "#modkonto":
                                Modkonto = p[1].Trim();
                                break;

                            case "#moms_modkonto":
                                Moms_Modkonto = p[1].Trim();
                                break;

                            case "#debit":
                                Debit = double.Parse(p[1].Trim(), System.Globalization.NumberStyles.Any);
                                break;

                            case "#kredit":
                                Kredit = double.Parse(p[1].Trim(), System.Globalization.NumberStyles.Any);
                                break;

                            case "#faktura_nr":
                                Faktura_Nr = int.Parse(p[1].Trim(), System.Globalization.NumberStyles.Any);
                                break;

                            case "#lin0_tekst":
                                Lin0_Tekst = p[1].Trim();
                                break;

                            case "#lin0_antal":
                                Lin0_Antal = double.Parse(p[1].Trim(), System.Globalization.NumberStyles.Any);
                                break;

                            case "#lin0_pris":
                                Lin0_Pris = double.Parse(p[1].Trim(), System.Globalization.NumberStyles.Any);
                                break;

                            case "#lin0_konto":
                                Lin0_Konto = p[1].Trim();
                                break;

                            case "#lin0_moms":
                                Lin0_Moms = p[1].Trim();
                                break;

                            default:
                                break;
                        }

                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
    }
}
