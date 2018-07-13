namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblmobilepay
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblmobilepay()
        {
            this.tblbankkonto = new HashSet<tblbankkonto>();
        }
        [Key]
        public int pid { get; set; }
        public string Type { get; set; }
        public string Navn { get; set; }
        public string MobilNummer { get; set; }
        public Nullable<decimal> Belob { get; set; }
        public string MobilePayNummer { get; set; }
        public string NavnBetalingssted { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string TransaktionsID { get; set; }
        public string Besked { get; set; }
        public Nullable<decimal> Gebyr { get; set; }
        public Nullable<decimal> Saldo { get; set; }
        public Nullable<bool> Imported { get; set; }
        public Nullable<bool> Bogfoert { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblbankkonto> tblbankkonto { get; set; }
    }
}
