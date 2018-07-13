namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblkontoudtog
    {
        [Key]
        public int pid { get; set; }
        public string name { get; set; }
        public string savefile { get; set; }
        public Nullable<int> bogfkonto { get; set; }
        public string afstemningskonto { get; set; }
    }
}
