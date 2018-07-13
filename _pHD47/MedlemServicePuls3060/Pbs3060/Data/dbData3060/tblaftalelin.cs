namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblaftalelin
    {
        [Key]
        public int id { get; set; }
        public int frapbsid { get; set; }
        public string pbstranskode { get; set; }
        public int Nr { get; set; }
        public string debitorkonto { get; set; }
        public string debgrpnr { get; set; }
        public Nullable<int> aftalenr { get; set; }
        public Nullable<System.DateTime> aftalestartdato { get; set; }
        public Nullable<System.DateTime> aftaleslutdato { get; set; }
        public string pbssektionnr { get; set; }
    
        public virtual tblfrapbs tblfrapbs { get; set; }
    }
}
