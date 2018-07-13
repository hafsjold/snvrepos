namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblfrapbs
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblfrapbs()
        {
            this.tblaftalelin = new HashSet<tblaftalelin>();
            this.tblbet = new HashSet<tblbet>();
            this.tblindbetalingskort = new HashSet<tblindbetalingskort>();
        }
        [Key]
        public int id { get; set; }
        public string delsystem { get; set; }
        public string leverancetype { get; set; }
        public Nullable<System.DateTime> udtrukket { get; set; }
        public Nullable<System.DateTime> bilagdato { get; set; }
        public Nullable<int> pbsforsendelseid { get; set; }
        public string leverancespecifikation { get; set; }
        public Nullable<System.DateTime> leverancedannelsesdato { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblaftalelin> tblaftalelin { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblbet> tblbet { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblindbetalingskort> tblindbetalingskort { get; set; }
        public virtual tblpbsforsendelse tblpbsforsendelse { get; set; }
    }
}
