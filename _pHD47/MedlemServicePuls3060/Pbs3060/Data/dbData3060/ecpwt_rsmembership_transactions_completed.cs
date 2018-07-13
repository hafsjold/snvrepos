namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ecpwt_rsmembership_transactions_completed
    {
        [Key]
        public int id { get; set; }
        public string status { get; set; }
    }
}
