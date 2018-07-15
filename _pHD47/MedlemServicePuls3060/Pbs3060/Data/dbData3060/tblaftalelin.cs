namespace Pbs3060
{
    using System;
    using System.Collections.Generic;

    public partial class Tblaftalelin
    {
        public int Id { get; set; }
        public int Frapbsid { get; set; }
        public string Pbstranskode { get; set; }
        public int Nr { get; set; }
        public string Debitorkonto { get; set; }
        public string Debgrpnr { get; set; }
        public int? Aftalenr { get; set; }
        public DateTime? Aftalestartdato { get; set; }
        public DateTime? Aftaleslutdato { get; set; }
        public string Pbssektionnr { get; set; }
    
        public Tblfrapbs Frapbs { get; set; }
    }
}
