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
    
    public partial class tbladvis
    {
        public int id { get; set; }
        public Nullable<int> tilpbsid { get; set; }
        public Nullable<System.DateTime> betalingsdato { get; set; }
        public Nullable<int> Nr { get; set; }
        public Nullable<int> faknr { get; set; }
        public string advistekst { get; set; }
        public Nullable<decimal> advisbelob { get; set; }
        public Nullable<int> infotekst { get; set; }
        public Nullable<System.DateTime> maildato { get; set; }
    
        public virtual tbltilpbs tbltilpbs { get; set; }
    }
}