namespace Pbs3060
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Data.SqlClient;

    public partial class dbData3060DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = string.Format("data source=qynhbd9h4f.database.windows.net;initial catalog=dbPuls3060Medlem;user id={0};password={1};encrypt=True;MultipleActiveResultSets=True;App=EntityFramework", clsApp.dbPuls3060MedlemUser, clsApp.dbPuls3060MedlemPW);
            optionsBuilder.UseSqlServer(connectionString);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<tblpbsfilename>()
                .HasOne(p => p.tblpbsforsendelse)
                .WithMany(b => b.tblpbsfilename)
                .HasForeignKey(p => p.pbsforsendelseid)
                .HasConstraintName("pbsforsendelse_tblpbsfiles");
            */
        }

        public virtual DbSet<tbladvis> tbladvis { get; set; }
        public virtual DbSet<tblaftalelin> tblaftalelin { get; set; }
        public virtual DbSet<tblAktivitet> tblAktivitet { get; set; }
        public virtual DbSet<tblbanker> tblbanker { get; set; }
        public virtual DbSet<tblbankkonto> tblbankkonto { get; set; }
        public virtual DbSet<tblbet> tblbet { get; set; }
        public virtual DbSet<tblbetalingsidentifikation> tblbetalingsidentifikation { get; set; }
        public virtual DbSet<tblbetlin> tblbetlin { get; set; }
        public virtual DbSet<tblfak> tblfak { get; set; }
        public virtual DbSet<tblfrapbs> tblfrapbs { get; set; }
        public virtual DbSet<tblHelsingorKommunePostnr> tblHelsingorKommunePostnr { get; set; }
        public virtual DbSet<tblindbetalingskort> tblindbetalingskort { get; set; }
        public virtual DbSet<tblinfotekst> tblinfotekst { get; set; }
        public virtual DbSet<tblipn> tblipn { get; set; }
        public virtual DbSet<tblKontingent> tblKontingent { get; set; }
        public virtual DbSet<tblkontoudtog> tblkontoudtog { get; set; }
        public virtual DbSet<tblkreditor> tblkreditor { get; set; }
        public virtual DbSet<tblMedlem> tblMedlem { get; set; }
        public virtual DbSet<tblMedlemExtra> tblMedlemExtra { get; set; }
        public virtual DbSet<tblMedlemLog> tblMedlemLog { get; set; }
        public virtual DbSet<tblmobilepay> tblmobilepay { get; set; }
        public virtual DbSet<tblnrserie> tblnrserie { get; set; }
        public virtual DbSet<tblNytMedlem> tblNytMedlem { get; set; }
        public virtual DbSet<tbloverforsel> tbloverforsel { get; set; }
        public virtual DbSet<tblpaypal> tblpaypal { get; set; }
        public virtual DbSet<tblpbsfile> tblpbsfile { get; set; }
        public virtual DbSet<tblpbsfilename> tblpbsfilename { get; set; }
        public virtual DbSet<tblpbsforsendelse> tblpbsforsendelse { get; set; }
        public virtual DbSet<tblpbsnetdir> tblpbsnetdir { get; set; }
        public virtual DbSet<tblRegnskab> tblRegnskab { get; set; }
        public virtual DbSet<tblrsmembership_payments> tblrsmembership_payments { get; set; }
        public virtual DbSet<tblrsmembership_transactions> tblrsmembership_transactions { get; set; }
        public virtual DbSet<tblrykker> tblrykker { get; set; }
        public virtual DbSet<tblsftp> tblsftp { get; set; }
        public virtual DbSet<tblSysinfo> tblSysinfo { get; set; }
        public virtual DbSet<tbltilpbs> tbltilpbs { get; set; }
        public virtual DbSet<ecpwt_rsmembership_transactions_completed> ecpwt_rsmembership_transactions_completed { get; set; }
        public virtual DbSet<vAdvis_indbetalingskort> vAdvis_indbetalingskort { get; set; }

        [DbFunctionAttribute("OcrString", "dbo")]
        public string OcrString_Qry(int? pFaknr)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }

        [DbFunctionAttribute("SendtSomString", "dbo")]
        public string SendtSomString_Qry(int? pFaknr)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }

        [DbFunctionAttribute("erPBS", "dbo")]
        public bool erPBS_Qry(int pNr)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }

    }
}
