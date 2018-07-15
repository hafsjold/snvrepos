namespace MedlemServicePuls3060
{
    using System;
    using System.Collections.Generic;
    
    public partial class TblSchedule
    {
        public TblSchedule()
        {
            this.TblJobqueue = new HashSet<TblJobqueue>();
        }
    
        public int Id { get; set; }
        public string Schedule { get; set; }
        public string Jobname { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    
        public ICollection<TblJobqueue> TblJobqueue { get; set; }
    }
}
