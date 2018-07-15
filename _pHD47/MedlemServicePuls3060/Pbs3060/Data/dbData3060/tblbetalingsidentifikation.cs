namespace Pbs3060
{
    using System;
    using System.Collections.Generic;

    public partial class Tblbetalingsidentifikation
    {
        public int Id { get; set; }
        public string Betalingsidentifikation { get; set; }
        public DateTime? Betalingsdato { get; set; }
        public decimal? Belob { get; set; }
        public int? Nr { get; set; }
        public int? Bogfkonto { get; set; }
    }
}
