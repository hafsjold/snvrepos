namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tbltilpbs
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbltilpbs()
        {
            this.tbladvis = new HashSet<tbladvis>();
            this.tblfak = new HashSet<tblfak>();
            this.tbloverforsel = new HashSet<tbloverforsel>();
            this.tblrykker = new HashSet<tblrykker>();
        }
        [Key]
        public int id { get; set; }
        public string delsystem { get; set; }
        public string leverancetype { get; set; }
        public Nullable<System.DateTime> bilagdato { get; set; }
        public Nullable<int> pbsforsendelseid { get; set; }
        public Nullable<System.DateTime> udtrukket { get; set; }
        public string leverancespecifikation { get; set; }
        public Nullable<System.DateTime> leverancedannelsesdato { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbladvis> tbladvis { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblfak> tblfak { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbloverforsel> tbloverforsel { get; set; }
        public virtual tblpbsforsendelse tblpbsforsendelse { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblrykker> tblrykker { get; set; }
    }
}
