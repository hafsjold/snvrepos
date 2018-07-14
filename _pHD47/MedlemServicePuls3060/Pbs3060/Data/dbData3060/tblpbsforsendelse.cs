namespace Pbs3060
{
    using System;
    using System.Collections.Generic;

    public partial class Tblpbsforsendelse
    {
        public Tblpbsforsendelse()
        {
            Tblfrapbs = new HashSet<Tblfrapbs>();
            Tblpbsfilename = new HashSet<Tblpbsfilename>();
            Tbltilpbs = new HashSet<Tbltilpbs>();
        }
        public int Id { get; set; }
        public string Delsystem { get; set; }
        public string Leverancetype { get; set; }
        public string Oprettetaf { get; set; }
        public DateTime? Oprettet { get; set; }
        public int? Leveranceid { get; set; }
    
        public ICollection<Tblfrapbs> Tblfrapbs { get; set; }
        public ICollection<Tblpbsfilename> Tblpbsfilename { get; set; }
        public ICollection<Tbltilpbs> Tbltilpbs { get; set; }
    }
}
