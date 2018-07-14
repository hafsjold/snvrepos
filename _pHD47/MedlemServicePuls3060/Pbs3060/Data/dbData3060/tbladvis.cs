namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Tbladvis
    {
        [Key]
        public int id { get; set; }
        public Nullable<int> tilpbsid { get; set; }
        public Nullable<System.DateTime> betalingsdato { get; set; }
        public Nullable<int> Nr { get; set; }
        public Nullable<int> faknr { get; set; }
        public string advistekst { get; set; }
        public Nullable<decimal> advisbelob { get; set; }
        public Nullable<int> infotekst { get; set; }
        public Nullable<System.DateTime> maildato { get; set; }
    
        public virtual Tbltilpbs tbltilpbs { get; set; }
    }
}
