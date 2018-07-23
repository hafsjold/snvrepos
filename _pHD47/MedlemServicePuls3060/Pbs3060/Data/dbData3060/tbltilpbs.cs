namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Tbltilpbs
    {
        public Tbltilpbs()
        {
            this.Tbladvis = new HashSet<Tbladvis>();
            this.Tblfak = new HashSet<Tblfak>();
            this.Tbloverforsel = new HashSet<Tbloverforsel>();
            this.Tblrykker = new HashSet<Tblrykker>();
            this.Tblkvitering = new HashSet<Tblkvitering>();
        }
        public int Id { get; set; }
        public string Delsystem { get; set; }
        public string Leverancetype { get; set; }
        public DateTime? Bilagdato { get; set; }
        public int? Pbsforsendelseid { get; set; }
        public DateTime? Udtrukket { get; set; }
        public string Leverancespecifikation { get; set; }
        public DateTime? Leverancedannelsesdato { get; set; }

        public ICollection<Tbladvis> Tbladvis { get; set; }
        public ICollection<Tblfak> Tblfak { get; set; }
        public ICollection<Tbloverforsel> Tbloverforsel { get; set; }
        public Tblpbsforsendelse Pbsforsendelse { get; set; }
        public ICollection<Tblrykker> Tblrykker { get; set; }
        public ICollection<Tblkvitering> Tblkvitering { get; set; }
    }
}
