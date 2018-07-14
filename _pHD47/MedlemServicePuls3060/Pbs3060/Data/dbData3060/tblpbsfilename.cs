namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class tblpbsfilename
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblpbsfilename()
        {
            this.tblpbsfile = new HashSet<tblpbsfile>();
        }
        [Key]
        public int id { get; set; }
        public Nullable<int> type { get; set; }
        public string path { get; set; }
        public string filename { get; set; }
        public Nullable<int> size { get; set; }
        public Nullable<System.DateTime> atime { get; set; }
        public Nullable<System.DateTime> mtime { get; set; }
        public string perm { get; set; }
        public Nullable<int> uid { get; set; }
        public Nullable<int> gid { get; set; }
        public Nullable<System.DateTime> transmittime { get; set; }
        public Nullable<int> pbsforsendelseid { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblpbsfile> tblpbsfile { get; set; }
        public virtual tblpbsforsendelse tblpbsforsendelse{ get; set; }
    }
}
