namespace MedlemServicePuls3060
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblJobqueue
    {
        public int id { get; set; }
        public Nullable<int> scheduleid { get; set; }
        public System.DateTime starttime { get; set; }
        public string jobname { get; set; }
        public Nullable<bool> onhold { get; set; }
        public Nullable<bool> selected { get; set; }
        public Nullable<bool> completed { get; set; }
    
        public virtual tblSchedule tblSchedule { get; set; }
    }
}
