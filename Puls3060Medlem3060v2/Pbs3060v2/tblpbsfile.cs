//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace nsPbs3060v2
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblpbsfile
    {
        public int id { get; set; }
        public int pbsfilesid { get; set; }
        public int seqnr { get; set; }
        public string data { get; set; }
    
        public virtual tblpbsfilename tblpbsfilename { get; set; }
    }
}
