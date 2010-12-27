using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;

namespace nsPuls3060
{
    public class clsLog2
    {
        public byte Source;
        public int? Id;
        public int? Nr;
        public DateTime? Logdato;
        public int? Akt_id;
        public DateTime? Akt_dato;
    }

    public class clsImEksportAppEngMedlem
    {
        public clsImEksportAppEngMedlem()
        {
            bNr = false;
            bNavn = false;
            bKaldenavn = false;
            bAdresse = false;
            bPostnr = false;
            bBynavn = false;
            bEmail = false;
            bTelefon = false;
            bKon = false;
            bFodtDato = false;
            bBank = false;
        }

        public ImpExp ieAction { get; set; }
        public string Act { get; set; }
        public bool bNr { get; set; }
        public bool bNavn { get; set; }
        public bool bKaldenavn { get; set; }
        public bool bAdresse { get; set; }
        public bool bPostnr { get; set; }
        public bool bBynavn { get; set; }
        public bool bEmail { get; set; }
        public bool bTelefon { get; set; }
        public bool bKon { get; set; }
        public bool bFodtDato { get; set; }
        public bool bBank { get; set; }
        public int? Nr { get; set; }
        public string Navn { get; set; }
        public string Kaldenavn { get; set; }
        public string Adresse { get; set; }
        public string Postnr { get; set; }
        public string Bynavn { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Kon { get; set; }
        public DateTime? FodtDato { get; set; }
        public string Bank { get; set; }
        public void ExecuteImEksport()
        {
            if (ieAction == ImpExp.fdImport) Import();
            else Eksport();
        }
        private void Eksport()
        {
            clsRest objRest = new clsRest();
            if (Act == "del")
            {
                string retur = objRest.HttpDelete2("Medlem/" + Nr);
            }
            else
            {
                XElement xml = new XElement("Medlem", new XElement("Nr", Nr));
                if (bNavn) xml.Add(new XElement("Navn", Navn));
                if (bKaldenavn) xml.Add(new XElement("Kaldenavn", Kaldenavn));
                if (bAdresse) xml.Add(new XElement("Adresse", Adresse));
                if (bPostnr) xml.Add(new XElement("Postnr", Postnr));
                if (bBynavn) xml.Add(new XElement("Bynavn", Bynavn));
                if (bTelefon) xml.Add(new XElement("Telefon", Telefon));
                if (bEmail) xml.Add(new XElement("Email", Email));
                if (bKon) xml.Add(new XElement("Kon", Kon));
                if (bFodtDato) xml.Add(new XElement("FodtDato", ((DateTime)FodtDato).ToString("yyyy-MM-dd")));
                if (bBank) xml.Add(new XElement("Bank", Bank));
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2("Medlem", strxml);
            }

        }
        private void Import()
        {
            object[] val = null;
            try
            {
                DataRow row = Program.dsMedlemImport.Kartotek.Rows.Find(Nr);
                val = row.ItemArray;
                if (bNavn) val[1] = Navn;
                if (bKaldenavn) val[2] = Kaldenavn;
                if (bAdresse) val[3] = Adresse;
                if (bPostnr) val[4] = Postnr;
                if (bBynavn) val[5] = Bynavn;
                if (bTelefon) val[6] = Telefon;
                if (bEmail) val[7] = Email;
                if (bKon) val[8] = Kon;
                if (bFodtDato) val[9] = FodtDato;
                if (bBank) val[10] = Bank;

                row.BeginEdit();
                row.ItemArray = val;
                row.EndEdit();
            }
            catch (MissingPrimaryKeyException e)
            {
                e.GetType();
                val = new object[11];
                val[0] = Nr;
                if (bNavn) val[1] = Navn;
                if (bKaldenavn) val[2] = Kaldenavn;
                if (bAdresse) val[3] = Adresse;
                if (bPostnr) val[4] = Postnr;
                if (bBynavn) val[5] = Bynavn;
                if (bTelefon) val[6] = Telefon;
                if (bEmail) val[7] = Email;
                if (bKon) val[8] = Kon;
                if (bFodtDato) val[9] = FodtDato;
                if (bBank) val[10] = Bank;
                Program.dsMedlemImport.Kartotek.Rows.Add(val);
            }
            Program.dsMedlemImport.savedsMedlem();
        }
    }

    public class clsImEksportAppEngMedlemlog
    {
        public clsImEksportAppEngMedlemlog()
        {
            bId = false;
            bNr = false;
            bLogdato = false;
            bAkt_id = false;
            bAkt_dato = false;
        }
        public ImpExp ieAction { get; set; }
        public string Act { get; set; }
        public bool bSource { get; set; }
        public bool bId { get; set; }
        public bool bNr { get; set; }
        public bool bLogdato { get; set; }
        public bool bAkt_id { get; set; }
        public bool bAkt_dato { get; set; }
        public byte? Source { get; set; }
        public int? Id { get; set; }
        public int? Nr { get; set; }
        public DateTime? Logdato { get; set; }
        public int? Akt_id { get; set; }
        public DateTime? Akt_dato { get; set; }
        public void ExecuteImEksport()
        {
            if (ieAction == ImpExp.fdImport) Import();
            else Eksport();
        }
        private void Eksport()
        {
            clsRest objRest = new clsRest();
            if (Act == "del")
            {
                string retur = objRest.HttpDelete2("Medlemlog/" + Nr + "/" + Source + "/" + Id);
            }
            else
            {
                XElement xml = new XElement("Medlemlog", new XElement("Source", Source), new XElement("Source_id", Id), new XElement("Nr", Nr));
                if (bLogdato) xml.Add(new XElement("Logdato", ((DateTime)Logdato).ToString("yyyy-MM-ddTHH:mm:ss")));
                if (bAkt_id) xml.Add(new XElement("Akt_id", Akt_id));
                if (bAkt_dato) xml.Add(new XElement("Akt_dato", ((DateTime)Akt_dato).ToString("yyyy-MM-ddTHH:mm:ss")));
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2("Medlemlog", strxml);
            }
        }
        private void Import()
        {
            if (Source == 2)
            {
                TblMedlemLog recMedlemLog = null;
                try
                {
                    recMedlemLog = (from m in Program.dbData3060.TblMedlemLog where m.Id == Id select m).First();
                    if (bLogdato) recMedlemLog.Logdato = Logdato;
                    if (bAkt_id) recMedlemLog.Akt_id = Akt_id;
                    if (bAkt_dato) recMedlemLog.Akt_dato = Akt_dato;
                }
                catch
                {
                    recMedlemLog = new TblMedlemLog { Id = (int)Id, Nr = Nr };
                    if (bLogdato) recMedlemLog.Logdato = Logdato;
                    if (bAkt_id) recMedlemLog.Akt_id = Akt_id;
                    if (bAkt_dato) recMedlemLog.Akt_dato = Akt_dato;
                    Program.dbData3060.TblMedlemLog.InsertOnSubmit(recMedlemLog);
                }
                Program.dbData3060.SubmitChanges();
            }
        }
    }

    public class clsImEksportAppEngKreditor
    {
        public clsImEksportAppEngKreditor()
        {
            bId = false;
            bDatalevnr = false;
            bDatalevnavn = false;
            bPbsnr = false;
            bDelsystem = false;
            bRegnr = false;
            bKontonr = false;
            bDebgrpnr = false;
            bSektionnr = false;
            bTranskodebetaling = false;
        }

        public ImpExp ieAction { get; set; }
        public string Act { get; set; }
        public bool bId { get; set; }
        public bool bDatalevnr { get; set; }
        public bool bDatalevnavn { get; set; }
        public bool bPbsnr { get; set; }
        public bool bDelsystem { get; set; }
        public bool bRegnr { get; set; }
        public bool bKontonr { get; set; }
        public bool bDebgrpnr { get; set; }
        public bool bSektionnr { get; set; }
        public bool bTranskodebetaling { get; set; }
        public int? Id { get; set; }
        public string Datalevnr { get; set; }
        public string Datalevnavn { get; set; }
        public string Pbsnr { get; set; }
        public string Delsystem { get; set; }
        public string Regnr { get; set; }
        public string Kontonr { get; set; }
        public string Debgrpnr { get; set; }
        public string Sektionnr { get; set; }
        public string Transkodebetaling { get; set; }
        public void ExecuteImEksport()
        {
            if (ieAction == ImpExp.fdImport) Import();
            else Eksport();
        }
        private void Eksport()
        {
            clsRest objRest = new clsRest();
            if (Act == "del")
            {
                string retur = objRest.HttpDelete2("Kreditor/" + Id);
            }
            else
            {
                XElement xml = new XElement("Kreditor", new XElement("Id", Id));
                if (bDatalevnr) xml.Add(new XElement("Datalevnr", Datalevnr));
                if (bDatalevnavn) xml.Add(new XElement("Datalevnavn", Datalevnavn));
                if (bPbsnr) xml.Add(new XElement("Pbsnr", Pbsnr));
                if (bDelsystem) xml.Add(new XElement("Delsystem", Delsystem));
                if (bRegnr) xml.Add(new XElement("Regnr", Regnr));
                if (bDebgrpnr) xml.Add(new XElement("Debgrpnr", Debgrpnr));
                if (bKontonr) xml.Add(new XElement("Kontonr", Kontonr));
                if (bSektionnr) xml.Add(new XElement("Sektionnr", Sektionnr));
                if (bTranskodebetaling) xml.Add(new XElement("Transkodebetaling", Transkodebetaling));
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2("Kreditor", strxml);
            }

        }
        private void Import()
        {
            Tblkreditor recKreditor = null;
            try
            {
                recKreditor = (from k in Program.dbData3060.Tblkreditor where k.Id == Id select k).First();
                if (bDatalevnr) recKreditor.Datalevnr = Datalevnr;
                if (bDatalevnavn) recKreditor.Datalevnavn = Datalevnavn;
                if (bPbsnr) recKreditor.Pbsnr = Pbsnr;
                if (bDelsystem) recKreditor.Delsystem = Delsystem;
                if (bRegnr) recKreditor.Regnr = Regnr;
                if (bDebgrpnr) recKreditor.Debgrpnr = Debgrpnr;
                if (bKontonr) recKreditor.Kontonr = Kontonr;
                if (bSektionnr) recKreditor.Sektionnr = Sektionnr;
                if (bTranskodebetaling) recKreditor.Transkodebetaling = Transkodebetaling;
            }
            catch
            {
                recKreditor = new Tblkreditor { Id = (int)Id };
                if (bDatalevnr) recKreditor.Datalevnr = Datalevnr;
                if (bDatalevnavn) recKreditor.Datalevnavn = Datalevnavn;
                if (bPbsnr) recKreditor.Pbsnr = Pbsnr;
                if (bDelsystem) recKreditor.Delsystem = Delsystem;
                if (bRegnr) recKreditor.Regnr = Regnr;
                if (bDebgrpnr) recKreditor.Debgrpnr = Debgrpnr;
                if (bKontonr) recKreditor.Kontonr = Kontonr;
                if (bSektionnr) recKreditor.Sektionnr = Sektionnr;
                if (bTranskodebetaling) recKreditor.Transkodebetaling = Transkodebetaling;
                Program.dbData3060.Tblkreditor.InsertOnSubmit(recKreditor);
            }
            Program.dbData3060.SubmitChanges();
        }
    }

    public class clsImEksportAppEngSftp
    {
        public clsImEksportAppEngSftp()
        {
            bId = false;
            bNavn = false;
            bHost = false;
            bPort = false;
            bUser = false;
            bOutbound = false;
            bInbound = false;
            bPincode = false;
            bCertificate = false;
        }

        public ImpExp ieAction { get; set; }
        public string Act { get; set; }
        public bool bId { get; set; }
        public bool bNavn { get; set; }
        public bool bHost { get; set; }
        public bool bPort { get; set; }
        public bool bUser { get; set; }
        public bool bOutbound { get; set; }
        public bool bInbound { get; set; }
        public bool bPincode { get; set; }
        public bool bCertificate { get; set; }
        public int? Id { get; set; }
        public string Navn { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string User { get; set; }
        public string Outbound { get; set; }
        public string Inbound { get; set; }
        public string Pincode { get; set; }
        public string Certificate { get; set; }
        public void ExecuteImEksport()
        {
            if (ieAction == ImpExp.fdImport) Import();
            else Eksport();
        }
        private void Eksport()
        {
            clsRest objRest = new clsRest();
            if (Act == "del")
            {
                string retur = objRest.HttpDelete2("Sftp/" + Id);
            }
            else
            {
                XElement xml = new XElement("Sftp", new XElement("Id", Id));
                if (bNavn) xml.Add(new XElement("Navn", Navn));
                if (bHost) xml.Add(new XElement("Host", Host));
                if (bPort) xml.Add(new XElement("Port", Port));
                if (bUser) xml.Add(new XElement("User", User));
                if (bOutbound) xml.Add(new XElement("Outbound", Outbound));
                if (bInbound) xml.Add(new XElement("Inbound", Inbound));
                if (bPincode) xml.Add(new XElement("Pincode", Pincode));
                if (bCertificate) xml.Add(new XElement("Certificate", Certificate));
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2("Sftp", strxml);
            }

        }
        private void Import()
        {
            Tblsftp recSftp = null;
            try
            {
                recSftp = (from k in Program.dbData3060.Tblsftp where k.Id == Id select k).First();
                if (bNavn) recSftp.Navn = Navn;
                if (bHost) recSftp.Host = Host;
                if (bPort) recSftp.Port = Port;
                if (bUser) recSftp.User = User;
                if (bOutbound) recSftp.Outbound = Outbound;
                if (bInbound) recSftp.Inbound = Inbound;
                if (bPincode) recSftp.Pincode = Pincode;
                if (bCertificate) recSftp.Certificate = Certificate;
            }
            catch
            {
                recSftp = new Tblsftp { Id = (int)Id };
                if (bNavn) recSftp.Navn = Navn;
                if (bHost) recSftp.Host = Host;
                if (bPort) recSftp.Port = Port;
                if (bUser) recSftp.User = User;
                if (bOutbound) recSftp.Outbound = Outbound;
                if (bInbound) recSftp.Inbound = Inbound;
                if (bPincode) recSftp.Pincode = Pincode;
                if (bCertificate) recSftp.Certificate = Certificate; 
                Program.dbData3060.Tblsftp.InsertOnSubmit(recSftp);
            }
            Program.dbData3060.SubmitChanges();
        }
    }

    public class clsImEksportAppEngInfotekst
    {
        public clsImEksportAppEngInfotekst()
        {
            bId = false;
            bNavn = false;
            bMsgtext = false;
        }

        public ImpExp ieAction { get; set; }
        public string Act { get; set; }
        public bool bId { get; set; }
        public bool bNavn { get; set; }
        public bool bMsgtext { get; set; }
        public int? Id { get; set; }
        public string Navn { get; set; }
        public string Msgtext { get; set; }
        public void ExecuteImEksport()
        {
            if (ieAction == ImpExp.fdImport) Import();
            else Eksport();
        }
        private void Eksport()
        {
            clsRest objRest = new clsRest();
            if (Act == "del")
            {
                string retur = objRest.HttpDelete2("Infotekst/" + Id);
            }
            else
            {
                XElement xml = new XElement("Infotekst", new XElement("Id", Id));
                if (bNavn) xml.Add(new XElement("Navn", Navn));
                if (bMsgtext) xml.Add(new XElement("Msgtext", Msgtext));
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2("Infotekst", strxml);
            }

        }
        private void Import()
        {
            Tblinfotekst recInfotekst = null;
            try
            {
                recInfotekst = (from k in Program.dbData3060.Tblinfotekst where k.Id == Id select k).First();
                if (bNavn) recInfotekst.Navn = Navn;
                if (bMsgtext) recInfotekst.Msgtext = Msgtext;
            }
            catch
            {
                recInfotekst = new Tblinfotekst { Id = (int)Id };
                if (bNavn) recInfotekst.Navn = Navn;
                if (bMsgtext) recInfotekst.Msgtext = Msgtext;
                Program.dbData3060.Tblinfotekst.InsertOnSubmit(recInfotekst);
            }
            Program.dbData3060.SubmitChanges();
        }
    }

    public class clsImEksportAppEngSysinfo
    {
        public clsImEksportAppEngSysinfo()
        {
            bId = false;
            bVkey = false;
            bVal = false;
        }

        public ImpExp ieAction { get; set; }
        public string Act { get; set; }
        public bool bId { get; set; }
        public bool bVkey { get; set; }
        public bool bVal { get; set; }
        public int? Id { get; set; }
        public string Vkey { get; set; }
        public string Val { get; set; }
        public void ExecuteImEksport()
        {
            if (ieAction == ImpExp.fdImport) Import();
            else Eksport();
        }
        private void Eksport()
        {
            clsRest objRest = new clsRest();
            if (Act == "del")
            {
                string retur = objRest.HttpDelete2("Sysinfo/" + Id);
            }
            else
            {
                XElement xml = new XElement("Sysinfo", new XElement("Id", Id));
                if (bVkey) xml.Add(new XElement("Vkey", Vkey));
                if (bVal) xml.Add(new XElement("Val", Val));
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2("Sysinfo", strxml);
            }

        }
        private void Import()
        {
            TblSysinfo recSysinfo = null;
            try
            {
                recSysinfo = (from k in Program.dbData3060.TblSysinfo where k.Id == Id select k).First();
                if (bVkey) recSysinfo.Vkey = Vkey;
                if (bVal) recSysinfo.Val = Val;
            }
            catch
            {
                recSysinfo = new TblSysinfo { Id = (int)Id };
                if (bVkey) recSysinfo.Vkey = Vkey;
                if (bVal) recSysinfo.Val = Val;
                Program.dbData3060.TblSysinfo.InsertOnSubmit(recSysinfo);
            }
            Program.dbData3060.SubmitChanges();
        }
    }

    public enum ImpExp : int
    {
        fdImport = 1,
        fdEksport = 2
    }

    public class clsSync
    {
        private int m_action;
        public void actionSync(int action)
        {
            m_action = action;
            if (m_action == 1)
            {
                Program.dbData3060.Tblsync.DeleteAllOnSubmit(Program.dbData3060.Tblsync);
            }
            if (m_action == 2)
            {
                Program.dbData3060.Tempsync.DeleteAllOnSubmit(Program.dbData3060.Tempsync);
            }
            if (m_action == 3)
            {
                Program.dbData3060.Tempsync2.DeleteAllOnSubmit(Program.dbData3060.Tempsync2);
            }

            Program.dbData3060.SubmitChanges();
            /*
            try
            {
                Program.dbData3060.SubmitChanges(ConflictMode.ContinueOnConflict);
            }

            catch (ChangeConflictException e)
            {
                // Automerge database values for members that client has not modified.
                foreach (ObjectChangeConflict occ in Program.dbData3060.ChangeConflicts)
                {
                    occ.Resolve(RefreshMode.KeepChanges);
                }
            }
            // Submit succeeds on second try.
            Program.dbData3060.SubmitChanges(ConflictMode.FailOnFirstConflict);
            */

            if ((m_action == 1) | (m_action == 2))
            {
                actionMedlemSync();
                actionMedlemlogSync();
                actionKreditorSync();
                actionSftpSync();
                actionInfotekstSync();
                actionSysinfoSync();
            }
            if (m_action == 3)
            {
                actionMedlemxmlSync();
                actionMedlemlogxmlSync();
                actionKreditorxmlSync();
                actionSftpxmlSync();
                actionInfotekstxmlSync();
                actionSysinfoxmlSync();
            }
        }

        private void actionMedlemSync()
        {
            var medlem = from m1 in Program.karMedlemmer
                         join m2 in Program.dbData3060.TblMedlem on m1.Nr equals m2.Nr into medlem2
                         from m2 in medlem2.DefaultIfEmpty()
                         select new
                         {
                             Nr = m1.Nr,
                             Navn = m1.Navn,
                             Kaldenavn = m1.Kaldenavn,
                             Adresse = m1.Adresse,
                             Postnr = m1.Postnr,
                             Bynavn = m1.Bynavn,
                             Telefon = m1.Telefon,
                             Email = m1.Email,
                             Kon = m2.Kon,
                             FodtDato = m2.FodtDato,
                             Bank = m1.Bank,
                         };
            Tblsync s;
            foreach (var m in medlem)
            {
                //Nr
                s = new Tblsync
                {
                    Nr = (int)m.Nr,
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.medlem_nr,
                    Value = getString(m.Nr),
                };
                action(s);

                //Navn
                s = new Tblsync
                {
                    Nr = (int)m.Nr,
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.navn,
                    Value = getString(m.Navn)
                };
                action(s);

                //Kaldenavn
                s = new Tblsync
                {
                    Nr = (int)m.Nr,
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.kaldenavn,
                    Value = getString(m.Kaldenavn)
                };
                action(s);

                //Adresse
                s = new Tblsync
                {
                    Nr = (int)m.Nr,
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.adresse,
                    Value = getString(m.Adresse)
                };
                action(s);

                //Postnr
                s = new Tblsync
                {
                    Nr = (int)m.Nr,
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.postnr,
                    Value = getString(m.Postnr)
                };
                action(s);

                //Bynavn
                s = new Tblsync
                {
                    Nr = (int)m.Nr,
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.bynavn,
                    Value = getString(m.Bynavn)
                };
                action(s);

                //Telefon
                s = new Tblsync
                {
                    Nr = (int)m.Nr,
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.telefon,
                    Value = getString(m.Telefon)
                };
                action(s);

                //Email
                s = new Tblsync
                {
                    Nr = (int)m.Nr,
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.email,
                    Value = getString(m.Email)
                };
                action(s);

                //Kon
                s = new Tblsync
                {
                    Nr = (int)m.Nr,
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.kon,
                    Value = getString(m.Kon)
                };
                action(s);

                //FodtDato
                s = new Tblsync
                {
                    Nr = (int)m.Nr,
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.fodtdato,
                    Value = getString(m.FodtDato, "yyyy-MM-dd")
                };
                action(s);

                //Bank
                s = new Tblsync
                {
                    Nr = (int)m.Nr,
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.bank,
                    Value = getString(m.Bank)
                };
                action(s);
            }
            Program.dbData3060.SubmitChanges();
        }

        private void actionMedlemlogSync()
        {
            var qryMedlemLog = from m in Program.dbData3060.TblMedlemLog
                               select new clsLog2
                               {
                                   Source = (byte)tblsource.medlemlog,
                                   Id = (int?)m.Id,
                                   Nr = (int?)m.Nr,
                                   Logdato = (DateTime?)m.Logdato,
                                   Akt_id = (int?)m.Akt_id,
                                   Akt_dato = (DateTime?)m.Akt_dato
                               };
            var qryFak = from f in Program.dbData3060.Tblfak
                         join p in Program.dbData3060.Tbltilpbs on f.Tilpbsid equals p.Id
                         select new clsLog2
                         {
                             Source = (byte)tblsource.fak,
                             Id = (int?)f.Id,
                             Nr = (int?)f.Nr,
                             Logdato = (DateTime)p.Bilagdato,
                             Akt_id = (int?)20,
                             Akt_dato = (DateTime?)f.Betalingsdato
                         };

            var qryBetlin = from b in Program.dbData3060.Tblbetlin
                            join f in Program.dbData3060.Tblfak on b.Faknr equals f.Faknr
                            where b.Pbstranskode == "0236" || b.Pbstranskode == "0297"
                            select new clsLog2
                            {
                                Source = (byte)tblsource.betlin,
                                Id = (int?)b.Id,
                                Nr = (int?)b.Nr,
                                Logdato = (DateTime?)b.Indbetalingsdato,
                                Akt_id = (int?)30,
                                Akt_dato = (DateTime?)f.Tildato
                            };

            var qryBetlin40 = from b in Program.dbData3060.Tblbetlin
                              where b.Pbstranskode == "0237"
                              select new clsLog2
                              {
                                  Source = (byte)tblsource.betlin40,
                                  Id = (int?)b.Id,
                                  Nr = (int?)b.Nr,
                                  Logdato = (DateTime?)(((DateTime)b.Betalingsdato).AddSeconds(-30)),  //Workaround for problem med samme felt (b.Betalingsdato) 2 gange
                                  Akt_id = (int?)40,
                                  Akt_dato = (DateTime?)b.Betalingsdato
                              };


            var medlemlog = qryMedlemLog.Union(qryFak)
                                           .Union(qryBetlin)
                                           .Union(qryBetlin40);




            Tblsync s;
            foreach (var l in medlemlog)
            {
                //Id
                s = new Tblsync
                {
                    Nr = (int)l.Nr,
                    Source = l.Source,
                    Source_id = (int)l.Id,
                    Field_id = (byte)tblfield.medlemlog_id,
                    Value = getString(l.Id)
                };
                action(s);

                //Nr
                s = new Tblsync
                {
                    Nr = (int)l.Nr,
                    Source = l.Source,
                    Source_id = (int)l.Id,
                    Field_id = (byte)tblfield.medlemlog_nr,
                    Value = getString(l.Nr)
                };
                action(s);

                //Logdato
                s = new Tblsync
                {
                    Nr = (int)l.Nr,
                    Source = l.Source,
                    Source_id = (int)l.Id,
                    Field_id = (byte)tblfield.logdato,
                    Value = getString(l.Logdato, "yyyy-MM-dd HH:mm:ss")
                };
                action(s);

                //Akt_id
                s = new Tblsync
                {
                    Nr = (int)l.Nr,
                    Source = l.Source,
                    Source_id = (int)l.Id,
                    Field_id = (byte)tblfield.akt_id,
                    Value = getString(l.Akt_id)
                };
                action(s);

                //Akt_dato
                s = new Tblsync
                {
                    Nr = (int)l.Nr,
                    Source = l.Source,
                    Source_id = (int)l.Id,
                    Field_id = (byte)tblfield.akt_dato,
                    Value = getString(l.Akt_dato, "yyyy-MM-dd HH:mm:ss")
                };
                action(s);
            }
            Program.dbData3060.SubmitChanges();
        }

        private void actionKreditorSync()
        {
            var kreditor = from k in Program.dbData3060.Tblkreditor select k;
            Tblsync s;
            foreach (var k in kreditor)
            {
                //Id
                s = new Tblsync
                {
                    Nr = (int)k.Id,
                    Source = (byte)tblsource.kreditor,
                    Source_id = (int)k.Id,
                    Field_id = (byte)tblfield.kreditor_id,
                    Value = getString(k.Id),
                };
                action(s);

                //Datalevnr
                s = new Tblsync
                {
                    Nr = (int)k.Id,
                    Source = (byte)tblsource.kreditor,
                    Source_id = (int)k.Id,
                    Field_id = (byte)tblfield.datalevnr,
                    Value = getString(k.Datalevnr)
                };
                action(s);

                //Datalevnavn
                s = new Tblsync
                {
                    Nr = (int)k.Id,
                    Source = (byte)tblsource.kreditor,
                    Source_id = (int)k.Id,
                    Field_id = (byte)tblfield.datalevnavn,
                    Value = getString(k.Datalevnavn)
                };
                action(s);

                //Pbsnr
                s = new Tblsync
                {
                    Nr = (int)k.Id,
                    Source = (byte)tblsource.kreditor,
                    Source_id = (int)k.Id,
                    Field_id = (byte)tblfield.pbsnr,
                    Value = getString(k.Pbsnr)
                };
                action(s);

                //Delsystem
                s = new Tblsync
                {
                    Nr = (int)k.Id,
                    Source = (byte)tblsource.kreditor,
                    Source_id = (int)k.Id,
                    Field_id = (byte)tblfield.delsystem,
                    Value = getString(k.Delsystem)
                };
                action(s);

                //Regnr
                s = new Tblsync
                {
                    Nr = (int)k.Id,
                    Source = (byte)tblsource.kreditor,
                    Source_id = (int)k.Id,
                    Field_id = (byte)tblfield.regnr,
                    Value = getString(k.Regnr)
                };
                action(s);

                //Kontonr
                s = new Tblsync
                {
                    Nr = (int)k.Id,
                    Source = (byte)tblsource.kreditor,
                    Source_id = (int)k.Id,
                    Field_id = (byte)tblfield.kontonr,
                    Value = getString(k.Kontonr)
                };
                action(s);

                //Debgrpnr
                s = new Tblsync
                {
                    Nr = (int)k.Id,
                    Source = (byte)tblsource.kreditor,
                    Source_id = (int)k.Id,
                    Field_id = (byte)tblfield.debgrpnr,
                    Value = getString(k.Debgrpnr)
                };
                action(s);

                //Sektionnr
                s = new Tblsync
                {
                    Nr = (int)k.Id,
                    Source = (byte)tblsource.kreditor,
                    Source_id = (int)k.Id,
                    Field_id = (byte)tblfield.sektionnr,
                    Value = getString(k.Sektionnr)
                };
                action(s);

                //Transkodebetaling
                s = new Tblsync
                {
                    Nr = (int)k.Id,
                    Source = (byte)tblsource.kreditor,
                    Source_id = (int)k.Id,
                    Field_id = (byte)tblfield.transkodebetaling,
                    Value = getString(k.Transkodebetaling)
                };
                action(s);

            }
            Program.dbData3060.SubmitChanges();
        }

        private void actionSftpSync()
        {
            var sftp = from f in Program.dbData3060.Tblsftp select f;
            Tblsync s;
            foreach (var f in sftp)
            {
                //Id
                s = new Tblsync
                {
                    Nr = (int)f.Id,
                    Source = (byte)tblsource.sftp,
                    Source_id = (int)f.Id,
                    Field_id = (byte)tblfield.sftp_id,
                    Value = getString(f.Id),
                };
                action(s);
                Program.dbData3060.SubmitChanges();

                //Navn
                s = new Tblsync
                {
                    Nr = (int)f.Id,
                    Source = (byte)tblsource.sftp,
                    Source_id = (int)f.Id,
                    Field_id = (byte)tblfield.sftp_navn,
                    Value = getString(f.Navn)
                };
                action(s);
                Program.dbData3060.SubmitChanges();

                //Host
                s = new Tblsync
                {
                    Nr = (int)f.Id,
                    Source = (byte)tblsource.sftp,
                    Source_id = (int)f.Id,
                    Field_id = (byte)tblfield.host,
                    Value = getString(f.Host)
                };
                action(s);
                Program.dbData3060.SubmitChanges();

                //Port
                s = new Tblsync
                {
                    Nr = (int)f.Id,
                    Source = (byte)tblsource.sftp,
                    Source_id = (int)f.Id,
                    Field_id = (byte)tblfield.port,
                    Value = getString(f.Port)
                };
                action(s);
                Program.dbData3060.SubmitChanges();

                //User
                s = new Tblsync
                {
                    Nr = (int)f.Id,
                    Source = (byte)tblsource.sftp,
                    Source_id = (int)f.Id,
                    Field_id = (byte)tblfield.user,
                    Value = getString(f.User)
                };
                action(s);
                Program.dbData3060.SubmitChanges();

                //Outbound
                s = new Tblsync
                {
                    Nr = (int)f.Id,
                    Source = (byte)tblsource.sftp,
                    Source_id = (int)f.Id,
                    Field_id = (byte)tblfield.outbound,
                    Value = getString(f.Outbound)
                };
                action(s);
                Program.dbData3060.SubmitChanges();

                //Inbound
                s = new Tblsync
                {
                    Nr = (int)f.Id,
                    Source = (byte)tblsource.sftp,
                    Source_id = (int)f.Id,
                    Field_id = (byte)tblfield.inbound,
                    Value = getString(f.Inbound)
                };
                action(s);
                Program.dbData3060.SubmitChanges();

                //Pincode
                s = new Tblsync
                {
                    Nr = (int)f.Id,
                    Source = (byte)tblsource.sftp,
                    Source_id = (int)f.Id,
                    Field_id = (byte)tblfield.pincode,
                    Value = getString(f.Pincode)
                };
                action(s);
                Program.dbData3060.SubmitChanges();

                //Certificate
                s = new Tblsync
                {
                    Nr = (int)f.Id,
                    Source = (byte)tblsource.sftp,
                    Source_id = (int)f.Id,
                    Field_id = (byte)tblfield.certificate,
                    Value = getString(f.Certificate)
                };
                action(s);
                Program.dbData3060.SubmitChanges();

            }
            Program.dbData3060.SubmitChanges();
        }

        private void actionInfotekstSync()
        {
            var infotekst = from f in Program.dbData3060.Tblinfotekst select f;
            Tblsync s;
            foreach (var f in infotekst)
            {
                //Id
                s = new Tblsync
                {
                    Nr = (int)f.Id,
                    Source = (byte)tblsource.infotekst,
                    Source_id = (int)f.Id,
                    Field_id = (byte)tblfield.infotekst_id,
                    Value = getString(f.Id),
                };
                action(s);

                //Navn
                s = new Tblsync
                {
                    Nr = (int)f.Id,
                    Source = (byte)tblsource.infotekst,
                    Source_id = (int)f.Id,
                    Field_id = (byte)tblfield.infotekst_navn,
                    Value = getString(f.Navn)
                };
                action(s);

                //Msgtext
                s = new Tblsync
                {
                    Nr = (int)f.Id,
                    Source = (byte)tblsource.infotekst,
                    Source_id = (int)f.Id,
                    Field_id = (byte)tblfield.msgtext,
                    Value = getString(f.Msgtext)
                };
                action(s);

            }
            Program.dbData3060.SubmitChanges();
        }

        private void actionSysinfoSync()
        {
            var sysinfo = from f in Program.dbData3060.TblSysinfo select f;
            Tblsync s;
            foreach (var f in sysinfo)
            {
                //Id
                s = new Tblsync
                {
                    Nr = (int)f.Id,
                    Source = (byte)tblsource.sysinfo,
                    Source_id = (int)f.Id,
                    Field_id = (byte)tblfield.sysinfo_id,
                    Value = getString(f.Id),
                };
                action(s);

                //Vkey
                s = new Tblsync
                {
                    Nr = (int)f.Id,
                    Source = (byte)tblsource.sysinfo,
                    Source_id = (int)f.Id,
                    Field_id = (byte)tblfield.vkey,
                    Value = getString(f.Vkey)
                };
                action(s);

                //Val
                s = new Tblsync
                {
                    Nr = (int)f.Id,
                    Source = (byte)tblsource.sysinfo,
                    Source_id = (int)f.Id,
                    Field_id = (byte)tblfield.val,
                    Value = getString(f.Val)
                };
                action(s);

            }
            Program.dbData3060.SubmitChanges();
        }

        private void action(Tblsync s)
        {
            if (m_action == 1) Program.dbData3060.Tblsync.InsertOnSubmit(s);
            if (m_action == 2)
            {
                Tempsync ts = new Tempsync
                {
                    Nr = s.Nr,
                    Source = s.Source,
                    Source_id = s.Source_id,
                    Field_id = s.Field_id,
                    Value = s.Value
                };
                Program.dbData3060.Tempsync.InsertOnSubmit(ts);
            }
            if (m_action == 3)
            {
                Tempsync2 ts = new Tempsync2
                {
                    Nr = s.Nr,
                    Source = s.Source,
                    Source_id = s.Source_id,
                    Field_id = s.Field_id,
                    Value = s.Value
                };
                Program.dbData3060.Tempsync2.InsertOnSubmit(ts);
            }
        }

        public void actionMedlemxmlSync()
        {
            clsRest objRest = new clsRest();
            string retur = objRest.HttpGet2("Medlem");
            XDocument xdoc = XDocument.Parse(retur);
            var list = from person in xdoc.Descendants("Person") select person;
            int antal = list.Count();
            Tblsync s;
            foreach (var person in list)
            {

                var Nr = person.Descendants("Nr").First().Value;
                var Navn = person.Descendants("Navn").First().Value;
                var Kaldenavn = person.Descendants("Kaldenavn").First().Value;
                var Adresse = person.Descendants("Adresse").First().Value;
                var Postnr = person.Descendants("Postnr").First().Value;
                var Bynavn = person.Descendants("Bynavn").First().Value;
                var Telefon = person.Descendants("Telefon").First().Value;
                var Email = person.Descendants("Email").First().Value;
                var Kon = person.Descendants("Kon").First().Value;
                var FodtDato = person.Descendants("FodtDato").First().Value;
                var Bank = person.Descendants("Bank").First().Value;
                //Nr
                s = new Tblsync
                {
                    Nr = int.Parse(Nr),
                    Source = (byte)tblsource.medlem,
                    Source_id = int.Parse(Nr),
                    Field_id = (byte)tblfield.medlem_nr,
                    Value = Nr,
                };
                action(s);

                //Navn
                s = new Tblsync
                {
                    Nr = int.Parse(Nr),
                    Source = (byte)tblsource.medlem,
                    Source_id = int.Parse(Nr),
                    Field_id = (byte)tblfield.navn,
                    Value = Navn
                };
                action(s);

                //Kaldenavn
                s = new Tblsync
                {
                    Nr = int.Parse(Nr),
                    Source = (byte)tblsource.medlem,
                    Source_id = int.Parse(Nr),
                    Field_id = (byte)tblfield.kaldenavn,
                    Value = Kaldenavn
                };
                action(s);

                //Adresse
                s = new Tblsync
                {
                    Nr = int.Parse(Nr),
                    Source = (byte)tblsource.medlem,
                    Source_id = int.Parse(Nr),
                    Field_id = (byte)tblfield.adresse,
                    Value = Adresse
                };
                action(s);

                //Postnr
                s = new Tblsync
                {
                    Nr = int.Parse(Nr),
                    Source = (byte)tblsource.medlem,
                    Source_id = int.Parse(Nr),
                    Field_id = (byte)tblfield.postnr,
                    Value = Postnr
                };
                action(s);

                //Bynavn
                s = new Tblsync
                {
                    Nr = int.Parse(Nr),
                    Source = (byte)tblsource.medlem,
                    Source_id = int.Parse(Nr),
                    Field_id = (byte)tblfield.bynavn,
                    Value = Bynavn
                };
                action(s);

                //Telefon
                s = new Tblsync
                {
                    Nr = int.Parse(Nr),
                    Source = (byte)tblsource.medlem,
                    Source_id = int.Parse(Nr),
                    Field_id = (byte)tblfield.telefon,
                    Value = Telefon
                };
                action(s);

                //Email
                s = new Tblsync
                {
                    Nr = int.Parse(Nr),
                    Source = (byte)tblsource.medlem,
                    Source_id = int.Parse(Nr),
                    Field_id = (byte)tblfield.email,
                    Value = Email
                };
                action(s);

                //Kon
                s = new Tblsync
                {
                    Nr = int.Parse(Nr),
                    Source = (byte)tblsource.medlem,
                    Source_id = int.Parse(Nr),
                    Field_id = (byte)tblfield.kon,
                    Value = Kon
                };
                action(s);

                //FodtDato
                s = new Tblsync
                {
                    Nr = int.Parse(Nr),
                    Source = (byte)tblsource.medlem,
                    Source_id = int.Parse(Nr),
                    Field_id = (byte)tblfield.fodtdato,
                    Value = FodtDato
                };
                action(s);

                //Bank
                s = new Tblsync
                {
                    Nr = int.Parse(Nr),
                    Source = (byte)tblsource.medlem,
                    Source_id = int.Parse(Nr),
                    Field_id = (byte)tblfield.bank,
                    Value = Bank
                };
                action(s);
            }
            Program.dbData3060.SubmitChanges();
        }

        public void actionMedlemlogxmlSync()
        {
            clsRest objRest = new clsRest();
            string retur = objRest.HttpGet2("Medlemlog");
            XDocument xdoc = XDocument.Parse(retur);
            var list = from log in xdoc.Descendants("Medlemlog") select log;
            int antal = list.Count();
            Tblsync s;
            foreach (var log in list)
            {
                var Source = log.Descendants("Source").First().Value;
                var Source_id = log.Descendants("Source_id").First().Value;
                var Nr = log.Descendants("Nr").First().Value;
                var Logdato = log.Descendants("Logdato").First().Value;
                var Akt_id = log.Descendants("Akt_id").First().Value;
                var Akt_dato = log.Descendants("Akt_dato").First().Value;
                //Id
                s = new Tblsync
                {
                    Nr = int.Parse(Nr),
                    Source = byte.Parse(Source),
                    Source_id = int.Parse(Source_id),
                    Field_id = (byte)tblfield.medlemlog_id,
                    Value = Source_id
                };
                action(s);

                //Nr
                s = new Tblsync
                {
                    Nr = int.Parse(Nr),
                    Source = byte.Parse(Source),
                    Source_id = int.Parse(Source_id),
                    Field_id = (byte)tblfield.medlemlog_nr,
                    Value = Nr
                };
                action(s);

                //Logdato
                s = new Tblsync
                {
                    Nr = int.Parse(Nr),
                    Source = byte.Parse(Source),
                    Source_id = int.Parse(Source_id),
                    Field_id = (byte)tblfield.logdato,
                    Value = Logdato
                };
                action(s);

                //Akt_id
                s = new Tblsync
                {
                    Nr = int.Parse(Nr),
                    Source = byte.Parse(Source),
                    Source_id = int.Parse(Source_id),
                    Field_id = (byte)tblfield.akt_id,
                    Value = Akt_id
                };
                action(s);

                //Akt_dato
                s = new Tblsync
                {
                    Nr = int.Parse(Nr),
                    Source = byte.Parse(Source),
                    Source_id = int.Parse(Source_id),
                    Field_id = (byte)tblfield.akt_dato,
                    Value = Akt_dato
                };
                action(s);
            }
            Program.dbData3060.SubmitChanges();
        }

        public void actionKreditorxmlSync()
        {
            clsRest objRest = new clsRest();
            string retur = objRest.HttpGet2("Kreditor");
            XDocument xdoc = XDocument.Parse(retur);
            var list = from kreditor in xdoc.Descendants("Kreditor") select kreditor;
            int antal = list.Count();
            Tblsync s;
            foreach (var kreditor in list)
            {

                var Id = kreditor.Descendants("Id").First().Value;
                var Datalevnr = kreditor.Descendants("Datalevnr").First().Value;
                var Datalevnavn = kreditor.Descendants("Datalevnavn").First().Value;
                var Pbsnr = kreditor.Descendants("Pbsnr").First().Value;
                var Delsystem = kreditor.Descendants("Delsystem").First().Value;
                var Regnr = kreditor.Descendants("Regnr").First().Value;
                var Kontonr = kreditor.Descendants("Kontonr").First().Value;
                var Debgrpnr = kreditor.Descendants("Debgrpnr").First().Value;
                var Sektionnr = kreditor.Descendants("Sektionnr").First().Value;
                var Transkodebetaling = kreditor.Descendants("Transkodebetaling").First().Value;
                //Id
                s = new Tblsync
                {
                    Nr = int.Parse(Id),
                    Source = (byte)tblsource.kreditor,
                    Source_id = int.Parse(Id),
                    Field_id = (byte)tblfield.kreditor_id,
                    Value = Id,
                };
                action(s);

                //Datalevnr
                s = new Tblsync
                {
                    Nr = int.Parse(Id),
                    Source = (byte)tblsource.kreditor,
                    Source_id = int.Parse(Id),
                    Field_id = (byte)tblfield.datalevnr,
                    Value = Datalevnr
                };
                action(s);

                //Datalevnavn
                s = new Tblsync
                {
                    Nr = int.Parse(Id),
                    Source = (byte)tblsource.kreditor,
                    Source_id = int.Parse(Id),
                    Field_id = (byte)tblfield.datalevnavn,
                    Value = Datalevnavn
                };
                action(s);

                //Pbsnr
                s = new Tblsync
                {
                    Nr = int.Parse(Id),
                    Source = (byte)tblsource.kreditor,
                    Source_id = int.Parse(Id),
                    Field_id = (byte)tblfield.pbsnr,
                    Value = Pbsnr
                };
                action(s);

                //Delsystem
                s = new Tblsync
                {
                    Nr = int.Parse(Id),
                    Source = (byte)tblsource.kreditor,
                    Source_id = int.Parse(Id),
                    Field_id = (byte)tblfield.delsystem,
                    Value = Delsystem
                };
                action(s);

                //Regnr
                s = new Tblsync
                {
                    Nr = int.Parse(Id),
                    Source = (byte)tblsource.kreditor,
                    Source_id = int.Parse(Id),
                    Field_id = (byte)tblfield.regnr,
                    Value = Regnr
                };
                action(s);

                //Kontonr
                s = new Tblsync
                {
                    Nr = int.Parse(Id),
                    Source = (byte)tblsource.kreditor,
                    Source_id = int.Parse(Id),
                    Field_id = (byte)tblfield.kontonr,
                    Value = Kontonr
                };
                action(s);

                //Debgrpnr
                s = new Tblsync
                {
                    Nr = int.Parse(Id),
                    Source = (byte)tblsource.kreditor,
                    Source_id = int.Parse(Id),
                    Field_id = (byte)tblfield.debgrpnr,
                    Value = Debgrpnr
                };
                action(s);

                //Sektionnr
                s = new Tblsync
                {
                    Nr = int.Parse(Id),
                    Source = (byte)tblsource.kreditor,
                    Source_id = int.Parse(Id),
                    Field_id = (byte)tblfield.sektionnr,
                    Value = Sektionnr
                };
                action(s);

                //Transkodebetaling
                s = new Tblsync
                {
                    Nr = int.Parse(Id),
                    Source = (byte)tblsource.kreditor,
                    Source_id = int.Parse(Id),
                    Field_id = (byte)tblfield.transkodebetaling,
                    Value = Transkodebetaling
                };
                action(s);

            }
            Program.dbData3060.SubmitChanges();
        }

        public void actionSftpxmlSync()
        {
            clsRest objRest = new clsRest();
            string retur = objRest.HttpGet2("Sftp");
            XDocument xdoc = XDocument.Parse(retur);
            var list = from sftp in xdoc.Descendants("Sftp") select sftp;
            int antal = list.Count();
            Tblsync s;
            foreach (var sftp in list)
            {

                var Id = sftp.Descendants("Id").First().Value;
                var Navn = sftp.Descendants("Navn").First().Value;
                var Host = sftp.Descendants("Host").First().Value;
                var Port = sftp.Descendants("Port").First().Value;
                var User = sftp.Descendants("User").First().Value;
                var Outbound = sftp.Descendants("Outbound").First().Value;
                var Inbound = sftp.Descendants("Inbound").First().Value;
                var Pincode = sftp.Descendants("Pincode").First().Value;
                var Certificate = sftp.Descendants("Certificate").First().Value;
                //Id
                s = new Tblsync
                {
                    Nr = int.Parse(Id),
                    Source = (byte)tblsource.sftp,
                    Source_id = int.Parse(Id),
                    Field_id = (byte)tblfield.sftp_id,
                    Value = Id,
                };
                action(s);

                //Navn
                s = new Tblsync
                {
                    Nr = int.Parse(Id),
                    Source = (byte)tblsource.sftp,
                    Source_id = int.Parse(Id),
                    Field_id = (byte)tblfield.sftp_navn,
                    Value = Navn
                };
                action(s);

                //Host
                s = new Tblsync
                {
                    Nr = int.Parse(Id),
                    Source = (byte)tblsource.sftp,
                    Source_id = int.Parse(Id),
                    Field_id = (byte)tblfield.host,
                    Value = Host
                };
                action(s);

                //Port
                s = new Tblsync
                {
                    Nr = int.Parse(Id),
                    Source = (byte)tblsource.sftp,
                    Source_id = int.Parse(Id),
                    Field_id = (byte)tblfield.port,
                    Value = Port
                };
                action(s);

                //User
                s = new Tblsync
                {
                    Nr = int.Parse(Id),
                    Source = (byte)tblsource.sftp,
                    Source_id = int.Parse(Id),
                    Field_id = (byte)tblfield.user,
                    Value = User
                };
                action(s);

                //Outbound
                s = new Tblsync
                {
                    Nr = int.Parse(Id),
                    Source = (byte)tblsource.sftp,
                    Source_id = int.Parse(Id),
                    Field_id = (byte)tblfield.outbound,
                    Value = Outbound
                };
                action(s);

                //Inbound
                s = new Tblsync
                {
                    Nr = int.Parse(Id),
                    Source = (byte)tblsource.sftp,
                    Source_id = int.Parse(Id),
                    Field_id = (byte)tblfield.inbound,
                    Value = Inbound
                };
                action(s);

                //Pincode
                s = new Tblsync
                {
                    Nr = int.Parse(Id),
                    Source = (byte)tblsource.sftp,
                    Source_id = int.Parse(Id),
                    Field_id = (byte)tblfield.pincode,
                    Value = Pincode
                };
                action(s);

                //Certificate
                s = new Tblsync
                {
                    Nr = int.Parse(Id),
                    Source = (byte)tblsource.sftp,
                    Source_id = int.Parse(Id),
                    Field_id = (byte)tblfield.certificate,
                    Value = Certificate
                };
                action(s);

            }
            Program.dbData3060.SubmitChanges();
        }

        public void actionInfotekstxmlSync()
        {
            clsRest objRest = new clsRest();
            string retur = objRest.HttpGet2("Infotekst");
            XDocument xdoc = XDocument.Parse(retur);
            var list = from infotekst in xdoc.Descendants("Infotekst") select infotekst;
            int antal = list.Count();
            Tblsync s;
            foreach (var infotekst in list)
            {

                var Id = infotekst.Descendants("Id").First().Value;
                var Navn = infotekst.Descendants("Navn").First().Value;
                var Msgtext = infotekst.Descendants("Msgtext").First().Value;

                //Id
                s = new Tblsync
                {
                    Nr = int.Parse(Id),
                    Source = (byte)tblsource.infotekst,
                    Source_id = int.Parse(Id),
                    Field_id = (byte)tblfield.infotekst_id,
                    Value = Id,
                };
                action(s);

                //Navn
                s = new Tblsync
                {
                    Nr = int.Parse(Id),
                    Source = (byte)tblsource.infotekst,
                    Source_id = int.Parse(Id),
                    Field_id = (byte)tblfield.infotekst_navn,
                    Value = Navn
                };
                action(s);

                //Msgtext
                s = new Tblsync
                {
                    Nr = int.Parse(Id),
                    Source = (byte)tblsource.infotekst,
                    Source_id = int.Parse(Id),
                    Field_id = (byte)tblfield.msgtext,
                    Value = Msgtext
                };
                action(s);

            }
            Program.dbData3060.SubmitChanges();
        }

        public void actionSysinfoxmlSync()
        {
            clsRest objRest = new clsRest();
            string retur = objRest.HttpGet2("Sysinfo");
            XDocument xdoc = XDocument.Parse(retur);
            var list = from sysinfo in xdoc.Descendants("Sysinfo") select sysinfo;
            int antal = list.Count();
            Tblsync s;
            foreach (var sysinfo in list)
            {
                var Id = sysinfo.Descendants("Id").First().Value;
                var Vkey = sysinfo.Descendants("Vkey").First().Value;
                var Val = sysinfo.Descendants("Val").First().Value;

                //Id
                s = new Tblsync
                {
                    Nr = int.Parse(Id),
                    Source = (byte)tblsource.sysinfo,
                    Source_id = int.Parse(Id),
                    Field_id = (byte)tblfield.sysinfo_id,
                    Value = Id,
                };
                action(s);

                //Vkey
                s = new Tblsync
                {
                    Nr = int.Parse(Id),
                    Source = (byte)tblsource.sysinfo,
                    Source_id = int.Parse(Id),
                    Field_id = (byte)tblfield.vkey,
                    Value = Vkey
                };
                action(s);

                //Val
                s = new Tblsync
                {
                    Nr = int.Parse(Id),
                    Source = (byte)tblsource.sysinfo,
                    Source_id = int.Parse(Id),
                    Field_id = (byte)tblfield.val,
                    Value = Val
                };
                action(s);

            }
            Program.dbData3060.SubmitChanges();
        }

        public void medlemxml()
        {

            var medlem = from m1 in Program.karMedlemmer
                         join m2 in Program.dbData3060.TblMedlem on m1.Nr equals m2.Nr into medlem2
                         from m2 in medlem2.DefaultIfEmpty()
                         select new
                         {
                             Nr = m1.Nr,
                             Navn = m1.Navn,
                             Kaldenavn = m1.Kaldenavn,
                             Adresse = m1.Adresse,
                             Postnr = m1.Postnr,
                             Bynavn = m1.Bynavn,
                             Telefon = m1.Telefon,
                             Email = m1.Email,
                             Kon = m2.Kon,
                             FodtDato = m2.FodtDato,
                             Bank = m1.Bank,
                         };
            clsRest objRest = new clsRest();
            int antal = medlem.Count();
            foreach (var m in medlem)
            {
                XElement xml = new XElement("Medlem",
                                 new XElement("key", ""),
                                 new XElement("Nr", m.Nr),
                                 new XElement("Navn", m.Navn),
                                 new XElement("Kaldenavn", m.Kaldenavn),
                                 new XElement("Adresse", m.Adresse),
                                 new XElement("Postnr", m.Postnr),
                                 new XElement("Bynavn", m.Bynavn),
                                 new XElement("Telefon", m.Telefon),
                                 new XElement("Email", m.Email),
                                 new XElement("Kon", m.Kon),
                                 new XElement("FodtDato", ((DateTime)m.FodtDato).ToString("yyyy-MM-dd")),
                                 new XElement("Bank", m.Bank)
                         );
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2("Medlem", strxml);
            }

        }

        public void medlemlogxml()
        {
            var qryMedlemLog = from m in Program.dbData3060.TblMedlemLog
                               select new clsLog2
                               {
                                   Source = (byte)tblsource.medlemlog,
                                   Id = (int?)m.Id,
                                   Nr = (int?)m.Nr,
                                   Logdato = (DateTime?)m.Logdato,
                                   Akt_id = (int?)m.Akt_id,
                                   Akt_dato = (DateTime?)m.Akt_dato
                               };
            var qryFak = from f in Program.dbData3060.Tblfak
                         join p in Program.dbData3060.Tbltilpbs on f.Tilpbsid equals p.Id
                         select new clsLog2
                         {
                             Source = (byte)tblsource.fak,
                             Id = (int?)f.Id,
                             Nr = (int?)f.Nr,
                             Logdato = (DateTime)p.Bilagdato,
                             Akt_id = (int?)20,
                             Akt_dato = (DateTime?)f.Betalingsdato
                         };

            var qryBetlin = from b in Program.dbData3060.Tblbetlin
                            join f in Program.dbData3060.Tblfak on b.Faknr equals f.Faknr
                            where b.Pbstranskode == "0236" || b.Pbstranskode == "0297"
                            select new clsLog2
                            {
                                Source = (byte)tblsource.betlin,
                                Id = (int?)b.Id,
                                Nr = (int?)b.Nr,
                                Logdato = (DateTime?)b.Indbetalingsdato,
                                Akt_id = (int?)30,
                                Akt_dato = (DateTime?)f.Tildato
                            };

            var qryBetlin40 = from b in Program.dbData3060.Tblbetlin
                              where b.Pbstranskode == "0237"
                              select new clsLog2
                              {
                                  Source = (byte)tblsource.betlin40,
                                  Id = (int?)b.Id,
                                  Nr = (int?)b.Nr,
                                  Logdato = (DateTime?)(((DateTime)b.Betalingsdato).AddSeconds(-30)),  //Workaround for problem med samme felt (b.Betalingsdato) 2 gange
                                  Akt_id = (int?)40,
                                  Akt_dato = (DateTime?)b.Betalingsdato
                              };


            var qrymedlemlogunion = qryMedlemLog.Union(qryFak)
                                        .Union(qryBetlin)
                                        .Union(qryBetlin40);

            var medlemlog = from u in qrymedlemlogunion orderby u.Id select u;


            clsRest objRest = new clsRest();
            int antal = medlemlog.Count();
            foreach (var l in medlemlog)
            {
                XElement xml = new XElement("Medlemlog",
                                 new XElement("Source", l.Source),
                                 new XElement("Source_id", l.Id),
                                 new XElement("Nr", l.Nr),
                                 new XElement("Logdato", ((DateTime)l.Logdato).ToString("yyyy-MM-ddTHH:mm:ss")),
                                 new XElement("Akt_id", l.Akt_id),
                                 new XElement("Akt_dato", ((DateTime)l.Akt_dato).ToString("yyyy-MM-ddTHH:mm:ss"))
                         );
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2("Medlemlog", strxml);


            }

        }

        public void kreditorxml()
        {
            var kreditor = from k in Program.dbData3060.Tblkreditor
                         select k;
            clsRest objRest = new clsRest();
            int antal = kreditor.Count();
            foreach (var k in kreditor)
            {
                XElement xml = new XElement("Kreditor",
                                 new XElement("key", ""),
                                 new XElement("Id", k.Id),
                                 new XElement("Datalevnr", k.Datalevnr),
                                 new XElement("Datalevnavn", k.Datalevnavn),
                                 new XElement("Pbsnr", k.Pbsnr),
                                 new XElement("Delsystem", k.Delsystem),
                                 new XElement("Regnr", k.Regnr),
                                 new XElement("Kontonr", k.Kontonr) ,
                                 new XElement("Debgrpnr", k.Debgrpnr),
                                 new XElement("Sektionnr", k.Sektionnr),
                                 new XElement("Transkodebetaling", k.Transkodebetaling)
                                 );
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2("Kreditor", strxml);
            }
        }

        public void sftpxml()
        {
            var sftp = from s in Program.dbData3060.Tblsftp
                         select s;
            clsRest objRest = new clsRest();
            int antal = sftp.Count();
            foreach (var s in sftp)
            {
                XElement xml = new XElement("Sftp",
                                 new XElement("key", ""),
                                 new XElement("Id", s.Id),
                                 new XElement("Navn", s.Navn),
                                 new XElement("Host", s.Host),
                                 new XElement("Port", s.Port),
                                 new XElement("User", s.User),
                                 new XElement("Outbound", s.Outbound),
                                 new XElement("Inbound", s.Inbound),
                                 new XElement("Pincode", s.Pincode),
                                 new XElement("Certificate", s.Certificate)
                                 );
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2("Sftp", strxml);
            }
        }

        public void infotekstxml()
        {
            var infotekst = from i in Program.dbData3060.Tblinfotekst
                            select i;
            clsRest objRest = new clsRest();
            int antal = infotekst.Count();
            foreach (var i in infotekst)
            {
                XElement xml = new XElement("Infotekst",
                                 new XElement("key", ""),
                                 new XElement("Id", i.Id),
                                 new XElement("Navn", i.Navn),
                                 new XElement("Msgtext", i.Msgtext)
                                 );
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2("Infotekst", strxml);
            }
        }

        public void sysinfoxml()
        {
            var sysinfo = from s in Program.dbData3060.TblSysinfo
                          select s;
            clsRest objRest = new clsRest();
            int antal = sysinfo.Count();
            foreach (var s in sysinfo)
            {
                XElement xml = new XElement("Sysinfo",
                                 new XElement("key", ""),
                                 new XElement("Id", s.Id),
                                 new XElement("Vkey", s.Vkey),
                                 new XElement("Val", s.Val)
                                 );
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2("Sysinfo", strxml);
            }
        }        
        
        //********************************************************************************
        //********************************************************************************

        private string getString(string Value)
        {
            if (Value == null) return "None";
            if (Value.Length == 0) return "None";
            return Value;
        }

        private string getString(int? Value)
        {
            if (Value == null) return "None";
            return getString((int)Value);
        }

        private string getString(int Value)
        {
            return Value.ToString();
        }

        private string getString(DateTime? Value)
        {
            if (Value == null) return "None";
            return getString((DateTime)Value);
        }

        private string getString(DateTime Value)
        {
            return Value.ToString("yyyy-MM-dd");
        }

        private string getString(DateTime? Value, string Format)
        {
            if (Value == null) return "None";
            return getString((DateTime)Value, Format);
        }

        private string getString(DateTime Value, string Format)
        {
            return Value.ToString(Format);
        }

        public enum tblsource : byte
        {
            medlem = 254,
            medlemlog = 2,
            fak = 3,
            betlin = 4,
            betlin40 = 5,
            kreditor = 6,
            sftp = 7,
            infotekst = 8,
            sysinfo = 9
        }

        public enum tblfield : byte
        {
            medlem_nr = 1,
            navn = 2,
            kaldenavn = 3,
            adresse = 4,
            postnr = 5,
            bynavn = 6,
            telefon = 7,
            email = 8,
            kon = 9,
            fodtdato = 10,
            bank = 11,

            medlemlog_id = 13,
            logdato = 14,
            akt_id = 15,
            akt_dato = 16,
            medlemlog_nr = 17,

            kreditor_id = 20,
            datalevnr = 21,
            datalevnavn = 22,
            pbsnr = 23,
            delsystem = 24,
            regnr = 25,
            kontonr = 26,
            debgrpnr = 27,
            sektionnr = 28,
            transkodebetaling = 29,

            sftp_id = 30,
            sftp_navn = 31,
            host = 32,
            port = 33,
            user = 34,
            outbound = 35,
            inbound = 36,
            pincode = 37,
            certificate = 38,

            infotekst_id = 40,
            infotekst_navn = 41,
            msgtext = 42,

            sysinfo_id = 50,
            vkey = 51,
            val = 52,
        }

        internal void importeksport(ImpExp ieAction)
        {
            IOrderedQueryable<Tempimpexp> imp = null;
            if (ieAction == ImpExp.fdImport)
            {
                imp = from t in Program.dbData3060.Tempimpexp
                      where t.Ie == "i"
                      && t.Source < 3
                      && t.Act != "del"
                      orderby t.Nr, t.Source, t.Source_id, t.Field_id
                      select t;
            }
            if (ieAction == ImpExp.fdEksport)
            {
                imp = from t in Program.dbData3060.Tempimpexp
                      where t.Ie == "e"
                      orderby t.Nr, t.Source, t.Source_id, t.Field_id
                      select t;
            }

            int antal = imp.Count();
            int Last_Nr = 0;
            byte Last_Source = 0;
            int Last_Source_id = 0;
            bool bFirst = true;
            bool bBreak = false;
            clsImEksportAppEngMedlemlog objMedlemLog = null;
            clsImEksportAppEngMedlem objMedlem = null;
            clsImEksportAppEngKreditor objKreditor = null;
            clsImEksportAppEngSftp objSftp = null;
            clsImEksportAppEngInfotekst objInfotekst = null;
            clsImEksportAppEngSysinfo objSysinfo = null;

            foreach (var t in imp)
            {
                bBreak = ((t.Source_id != Last_Source_id) || (t.Source != Last_Source) || (t.Nr != Last_Nr));
                if ((bBreak) && (!bFirst)) //Save Data   
                {
                    switch (Last_Source)
                    {
                        case (byte)tblsource.medlem:  //Medlem
                            medlemupdate(objMedlem);  //Save Medlem
                            break;

                        case (byte)tblsource.medlemlog:   //Medlemlog
                        case (byte)tblsource.fak:         //fak
                        case (byte)tblsource.betlin:      //betlin
                        case (byte)tblsource.betlin40:    //betlin40
                            medlemlogupdate(objMedlemLog);//Save MedlemLog
                            break;

                        case (byte)tblsource.kreditor:   //kreditor
                            kreditorupdate(objKreditor); //Save kreditor
                            break;

                        case (byte)tblsource.sftp:  //sftp
                            sftpupdate(objSftp);    //Save sftp
                            break;

                        case (byte)tblsource.infotekst:    //infotekst
                            infotekstupdate(objInfotekst); //Save infotekst
                            break;

                        case (byte)tblsource.sysinfo:     //sysinfo
                            sysinfoupdate(objSysinfo);    //Save sysinfo
                            break;

                        default:
                            break;
                    }
                }
                if ((bBreak) || (bFirst)) //Init Data
                {
                    switch (t.Source)
                    {
                        case (byte)tblsource.medlem:    //Medlem
                            objMedlem = new clsImEksportAppEngMedlem();
                            objMedlem.ieAction = ieAction;
                            objMedlem.Nr = t.Nr;
                            objMedlem.bNr = true;
                            objMedlem.Act = t.Act;
                            break;

                        case (byte)tblsource.medlemlog:   //Medlemlog
                        case (byte)tblsource.fak:         //fak
                        case (byte)tblsource.betlin:      //betlin
                        case (byte)tblsource.betlin40:    //betlin40
                            objMedlemLog = new clsImEksportAppEngMedlemlog();
                            objMedlemLog.ieAction = ieAction;
                            objMedlemLog.Act = t.Act;
                            objMedlemLog.Source = t.Source;
                            objMedlemLog.bSource = true;
                            objMedlemLog.Id = t.Source_id;
                            objMedlemLog.bId = true;
                            break;

                        case (byte)tblsource.kreditor:    //Kreditor
                            objKreditor = new clsImEksportAppEngKreditor();
                            objKreditor.ieAction = ieAction;
                            objKreditor.Id  = t.Nr;
                            objKreditor.bId = true;
                            objKreditor.Act = t.Act;
                            break;

                        case (byte)tblsource.sftp:        //Sftp
                            objSftp = new clsImEksportAppEngSftp();
                            objSftp.ieAction = ieAction;
                            objSftp.Id = t.Nr;
                            objSftp.bId = true;
                            objSftp.Act = t.Act;
                            break;

                        case (byte)tblsource.infotekst:    //Infotekst
                            objInfotekst = new clsImEksportAppEngInfotekst();
                            objInfotekst.ieAction = ieAction;
                            objInfotekst.Id = t.Nr;
                            objInfotekst.bId = true;
                            objInfotekst.Act = t.Act;
                            break;

                        case (byte)tblsource.sysinfo:     //Sysinfo
                            objSysinfo = new clsImEksportAppEngSysinfo();
                            objSysinfo.ieAction = ieAction;
                            objSysinfo.Id = t.Nr;
                            objSysinfo.bId = true;
                            objSysinfo.Act = t.Act;
                            break;

                        default:
                            break;
                    }
                }
                // Get Data
                switch (t.Source)
                {
                    case (byte)tblsource.medlem:    //Medlem
                        switch (t.Field_id)
                        {
                            case 1:   //medlem_nr
                                objMedlem.Nr = int.Parse(t.Value);
                                objMedlem.bNr = true;
                                break;
                            case 2:   //navn
                                objMedlem.Navn = t.Value;
                                objMedlem.bNavn = true;
                                break;
                            case 3:   //kaldenavn
                                objMedlem.Kaldenavn = t.Value;
                                objMedlem.bKaldenavn = true;
                                break;
                            case 4:   //adresse
                                objMedlem.Adresse = t.Value;
                                objMedlem.bAdresse = true;
                                break;
                            case 5:   //postnr
                                objMedlem.Postnr = t.Value;
                                objMedlem.bPostnr = true;
                                break;
                            case 6:   //bynavn
                                objMedlem.Bynavn = t.Value;
                                objMedlem.bBynavn = true;
                                break;
                            case 7:   //telefon
                                objMedlem.Telefon = t.Value;
                                objMedlem.bTelefon = true;
                                break;
                            case 8:   //email
                                objMedlem.Email = t.Value;
                                objMedlem.bEmail = true;
                                break;
                            case 9:   //kon
                                objMedlem.Kon = t.Value;
                                objMedlem.bKon = true;
                                break;
                            case 10:   //fodtdato
                                objMedlem.FodtDato = DateTime.Parse(t.Value);
                                objMedlem.bFodtDato = true;
                                break;
                            case 11:   //bank
                                objMedlem.Bank = t.Value;
                                objMedlem.bBank = true;
                                break;

                            default:
                                break;
                        }
                        break;

                    case (byte)tblsource.medlemlog:   //Medlemlog
                    case (byte)tblsource.fak:         //fak
                    case (byte)tblsource.betlin:      //betlin
                    case (byte)tblsource.betlin40:    //betlin40
                        switch (t.Field_id)
                        {
                            case 13:   //medlemlog_id
                                objMedlemLog.Id = int.Parse(t.Value);
                                objMedlemLog.bId = true;
                                break;
                            case 14:   //logdato
                                objMedlemLog.Logdato = DateTime.Parse(t.Value);
                                objMedlemLog.bLogdato = true;
                                break;
                            case 15:   //akt_id
                                objMedlemLog.Akt_id = int.Parse(t.Value);
                                objMedlemLog.bAkt_id = true;
                                break;
                            case 16:   //akt_dato
                                objMedlemLog.Akt_dato = DateTime.Parse(t.Value);
                                objMedlemLog.bAkt_dato = true;
                                break;
                            case 17:   //medlemlog_nr
                                objMedlemLog.Nr = int.Parse(t.Value);
                                objMedlemLog.bNr = true;
                                break;
                            default:
                                break;
                        }
                        break;

                    case (byte)tblsource.kreditor:    //Kreditor
                        switch (t.Field_id)
                        {
                            case 20:   //kreditor_id
                                objKreditor.Id = int.Parse(t.Value);
                                objKreditor.bId = true;
                                break;
                            case 21:   //datalevnr
                                objKreditor.Datalevnr = t.Value;
                                objKreditor.bDatalevnr = true;
                                break;
                            case 22:   //datalevnavn
                                objKreditor.Datalevnavn = t.Value;
                                objKreditor.bDatalevnavn = true;
                                break;
                            case 23:   //pbsnr
                                objKreditor.Pbsnr = t.Value;
                                objKreditor.bPbsnr = true;
                                break;
                            case 24:   //delsystem
                                objKreditor.Delsystem = t.Value;
                                objKreditor.bDelsystem = true;
                                break;
                            case 25:   //regnr
                                objKreditor.Regnr = t.Value;
                                objKreditor.bRegnr = true;
                                break;
                            case 26:   //kontonr
                                objKreditor.Kontonr = t.Value;
                                objKreditor.bKontonr = true;
                                break;
                            case 27:   //debgrpnr
                                objKreditor.Debgrpnr = t.Value;
                                objKreditor.bDebgrpnr = true;
                                break;
                            case 28:   //sektionnr
                                objKreditor.Sektionnr = t.Value;
                                objKreditor.bSektionnr = true;
                                break;
                            case 29:   //transkodebetaling
                                objKreditor.Transkodebetaling = t.Value;
                                objKreditor.bTranskodebetaling = true;
                                break;

                            default:
                                break;
                        }
                        break;

                    case (byte)tblsource.sftp:    //Sftp
                        switch (t.Field_id)
                        {
                            case 30:   //sftp_id
                                objSftp.Id = int.Parse(t.Value);
                                objSftp.bId = true;
                                break;
                            case 31:   //sftp_navn
                                objSftp.Navn = t.Value;
                                objSftp.bNavn = true;
                                break;
                            case 32:   //host
                                objSftp.Host = t.Value;
                                objSftp.bHost = true;
                                break;
                            case 33:   //port
                                objSftp.Port = t.Value;
                                objSftp.bPort = true;
                                break;
                            case 34:   //user
                                objSftp.User = t.Value;
                                objSftp.bUser = true;
                                break;
                            case 35:   //outbound
                                objSftp.Outbound = t.Value;
                                objSftp.bOutbound = true;
                                break;
                            case 36:   //inbound
                                objSftp.Inbound = t.Value;
                                objSftp.bInbound = true;
                                break;
                            case 37:   //pincode
                                objSftp.Pincode = t.Value;
                                objSftp.bPincode = true;
                                break;
                            case 38:   //certificate
                                objSftp.Certificate = t.Value;
                                objSftp.bCertificate = true;
                                break;

                            default:
                                break;
                        }
                        break;

                    case (byte)tblsource.infotekst:    //Infotekst
                        switch (t.Field_id)
                        {
                            case 40:   //infotekst_id
                                objInfotekst.Id = int.Parse(t.Value);
                                objInfotekst.bId = true;
                                break;
                            case 41:   //infotekst_navn
                                objInfotekst.Navn = t.Value;
                                objInfotekst.bNavn = true;
                                break;
                            case 42:   //msgtext
                                objInfotekst.Msgtext = t.Value;
                                objInfotekst.bMsgtext = true;
                                break;

                            default:
                                break;
                        }
                        break;

                    case (byte)tblsource.sysinfo:    //Sysinfo
                        switch (t.Field_id)
                        {
                            case 50:   //sysinfo_id
                                objSysinfo.Id = int.Parse(t.Value);
                                objSysinfo.bId = true;
                                break;
                            case 51:   //vkey
                                objSysinfo.Vkey = t.Value;
                                objSysinfo.bVkey = true;
                                break;
                            case 52:   //val
                                objSysinfo.Val = t.Value;
                                objSysinfo.bVal = true;
                                break;

                            default:
                                break;
                        }
                        break;

                    default:
                        break;
                }

                // Save Status
                Last_Nr = t.Nr;
                Last_Source = t.Source;
                Last_Source_id = t.Source_id;
                bFirst = false;
            }
            if (!bFirst) //Save Data   
            {
                switch (Last_Source)
                {
                    case (byte)tblsource.medlem:    //Medlem
                        medlemupdate(objMedlem);    //Save Medlem
                        break;

                    case (byte)tblsource.medlemlog:   //Medlemlog
                    case (byte)tblsource.fak:         //fak
                    case (byte)tblsource.betlin:      //betlin
                    case (byte)tblsource.betlin40:    //betlin40
                        medlemlogupdate(objMedlemLog);//Save MedlemLog
                        break;

                    case (byte)tblsource.kreditor:    //Kreditor
                        kreditorupdate(objKreditor);  //Save Kreditor
                        break;

                    case (byte)tblsource.sftp:        //Sftp
                        sftpupdate(objSftp);          //Save Kreditor
                        break;

                    default:
                        break;
                }
            }
            if (ieAction == ImpExp.fdImport) Program.dsMedlemImport.savedsMedlem();
        }

        private void medlemupdate(clsImEksportAppEngMedlem objMedlem)
        {
            objMedlem.ExecuteImEksport(); //throw new NotImplementedException();
        }

        private void medlemlogupdate(clsImEksportAppEngMedlemlog objMedlemLog)
        {
            objMedlemLog.ExecuteImEksport();//throw new NotImplementedException();
        }
        
        private void kreditorupdate(clsImEksportAppEngKreditor objKreditor)
        {
            objKreditor.ExecuteImEksport();//throw new NotImplementedException();
        }
        
        private void sftpupdate(clsImEksportAppEngSftp objSftp)
        {
            objSftp.ExecuteImEksport();//throw new NotImplementedException();
        }

        private void infotekstupdate(clsImEksportAppEngInfotekst objInfotekst)
        {
            objInfotekst.ExecuteImEksport();//throw new NotImplementedException();
        }

        private void sysinfoupdate(clsImEksportAppEngSysinfo objSysinfo)
        {
            objSysinfo.ExecuteImEksport();//throw new NotImplementedException();
        }
    }
}
