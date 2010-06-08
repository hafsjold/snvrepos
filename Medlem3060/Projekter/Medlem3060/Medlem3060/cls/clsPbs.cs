using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using Microsoft.VisualBasic;
using System.Transactions;
using System.Text.RegularExpressions;

namespace nsPuls3060
{
    public class clsLog
    {
        public int? Id;
        public int? Nr;
        public DateTime? Logdato;
        public int? Akt_id;
        public DateTime? Akt_dato;
    }

    public partial class clsMedlem
    {
        private DateTime? m_indmeldelsesDato = null;
        private DateTime? m_udmeldelsesDato = null;
        private DateTime? m_kontingentTilbageførtDato = null;
        private DateTime? m_kontingentBetalingsDato = null;
        private DateTime? m_kontingentBetaltTilDato = null;
        private DateTime? m_opkrævningsDato = null;
        private int m_BetalingsFristiDageGamleMedlemmer = global::nsPuls3060.Properties.Settings.Default.BetalingsFristiDageGamleMedlemmer;
        private int m_BetalingsFristiDageNyeMedlemmer = global::nsPuls3060.Properties.Settings.Default.BetalingsFristiDageNyeMedlemmer;
        private DateTime m_kontingentTilDato31 = DateTime.MinValue;
        private DateTime m_kontingentBetaltDato31 = DateTime.MinValue;
        private DateTime m_restanceTilDatoGamleMedlemmer = DateTime.MinValue;
        private DateTime restanceTilDatoNyeMedlemmer = DateTime.MinValue;
        private bool m_b10 = false; // Seneste Indmelses dato fundet
        private bool m_b20 = false; // Seneste PBS opkrævnings dato fundet
        private bool m_b30 = false; // Seneste Kontingent betalt til dato fundet
        private bool m_b31 = false; // Næst seneste Kontingent betalt til dato fundet
        private bool m_b40 = false; // Seneste PBS betaling tilbageført fundet
        private bool m_b50 = false; // Udmeldelses dato fundet

        public DateTime? indmeldelsesDato
        {
            get
            {
                return m_indmeldelsesDato;
            }
            set
            {
                m_indmeldelsesDato = value;
            }
        }
        public DateTime? udmeldelsesDato
        {
            get
            {
                return m_udmeldelsesDato;
            }
            set
            {
                m_udmeldelsesDato = value;
            }
        }
        public DateTime? kontingentTilbageførtDato
        {
            get
            {
                return m_kontingentTilbageførtDato;
            }
            set
            {
                m_kontingentTilbageførtDato = value;
            }
        }
        public DateTime? kontingentBetalingsDato
        {
            get
            {
                return m_kontingentBetalingsDato;
            }
            set
            {
                m_kontingentBetalingsDato = value;
            }
        }
        public DateTime? kontingentBetaltTilDato
        {
            get
            {
                return m_kontingentBetaltTilDato;
            }
            set
            {
                m_kontingentBetaltTilDato = value;
            }
        }
        public DateTime? opkrævningsDato
        {
            get
            {
                return m_opkrævningsDato;
            }
            set
            {
                m_opkrævningsDato = value;
            }
        }
        public Boolean erMedlem() { return erMedlem(DateTime.Now); }
        public Boolean erMedlem(DateTime pDate)
        {


            var qrylog = Program.qryLog()
                                .Where(u => u.Nr == m_Nr)
                                .Where(u => u.Logdato <= pDate)
                                .OrderByDescending(u => u.Logdato);

            foreach (var MedlemLog in qrylog)
            {
                switch (MedlemLog.Akt_id)
                {
                    case 10: // Seneste Indmelses dato
                        if (!m_b10)
                        {
                            m_b10 = true;
                            m_indmeldelsesDato = (DateTime)MedlemLog.Akt_dato;
                        }
                        break;

                    case 20:  // Seneste PBS opkrævnings dato
                        if (!m_b20)
                        {
                            m_b20 = true;
                            m_opkrævningsDato = (DateTime)MedlemLog.Akt_dato;
                        }
                        break;

                    case 30:  // Kontingent betalt til dato
                        if ((m_b30) && (!m_b31)) // Næst seneste Kontingent betalt til dato
                        {
                            m_b31 = true;
                            m_kontingentBetaltDato31 = (DateTime)MedlemLog.Logdato;
                            m_kontingentTilDato31 = (DateTime)MedlemLog.Akt_dato;
                        }
                        if ((!m_b30) && (!m_b31)) // Seneste Kontingent betalt til dato
                        {
                            m_b30 = true;
                            m_kontingentBetalingsDato = (DateTime)MedlemLog.Logdato;
                            m_kontingentBetaltTilDato = (DateTime)MedlemLog.Akt_dato;
                        }
                        break;

                    case 40:  // Seneste PBS betaling tilbageført
                        if (!m_b40)
                        {
                            m_b40 = true;
                            m_kontingentTilbageførtDato = (DateTime)MedlemLog.Akt_dato;
                        }
                        break;

                    case 50:  // Udmeldelses dato
                        if (!m_b50)
                        {
                            m_b50 = true;
                            m_udmeldelsesDato = (DateTime)MedlemLog.Akt_dato;
                        }
                        break;
                }
            }

            //Undersøg vedr ind- og udmeldelse
            if (m_b10) //Findes der en indmeldelse
            {
                if (m_b50) //Findes der en udmeldelse
                {
                    if (m_udmeldelsesDato >= m_indmeldelsesDato) //Er udmeldelsen aktiv
                    {
                        if (m_udmeldelsesDato <= pDate) //Er udmeldelsen aktiv
                        {
                            return false;
                        }
                    }
                }
            }
            else //Der findes ingen indmeldelse
            {
                return false;
            }


            //Find aktive betalingsrecord
            if (m_b40) //Findes der en kontingent tilbageført
            {
                if (m_kontingentTilbageførtDato >= m_kontingentBetalingsDato) //Kontingenttilbageført er aktiv
                {
                    //''!!!Kontingent er tilbageført !!!!!!!!!
                    if (m_b31)
                    {
                        m_kontingentBetalingsDato = m_kontingentBetaltDato31;
                        m_kontingentBetaltTilDato = m_kontingentTilDato31;
                    }
                    else
                    {
                        m_b30 = false;
                    }
                }
            }


            //Undersøg om der er betalt kontingent
            if (m_b30) //Findes der en betaling
            {
                m_restanceTilDatoGamleMedlemmer = ((DateTime)m_kontingentBetaltTilDato).AddDays(m_BetalingsFristiDageGamleMedlemmer);
                if (m_restanceTilDatoGamleMedlemmer >= pDate) //Er kontingentTilDato aktiv
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            { //Der findes ingen betalinger. Nyt medlem?
                restanceTilDatoNyeMedlemmer = ((DateTime)m_indmeldelsesDato).AddDays(m_BetalingsFristiDageNyeMedlemmer);
                if (restanceTilDatoNyeMedlemmer >= pDate)
                { //Er kontingentTilDato aktiv
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Boolean kanRykkes() { return kanRykkes(DateTime.Now); }
        public Boolean kanRykkes(DateTime pDate)
        {
            var qrylog = Program.qryLog()
                                .Where(u => u.Nr == m_Nr)
                                .Where(u => u.Logdato <= pDate)
                                .OrderByDescending(u => u.Logdato);

            foreach (var MedlemLog in qrylog)
            {
                switch (MedlemLog.Akt_id)
                {
                    case 10: // Seneste Indmelses dato
                        if (!m_b10)
                        {
                            m_b10 = true;
                            m_indmeldelsesDato = (DateTime)MedlemLog.Akt_dato;
                        }
                        break;

                    case 20:  // Seneste PBS opkrævnings dato
                        if (!m_b20)
                        {
                            m_b20 = true;
                            m_opkrævningsDato = (DateTime)MedlemLog.Akt_dato;
                        }
                        break;

                    case 30:  // Kontingent betalt til dato
                        if ((m_b30) && (!m_b31)) // Næst seneste Kontingent betalt til dato
                        {
                            m_b31 = true;
                            m_kontingentBetaltDato31 = (DateTime)MedlemLog.Logdato;
                            m_kontingentTilDato31 = (DateTime)MedlemLog.Akt_dato;
                        }
                        if ((!m_b30) && (!m_b31)) // Seneste Kontingent betalt til dato
                        {
                            m_b30 = true;
                            m_kontingentBetalingsDato = (DateTime)MedlemLog.Logdato;
                            m_kontingentBetaltTilDato = (DateTime)MedlemLog.Akt_dato;
                        }
                        break;

                    case 40:  // Seneste PBS betaling tilbageført
                        if (!m_b40)
                        {
                            m_b40 = true;
                            m_kontingentTilbageførtDato = (DateTime)MedlemLog.Akt_dato;
                        }
                        break;

                    case 50:  // Udmeldelses dato
                        if (!m_b50)
                        {
                            m_b50 = true;
                            m_udmeldelsesDato = (DateTime)MedlemLog.Akt_dato;
                        }
                        break;
                }
            }

            //Undersøg vedr ind- og udmeldelse
            if (m_b10) //Findes der en indmeldelse
            {
                if (m_b50) //Findes der en udmeldelse
                {
                    if (m_udmeldelsesDato >= m_indmeldelsesDato) //Er udmeldelsen aktiv
                    {
                        if (m_udmeldelsesDato <= pDate) //Er udmeldelsen aktiv
                        {
                            return false;
                        }
                    }
                }
            }
            else //Der findes ingen indmeldelse
            {
                return false;
            }


            //Find aktive betalingsrecord
            if (m_b40) //Findes der en kontingent tilbageført
            {
                if (m_kontingentTilbageførtDato >= m_kontingentBetalingsDato) //Kontingenttilbageført er aktiv
                {
                    //''!!!Kontingent er tilbageført !!!!!!!!!
                    if (m_b31)
                    {
                        m_kontingentBetalingsDato = m_kontingentBetaltDato31;
                        m_kontingentBetaltTilDato = m_kontingentTilDato31;
                    }
                    else
                    {
                        m_b30 = false;
                    }
                }
            }


            //Undersøg om der er betalt kontingent
            if (m_b30) //Findes der en betaling
            {
                if ((DateTime)m_kontingentBetaltTilDato >= pDate) //Er kontingentTilDato aktiv
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            { //Der findes ingen betalinger
                return true;
            }
        }

    }

    class clsPbs
    {
        private TblRegnskab m_rec_Regnskab;

        public clsPbs() { }

        public static int nextval(string nrserienavn)
        {
            try
            {
                var rst = (from c in Program.dbData3060.Tblnrserie
                           where c.Nrserienavn == nrserienavn
                           select c).First();

                if (rst.Sidstbrugtenr != null)
                {
                    rst.Sidstbrugtenr += 1;
                    return rst.Sidstbrugtenr.Value;
                }
                else
                {
                    rst.Sidstbrugtenr = 0;
                    return rst.Sidstbrugtenr.Value;
                }
            }
            catch (System.InvalidOperationException)
            {
                Tblnrserie rec_nrserie = new Tblnrserie
                {
                    Nrserienavn = nrserienavn,
                    Sidstbrugtenr = 0
                };
                Program.dbData3060.Tblnrserie.InsertOnSubmit(rec_nrserie);
                Program.dbData3060.SubmitChanges();

                return 0;
            }
        }

        public static int nextvaltest(string nrserienavn)
        {
            try
            {
                var rst = (from c in Program.dbData3060.Tblnrserie
                           where c.Nrserienavn == nrserienavn
                           select c).First();

                if (rst.Sidstbrugtenr != null)
                {
                    return rst.Sidstbrugtenr.Value + 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (System.InvalidOperationException)
            {
                return 0;
            }
        }

        public static void nextvalset(string nrserienavn, int value)
        {
            try
            {
                var rst = (from c in Program.dbData3060.Tblnrserie
                           where c.Nrserienavn == nrserienavn
                           select c).First();

                rst.Sidstbrugtenr = value;
            }
            catch (System.InvalidOperationException)
            {
                Tblnrserie rec_nrserie = new Tblnrserie
                {
                    Nrserienavn = nrserienavn,
                    Sidstbrugtenr = value
                };
                Program.dbData3060.Tblnrserie.InsertOnSubmit(rec_nrserie);
                Program.dbData3060.SubmitChanges();
            }
        }

        public static bool gettilmeldtpbs(int? Nr)
        {
            var pbsaftalestart = from s in Program.dbData3060.Tblaftalelin
                                 where s.Nr == Nr & s.Pbstranskode == "0231"
                                 select s;
            var pbsaftaleslut = from s in Program.dbData3060.Tblaftalelin
                                where s.Nr == Nr & s.Pbstranskode != "0231"
                                select s;

            var pbsaftale = from a in pbsaftalestart
                            join s in pbsaftaleslut on a.Aftalenr equals s.Aftalenr into pbsaftaleslut2
                            from s in pbsaftaleslut2.DefaultIfEmpty()
                            where s.Id == null
                            orderby a.Aftalestartdato descending
                            select a;

            int antal = pbsaftale.Count();
            if (antal > 0) return true;
            else return false;
        }

        public static bool getbetaltudmeldt(int? Nr)
        {
            var qry = from l in Program.dbData3060.TblMedlemLog
                      where l.Nr == Nr
                      orderby l.Logdato descending
                      select l;

            if (qry.Count() > 0) return true;
            else return false;
        }


        public bool ReadRegnskaber()
        {
            string RegnskabId;
            string RegnskabMappe;
            string line;
            FileStream ts;
            string Eksportmappe;
            string Datamappe;

            try
            {
                Eksportmappe = (string)Registry.CurrentUser.OpenSubKey(@"SOFTWARE\STONE'S SOFTWARE\SUMMAPRO\START").GetValue("Eksportmappe");
                Datamappe = (string)Registry.CurrentUser.OpenSubKey(@"SOFTWARE\STONE'S SOFTWARE\SUMMAPRO\START").GetValue("Datamappe");
            }
            catch (System.NullReferenceException)
            {
                Program.dbData3060.ExecuteCommand("DELETE FROM TblRegnskab;");
                return false;
            }
            DirectoryInfo dir = new DirectoryInfo(Datamappe);
            foreach (var sub_dir in dir.GetDirectories())
            {
                if (Information.IsNumeric(sub_dir.Name))
                {
                    switch (sub_dir.Name.ToUpper())
                    {
                        case "-2":
                        case "-1":
                        case "0":
                            break;

                        default:
                            //do somthing here
                            RegnskabId = sub_dir.Name;

                            try
                            {
                                m_rec_Regnskab =
                                    (from d in Program.dbData3060.TblRegnskab
                                     where d.Rid.ToString() == RegnskabId
                                     select d).First();

                            }
                            catch (System.InvalidOperationException)
                            {
                                m_rec_Regnskab = new TblRegnskab
                                {
                                    Rid = int.Parse(RegnskabId)
                                };
                                Program.dbData3060.TblRegnskab.InsertOnSubmit(m_rec_Regnskab);
                                Program.dbData3060.SubmitChanges();
                            }
                            RegnskabMappe = Datamappe + sub_dir.Name + @"\";
                            m_rec_Regnskab.Placering = RegnskabMappe;

                            m_rec_Regnskab.Eksportmappe = Eksportmappe + @"\";

                            if (m_rec_Regnskab.FraPBS == null)
                            {
                                DirectoryInfo infoEksportmappe = new DirectoryInfo(m_rec_Regnskab.Eksportmappe);
                                if (infoEksportmappe.Exists)
                                {
                                    m_rec_Regnskab.FraPBS = Eksportmappe + @"\FraPBS\";
                                    DirectoryInfo infoFraPBS = new DirectoryInfo(m_rec_Regnskab.FraPBS);
                                    if (!infoFraPBS.Exists)
                                    {
                                        infoFraPBS.Create();
                                    }
                                }
                            }

                            if (m_rec_Regnskab.TilPBS == null)
                            {
                                DirectoryInfo infoEksportmappe = new DirectoryInfo(m_rec_Regnskab.Eksportmappe);
                                if (infoEksportmappe.Exists)
                                {
                                    m_rec_Regnskab.TilPBS = Eksportmappe + @"\TilPBS\";
                                    DirectoryInfo infoTilPBS = new DirectoryInfo(m_rec_Regnskab.TilPBS);
                                    if (!infoTilPBS.Exists)
                                    {
                                        infoTilPBS.Create();
                                    }
                                }
                            }
                            string[] files = new string[2];
                            files[0] = RegnskabMappe + "regnskab.dat";
                            files[1] = RegnskabMappe + "status.dat";
                            m_rec_Regnskab.Afsluttet = false;
                            foreach (var file in files)
                            {
                                ts = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.None);
                                using (StreamReader sr = new StreamReader(ts, Encoding.Default))
                                {
                                    while ((line = sr.ReadLine()) != null)
                                    {
                                        if (line.Length > 0)
                                        {
                                            string[] X = line.Split('=');
                                            switch (X[0])
                                            {
                                                case "Navn":
                                                    m_rec_Regnskab.Navn = X[1];
                                                    break;
                                                case "Oprettet":
                                                    m_rec_Regnskab.Oprettet = clsUtil.MSSerial2DateTime(double.Parse(X[1]));
                                                    break;
                                                case "Start":
                                                    m_rec_Regnskab.Start = clsUtil.MSSerial2DateTime(double.Parse(X[1]));
                                                    break;
                                                case "Slut":
                                                    m_rec_Regnskab.Slut = clsUtil.MSSerial2DateTime(double.Parse(X[1]));
                                                    break;
                                                case "DatoLaas":
                                                    m_rec_Regnskab.DatoLaas = clsUtil.MSSerial2DateTime(double.Parse(X[1]));
                                                    break;
                                                case "Afsluttet":
                                                    m_rec_Regnskab.Afsluttet = (X[1] == "1") ? true : false;
                                                    break;
                                                case "Firmanavn":
                                                    m_rec_Regnskab.Firmanavn = X[1];
                                                    break;
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }
            }
            return true;
        }

        public class clsSysinfo
        {
            public string vkey = "";
            public string val = "";
        }

        public void ExecuteSQLScript(string ScriptFile)
        {
            string ln = null;
            string sqlCommand = "";
            int i = 0;
            FileStream ts = new FileStream(ScriptFile, FileMode.Open, FileAccess.Read, FileShare.None);
            using (StreamReader sr = new StreamReader(ts, Encoding.Default))
            {
                while ((ln = sr.ReadLine()) != null)
                {
                    if (!ln.StartsWith("GO"))
                    {
                        if (i++ != 0) sqlCommand += "\r\n";
                        sqlCommand += ln;
                    }
                    else
                    {
                        Program.dbData3060.ExecuteCommand(sqlCommand);
                        sqlCommand = "";
                        i = 0;
                    }
                }
            }
        }

        public bool DatabaseUpdate()
        {
            string dbVersion = "";
            try
            {
                dbVersion = (Program.dbData3060.ExecuteQuery<clsSysinfo>("SELECT * FROM [tblSysinfo] WHERE [vkey] = N'VERSION';")).First().val;
            }
            catch (System.Data.SqlServerCe.SqlCeException)
            {
                dbVersion = "2.0.0.0";
            }

            if (dbVersion == "2.0.0.0")
            {
                try
                {
                    //version "2.0.0.0" --> "2.1.0.0" opgradering af SqlDatabasen
                    //Tilføj et ny tabel [tblSysinfo]  til SqlDatabasen 
                    Program.dbData3060.ExecuteCommand("CREATE TABLE [tblSysinfo] ([vkey] nvarchar(10) NOT NULL, [val] nvarchar(100) NOT NULL);");
                    Program.dbData3060.ExecuteCommand("ALTER TABLE [tblSysinfo] ADD PRIMARY KEY ([vkey]);");
                    Program.dbData3060.ExecuteCommand("CREATE UNIQUE INDEX [UQ__tblSysinfo__000000000000068A] ON [tblSysinfo] ([vkey] ASC);");
                    Program.dbData3060.ExecuteCommand("INSERT INTO [tblSysinfo] ([vkey],[val]) VALUES (N'VERSION',N'2.1.0.0');");
                    dbVersion = "2.1.0.0";
                    //Tilføj et nyt felt [Afsluttet] til SqlDatabasen 
                    Program.dbData3060.ExecuteCommand("ALTER TABLE [tblRegnskab] ADD COLUMN [Afsluttet] bit NULL;");

                    //Fjern et felt [tildato] fra tempKontforslag i SqlDatabasen 
                    Program.dbData3060.ExecuteCommand("ALTER TABLE [tempKontforslag] DROP COLUMN [tildato];");
                    //Tilføj et nyt felt [tildato] til tempKontforslaglinie i SqlDatabasen 
                    Program.dbData3060.ExecuteCommand("ALTER TABLE [tempKontforslaglinie] ADD COLUMN [tildato] datetime NULL;");
                }
                catch (System.Data.SqlServerCe.SqlCeException e)
                {
                    object x = e;
                }
            }

            if (dbVersion == "2.1.0.0")
            {
                try
                {
                    //version "2.1.0.0" --> "2.2.0.0" opgradering af SqlDatabasen
                    //Tilføj et ny tabel [tblsftp]  til SqlDatabasen 
                    Program.dbData3060.ExecuteCommand("CREATE TABLE [tblsftp] ([id] int NOT NULL  IDENTITY (1,1), [navn] nvarchar(64) NOT NULL, [host] nvarchar(64) NOT NULL, [port] nvarchar(5) NOT NULL, [user] nvarchar(16) NOT NULL, [outbound] nvarchar(64) NULL, [inbound] nvarchar(64) NULL, [pincode] nvarchar(64) NULL, [certificate] nvarchar(4000) NULL);");
                    Program.dbData3060.ExecuteCommand("ALTER TABLE [tblsftp] ADD PRIMARY KEY ([id]);");
                    Program.dbData3060.ExecuteCommand("CREATE UNIQUE INDEX [UQ__tblsftp__00000000000006E4] ON [tblsftp] ([id] ASC);");

                    Program.dbData3060.ExecuteCommand("UPDATE [tblSysinfo] SET [val] = '2.2.0.0'  WHERE [vkey] = 'VERSION';");
                    dbVersion = "2.2.0.0";
                    Program.dbData3060.ExecuteCommand("INSERT INTO [tblsftp] ([navn],[host],[port],[user],[outbound],[inbound],[pincode]) VALUES (N'Produktion',N'194.239.133.111',N'10022',N'LOEBEKLU',N'/LOEBEKLU',N'/LOEBEKLU',N'1234');");
                    Program.dbData3060.ExecuteCommand("INSERT INTO [tblsftp] ([navn],[host],[port],[user],[outbound],[inbound],[pincode]) VALUES (N'Test',N'194.239.133.112',N'10022',N'TOEBEKLU',N'/TOEBEKLU',N'/TOEBEKLU',N'1234');");
                }
                catch (System.Data.SqlServerCe.SqlCeException e)
                {
                    object x = e;
                }
            }

            if (dbVersion == "2.2.0.0")
            {
                try
                {
                    //version "2.2.0.0" --> "2.3.0.0" opgradering af SqlDatabasen
                    //Tilføj et ny tabel [tbloverforsel]  til SqlDatabasen 
                    Program.dbData3060.ExecuteCommand("CREATE TABLE [tbloverforsel] ([id] int NOT NULL  IDENTITY (1,1), [tilpbsid] int NULL, [Nr] int NULL, [SFaknr] int NULL, [SFakID] int NULL, [advistekst] nvarchar(20) NULL, [advisbelob] numeric(18,2) NULL, [emailtekst] nvarchar(4000) NULL, [emailsent] bit NULL);");
                    Program.dbData3060.ExecuteCommand("ALTER TABLE [tbloverforsel] ADD PRIMARY KEY ([id]);");
                    Program.dbData3060.ExecuteCommand("ALTER TABLE [tbloverforsel] ADD CONSTRAINT [FK_tbltilpbs_tbloverfoersel] FOREIGN KEY ([tilpbsid]) REFERENCES [tbltilpbs]([id]) ON DELETE CASCADE ON UPDATE CASCADE;");
                    Program.dbData3060.ExecuteCommand("CREATE UNIQUE INDEX [UQ__tbloverforsel__00000000000006B2] ON [tbloverforsel] ([id] ASC);");

                    Program.dbData3060.ExecuteCommand("UPDATE [tblSysinfo] SET [val] = '2.3.0.0'  WHERE [vkey] = 'VERSION';");
                    dbVersion = "2.3.0.0";
                }
                catch (System.Data.SqlServerCe.SqlCeException e)
                {
                    object x = e;
                }
            }

            if (dbVersion == "2.3.0.0")
            {
                try
                {
                    //version "2.3.0.0" --> "2.4.0.0" opgradering af SqlDatabasen
                    //Tilføj et nyt felt bsh til tabel [tempKontforslag]  til SqlDatabasen 
                    Program.dbData3060.ExecuteCommand("ALTER TABLE [tempKontforslag] ADD COLUMN [bsh] bit NULL;");
                    Program.dbData3060.ExecuteCommand("UPDATE [tblSysinfo] SET [val] = '2.4.0.0'  WHERE [vkey] = 'VERSION';");
                    dbVersion = "2.4.0.0";
                }
                catch (System.Data.SqlServerCe.SqlCeException e)
                {
                    object x = e;
                }
            }

            if (dbVersion == "2.4.0.0")
            {
                try
                {
                    //version "2.4.0.0" --> "2.5.0.0" opgradering af SqlDatabasen
                    //Add 2 new tabels [tempBetalforslag] and [tempBetalforslaglinie] til SqlDatabasen 
                    Program.dbData3060.ExecuteCommand("CREATE TABLE [tempBetalforslag] (  [id] int NOT NULL  IDENTITY (1,1), [betalingsdato] datetime NOT NULL);");
                    Program.dbData3060.ExecuteCommand("ALTER TABLE [tempBetalforslag] ADD PRIMARY KEY ([id]);");

                    Program.dbData3060.ExecuteCommand("CREATE TABLE [tempBetalforslaglinie] (  [id] int NOT NULL  IDENTITY (1,1), [Nr] int NOT NULL, [Betalforslagid] int NOT NULL, [advisbelob] numeric(18,2) NOT NULL);");
                    Program.dbData3060.ExecuteCommand("ALTER TABLE [tempBetalforslaglinie] ADD PRIMARY KEY ([id]);");
                    Program.dbData3060.ExecuteCommand("ALTER TABLE [tempBetalforslaglinie] ADD CONSTRAINT [FK_tempBetalforslag_tempBetalforslaglinie] FOREIGN KEY ([Betalforslagid]) REFERENCES [tempBetalforslag]([id]) ON DELETE CASCADE ON UPDATE CASCADE;");

                    Program.dbData3060.ExecuteCommand("UPDATE [tblSysinfo] SET [val] = '2.5.0.0'  WHERE [vkey] = 'VERSION';");
                    dbVersion = "2.5.0.0";
                }
                catch (System.Data.SqlServerCe.SqlCeException e)
                {
                    object x = e;
                }
            }

            if (dbVersion == "2.5.0.0")
            {
                try
                {
                    //version "2.5.0.0" --> "2.6.0.0" opgradering af SqlDatabasen
                    //Add Mailinfo
                    Program.dbData3060.ExecuteCommand("INSERT INTO [tblSysinfo] ([vkey],[val]) VALUES (N'SMTPHOST',N'smtp.gmail.com');");
                    Program.dbData3060.ExecuteCommand("INSERT INTO [tblSysinfo] ([vkey],[val]) VALUES (N'SMTPPORT',N'465');");
                    Program.dbData3060.ExecuteCommand("INSERT INTO [tblSysinfo] ([vkey],[val]) VALUES (N'SMTPSSL',N'true');");
                    Program.dbData3060.ExecuteCommand("INSERT INTO [tblSysinfo] ([vkey],[val]) VALUES (N'SMTPUSER',N'regnskab.puls3060@gmail.com');");
                    Program.dbData3060.ExecuteCommand("INSERT INTO [tblSysinfo] ([vkey],[val]) VALUES (N'SMTPPASSWD',N'n4vWYkAKsfRFcuLW 58Tb0P0t04wmo6YbC5d1y5h3');");
                    Program.dbData3060.ExecuteCommand("INSERT INTO [tblSysinfo] ([vkey],[val]) VALUES (N'MAILTONAME',N'Regnskab Puls3060');");
                    Program.dbData3060.ExecuteCommand("INSERT INTO [tblSysinfo] ([vkey],[val]) VALUES (N'MAILTOADDR',N'regnskab.puls3060@gmail.com');");
                    Program.dbData3060.ExecuteCommand("INSERT INTO [tblSysinfo] ([vkey],[val]) VALUES (N'MAILFROM',N'Regnskab Puls3060 <regnskab@puls3060.dk>');");
                    Program.dbData3060.ExecuteCommand("INSERT INTO [tblSysinfo] ([vkey],[val]) VALUES (N'MAILREPLY',N'Regnskab Puls3060 <regnskab@puls3060.dk>');");

                    Program.dbData3060.ExecuteCommand("ALTER TABLE [tempBetalforslaglinie] ADD COLUMN [fakid] int NULL;");
                    Program.dbData3060.ExecuteCommand("ALTER TABLE [tempBetalforslaglinie] ADD COLUMN [bankregnr] nvarchar(4) NULL;");
                    Program.dbData3060.ExecuteCommand("ALTER TABLE [tempBetalforslaglinie] ADD COLUMN [bankkontonr] nvarchar(10) NULL;");

                    Program.dbData3060.ExecuteCommand("ALTER TABLE [tbloverforsel] ADD COLUMN [bankregnr] nvarchar(4) NULL;");
                    Program.dbData3060.ExecuteCommand("ALTER TABLE [tbloverforsel] ADD COLUMN [bankkontonr] nvarchar(10) NULL;");

                    Program.dbData3060.ExecuteCommand("UPDATE [tblSysinfo] SET [val] = '2.6.0.0'  WHERE [vkey] = 'VERSION';");
                    dbVersion = "2.6.0.0";
                }
                catch (System.Data.SqlServerCe.SqlCeException e)
                {
                    object x = e;
                }

            }

            if (dbVersion == "2.6.0.0")
            {
                try
                {
                    //version "2.6.0.0" --> "2.7.0.0" opgradering af SqlDatabasen

                    //Program.dbData3060.ExecuteCommand("CREATE TABLE [tempBetalforslaglinie] (  [id] int NOT NULL  IDENTITY (1,1), [Nr] int NOT NULL, [Betalforslagid] int NOT NULL, [advisbelob] numeric(18,2) NOT NULL);");
                    //Program.dbData3060.ExecuteCommand("ALTER TABLE [tempBetalforslaglinie] ADD PRIMARY KEY ([id]);");
                    //Program.dbData3060.ExecuteCommand("ALTER TABLE [tempBetalforslaglinie] ADD CONSTRAINT [FK_tempBetalforslag_tempBetalforslaglinie] FOREIGN KEY ([Betalforslagid]) REFERENCES [tempBetalforslag]([id]) ON DELETE CASCADE ON UPDATE CASCADE;");

                    Program.dbData3060.ExecuteCommand("UPDATE [tblSysinfo] SET [val] = '2.7.0.0'  WHERE [vkey] = 'VERSION';");
                    dbVersion = "2.7.0.0";
                }
                catch (System.Data.SqlServerCe.SqlCeException e)
                {
                    object x = e;
                }
            }

            if (dbVersion == "2.7.0.0")
            {
                try
                {
                    //version "2.7.0.0" --> "2.8.0.0" opgradering af SqlDatabasen

                    Program.dbData3060.ExecuteCommand("CREATE TABLE [tblaftalelin] (  [id] int NOT NULL  IDENTITY (1,1), [frapbsid] int NOT NULL, [pbstranskode] nvarchar(4) NOT NULL, [Nr] int NOT NULL, [debitorkonto] nvarchar(15) NOT NULL, [debgrpnr] nvarchar(5) NOT NULL, [aftalenr] int NULL, [aftalestartdato] datetime NULL, [aftaleslutdato] datetime NULL, [pbssektionnr] nvarchar(5) NOT NULL);");
                    Program.dbData3060.ExecuteCommand("ALTER TABLE [tblaftalelin] ADD PRIMARY KEY ([id]);");
                    Program.dbData3060.ExecuteCommand("ALTER TABLE [tblaftalelin] ADD CONSTRAINT [FK_tblfrapbs_tblaftalelin] FOREIGN KEY ([frapbsid]) REFERENCES [tblfrapbs]([id]) ON DELETE CASCADE ON UPDATE CASCADE;");

                    Program.dbData3060.ExecuteCommand("DELETE FROM [tblpbsfile] WHERE data = '';");

                    Program.dbData3060.ExecuteCommand("UPDATE [tblSysinfo] SET [val] = '2.8.0.0'  WHERE [vkey] = 'VERSION';");
                    dbVersion = "2.8.0.0";
                }
                catch (System.Data.SqlServerCe.SqlCeException e)
                {
                    object x = e;
                }
            }

            if (dbVersion == "2.8.0.0")
            {
                try
                {
                    //version "2.8.0.0" --> "2.9.0.0" opgradering af SqlDatabasen

                    Program.dbData3060.ExecuteCommand("CREATE TABLE [tblpbsnetdir] (  [id] int NOT NULL  IDENTITY (1,1), [type] int NULL, [path] nvarchar(255) NULL, [filename] nvarchar(50) NULL, [size] int NULL, [atime] datetime NULL, [mtime] datetime NULL, [perm] nvarchar(50) NULL, [uid] int NULL, [gid] int NULL);");
                    Program.dbData3060.ExecuteCommand("ALTER TABLE [tblpbsnetdir] ADD PRIMARY KEY ([id]);");
                    Program.dbData3060.ExecuteCommand("CREATE UNIQUE INDEX [UQ__tblpbsnetdir__0000000000000433] ON [tblpbsnetdir] ([id] ASC);");

                    Program.dbData3060.ExecuteCommand("UPDATE [tblSysinfo] SET [val] = '2.9.0.0'  WHERE [vkey] = 'VERSION';");
                    dbVersion = "2.9.0.0";
                }
                catch (System.Data.SqlServerCe.SqlCeException e)
                {
                    object x = e;
                }
            }

            if (dbVersion == "2.9.0.0")
            {
                try
                {
                    //version "2.9.0.0" --> "2.10.0.0" opgradering af SqlDatabasen

                    Program.dbData3060.ExecuteCommand("CREATE TABLE [tblindbetalingskort] ([id] int NOT NULL  IDENTITY (1,1), [frapbsid] int NOT NULL, [pbstranskode] nvarchar(4) NOT NULL, [Nr] int NOT NULL, [faknr] int NULL, [debitorkonto] nvarchar(15) NOT NULL, [debgrpnr] nvarchar(5) NOT NULL, [kortartkode] nvarchar(2) NOT NULL, [fikreditornr] nvarchar(8) NOT NULL, [indbetalerident] nvarchar(19) NOT NULL, [dato] datetime NULL, [belob] numeric(18,2) NULL, [pbssektionnr] nvarchar(5) NOT NULL);");
                    Program.dbData3060.ExecuteCommand("ALTER TABLE [tblindbetalingskort] ADD PRIMARY KEY ([id]);");
                    Program.dbData3060.ExecuteCommand("ALTER TABLE [tblindbetalingskort] ADD CONSTRAINT [FK_tblfrapbs_tblindbetalingskort] FOREIGN KEY ([frapbsid]) REFERENCES [tblfrapbs]([id]) ON DELETE CASCADE ON UPDATE CASCADE;");

                    Program.dbData3060.ExecuteCommand("UPDATE [tblSysinfo] SET [val] = '2.10.0.0'  WHERE [vkey] = 'VERSION';");
                    dbVersion = "2.10.0.0";
                }
                catch (System.Data.SqlServerCe.SqlCeException e)
                {
                    object x = e;
                }
            }

            if (dbVersion == "2.10.0.0")
            {
                try
                {
                    //version "2.10.0.0" --> "2.11.0.0" opgradering af SqlDatabasen
                    Program.dbData3060.ExecuteCommand("CREATE TABLE [tempRykkerforslag] ([id] int NOT NULL  IDENTITY (1,1), [betalingsdato] datetime NOT NULL, [bsh] bit NULL);");
                    Program.dbData3060.ExecuteCommand("ALTER TABLE [tempRykkerforslag] ADD PRIMARY KEY ([id]);");
                    Program.dbData3060.ExecuteCommand("CREATE UNIQUE INDEX [UQ__tempRykkerforslag__000000000000055D] ON [tempRykkerforslag] ([id] ASC);");
                    Program.dbData3060.ExecuteCommand("CREATE TABLE [tempRykkerforslaglinie] ( [id] int NOT NULL  IDENTITY (1,1), [Rykkerforslagid] int NOT NULL, [Nr] int NOT NULL, [faknr] int NOT NULL, [advistekst] nvarchar(4000) NULL, [advisbelob] numeric(18,2) NOT NULL);");
                    Program.dbData3060.ExecuteCommand("ALTER TABLE [tempRykkerforslaglinie] ADD PRIMARY KEY ([id]);");
                    Program.dbData3060.ExecuteCommand("ALTER TABLE [tempRykkerforslaglinie] ADD CONSTRAINT [FK_tempRykkerforslag_tempRykkerforslaglinie] FOREIGN KEY ([Rykkerforslagid]) REFERENCES [tempRykkerforslag]([id]) ON DELETE CASCADE ON UPDATE CASCADE;");
                    Program.dbData3060.ExecuteCommand("CREATE TABLE [tblrykker] ( [id] int NOT NULL  IDENTITY (30641,1), [tilpbsid] int NULL, [betalingsdato] datetime NULL, [Nr] int NULL, [faknr] int NULL, [advistekst] nvarchar(4000) NULL, [advisbelob] numeric(18,2) NULL, [infotekst] int NULL, [rykkerdato] datetime NULL, [maildato] datetime NULL);");
                    Program.dbData3060.ExecuteCommand("ALTER TABLE [tblrykker] ADD PRIMARY KEY ([id]);");
                    Program.dbData3060.ExecuteCommand("ALTER TABLE [tblrykker] ADD CONSTRAINT [FK_tbltilpbs_tblrykker] FOREIGN KEY ([tilpbsid]) REFERENCES [tbltilpbs]([id]) ON DELETE CASCADE ON UPDATE CASCADE;");
                    Program.dbData3060.ExecuteCommand("CREATE INDEX [tblmedlem_tblrykker] ON [tblrykker] ([Nr] ASC,[faknr] ASC);");
                    Program.dbData3060.ExecuteCommand("CREATE UNIQUE INDEX [UQ__tblrykker__00000000000001E7] ON [tblrykker] ([id] ASC);");

                    Program.dbData3060.ExecuteCommand("UPDATE [tblSysinfo] SET [val] = '2.11.0.0'  WHERE [vkey] = 'VERSION';");
                    dbVersion = "2.11.0.0";
                }
                catch (System.Data.SqlServerCe.SqlCeException e)
                {
                    object x = e;
                }
            }

            if (dbVersion == "2.11.0.0")
            {
                try
                {
                    //version "2.11.0.0" --> "2.12.0.0" opgradering af SqlDatabasen
                    Program.dbData3060.ExecuteCommand("CREATE TABLE [tblinfotekst] ([id] int NOT NULL, [navn] nvarchar(50) NULL, [msgtext] nvarchar(4000) NULL);");
                    Program.dbData3060.ExecuteCommand("ALTER TABLE [tblinfotekst] ADD PRIMARY KEY ([id]);");
                    Program.dbData3060.ExecuteCommand("CREATE UNIQUE INDEX [UQ__tblinfotekst__0000000000000230] ON [tblinfotekst] ([id] ASC);");

                    Program.dbData3060.ExecuteCommand("UPDATE [tblSysinfo] SET [val] = '2.12.0.0'  WHERE [vkey] = 'VERSION';");
                    dbVersion = "2.12.0.0";
                }
                catch (System.Data.SqlServerCe.SqlCeException e)
                {
                    object x = e;
                }
            }

            if (dbVersion == "2.12.0.0")
            {
                using (var dbts = new TransactionScope())
                {
                    try
                    {
                        //version "2.12.0.0" --> "2.13.0.0" opgradering af SqlDatabasen
                        ExecuteSQLScript(@"sql\script13.sql");
                        Program.dbData3060.ExecuteCommand("UPDATE [tblSysinfo] SET [val] = '2.13.0.0'  WHERE [vkey] = 'VERSION';");
                        dbVersion = "2.13.0.0";
                        dbts.Complete();
                    }

                    catch (System.Data.SqlServerCe.SqlCeException e)
                    {
                        dbts.Dispose();
                        object x = e;
                    }
                }
            }

            if (dbVersion == "2.13.0.0")
            {
                using (var dbts = new TransactionScope())
                {
                    try
                    {
                        //version "2.13.0.0" --> "2.14.0.0" opgradering af SqlDatabasen
                        ExecuteSQLScript(@"sql\script14.sql");
                        Program.dbData3060.ExecuteCommand("UPDATE [tblSysinfo] SET [val] = '2.14.0.0'  WHERE [vkey] = 'VERSION';");
                        dbVersion = "2.14.0.0";
                        dbts.Complete();
                    }

                    catch (System.Data.SqlServerCe.SqlCeException e)
                    {
                        dbts.Dispose();
                        object x = e;
                    }
                }
            }
            return true;
        }

    }
}
