namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ecpwt_usergroups
    {
        [Key]
        public long id { get; set; }
        public long parent_id { get; set; }
        public int lft { get; set; }
        public int rgt { get; set; }
        public string title { get; set; }
    }
}
