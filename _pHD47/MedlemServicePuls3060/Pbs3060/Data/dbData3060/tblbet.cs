namespace Pbs3060
{
    using System;
    using System.Collections.Generic;

    public partial class Tblbet
    {
        public Tblbet()
        {
            this.Tblbetlin = new HashSet<Tblbetlin>();
        }
        public int Id { get; set; }
        public int? Frapbsid { get; set; }
        public string Pbssektionnr { get; set; }
        public string Transkode { get; set; }
        public DateTime? Bogforingsdato { get; set; }
        public decimal? Indbetalingsbelob { get; set; }
        public bool? Summabogfort { get; set; }
        public bool? Rsmembership { get; set; }
    
        public ICollection<Tblbetlin> Tblbetlin { get; set; }
        public Tblfrapbs Frapbs { get; set; }
    }
}
