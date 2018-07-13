namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblpbsfile
    {
        [Key]
        public int id { get; set; }
        public int pbsfilesid { get; set; }
        public int seqnr { get; set; }
        public string data { get; set; }
    
        public virtual tblpbsfilename tblpbsfilename { get; set; }
    }
}
