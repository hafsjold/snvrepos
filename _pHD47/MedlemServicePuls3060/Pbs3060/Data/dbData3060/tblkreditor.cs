namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblkreditor
    {
        [Key]
        public int id { get; set; }
        public string datalevnr { get; set; }
        public string datalevnavn { get; set; }
        public string pbsnr { get; set; }
        public string delsystem { get; set; }
        public string regnr { get; set; }
        public string kontonr { get; set; }
        public string debgrpnr { get; set; }
        public string sektionnr { get; set; }
        public string transkodebetaling { get; set; }
    }
}
