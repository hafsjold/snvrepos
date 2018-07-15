namespace Pbs3060
{
    using System;
    using System.Collections.Generic;

    public partial class Tblbankkonto
    {
        public int Pid { get; set; }
        public decimal? Saldo { get; set; }
        public bool? Skjul { get; set; }
        public DateTime? Dato { get; set; }
        public string Tekst { get; set; }
        public decimal? Belob { get; set; }
        public int? Afstem { get; set; }
        public int? Bankkontoid { get; set; }
        public int? PaypalPid { get; set; }
        public int? MobilepayPid { get; set; }
    
        public virtual Tblmobilepay MobilepayP { get; set; }
        public virtual Tblpaypal PaypalP { get; set; }
    }
}
