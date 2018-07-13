namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblKontingent
    {
        [Key]
        public int id { get; set; }
        public System.DateTime startdato { get; set; }
        public System.DateTime slutdato { get; set; }
        public int startalder { get; set; }
        public int slutalder { get; set; }
        public decimal aarskontingent { get; set; }
    }
}
