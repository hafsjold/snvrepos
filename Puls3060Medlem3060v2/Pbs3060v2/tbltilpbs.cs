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
    
    public partial class tbltilpbs
    {
        public tbltilpbs()
        {
            this.tbladvis = new HashSet<tbladvis>();
            this.tblfak = new HashSet<tblfak>();
            this.tbloverforsel = new HashSet<tbloverforsel>();
            this.tblrykker = new HashSet<tblrykker>();
        }
    
        public int id { get; set; }
        public string delsystem { get; set; }
        public string leverancetype { get; set; }
        public Nullable<System.DateTime> bilagdato { get; set; }
        public Nullable<int> pbsforsendelseid { get; set; }
        public Nullable<System.DateTime> udtrukket { get; set; }
        public string leverancespecifikation { get; set; }
        public Nullable<System.DateTime> leverancedannelsesdato { get; set; }
    
        public virtual ICollection<tbladvis> tbladvis { get; set; }
        public virtual ICollection<tblfak> tblfak { get; set; }
        public virtual ICollection<tbloverforsel> tbloverforsel { get; set; }
        public virtual tblpbsforsendelse tblpbsforsendelse { get; set; }
        public virtual ICollection<tblrykker> tblrykker { get; set; }
    }
}
