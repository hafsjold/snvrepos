namespace MedlemServicePuls3060
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Data.SqlClient;

    public partial class dbJobQEntities : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("data source=qynhbd9h4f.database.windows.net;initial catalog=dbPuls3060Medlem;user id=sqlUser;password=Puls3060;encrypt=True;MultipleActiveResultSets=True;App=EntityFramework");
        }

        /*
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
        */
    
        public virtual DbSet<tblJobqueue> tblJobqueue { get; set; }
        public virtual DbSet<tblSchedule> tblSchedule { get; set; }



        /*
        public virtual int jobqueueadd(Nullable<System.DateTime> starttime, string jobname, Nullable<int> scheduleid, Nullable<bool> onhold)
        {
            var starttimeParameter = starttime.HasValue ?
                new ObjectParameter("starttime", starttime) :
                new ObjectParameter("starttime", typeof(System.DateTime));
    
            var jobnameParameter = jobname != null ?
                new ObjectParameter("jobname", jobname) :
                new ObjectParameter("jobname", typeof(string));
    
            var scheduleidParameter = scheduleid.HasValue ?
                new ObjectParameter("scheduleid", scheduleid) :
                new ObjectParameter("scheduleid", typeof(int));
    
            var onholdParameter = onhold.HasValue ?
                new ObjectParameter("onhold", onhold) :
                new ObjectParameter("onhold", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("jobqueueadd", starttimeParameter, jobnameParameter, scheduleidParameter, onholdParameter);
        }
    
        public virtual int jobqueuecomplete(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("jobqueuecomplete", idParameter);
        }
 
        public virtual int jobqueuenext(ObjectParameter id, ObjectParameter jobname)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("jobqueuenext", id, jobname);
        }
       */


    }
}
