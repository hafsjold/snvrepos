namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ecpwt_user_usergroup_map
    {
        [Key]
        public long user_id { get; set; }
        public long group_id { get; set; }
    }
}
