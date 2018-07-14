namespace Pbs3060
{
    using System;
    using System.Collections.Generic;

    public partial class Tblpbsfile
    {
        public int Id { get; set; }
        public int Pbsfilesid { get; set; }
        public int Seqnr { get; set; }
        public string Data { get; set; }
    
        public Tblpbsfilename Pbsfiles { get; set; }
    }
}
