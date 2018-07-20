using System;
using System.Collections.Generic;
using System.Text;

namespace Pbs3060
{
    public class recBogfoeringsKlader
    {
        public recBogfoeringsKlader() { }

        public DateTime? Dato { get; set; }
        public int? Bilag { get; set; }
        public string Tekst { get; set; }
        public string Afstemningskonto { get; set; }
        public decimal? Belob { get; set; }
        public int? Kontonr { get; set; }
        public int? Faknr { get; set; }
        public int? Sagnr { get; set; }
    }

    public class MemBogfoeringsKlader : List<recBogfoeringsKlader>
    {
    }
}
