namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ecpwt_rsmembership_subscribers
    {
        [Key]
        public int user_id { get; set; }
        public string f1 { get; set; }
        public string f2 { get; set; }
        public string f4 { get; set; }
        public string f6 { get; set; }
        public string f14 { get; set; }
    }
}
