namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblnrserie
    {
        [Key]
        public string nrserienavn { get; set; }
        public Nullable<int> sidstbrugtenr { get; set; }
    }
}
