namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblpbsnetdir
    {
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
    }
}
