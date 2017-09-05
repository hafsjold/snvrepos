using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nsPbs3060
{
    public class clsParam
    {
        public string Delsystem { get; set; }
        public string Kontotype { get; set; }
        public string Konto { get; set; }
        public string Tekst { get; set; }
        public string Moms_Konto { get; set; }
        public string Modkontotype { get; set; }
        public string Modkonto { get; set; }
        public string Moms_Modkonto { get; set; }
        public double? Debit { get; set; }
        public double? Kredit { get; set; }

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

                            case "#tekst":
                                Tekst = p[1].Trim();
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
