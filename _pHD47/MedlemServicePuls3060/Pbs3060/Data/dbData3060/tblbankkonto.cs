namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblbankkonto
    {
        [Key]
        public int pid { get; set; }
        public Nullable<decimal> saldo { get; set; }
        public Nullable<bool> skjul { get; set; }
        public Nullable<System.DateTime> dato { get; set; }
        public string tekst { get; set; }
        public Nullable<decimal> belob { get; set; }
        public Nullable<int> afstem { get; set; }
        public Nullable<int> bankkontoid { get; set; }
        public Nullable<int> paypal_pid { get; set; }
        public Nullable<int> mobilepay_pid { get; set; }
    
        public virtual tblmobilepay tblmobilepay { get; set; }
        public virtual tblpaypal tblpaypal { get; set; }
    }
}
