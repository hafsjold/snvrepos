namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblMedlemExtra
    {
        [Key]
        public int Nr { get; set; }
        public Nullable<int> Pusterummet { get; set; }
    }
}
