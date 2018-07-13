namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblMedlem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblMedlem()
        {
            this.tblMedlemLog = new HashSet<tblMedlemLog>();
        }
        [Key]
        public int Nr { get; set; }
        public string Kon { get; set; }
        public Nullable<System.DateTime> FodtDato { get; set; }
        public string Navn { get; set; }
        public string Kaldenavn { get; set; }
        public string Adresse { get; set; }
        public string Postnr { get; set; }
        public string Bynavn { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string Bank { get; set; }
        public Nullable<int> Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblMedlemLog> tblMedlemLog { get; set; }
    }
}
