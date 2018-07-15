namespace Pbs3060
{
    using System;
    using System.Collections.Generic;

    public partial class Tblmobilepay
    {
        public Tblmobilepay()
        {
            Tblbankkonto = new HashSet<Tblbankkonto>();
        }
        public int Pid { get; set; }
        public string Type { get; set; }
        public string Navn { get; set; }
        public string MobilNummer { get; set; }
        public decimal? Belob { get; set; }
        public string MobilePayNummer { get; set; }
        public string NavnBetalingssted { get; set; }
        public DateTime? Date { get; set; }
        public string TransaktionsId { get; set; }
        public string Besked { get; set; }
        public decimal? Gebyr { get; set; }
        public decimal? Saldo { get; set; }
        public bool? Imported { get; set; }
        public bool? Bogfoert { get; set; }
    
        public ICollection<Tblbankkonto> Tblbankkonto { get; set; }
    }
}
