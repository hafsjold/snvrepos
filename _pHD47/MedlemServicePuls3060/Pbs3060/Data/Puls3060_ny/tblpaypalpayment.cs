namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblpaypalpayment
    {
        [Key]
        public string paypal_transactions_id { get; set; }
        public Nullable<bool> bogfoert { get; set; }
    }
}
