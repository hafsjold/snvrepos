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
            modelBuilder.Entity<Tblfak>(entity =>
            {
                entity.ToTable("tblfak");

                entity.HasIndex(e => e.Id)
                    .HasName("UQ__tblfak__3213E83EF64A1689")
                    .IsUnique();

                entity.HasIndex(e => new { e.Nr, e.Faknr })
                    .HasName("medlem_fak");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Advisbelob)
                    .HasColumnName("advisbelob")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Advistekst)
                    .HasColumnName("advistekst")
                    .HasMaxLength(4000);

                entity.Property(e => e.Betalingsdato)
                    .HasColumnName("betalingsdato")
                    .HasColumnType("datetime");

                entity.Property(e => e.Betalt).HasColumnName("betalt");

                entity.Property(e => e.Bogfkonto).HasColumnName("bogfkonto");

                entity.Property(e => e.Faknr).HasColumnName("faknr");

                entity.Property(e => e.Fradato)
                    .HasColumnName("fradato")
                    .HasColumnType("datetime");

                entity.Property(e => e.Indbetalerident)
                    .HasColumnName("indbetalerident")
                    .HasMaxLength(19);

                entity.Property(e => e.Indmeldelse).HasColumnName("indmeldelse");

                entity.Property(e => e.Infotekst).HasColumnName("infotekst");

                entity.Property(e => e.Maildato)
                    .HasColumnName("maildato")
                    .HasColumnType("datetime");

                entity.Property(e => e.Rykkerdato)
                    .HasColumnName("rykkerdato")
                    .HasColumnType("datetime");

                entity.Property(e => e.Rykkerstop).HasColumnName("rykkerstop");

                entity.Property(e => e.SfakId).HasColumnName("SFakID");

                entity.Property(e => e.Sfaknr).HasColumnName("SFaknr");

                entity.Property(e => e.Tildato)
                    .HasColumnName("tildato")
                    .HasColumnType("datetime");

                entity.Property(e => e.Tilmeldtpbs).HasColumnName("tilmeldtpbs");

                entity.Property(e => e.Tilpbsid).HasColumnName("tilpbsid");

                entity.Property(e => e.Vnr).HasColumnName("vnr");

                entity.HasOne(d => d.Tilpbs)
                    .WithMany(p => p.Tblfak)
                    .HasForeignKey(d => d.Tilpbsid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_tbltilpbs_tblfak");
            });

            modelBuilder.Entity<Tblfrapbs>(entity =>
            {
                entity.ToTable("tblfrapbs");

                entity.HasIndex(e => e.Id)
                    .HasName("UQ__tblfrapb__3213E83ED2C4C42F")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bilagdato)
                    .HasColumnName("bilagdato")
                    .HasColumnType("datetime");

                entity.Property(e => e.Delsystem)
                    .HasColumnName("delsystem")
                    .HasMaxLength(3);

                entity.Property(e => e.Leverancedannelsesdato)
                    .HasColumnName("leverancedannelsesdato")
                    .HasColumnType("datetime");

                entity.Property(e => e.Leverancespecifikation)
                    .HasColumnName("leverancespecifikation")
                    .HasMaxLength(50);

                entity.Property(e => e.Leverancetype)
                    .HasColumnName("leverancetype")
                    .HasMaxLength(4);

                entity.Property(e => e.Pbsforsendelseid).HasColumnName("pbsforsendelseid");

                entity.Property(e => e.Udtrukket)
                    .HasColumnName("udtrukket")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Pbsforsendelse)
                    .WithMany(p => p.Tblfrapbs)
                    .HasForeignKey(d => d.Pbsforsendelseid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_tblPBSForsendelse_tblfrapbs");
            });

            modelBuilder.Entity<Tblpbsfilename>(entity =>
            {
                entity.ToTable("tblpbsfilename");

                entity.HasIndex(e => e.Id)
                    .HasName("UQ__tblpbsfi__3213E83E799CDE1C")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Atime)
                    .HasColumnName("atime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Filename)
                    .HasColumnName("filename")
                    .HasMaxLength(50);

                entity.Property(e => e.Gid).HasColumnName("gid");

                entity.Property(e => e.Mtime)
                    .HasColumnName("mtime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Path)
                    .HasColumnName("path")
                    .HasMaxLength(255);

                entity.Property(e => e.Pbsforsendelseid).HasColumnName("pbsforsendelseid");

                entity.Property(e => e.Perm)
                    .HasColumnName("perm")
                    .HasMaxLength(50);

                entity.Property(e => e.Size).HasColumnName("size");

                entity.Property(e => e.Transmittime)
                    .HasColumnName("transmittime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.Uid).HasColumnName("uid");
                
                entity.HasOne(d => d.Pbsforsendelse)
                       .WithMany(p => p.Tblpbsfilename)
                       .HasForeignKey(d => d.Pbsforsendelseid)
                       .OnDelete(DeleteBehavior.Cascade)
                       .HasConstraintName("pbsforsendelse_tblpbsfiles");
                
            });

            modelBuilder.Entity<Tblpbsfile>(entity =>
                {
                    entity.ToTable("tblpbsfile");

                    entity.HasIndex(e => e.Id)
                        .HasName("UQ__tblpbsfi__3213E83EE73A395F")
                        .IsUnique();

                    entity.HasIndex(e => e.Seqnr)
                        .HasName("nci_wi_tblpbsfile_7DB29AA2A7B549618D28");

                    entity.Property(e => e.Id).HasColumnName("id");

                    entity.Property(e => e.Data)
                        .HasColumnName("data")
                        .HasMaxLength(300);

                    entity.Property(e => e.Pbsfilesid).HasColumnName("pbsfilesid");

                    entity.Property(e => e.Seqnr).HasColumnName("seqnr");

                    entity.HasOne(d => d.Pbsfiles)
                        .WithMany(p => p.Tblpbsfile)
                        .HasForeignKey(d => d.Pbsfilesid)
                        .HasConstraintName("FK_tblpbsfiles_tblpbsfile");

                });

            modelBuilder.Entity<Tblpbsforsendelse>(entity =>
            {
                entity.ToTable("tblpbsforsendelse");

                entity.HasIndex(e => e.Id)
                    .HasName("UQ__tblpbsfo__3213E83E5F1DB274")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Delsystem)
                    .IsRequired()
                    .HasColumnName("delsystem")
                    .HasMaxLength(3);

                entity.Property(e => e.Leveranceid).HasColumnName("leveranceid");

                entity.Property(e => e.Leverancetype)
                    .HasColumnName("leverancetype")
                    .HasMaxLength(4);

                entity.Property(e => e.Oprettet)
                    .HasColumnName("oprettet")
                    .HasColumnType("datetime");

                entity.Property(e => e.Oprettetaf)
                    .HasColumnName("oprettetaf")
                    .HasMaxLength(3);
            });

            modelBuilder.Entity<TblrsmembershipTransactions>(entity =>
            {
                entity.ToTable("tblrsmembership_transactions");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Adresse)
                    .IsRequired()
                    .HasColumnName("adresse")
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Bynavn)
                    .IsRequired()
                    .HasColumnName("bynavn")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Coupon)
                    .IsRequired()
                    .HasColumnName("coupon")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Currency)
                    .IsRequired()
                    .HasColumnName("currency")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Custom)
                    .IsRequired()
                    .HasColumnName("custom")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Gateway)
                    .IsRequired()
                    .HasColumnName("gateway")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Hash)
                    .IsRequired()
                    .HasColumnName("hash")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasColumnName("ip")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Memberid).HasColumnName("memberid");

                entity.Property(e => e.MembershipId).HasColumnName("membership_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Params)
                    .IsRequired()
                    .HasColumnName("params")
                    .HasColumnType("text");

                entity.Property(e => e.Postnr)
                    .IsRequired()
                    .HasColumnName("postnr")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ResponseLog)
                    .IsRequired()
                    .HasColumnName("response_log")
                    .HasColumnType("text");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.SubscriberId).HasColumnName("subscriber_id");

                entity.Property(e => e.TransId).HasColumnName("trans_id");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.UserData)
                    .IsRequired()
                    .HasColumnName("user_data")
                    .HasColumnType("text");

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasColumnName("user_email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("user_id");
                /*
                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.TblrsmembershipTransactions)
                    .HasForeignKey<TblrsmembershipTransactions>(d => d.Id);
                */
            });

            modelBuilder.Entity<Tblpbsnetdir>(entity =>
            {
                entity.ToTable("tblpbsnetdir");

                entity.HasIndex(e => e.Id)
                    .HasName("UQ__tblpbsne__3213E83EA72A88E5")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Atime)
                    .HasColumnName("atime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Filename)
                    .HasColumnName("filename")
                    .HasMaxLength(50);

                entity.Property(e => e.Gid).HasColumnName("gid");

                entity.Property(e => e.Mtime)
                    .HasColumnName("mtime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Path)
                    .HasColumnName("path")
                    .HasMaxLength(255);

                entity.Property(e => e.Perm)
                    .HasColumnName("perm")
                    .HasMaxLength(50);

                entity.Property(e => e.Size).HasColumnName("size");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.Uid).HasColumnName("uid");
            });



            modelBuilder.Entity<Tbltilpbs>(entity =>
            {
                entity.ToTable("tbltilpbs");

                entity.HasIndex(e => e.Id)
                    .HasName("UQ__tbltilpb__3213E83E58CEF3B7")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bilagdato)
                    .HasColumnName("bilagdato")
                    .HasColumnType("datetime");

                entity.Property(e => e.Delsystem)
                    .HasColumnName("delsystem")
                    .HasMaxLength(3);

                entity.Property(e => e.Leverancedannelsesdato)
                    .HasColumnName("leverancedannelsesdato")
                    .HasColumnType("datetime");

                entity.Property(e => e.Leverancespecifikation)
                    .HasColumnName("leverancespecifikation")
                    .HasMaxLength(10);

                entity.Property(e => e.Leverancetype)
                    .HasColumnName("leverancetype")
                    .HasMaxLength(4);

                entity.Property(e => e.Pbsforsendelseid).HasColumnName("pbsforsendelseid");

                entity.Property(e => e.Udtrukket)
                    .HasColumnName("udtrukket")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Pbsforsendelse)
                    .WithMany(p => p.Tbltilpbs)
                    .HasForeignKey(d => d.Pbsforsendelseid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_tblPBSForsendelse_tbltilpbs");
            });




            //*********** END *****************************************************************'
        }







        public virtual DbSet<Tbladvis> Tbladvis { get; set; }
        public virtual DbSet<Tblaftalelin> Tblaftalelin { get; set; }
        public virtual DbSet<tblAktivitet> tblAktivitet { get; set; }
        public virtual DbSet<tblbanker> tblbanker { get; set; }
        public virtual DbSet<tblbankkonto> tblbankkonto { get; set; }
        public virtual DbSet<Tblbet> Tblbet { get; set; }
        public virtual DbSet<Tblbetalingsidentifikation> Tblbetalingsidentifikation { get; set; }
        public virtual DbSet<Tblbetlin> Tblbetlin { get; set; }
        public virtual DbSet<Tblfak> Tblfak { get; set; }
        public virtual DbSet<Tblfrapbs> Tblfrapbs { get; set; }
        public virtual DbSet<tblHelsingorKommunePostnr> tblHelsingorKommunePostnr { get; set; }
        public virtual DbSet<Tblindbetalingskort> Tblindbetalingskort { get; set; }
        public virtual DbSet<Tblinfotekst> tblinfotekst { get; set; }
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
        public virtual DbSet<Tbloverforsel> tbloverforsel { get; set; }
        public virtual DbSet<tblpaypal> tblpaypal { get; set; }
        public virtual DbSet<Tblpbsfile> Tblpbsfile { get; set; }
        public virtual DbSet<Tblpbsfilename> Tblpbsfilename { get; set; }
        public virtual DbSet<Tblpbsforsendelse> Tblpbsforsendelse { get; set; }
        public virtual DbSet<Tblpbsnetdir> Tblpbsnetdir { get; set; }
        public virtual DbSet<tblRegnskab> tblRegnskab { get; set; }
        public virtual DbSet<tblrsmembership_payments> tblrsmembership_payments { get; set; }
        public virtual DbSet<TblrsmembershipTransactions> TblrsmembershipTransactions { get; set; }
        public virtual DbSet<Tblrykker> tblrykker { get; set; }
        public virtual DbSet<tblsftp> tblsftp { get; set; }
        public virtual DbSet<tblSysinfo> tblSysinfo { get; set; }
        public virtual DbSet<Tbltilpbs> Tbltilpbs { get; set; }
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
