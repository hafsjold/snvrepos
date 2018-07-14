namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Tblbetlin
    {
        [Key]
        public int id { get; set; }
        public Nullable<int> betid { get; set; }
        public string pbssektionnr { get; set; }
        public string pbstranskode { get; set; }
        public Nullable<int> Nr { get; set; }
        public Nullable<int> faknr { get; set; }
        public string debitorkonto { get; set; }
        public Nullable<int> aftalenr { get; set; }
        public Nullable<System.DateTime> betalingsdato { get; set; }
        public Nullable<decimal> belob { get; set; }
        public Nullable<System.DateTime> indbetalingsdato { get; set; }
        public Nullable<System.DateTime> bogforingsdato { get; set; }
        public Nullable<decimal> indbetalingsbelob { get; set; }
        public string pbskortart { get; set; }
        public Nullable<decimal> pbsgebyrbelob { get; set; }
        public string pbsarkivnr { get; set; }
    
        public virtual Tblbet tblbet { get; set; }
    }
}
