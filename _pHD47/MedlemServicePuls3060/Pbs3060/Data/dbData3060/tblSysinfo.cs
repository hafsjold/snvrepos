namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblSysinfo
    {
        public string vkey { get; set; }
        public string val { get; set; }
        [Key]
        public int id { get; set; }
    }
}
