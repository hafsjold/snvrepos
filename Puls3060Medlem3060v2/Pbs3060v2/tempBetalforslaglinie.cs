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
    
    public partial class tempBetalforslaglinie
    {
        public int id { get; set; }
        public int Nr { get; set; }
        public int Betalforslagid { get; set; }
        public decimal advisbelob { get; set; }
        public Nullable<int> fakid { get; set; }
        public string bankregnr { get; set; }
        public string bankkontonr { get; set; }
        public Nullable<int> faknr { get; set; }
    
        public virtual tempBetalforslag tempBetalforslag { get; set; }
    }
}
