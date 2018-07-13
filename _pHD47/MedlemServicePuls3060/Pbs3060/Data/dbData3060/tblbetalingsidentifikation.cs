namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblbetalingsidentifikation
    {
        [Key]
        public int id { get; set; }
        public string betalingsidentifikation { get; set; }
        public Nullable<System.DateTime> betalingsdato { get; set; }
        public Nullable<decimal> belob { get; set; }
        public Nullable<int> Nr { get; set; }
        public Nullable<int> bogfkonto { get; set; }
    }
}
