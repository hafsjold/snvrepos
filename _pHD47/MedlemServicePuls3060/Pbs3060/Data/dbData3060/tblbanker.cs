namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblbanker
    {
        [Key]
        public string regnr { get; set; }
        public string banknavn { get; set; }
    }
}
