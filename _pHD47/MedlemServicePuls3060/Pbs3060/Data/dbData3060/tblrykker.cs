namespace Pbs3060
{
    using System;
    using System.Collections.Generic;

    public partial class Tblrykker
    {
        public int Id { get; set; }
        public int? Tilpbsid { get; set; }
        public DateTime? Betalingsdato { get; set; }
        public int? Nr { get; set; }
        public int? Faknr { get; set; }
        public string Advistekst { get; set; }
        public decimal? Advisbelob { get; set; }
        public int? Infotekst { get; set; }
        public DateTime? Rykkerdato { get; set; }
        public DateTime? Maildato { get; set; }
    
        public Tbltilpbs Tilpbs { get; set; }
    }
}
