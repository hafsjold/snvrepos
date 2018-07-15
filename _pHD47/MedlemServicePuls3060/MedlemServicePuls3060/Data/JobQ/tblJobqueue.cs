namespace MedlemServicePuls3060
{
    using System;
    using System.Collections.Generic;
    
    public partial class TblJobqueue
    {
        public int Id { get; set; }
        public int? Scheduleid { get; set; }
        public DateTime Starttime { get; set; }
        public string Jobname { get; set; }
        public bool? Onhold { get; set; }
        public bool? Selected { get; set; }
        public bool? Completed { get; set; }
    
        public virtual TblSchedule Schedule { get; set; }
    }
}
