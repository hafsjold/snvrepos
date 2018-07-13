namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblrsmembership_transactions
    {
        [Key]
        public int id { get; set; }
        public int trans_id { get; set; }
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
        public string name { get; set; }
        public string adresse { get; set; }
        public string postnr { get; set; }
        public string bynavn { get; set; }
        public int memberid { get; set; }
        public int membership_id { get; set; }
        public Nullable<int> subscriber_id { get; set; }
    
        //public virtual tblfak tblfak { get; set; }
    }
}
