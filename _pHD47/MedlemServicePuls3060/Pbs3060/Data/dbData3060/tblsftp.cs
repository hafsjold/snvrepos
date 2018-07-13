namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblsftp
    {
        [Key]
        public int id { get; set; }
        public string navn { get; set; }
        public string host { get; set; }
        public string port { get; set; }
        public string user { get; set; }
        public string outbound { get; set; }
        public string inbound { get; set; }
        public string pincode { get; set; }
        public string certificate { get; set; }
    }
}
