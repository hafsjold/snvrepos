namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Tblbet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tblbet()
        {
            this.tblbetlin = new HashSet<Tblbetlin>();
        }
        [Key]
        public int id { get; set; }
        public Nullable<int> frapbsid { get; set; }
        public string pbssektionnr { get; set; }
        public string transkode { get; set; }
        public Nullable<System.DateTime> bogforingsdato { get; set; }
        public Nullable<decimal> indbetalingsbelob { get; set; }
        public Nullable<bool> summabogfort { get; set; }
        public Nullable<bool> rsmembership { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tblbetlin> tblbetlin { get; set; }
        public virtual Tblfrapbs tblfrapbs { get; set; }
    }
}
