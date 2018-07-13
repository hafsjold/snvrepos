namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblinfotekst
    {
        [Key]
        public int id { get; set; }
        public string navn { get; set; }
        public string msgtext { get; set; }
    }
}
