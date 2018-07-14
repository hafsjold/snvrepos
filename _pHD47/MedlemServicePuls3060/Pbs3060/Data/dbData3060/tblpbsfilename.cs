namespace Pbs3060
{
    using System;
    using System.Collections.Generic;

    public partial class Tblpbsfilename
    {
        public Tblpbsfilename()
        {
            this.Tblpbsfile = new HashSet<Tblpbsfile>();
        }
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
        public DateTime? Transmittime { get; set; }
        public int? Pbsforsendelseid { get; set; }
    
        public Tblpbsforsendelse Pbsforsendelse{ get; set; }
        public ICollection<Tblpbsfile> Tblpbsfile { get; set; }
    }
}
