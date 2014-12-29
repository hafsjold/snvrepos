using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsPbs3060v2
{

    public class recPbsnetdir
    {
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

    public class MemPbsnetdir : List<recPbsnetdir>
    {

    }
}

