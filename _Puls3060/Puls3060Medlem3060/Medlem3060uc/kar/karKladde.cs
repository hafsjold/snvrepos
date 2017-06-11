using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Medlem3060uc
{
    public class recKladde
    {
        public recKladde() { }

        public DateTime? Dato { get; set; }
        public int? Bilag { get; set; }
        public string Tekst { get; set; }
        public string Afstemningskonto { get; set; }
        public decimal? Belob { get; set; }
        public int? Kontonr { get; set; }
        public int? Faknr { get; set; }
        public int? Sagnr { get; set; }

    }

    public class KarKladde : List<recKladde>
    {
        private string m_path { get; set; }

        public KarKladde()
        {

        }

        public void save()
        {

        }
    }
}
