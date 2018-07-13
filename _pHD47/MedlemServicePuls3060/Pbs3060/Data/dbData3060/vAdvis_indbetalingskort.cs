namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    
    public partial class vAdvis_indbetalingskort
    {
        public int id { get; set; }
        public int frapbsid { get; set; }
        public string pbstranskode { get; set; }
        public int Nr { get; set; }
        public Nullable<int> faknr { get; set; }
        public string debitorkonto { get; set; }
        public string debgrpnr { get; set; }
        public string kortartkode { get; set; }
        public string fikreditornr { get; set; }
        public string indbetalerident { get; set; }
        public Nullable<System.DateTime> dato { get; set; }
        public Nullable<decimal> belob { get; set; }
        public string pbssektionnr { get; set; }
        public string regnr { get; set; }
    }
}
