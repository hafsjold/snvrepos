namespace Pbs3060
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Pomelo.EntityFrameworkCore.MySql;
    
    public partial class puls3060_dkEntities : DbContext
    {
       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = string.Format(@"server=mysql3.gigahost.dk;port=3306;user={0};password={1};database=puls3060_dk;convert zero datetime=True", clsApp.puls3060_dkUser, clsApp.puls3060_dkPW);
            optionsBuilder.UseMySql(connectionString);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ecpwt_rsform_submission_values> ecpwt_rsform_submission_values { get; set; }
        public DbSet<ecpwt_rsform_submissions> ecpwt_rsform_submissions { get; set; }

    }
}
