namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblAktivitet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblAktivitet()
        {
            this.tblMedlemLog = new HashSet<tblMedlemLog>();
        }
        [Key]
        public int id { get; set; }
        public string akt_tekst { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblMedlemLog> tblMedlemLog { get; set; }
    }
}
