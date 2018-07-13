namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ecpwt_rsmembership_transactions_user_data
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string adresse { get; set; }
        public string postnr { get; set; }
        public string bynavn { get; set; }
        public string mobil { get; set; }
        public string email { get; set; }
        public string kon { get; set; }
        public string fodtaar { get; set; }
        public string memberid { get; set; }
    }
}
