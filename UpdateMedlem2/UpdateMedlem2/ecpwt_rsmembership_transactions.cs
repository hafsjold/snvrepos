//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UpdateMedlem2
{
    using System;
    using System.Collections.Generic;
    
    public partial class ecpwt_rsmembership_transactions
    {
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
