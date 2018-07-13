namespace MedlemServicePuls3060
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblSchedule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblSchedule()
        {
            this.tblJobqueues = new HashSet<tblJobqueue>();
        }
    
        public int id { get; set; }
        public string schedule { get; set; }
        public string jobname { get; set; }
        public Nullable<System.DateTime> start { get; set; }
        public Nullable<System.DateTime> end { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblJobqueue> tblJobqueues { get; set; }
    }
}
