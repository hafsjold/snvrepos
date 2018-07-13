namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ecpwt_rsmembership_membership_subscribers
    {
        [Key]
        public int id { get; set; }
        public int user_id { get; set; }
        public int membership_id { get; set; }
        public System.DateTime membership_start { get; set; }
        public System.DateTime membership_end { get; set; }
        public decimal price { get; set; }
        public string currency { get; set; }
        public sbyte status { get; set; }
        public string extras { get; set; }
        public string notes { get; set; }
        public int from_transaction_id { get; set; }
        public int last_transaction_id { get; set; }
        public string custom_1 { get; set; }
        public string custom_2 { get; set; }
        public string custom_3 { get; set; }
        public System.DateTime notified { get; set; }
        public bool published { get; set; }
    }
}
