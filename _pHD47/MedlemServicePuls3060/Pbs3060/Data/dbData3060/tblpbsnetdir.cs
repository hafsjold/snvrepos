namespace Pbs3060
{
    using System;
    using System.Collections.Generic;

    public partial class Tblpbsnetdir
    {
        public int Id { get; set; }
        public int? Type { get; set; }
        public string Path { get; set; }
        public string Filename { get; set; }
        public int? Size { get; set; }
        public DateTime? Atime { get; set; }
        public DateTime? Mtime { get; set; }
        public string Perm { get; set; }
        public int? Uid { get; set; }
        public int? Gid { get; set; }
    }
}
