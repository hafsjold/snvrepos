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
    
    public partial class tempRykkerforslaglinie
    {
        public int id { get; set; }
        public int Rykkerforslagid { get; set; }
        public int Nr { get; set; }
        public int faknr { get; set; }
        public string advistekst { get; set; }
        public decimal advisbelob { get; set; }
    
        public virtual tempRykkerforslag tempRykkerforslag { get; set; }
    }
}