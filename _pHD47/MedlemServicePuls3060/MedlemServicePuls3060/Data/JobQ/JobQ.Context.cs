namespace MedlemServicePuls3060
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Data.SqlClient;
    using Pbs3060;

    public partial class dbJobQEntities : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = string.Format("data source=qynhbd9h4f.database.windows.net;initial catalog=dbPuls3060Medlem;user id={0};password={1};encrypt=True;MultipleActiveResultSets=True;App=EntityFramework", clsApp.dbPuls3060MedlemUser, clsApp.dbPuls3060MedlemPW);
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblJobqueue>(entity =>
            {
                entity.ToTable("tblJobqueue");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Completed)
                    .HasColumnName("completed")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Jobname)
                    .IsRequired()
                    .HasColumnName("jobname")
                    .HasMaxLength(25);

                entity.Property(e => e.Onhold)
                    .HasColumnName("onhold")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Scheduleid).HasColumnName("scheduleid");

                entity.Property(e => e.Selected)
                    .HasColumnName("selected")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Starttime)
                    .HasColumnName("starttime")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.TblJobqueue)
                    .HasForeignKey(d => d.Scheduleid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("tblJobqueue_fk");
            });

            modelBuilder.Entity<TblJobqueue>(entity =>
            {
                entity.ToTable("tblJobqueue");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Completed)
                    .HasColumnName("completed")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Jobname)
                    .IsRequired()
                    .HasColumnName("jobname")
                    .HasMaxLength(25);

                entity.Property(e => e.Onhold)
                    .HasColumnName("onhold")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Scheduleid).HasColumnName("scheduleid");

                entity.Property(e => e.Selected)
                    .HasColumnName("selected")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Starttime)
                    .HasColumnName("starttime")
                    .HasColumnType("datetime");
                
                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.TblJobqueue)
                    .HasForeignKey(d => d.Scheduleid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("tblJobqueue_fk");
                
            });
        }


        public virtual DbSet<TblJobqueue> tblJobqueue { get; set; }
        public virtual DbSet<TblSchedule> tblSchedule { get; set; }
    }
}
