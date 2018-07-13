namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblipn
    {
        [Key]
        public int id { get; set; }
        public Nullable<System.DateTimeOffset> created { get; set; }
        public string ipnmsg { get; set; }
    }
}
