namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblmembershippayment
    {
        [Key]
        public int rsmembership_transactions_id { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
    }
}
