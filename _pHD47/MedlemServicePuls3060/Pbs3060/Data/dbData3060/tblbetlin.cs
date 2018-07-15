namespace Pbs3060
{
    using System;
    using System.Collections.Generic;

    public partial class Tblbetlin
    {
        public int Id { get; set; }
        public int? Betid { get; set; }
        public string Pbssektionnr { get; set; }
        public string Pbstranskode { get; set; }
        public int? Nr { get; set; }
        public int? Faknr { get; set; }
        public string Debitorkonto { get; set; }
        public int? Aftalenr { get; set; }
        public DateTime? Betalingsdato { get; set; }
        public decimal? Belob { get; set; }
        public DateTime? Indbetalingsdato { get; set; }
        public DateTime? Bogforingsdato { get; set; }
        public decimal? Indbetalingsbelob { get; set; }
        public string Pbskortart { get; set; }
        public decimal? Pbsgebyrbelob { get; set; }
        public string Pbsarkivnr { get; set; }
    
        public virtual Tblbet Bet { get; set; }
    }
}
