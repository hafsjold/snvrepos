namespace Pbs3060
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Pomelo.EntityFrameworkCore.MySql;
    
    public partial class puls3060_nyEntities : DbContext
    {
       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = string.Format(@"server=mysql3.gigahost.dk;port=3306;user={0};password={1};database=puls3060_dk;convert zero datetime=True", clsApp.puls3060_dkUser, clsApp.puls3060_dkPW);
            optionsBuilder.UseMySql(connectionString);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            /*
            modelBuilder.Entity<ecpwt_users>(entity => { entity.HasKey(e => e.id); });
            modelBuilder.Entity<ecpwt_rsmembership_membership_subscribers>(entity => { entity.HasKey(e => e.id); });
            modelBuilder.Entity<ecpwt_rsmembership_subscribers>(entity => { entity.HasKey(e => e.user_id); });
            modelBuilder.Entity<ecpwt_rsmembership_transactions>(entity => { entity.HasKey(e => e.id); });
            modelBuilder.Entity<ecpwt_rsmembership_transactions_user_data>(entity => { entity.HasKey(e => e.id); });
            modelBuilder.Entity<ecpwt_user_usergroup_map>(entity => { entity.HasKey(e => e.user_id); });
            modelBuilder.Entity<ecpwt_usergroups>(entity => { entity.HasKey(e => e.id); });
            modelBuilder.Entity<ecpwt_rsmembership_memberships>(entity => { entity.HasKey(e => e.id); });
            modelBuilder.Entity<tblmembershippayments>(entity => { entity.HasKey(e => e.id); });
            modelBuilder.Entity<tblpaypalpayment>(entity => { entity.HasKey(e => e.paypal_transactions_id); });
            */
        }

        public DbSet<ecpwt_users> ecpwt_users { get; set; }
        public DbSet<ecpwt_rsmembership_membership_subscribers> ecpwt_rsmembership_membership_subscribers { get; set; }
        public DbSet<ecpwt_rsmembership_subscribers> ecpwt_rsmembership_subscribers { get; set; }
        public DbSet<ecpwt_rsmembership_transactions> ecpwt_rsmembership_transactions { get; set; }
        public DbSet<ecpwt_rsmembership_transactions_user_data> ecpwt_rsmembership_transactions_user_data { get; set; }
        public DbSet<ecpwt_user_usergroup_map> ecpwt_user_usergroup_map { get; set; }
        public DbSet<ecpwt_usergroups> ecpwt_usergroups { get; set; }
        public DbSet<ecpwt_rsmembership_memberships> ecpwt_rsmembership_memberships { get; set; }
        public DbSet<tblmembershippayment> tblmembershippayment { get; set; }
        public DbSet<tblpaypalpayment> tblpaypalpayment { get; set; }
    }
}
