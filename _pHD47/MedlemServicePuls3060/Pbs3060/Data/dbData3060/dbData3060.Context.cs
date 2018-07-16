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
            modelBuilder.Entity<Tbladvis>(entity =>
            {
                entity.ToTable("tbladvis");

                entity.HasIndex(e => e.Id)
                    .HasName("UQ__tbladvis__3213E83E6E3AB7F5")
                    .IsUnique();

                entity.HasIndex(e => new { e.Nr, e.Faknr })
                    .HasName("tblmedlem_tbladvis");

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

                entity.Property(e => e.Faknr).HasColumnName("faknr");

                entity.Property(e => e.Infotekst).HasColumnName("infotekst");

                entity.Property(e => e.Maildato)
                    .HasColumnName("maildato")
                    .HasColumnType("datetime");

                entity.Property(e => e.Tilpbsid).HasColumnName("tilpbsid");

                entity.HasOne(d => d.Tilpbs)
                    .WithMany(p => p.Tbladvis)
                    .HasForeignKey(d => d.Tilpbsid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_tbltilpbs_tbladvis");
            });

            modelBuilder.Entity<Tblbankkonto>(entity =>
            {
                entity.HasKey(e => e.Pid);

                entity.ToTable("tblbankkonto");

                entity.Property(e => e.Pid).HasColumnName("pid");

                entity.Property(e => e.Afstem).HasColumnName("afstem");

                entity.Property(e => e.Bankkontoid).HasColumnName("bankkontoid");

                entity.Property(e => e.Belob)
                    .HasColumnName("belob")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Dato)
                    .HasColumnName("dato")
                    .HasColumnType("datetime");

                entity.Property(e => e.MobilepayPid).HasColumnName("mobilepay_pid");

                entity.Property(e => e.PaypalPid).HasColumnName("paypal_pid");

                entity.Property(e => e.Saldo)
                    .HasColumnName("saldo")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Skjul).HasColumnName("skjul");

                entity.Property(e => e.Tekst)
                    .HasColumnName("tekst")
                    .HasMaxLength(60);

                entity.HasOne(d => d.MobilepayP)
                    .WithMany(p => p.Tblbankkonto)
                    .HasForeignKey(d => d.MobilepayPid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_tblbankkonto_tblmobilepay_pid");
                
                entity.HasOne(d => d.PaypalP)
                    .WithMany(p => p.Tblbankkonto)
                    .HasForeignKey(d => d.PaypalPid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_tblbankkonto_tblpaypal_pid");
               
            });

            modelBuilder.Entity<Tblaftalelin>(entity =>
            {
                entity.ToTable("tblaftalelin");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Aftalenr).HasColumnName("aftalenr");

                entity.Property(e => e.Aftaleslutdato)
                    .HasColumnName("aftaleslutdato")
                    .HasColumnType("datetime");

                entity.Property(e => e.Aftalestartdato)
                    .HasColumnName("aftalestartdato")
                    .HasColumnType("datetime");

                entity.Property(e => e.Debgrpnr)
                    .IsRequired()
                    .HasColumnName("debgrpnr")
                    .HasMaxLength(5);

                entity.Property(e => e.Debitorkonto)
                    .IsRequired()
                    .HasColumnName("debitorkonto")
                    .HasMaxLength(15);

                entity.Property(e => e.Frapbsid).HasColumnName("frapbsid");

                entity.Property(e => e.Pbssektionnr)
                    .IsRequired()
                    .HasColumnName("pbssektionnr")
                    .HasMaxLength(5);

                entity.Property(e => e.Pbstranskode)
                    .IsRequired()
                    .HasColumnName("pbstranskode")
                    .HasMaxLength(4);

                entity.HasOne(d => d.Frapbs)
                    .WithMany(p => p.Tblaftalelin)
                    .HasForeignKey(d => d.Frapbsid)
                    .HasConstraintName("FK_tblfrapbs_tblaftalelin");
            });

            modelBuilder.Entity<Tblbet>(entity =>
            {
                entity.ToTable("tblbet");

                entity.HasIndex(e => e.Id)
                    .HasName("UQ__tblbet__3213E83E7CB200C4")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bogforingsdato)
                    .HasColumnName("bogforingsdato")
                    .HasColumnType("datetime");

                entity.Property(e => e.Frapbsid).HasColumnName("frapbsid");

                entity.Property(e => e.Indbetalingsbelob)
                    .HasColumnName("indbetalingsbelob")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Pbssektionnr)
                    .HasColumnName("pbssektionnr")
                    .HasMaxLength(4);

                entity.Property(e => e.Rsmembership).HasColumnName("rsmembership");

                entity.Property(e => e.Summabogfort).HasColumnName("summabogfort");

                entity.Property(e => e.Transkode)
                    .HasColumnName("transkode")
                    .HasMaxLength(4);

                entity.HasOne(d => d.Frapbs)
                    .WithMany(p => p.Tblbet)
                    .HasForeignKey(d => d.Frapbsid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_tblfrapbs_tblbet");
            });

            modelBuilder.Entity<Tblbetalingsidentifikation>(entity =>
            {
                entity.ToTable("tblbetalingsidentifikation");

                entity.HasIndex(e => e.Id)
                    .HasName("UQ__tblbetal__3213E83E08067749")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Belob)
                    .HasColumnName("belob")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Betalingsdato)
                    .HasColumnName("betalingsdato")
                    .HasColumnType("date");

                entity.Property(e => e.Betalingsidentifikation)
                    .IsRequired()
                    .HasColumnName("betalingsidentifikation")
                    .HasMaxLength(15);

                entity.Property(e => e.Bogfkonto).HasColumnName("bogfkonto");
            });

            modelBuilder.Entity<Tblbetlin>(entity =>
            {
                entity.ToTable("tblbetlin");

                entity.HasIndex(e => e.Id)
                    .HasName("UQ__tblbetli__3213E83EAFB5144E")
                    .IsUnique();

                entity.HasIndex(e => new { e.Nr, e.Faknr })
                    .HasName("medlem_betaling");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Aftalenr).HasColumnName("aftalenr");

                entity.Property(e => e.Belob)
                    .HasColumnName("belob")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Betalingsdato)
                    .HasColumnName("betalingsdato")
                    .HasColumnType("datetime");

                entity.Property(e => e.Betid).HasColumnName("betid");

                entity.Property(e => e.Bogforingsdato)
                    .HasColumnName("bogforingsdato")
                    .HasColumnType("datetime");

                entity.Property(e => e.Debitorkonto)
                    .HasColumnName("debitorkonto")
                    .HasMaxLength(15);

                entity.Property(e => e.Faknr).HasColumnName("faknr");

                entity.Property(e => e.Indbetalingsbelob)
                    .HasColumnName("indbetalingsbelob")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Indbetalingsdato)
                    .HasColumnName("indbetalingsdato")
                    .HasColumnType("datetime");

                entity.Property(e => e.Pbsarkivnr)
                    .HasColumnName("pbsarkivnr")
                    .HasMaxLength(22);

                entity.Property(e => e.Pbsgebyrbelob)
                    .HasColumnName("pbsgebyrbelob")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Pbskortart)
                    .HasColumnName("pbskortart")
                    .HasMaxLength(2);

                entity.Property(e => e.Pbssektionnr)
                    .HasColumnName("pbssektionnr")
                    .HasMaxLength(4);

                entity.Property(e => e.Pbstranskode)
                    .HasColumnName("pbstranskode")
                    .HasMaxLength(4);

                entity.HasOne(d => d.Bet)
                    .WithMany(p => p.Tblbetlin)
                    .HasForeignKey(d => d.Betid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_tblbet_tblbetlin");
            });

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

            modelBuilder.Entity<Tblindbetalingskort>(entity =>
            {
                entity.ToTable("tblindbetalingskort");

                entity.HasIndex(e => e.Faknr)
                    .HasName("nci_wi_tblindbetalingskort_9E737406495A511ACCC4989AC40B7267");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Belob)
                    .HasColumnName("belob")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Dato)
                    .HasColumnName("dato")
                    .HasColumnType("datetime");

                entity.Property(e => e.Debgrpnr)
                    .IsRequired()
                    .HasColumnName("debgrpnr")
                    .HasMaxLength(5);

                entity.Property(e => e.Debitorkonto)
                    .IsRequired()
                    .HasColumnName("debitorkonto")
                    .HasMaxLength(15);

                entity.Property(e => e.Faknr).HasColumnName("faknr");

                entity.Property(e => e.Fikreditornr)
                    .IsRequired()
                    .HasColumnName("fikreditornr")
                    .HasMaxLength(8);

                entity.Property(e => e.Frapbsid).HasColumnName("frapbsid");

                entity.Property(e => e.Indbetalerident)
                    .IsRequired()
                    .HasColumnName("indbetalerident")
                    .HasMaxLength(19);

                entity.Property(e => e.Kortartkode)
                    .IsRequired()
                    .HasColumnName("kortartkode")
                    .HasMaxLength(2);

                entity.Property(e => e.Pbssektionnr)
                    .IsRequired()
                    .HasColumnName("pbssektionnr")
                    .HasMaxLength(5);

                entity.Property(e => e.Pbstranskode)
                    .IsRequired()
                    .HasColumnName("pbstranskode")
                    .HasMaxLength(4);

                entity.Property(e => e.Regnr)
                    .HasColumnName("regnr")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.HasOne(d => d.Frapbs)
                    .WithMany(p => p.Tblindbetalingskort)
                    .HasForeignKey(d => d.Frapbsid)
                    .HasConstraintName("FK_tblfrapbs_tblindbetalingskort");
            });

            modelBuilder.Entity<Tblinfotekst>(entity =>
            {
                entity.ToTable("tblinfotekst");

                entity.HasIndex(e => e.Id)
                    .HasName("UQ__tblinfot__3213E83EA93654C8")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Msgtext)
                    .HasColumnName("msgtext")
                    .HasMaxLength(4000);

                entity.Property(e => e.Navn)
                    .HasColumnName("navn")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblKontingent>(entity =>
            {
                entity.ToTable("tblKontingent");

                entity.HasIndex(e => e.Id)
                    .HasName("UQ__tblKonti__3213E83E2685A0EA")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Aarskontingent)
                    .HasColumnName("aarskontingent")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Slutalder).HasColumnName("slutalder");

                entity.Property(e => e.Slutdato)
                    .HasColumnName("slutdato")
                    .HasColumnType("datetime");

                entity.Property(e => e.Startalder).HasColumnName("startalder");

                entity.Property(e => e.Startdato)
                    .HasColumnName("startdato")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Tblmobilepay>(entity =>
            {
                entity.HasKey(e => e.Pid);

                entity.ToTable("tblmobilepay");

                entity.Property(e => e.Pid).HasColumnName("pid");

                entity.Property(e => e.Belob).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Besked).HasMaxLength(250);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Gebyr).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.MobilNummer).HasMaxLength(15);

                entity.Property(e => e.MobilePayNummer).HasMaxLength(15);

                entity.Property(e => e.Navn).HasMaxLength(50);

                entity.Property(e => e.NavnBetalingssted).HasMaxLength(50);

                entity.Property(e => e.Saldo).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TransaktionsId)
                    .HasColumnName("TransaktionsID")
                    .HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(25);
            });

            modelBuilder.Entity<Tblnrserie>(entity =>
            {
                entity.HasKey(e => e.Nrserienavn);

                entity.ToTable("tblnrserie");

                entity.HasIndex(e => e.Nrserienavn)
                    .HasName("UQ__tblnrser__A6C9DFDDA1091CB1")
                    .IsUnique();

                entity.Property(e => e.Nrserienavn)
                    .HasColumnName("nrserienavn")
                    .HasMaxLength(30)
                    .ValueGeneratedNever();

                entity.Property(e => e.Sidstbrugtenr).HasColumnName("sidstbrugtenr");
            });

            modelBuilder.Entity<Tbloverforsel>(entity =>
            {
                entity.ToTable("tbloverforsel");

                entity.HasIndex(e => e.Id)
                    .HasName("UQ__tbloverf__3213E83EA57D1501")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Advisbelob)
                    .HasColumnName("advisbelob")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Advistekst)
                    .HasColumnName("advistekst")
                    .HasMaxLength(20);

                entity.Property(e => e.Bankkontonr)
                    .HasColumnName("bankkontonr")
                    .HasMaxLength(10);

                entity.Property(e => e.Bankregnr)
                    .HasColumnName("bankregnr")
                    .HasMaxLength(4);

                entity.Property(e => e.Betalingsdato)
                    .HasColumnName("betalingsdato")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Emailsent).HasColumnName("emailsent");

                entity.Property(e => e.Emailtekst)
                    .HasColumnName("emailtekst")
                    .HasMaxLength(4000);

                entity.Property(e => e.Kaldenavn).HasMaxLength(25);

                entity.Property(e => e.Navn).HasMaxLength(35);

                entity.Property(e => e.SfakId).HasColumnName("SFakID");

                entity.Property(e => e.Sfaknr).HasColumnName("SFaknr");

                entity.Property(e => e.Tilpbsid).HasColumnName("tilpbsid");

                entity.HasOne(d => d.Tilpbs)
                    .WithMany(p => p.Tbloverforsel)
                    .HasForeignKey(d => d.Tilpbsid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_tbltilpbs_tbloverfoersel");
            });

            modelBuilder.Entity<Tblpaypal>(entity =>
            {
                entity.HasKey(e => e.Pid);

                entity.ToTable("tblpaypal");

                entity.Property(e => e.Pid).HasColumnName("pid");

                entity.Property(e => e.AddressLine1)
                    .HasColumnName("Address_Line_1")
                    .HasMaxLength(395);

                entity.Property(e => e.AddressLine2)
                    .HasColumnName("Address_Line_2")
                    .HasMaxLength(395);

                entity.Property(e => e.AddressStatus)
                    .HasColumnName("Address_Status")
                    .HasMaxLength(64);

                entity.Property(e => e.AuctionSite)
                    .HasColumnName("Auction_Site")
                    .HasMaxLength(20);

                entity.Property(e => e.Balance).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BuyerId)
                    .HasColumnName("Buyer_ID")
                    .HasMaxLength(50);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.ClosingDate)
                    .HasColumnName("Closing_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ContactPhoneNumber)
                    .HasColumnName("Contact_Phone_Number")
                    .HasMaxLength(50);

                entity.Property(e => e.CounterpartyStatus)
                    .HasColumnName("Counterparty_Status")
                    .HasMaxLength(64);

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.Currency).HasMaxLength(50);

                entity.Property(e => e.CustomNumber)
                    .HasColumnName("Custom_Number")
                    .HasMaxLength(50);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.EscrowId)
                    .HasColumnName("Escrow_Id")
                    .HasMaxLength(50);

                entity.Property(e => e.Fee).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.FromEmailAddress)
                    .HasColumnName("From_Email_Address")
                    .HasMaxLength(128);

                entity.Property(e => e.Gross).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.InsuranceAmount)
                    .HasColumnName("Insurance_Amount")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.InvoiceId)
                    .HasColumnName("Invoice_Id")
                    .HasMaxLength(50);

                entity.Property(e => e.InvoiceNumber)
                    .HasColumnName("Invoice_Number")
                    .HasMaxLength(50);

                entity.Property(e => e.ItemId)
                    .HasColumnName("Item_ID")
                    .HasMaxLength(256);

                entity.Property(e => e.ItemTitle)
                    .HasColumnName("Item_Title")
                    .HasMaxLength(128);

                entity.Property(e => e.ItemUrl)
                    .HasColumnName("Item_URL")
                    .HasMaxLength(256);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Net).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Option1Name)
                    .HasColumnName("Option_1_Name")
                    .HasMaxLength(60);

                entity.Property(e => e.Option1Value)
                    .HasColumnName("Option_1_Value")
                    .HasMaxLength(30);

                entity.Property(e => e.Option2Name)
                    .HasColumnName("Option_2_Name")
                    .HasMaxLength(60);

                entity.Property(e => e.Option2Value)
                    .HasColumnName("Option_2_Value")
                    .HasMaxLength(30);

                entity.Property(e => e.Quantity).HasMaxLength(50);

                entity.Property(e => e.ReceiptId)
                    .HasColumnName("Receipt_ID")
                    .HasMaxLength(50);

                entity.Property(e => e.ReferenceTxnId)
                    .HasColumnName("Reference_Txn_ID")
                    .HasMaxLength(50);

                entity.Property(e => e.SalesTax)
                    .HasColumnName("Sales_Tax")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ShippingAndHandlingAmount)
                    .HasColumnName("Shipping_and_Handling_Amount")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.State).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(15);

                entity.Property(e => e.TimeZone)
                    .HasColumnName("Time_Zone")
                    .HasMaxLength(50);

                entity.Property(e => e.ToEmailAddress)
                    .HasColumnName("To_Email_Address")
                    .HasMaxLength(128);

                entity.Property(e => e.TransactionId)
                    .HasColumnName("Transaction_ID")
                    .HasMaxLength(25);

                entity.Property(e => e.Type).HasMaxLength(64);

                entity.Property(e => e.ZipCode)
                    .HasColumnName("Zip_Code")
                    .HasMaxLength(50);
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

            modelBuilder.Entity<TblrsmembershipPayments>(entity =>
            {
                entity.ToTable("tblrsmembership_payments");

                entity.Property(e => e.Id).HasColumnName("id");

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

                entity.Property(e => e.MembershipEnd)
                    .HasColumnName("membership_end")
                    .HasColumnType("date");

                entity.Property(e => e.MembershipId).HasColumnName("membership_id");

                entity.Property(e => e.MembershipStart)
                    .HasColumnName("membership_start")
                    .HasColumnType("date");

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
                    .WithMany(p => p.TblrsmembershipTransactions)
                    .HasForeignKey<TblrsmembershipTransactions>(d => d.Id);
                */

            });

            modelBuilder.Entity<Tblrykker>(entity =>
            {
                entity.ToTable("tblrykker");

                entity.HasIndex(e => e.Id)
                    .HasName("UQ__tblrykke__3213E83EB6C0CA73")
                    .IsUnique();

                entity.HasIndex(e => new { e.Nr, e.Faknr })
                    .HasName("tblmedlem_tblrykker");

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

                entity.Property(e => e.Faknr).HasColumnName("faknr");

                entity.Property(e => e.Infotekst).HasColumnName("infotekst");

                entity.Property(e => e.Maildato)
                    .HasColumnName("maildato")
                    .HasColumnType("datetime");

                entity.Property(e => e.Rykkerdato)
                    .HasColumnName("rykkerdato")
                    .HasColumnType("datetime");

                entity.Property(e => e.Tilpbsid).HasColumnName("tilpbsid");

                entity.HasOne(d => d.Tilpbs)
                    .WithMany(p => p.Tblrykker)
                    .HasForeignKey(d => d.Tilpbsid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_tbltilpbs_tblrykker");
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

            modelBuilder.Entity<Tblsftp>(entity =>
            {
                entity.ToTable("tblsftp");

                entity.HasIndex(e => e.Id)
                    .HasName("UQ__tblsftp__3213E83EE39573CF")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Certificate)
                    .HasColumnName("certificate")
                    .HasMaxLength(4000);

                entity.Property(e => e.Host)
                    .IsRequired()
                    .HasColumnName("host")
                    .HasMaxLength(64);

                entity.Property(e => e.Inbound)
                    .HasColumnName("inbound")
                    .HasMaxLength(64);

                entity.Property(e => e.Navn)
                    .IsRequired()
                    .HasColumnName("navn")
                    .HasMaxLength(64);

                entity.Property(e => e.Outbound)
                    .HasColumnName("outbound")
                    .HasMaxLength(64);

                entity.Property(e => e.Pincode)
                    .HasColumnName("pincode")
                    .HasMaxLength(64);

                entity.Property(e => e.Port)
                    .IsRequired()
                    .HasColumnName("port")
                    .HasMaxLength(5);

                entity.Property(e => e.User)
                    .IsRequired()
                    .HasColumnName("user")
                    .HasMaxLength(16);
            });

            modelBuilder.Entity<TblSysinfo>(entity =>
            {
                entity.HasKey(e => e.Vkey);

                entity.ToTable("tblSysinfo");

                entity.HasIndex(e => e.Vkey)
                    .HasName("UQ__tblSysin__70E52068602951ED")
                    .IsUnique();

                entity.Property(e => e.Vkey)
                    .HasColumnName("vkey")
                    .HasMaxLength(10)
                    .ValueGeneratedNever();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Val)
                    .IsRequired()
                    .HasColumnName("val")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<EcpwtRsmembershipTransactionsCompleted>(entity =>
            {
                entity.ToTable("ecpwt_rsmembership_transactions_completed");

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasMaxLength(50);
            });
        }

        public virtual DbSet<Tbladvis> Tbladvis { get; set; }
        public virtual DbSet<Tblaftalelin> Tblaftalelin { get; set; }
        public virtual DbSet<Tblbankkonto> Tblbankkonto { get; set; }
        public virtual DbSet<Tblbet> Tblbet { get; set; }
        public virtual DbSet<Tblbetalingsidentifikation> Tblbetalingsidentifikation { get; set; }
        public virtual DbSet<Tblbetlin> Tblbetlin { get; set; }
        public virtual DbSet<Tblfak> Tblfak { get; set; }
        public virtual DbSet<Tblfrapbs> Tblfrapbs { get; set; }
        public virtual DbSet<Tblindbetalingskort> Tblindbetalingskort { get; set; }
        public virtual DbSet<Tblinfotekst> Tblinfotekst { get; set; }
        public virtual DbSet<TblKontingent> TblKontingent { get; set; }
        public virtual DbSet<Tblmobilepay> Tblmobilepay { get; set; }
        public virtual DbSet<Tblnrserie> Tblnrserie { get; set; }
        public virtual DbSet<Tbloverforsel> Tbloverforsel { get; set; }
        public virtual DbSet<Tblpaypal> Tblpaypal { get; set; }
        public virtual DbSet<Tblpbsfile> Tblpbsfile { get; set; }
        public virtual DbSet<Tblpbsfilename> Tblpbsfilename { get; set; }
        public virtual DbSet<Tblpbsforsendelse> Tblpbsforsendelse { get; set; }
        public virtual DbSet<Tblpbsnetdir> Tblpbsnetdir { get; set; }
        public virtual DbSet<TblrsmembershipPayments> TblrsmembershipPayments { get; set; }
        public virtual DbSet<TblrsmembershipTransactions> TblrsmembershipTransactions { get; set; }
        public virtual DbSet<Tblrykker> Tblrykker { get; set; }
        public virtual DbSet<Tblsftp> Tblsftp { get; set; }
        public virtual DbSet<TblSysinfo> TblSysinfo { get; set; }
        public virtual DbSet<Tbltilpbs> Tbltilpbs { get; set; }
        public virtual DbSet<EcpwtRsmembershipTransactionsCompleted> EcpwtRsmembershipTransactionsCompleted { get; set; }

        public virtual DbSet<tblAktivitet> tblAktivitet { get; set; }
        public virtual DbSet<tblbanker> tblbanker { get; set; }
        public virtual DbSet<tblHelsingorKommunePostnr> tblHelsingorKommunePostnr { get; set; }
        public virtual DbSet<tblipn> tblipn { get; set; }
        public virtual DbSet<tblkontoudtog> tblkontoudtog { get; set; }
        public virtual DbSet<tblkreditor> tblkreditor { get; set; }
        public virtual DbSet<tblMedlem> tblMedlem { get; set; }
        public virtual DbSet<tblMedlemExtra> tblMedlemExtra { get; set; }
        public virtual DbSet<tblMedlemLog> tblMedlemLog { get; set; }
        public virtual DbSet<tblNytMedlem> tblNytMedlem { get; set; }
        public virtual DbSet<tblRegnskab> tblRegnskab { get; set; }
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
