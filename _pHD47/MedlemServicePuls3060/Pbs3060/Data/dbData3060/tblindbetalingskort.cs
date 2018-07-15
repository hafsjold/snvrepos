namespace Pbs3060
{
    using System;
    using System.Collections.Generic;

    public partial class Tblindbetalingskort
    {
        public int Id { get; set; }
        public int Frapbsid { get; set; }
        public string Pbstranskode { get; set; }
        public int Nr { get; set; }
        public int? Faknr { get; set; }
        public string Debitorkonto { get; set; }
        public string Debgrpnr { get; set; }
        public string Kortartkode { get; set; }
        public string Fikreditornr { get; set; }
        public string Indbetalerident { get; set; }
        public DateTime? Dato { get; set; }
        public decimal? Belob { get; set; }
        public string Pbssektionnr { get; set; }
        public string Regnr { get; set; }
    
        public Tblfrapbs Frapbs { get; set; }
    }
}
