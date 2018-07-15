namespace Pbs3060
{
    using System;
    using System.Collections.Generic;

    public partial class Tbloverforsel
    {
        public int Id { get; set; }
        public int? Tilpbsid { get; set; }
        public int? Nr { get; set; }
        public string Navn { get; set; }
        public string Kaldenavn { get; set; }
        public string Email { get; set; }
        public int? Sfaknr { get; set; }
        public int? SfakId { get; set; }
        public string Advistekst { get; set; }
        public decimal? Advisbelob { get; set; }
        public string Emailtekst { get; set; }
        public bool? Emailsent { get; set; }
        public string Bankregnr { get; set; }
        public string Bankkontonr { get; set; }
        public DateTime? Betalingsdato { get; set; }
    
        public Tbltilpbs Tilpbs { get; set; }
    }
}
