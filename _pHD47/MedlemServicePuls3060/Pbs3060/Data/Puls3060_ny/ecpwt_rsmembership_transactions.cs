namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ecpwt_rsmembership_transactions
    {
        [Key]
        public int id { get; set; }
        public int user_id { get; set; }
        public string user_email { get; set; }
        public string user_data { get; set; }
        public string type { get; set; }
        public string @params { get; set; }
        public System.DateTime date { get; set; }
        public string ip { get; set; }
        public decimal price { get; set; }
        public string coupon { get; set; }
        public string currency { get; set; }
        public string hash { get; set; }
        public string custom { get; set; }
        public string gateway { get; set; }
        public string status { get; set; }
        public string response_log { get; set; }
    }
}
