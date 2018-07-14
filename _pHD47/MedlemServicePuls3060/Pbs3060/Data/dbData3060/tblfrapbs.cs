namespace Pbs3060
{
    using System;
    using System.Collections.Generic;

    public partial class Tblfrapbs
    {
        public Tblfrapbs()
        {
            Tblaftalelin = new HashSet<Tblaftalelin>();
            Tblbet = new HashSet<Tblbet>();
            Tblindbetalingskort = new HashSet<Tblindbetalingskort>();
        }
        public int Id { get; set; }
        public string Delsystem { get; set; }
        public string Leverancetype { get; set; }
        public DateTime? Udtrukket { get; set; }
        public DateTime? Bilagdato { get; set; }
        public int? Pbsforsendelseid { get; set; }
        public string Leverancespecifikation { get; set; }
        public DateTime? Leverancedannelsesdato { get; set; }

        public ICollection<Tblaftalelin> Tblaftalelin { get; set; }
        public ICollection<Tblbet> Tblbet { get; set; }
        public ICollection<Tblindbetalingskort> Tblindbetalingskort { get; set; }
        public Tblpbsforsendelse Pbsforsendelse { get; set; }
    }
}
