//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BJDoc2
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblIpRelations
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTimeOffset> Created { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTimeOffset> Modified { get; set; }
        public byte[] RowVersion { get; set; }
        public int tblIpRelation_tblComputer { get; set; }
    
        public virtual tblComputers tblComputers { get; set; }
        public virtual tblIps tblIps { get; set; }
    }
}
