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
    
    public partial class tempKontforslaglinie
    {
        public int id { get; set; }
        public int Nr { get; set; }
        public int Kontforslagid { get; set; }
        public System.DateTime fradato { get; set; }
        public decimal advisbelob { get; set; }
        public Nullable<System.DateTime> tildato { get; set; }
        public bool tilmeldtpbs { get; set; }
        public bool indmeldelse { get; set; }
    
        public virtual tempKontforslag tempKontforslag { get; set; }
    }
}