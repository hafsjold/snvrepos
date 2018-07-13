namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblpbsforsendelse
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblpbsforsendelse()
        {
            this.tblfrapbs = new HashSet<tblfrapbs>();
            this.tblpbsfilename = new HashSet<tblpbsfilename>();
            this.tbltilpbs = new HashSet<tbltilpbs>();
        }
        [Key]
        public int id { get; set; }
        public string delsystem { get; set; }
        public string leverancetype { get; set; }
        public string oprettetaf { get; set; }
        public Nullable<System.DateTime> oprettet { get; set; }
        public Nullable<int> leveranceid { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblfrapbs> tblfrapbs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblpbsfilename> tblpbsfilename { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbltilpbs> tbltilpbs { get; set; }
    }
}
