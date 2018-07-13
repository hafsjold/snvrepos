namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tbloverforsel
    {
        [Key]
        public int id { get; set; }
        public Nullable<int> tilpbsid { get; set; }
        public Nullable<int> Nr { get; set; }
        public string Navn { get; set; }
        public string Kaldenavn { get; set; }
        public string Email { get; set; }
        public Nullable<int> SFaknr { get; set; }
        public Nullable<int> SFakID { get; set; }
        public string advistekst { get; set; }
        public Nullable<decimal> advisbelob { get; set; }
        public string emailtekst { get; set; }
        public Nullable<bool> emailsent { get; set; }
        public string bankregnr { get; set; }
        public string bankkontonr { get; set; }
        public Nullable<System.DateTime> betalingsdato { get; set; }
    
        public virtual tbltilpbs tbltilpbs { get; set; }
    }
}
