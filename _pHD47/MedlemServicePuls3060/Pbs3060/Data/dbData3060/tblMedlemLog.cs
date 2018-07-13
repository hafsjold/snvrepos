namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblMedlemLog
    {
        [Key]
        public int id { get; set; }
        public Nullable<int> Nr { get; set; }
        public Nullable<System.DateTime> logdato { get; set; }
        public Nullable<int> akt_id { get; set; }
        public Nullable<System.DateTime> akt_dato { get; set; }
    
        public virtual tblAktivitet tblAktivitet { get; set; }
        public virtual tblMedlem tblMedlem { get; set; }
    }
}
