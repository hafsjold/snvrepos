using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AccessToSQL
{
    public partial class Form1 : Form
    {
        private string m_path = @"C:\Documents and Settings\mha\Dokumenter\Medlem3060\Databaser\SQLCompact\Scripts\";

        public Form1()
        {
            InitializeComponent();
        }
        private void MedlemToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.DefaultExt = "sqlce";
            openFileDialog1.FileName = m_path + "Medlem.sqlce";
            openFileDialog1.CheckFileExists = false;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.Filter = "Database files (*.sqlce)|*.sqlce|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = false;
            openFileDialog1.Title = "Vælg File";

            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                FileStream ts;
                if (File.Exists(openFileDialog1.FileName))
                {
                    ts = new FileStream(openFileDialog1.FileName, FileMode.Truncate, FileAccess.Write, FileShare.None);
                }
                else
                {
                    ts = new FileStream(openFileDialog1.FileName, FileMode.CreateNew, FileAccess.Write, FileShare.None);
                }
                using (StreamWriter sr = new StreamWriter(ts, Encoding.GetEncoding("Windows-1252")))
                {
                    sr.WriteLine("DELETE FROM [tblMedlemLog];");
                    sr.WriteLine("GO");
                    sr.WriteLine("DELETE FROM [tblMedlem];");
                    sr.WriteLine("GO");
                    tblMedlem(sr);
                    tblMedlemLog(sr);
                }
            }
        }


        private void tblMedlem(StreamWriter sr)
        {
            this.taTblMedlem.Fill(this.dsAccess.tblMedlem);
            string SQL = "INSERT INTO [tblMedlem] ([Nr],[Knr],[Kon],[FodtDato]) VALUES ({0},{1},{2},{3});";
            string[] p = new string[4];
            foreach (AccessDataSet.tblMedlemRow h in this.dsAccess.tblMedlem.Rows)
            {
                p[0] = qf(h.Nr);
                p[1] = h.IsKnrNull() ? "null" : qf(h.Knr);
                p[2] = h.IsKonNull() ? "null" : qf(h.Kon);
                p[3] = h.IsFodtDatoNull() ? "null" : qf(h.FodtDato);

                string myString = string.Format(SQL, p[0], p[1], p[2], p[3]);

                sr.WriteLine(myString);
                sr.WriteLine("GO");
            }
        }

        private void tblMedlemLog(StreamWriter sr)
        {
            this.taTblMedlemLog.Fill(this.dsAccess.tblMedlemLog);
            string SQL = "INSERT INTO [tblMedlemLog] ([id],[Nr],[logdato],[akt_id],[akt_dato]) VALUES ({0},{1},{2},{3},{4});";
            sr.WriteLine("SET IDENTITY_INSERT [tblMedlemLog] ON");
            sr.WriteLine("GO");

            string[] p = new string[5];
            foreach (AccessDataSet.tblMedlemLogRow h in this.dsAccess.tblMedlemLog.Rows)
            {
                p[0] = qf(h.id);
                p[1] = h.IsNrNull() ? "null" : qf(h.Nr);
                p[2] = h.IslogdatoNull() ? "null" : qf(h.logdato);
                p[3] = h.Isakt_idNull() ? "null" : qf(h.akt_id);
                p[4] = h.Isakt_datoNull() ? "null" : qf(h.akt_dato);

                string myString = string.Format(SQL, p[0], p[1], p[2], p[3], p[4]);

                sr.WriteLine(myString);
                sr.WriteLine("GO");
            }
            sr.WriteLine("SET IDENTITY_INSERT [tblMedlemLog] OFF");
            sr.WriteLine("GO");
        }

        private void PBSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.DefaultExt = "sqlce";
            openFileDialog1.FileName = m_path + "PBS.sqlce";
            openFileDialog1.CheckFileExists = false;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.Filter = "Database files (*.sqlce)|*.sqlce|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = false;
            openFileDialog1.Title = "Vælg File";

            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                FileStream ts;
                if (File.Exists(openFileDialog1.FileName))
                {
                    ts = new FileStream(openFileDialog1.FileName, FileMode.Truncate, FileAccess.Write, FileShare.None);
                }
                else
                {
                    ts = new FileStream(openFileDialog1.FileName, FileMode.CreateNew, FileAccess.Write, FileShare.None);
                }
                using (StreamWriter sr = new StreamWriter(ts, Encoding.GetEncoding("Windows-1252")))
                {
                    sr.WriteLine("DELETE FROM [tblbetlin];");
                    sr.WriteLine("GO");
                    sr.WriteLine("DELETE FROM [tblbet];");
                    sr.WriteLine("GO");
                    sr.WriteLine("DELETE FROM [tblfrapbs];");
                    sr.WriteLine("GO");
                    sr.WriteLine("DELETE FROM [tblpbsfile];");
                    sr.WriteLine("GO");
                    sr.WriteLine("DELETE FROM [tblpbsfiles];");
                    sr.WriteLine("GO");
                    sr.WriteLine("DELETE FROM [tblfak];");
                    sr.WriteLine("GO");
                    sr.WriteLine("DELETE FROM [tbltilpbs];");
                    sr.WriteLine("GO");
                    sr.WriteLine("DELETE FROM [tblpbsforsendelse];");
                    sr.WriteLine("GO");

                    tblpbsforsendelse(sr);
                    tbltilpbs(sr);
                    tblfak(sr);
                    tblpbsfiles(sr);
                    tblpbsfile(sr);
                    tblfrapbs(sr);
                    tblbet(sr);
                    tblbetlin(sr);
                }
            }
        }

        private void tblpbsforsendelse(StreamWriter sr)
        {
            this.taTblpbsforsendelse.Fill(this.dsAccess.tblpbsforsendelse);
            string SQL = "INSERT INTO [tblpbsforsendelse] ([id],[delsystem],[leverancetype],[oprettetaf],[oprettet],[leveranceid]) VALUES ({0}, {1},{2},{3},{4},{5});";
            sr.WriteLine("SET IDENTITY_INSERT [tblpbsforsendelse] ON");
            sr.WriteLine("GO");
            string[] p = new string[6];
            foreach (AccessDataSet.tblpbsforsendelseRow h in this.dsAccess.tblpbsforsendelse.Rows)
            {
                p[0] = qf(h.id);
                p[1] = h.IsdelsystemNull() ? "null" : qf(h.delsystem);
                p[2] = h.IsleverancetypeNull() ? "null" : qf(h.leverancetype);
                p[3] = h.IsoprettetafNull() ? "null" : qf(h.oprettetaf);
                p[4] = h.IsoprettetNull() ? "null" : qf(h.oprettet);
                p[5] = h.IsleveranceidNull() ? "null" : qf(h.leveranceid);
                string myString = string.Format(SQL, p[0], p[1], p[2], p[3], p[4], p[5]);
                sr.WriteLine(myString);
                sr.WriteLine("GO");
            }
            sr.WriteLine("SET IDENTITY_INSERT [tblpbsforsendelse] OFF");
            sr.WriteLine("GO");
        }

        private void tbltilpbs(StreamWriter sr)
        {
            this.taTbltilpbs.Fill(this.dsAccess.tbltilpbs);
            string SQL = "INSERT INTO [tbltilpbs] ([id],[delsystem],[leverancetype],[bilagdato],[pbsforsendelseid],[udtrukket],[leverancespecifikation],[leverancedannelsesdato]) VALUES ({0},{1},{2},{3},{4},{5},{6},{7});";
            sr.WriteLine("SET IDENTITY_INSERT [tbltilpbs] ON");
            sr.WriteLine("GO");
            string[] p = new string[8];
            foreach (AccessDataSet.tbltilpbsRow h in this.dsAccess.tbltilpbs.Rows)
            {
                p[0] = qf(h.id);
                p[1] = h.IsdelsystemNull() ? "null" : qf(h.delsystem);
                p[2] = h.IsleverancetypeNull() ? "null" : qf(h.leverancetype);
                p[3] = h.IsbilagdatoNull() ? "null" : qf(h.bilagdato);
                p[4] = h.IspbsforsendelseidNull() ? "null" : qf(h.pbsforsendelseid);
                p[5] = h.IsudtrukketNull() ? "null" : qf(h.udtrukket);
                p[6] = h.IsleverancespecifikationNull() ? "null" : qf(h.leverancespecifikation);
                p[7] = h.IsleverancedannelsesdatoNull() ? "null" : qf(h.leverancedannelsesdato);

                string myString = string.Format(SQL, p[0], p[1], p[2], p[3], p[4], p[5], p[6], p[7]);

                sr.WriteLine(myString);
                sr.WriteLine("GO");
            }
            sr.WriteLine("SET IDENTITY_INSERT [tbltilpbs] OFF");
            sr.WriteLine("GO");
        }

        private void tblfak(StreamWriter sr)
        {
            this.taTblfak.Fill(this.dsAccess.tblfak);
            string SQL = "INSERT INTO [tblfak] ([id],[tilpbsid],[betalingsdato],[Nr],[faknr],[advistekst],[advisbelob],[infotekst],[bogfkonto],[vnr],[fradato],[tildato],[SFakID],[SFaknr],[rykkerdato],[maildato],[rykkerstop],[betalt]) VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17});";
            sr.WriteLine("SET IDENTITY_INSERT [tblfak] ON");
            sr.WriteLine("GO");
            string[] p = new string[18];
            foreach (AccessDataSet.tblfakRow h in this.dsAccess.tblfak.Rows)
            {
                p[0] = qf(h.id);
                p[1] = h.IstilpbsidNull() ? "null" : qf(h.tilpbsid);
                p[2] = h.IsbetalingsdatoNull() ? "null" : qf(h.betalingsdato);
                p[3] = h.IsNrNull() ? "null" : qf(h.Nr);
                p[4] = h.IsfaknrNull() ? "null" : qf(h.faknr);
                p[5] = h.IsadvistekstNull() ? "null" : qf(h.advistekst);
                p[6] = h.IsadvisbelobNull() ? "null" : qf(h.advisbelob);
                p[7] = h.IsinfotekstNull() ? "null" : qf(h.infotekst);
                p[8] = h.IsbogfkontoNull() ? "null" : qf(h.bogfkonto);
                p[9] = h.IsvnrNull() ? "null" : qf(h.vnr);
                p[10] = h.IsfradatoNull() ? "null" : qf(h.fradato);
                p[11] = h.IstildatoNull() ? "null" : qf(h.tildato);
                p[12] = h.IsSFakIDNull() ? "null" : qf(h.SFakID);
                p[13] = h.IsSFaknrNull() ? "null" : qf(h.SFaknr);
                p[14] = h.IsrykkerdatoNull() ? "null" : qf(h.rykkerdato);
                p[15] = h.IsmaildatoNull() ? "null" : qf(h.maildato);
                p[16] = h.IsrykkerstopNull() ? "null" : qf(h.rykkerstop);
                p[17] = h.IsbetaltNull() ? "null" : qf(h.betalt);

                string myString = string.Format(SQL, p[0], p[1], p[2], p[3], p[4], p[5], p[6], p[7], p[8], p[9], p[10], p[11], p[12], p[13], p[14], p[15], p[16], p[17]);

                sr.WriteLine(myString);
                sr.WriteLine("GO");
            }
            sr.WriteLine("SET IDENTITY_INSERT [tblfak] OFF");
            sr.WriteLine("GO");
        }

        private void tblpbsfiles(StreamWriter sr)
        {
            this.taTblpbsfiles.Fill(this.dsAccess.tblpbsfiles);
            string SQL = "INSERT INTO [tblpbsfiles] ([id],[type],[path],[filename],[size],[atime],[mtime],[perm],[uid],[gid],[transmittime],[pbsforsendelseid]) VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11});";
            sr.WriteLine("SET IDENTITY_INSERT [tblpbsfiles] ON");
            sr.WriteLine("GO");
            string[] p = new string[12];
            foreach (AccessDataSet.tblpbsfilesRow h in this.dsAccess.tblpbsfiles.Rows)
            {
                p[0] = qf(h.id);
                p[1] = h.IstypeNull() ? "null" : qf(h.type);
                p[2] = h.IspathNull() ? "null" : qf(h.path);
                p[3] = h.IsfilenameNull() ? "null" : qf(h.filename);
                p[4] = h.IssizeNull() ? "null" : qf(h.size);
                p[5] = h.IsatimeNull() ? "null" : qf(h.atime);
                p[6] = h.IsmtimeNull() ? "null" : qf(h.mtime);
                p[7] = h.IspermNull() ? "null" : qf(h.perm);
                p[8] = h.IsuidNull() ? "null" : qf(h.uid);
                p[9] = h.IsgidNull() ? "null" : qf(h.gid);
                p[10] = h.IstransmittimeNull() ? "null" : qf(h.transmittime);
                p[11] = h.IspbsforsendelseidNull() ? "null" : qf(h.pbsforsendelseid);

                string myString = string.Format(SQL, p[0], p[1], p[2], p[3], p[4], p[5], p[6], p[7], p[8], p[9], p[10], p[11]);

                sr.WriteLine(myString);
                sr.WriteLine("GO");
            }
            sr.WriteLine("SET IDENTITY_INSERT [tblpbsfiles] OFF");
            sr.WriteLine("GO");
        }

        private void tblpbsfile(StreamWriter sr)
        {
            this.taTblpbsfile.Fill(this.dsAccess.tblpbsfile);
            string SQL = "INSERT INTO [tblpbsfile] ([id],[pbsfilesid],[seqnr],[data]) VALUES ({0},{1},{2},{3});";
            sr.WriteLine("SET IDENTITY_INSERT [tblpbsfile] ON");
            sr.WriteLine("GO");
            string[] p = new string[4];
            foreach (AccessDataSet.tblpbsfileRow h in this.dsAccess.tblpbsfile.Rows)
            {
                p[0] = qf(h.id);
                p[1] = h.IspbsfilesidNull() ? "null" : qf(h.pbsfilesid);
                p[2] = h.IsseqnrNull() ? "null" : qf(h.seqnr);
                p[3] = h.IsdataNull() ? "null" : qf(h.data);

                string myString = string.Format(SQL, p[0], p[1], p[2], p[3]);

                sr.WriteLine(myString);
                sr.WriteLine("GO");
            }
            sr.WriteLine("SET IDENTITY_INSERT [tblpbsfile] OFF");
            sr.WriteLine("GO");
        }

        private void tblfrapbs(StreamWriter sr)
        {
            this.taTblfrapbs.Fill(this.dsAccess.tblfrapbs);
            string SQL = "INSERT INTO [tblfrapbs] ([id],[delsystem],[leverancetype],[udtrukket],[bilagdato],[pbsforsendelseid],[leverancespecifikation],[leverancedannelsesdato]) VALUES ({0},{1},{2},{3},{4},{5},{6},{7});";
            sr.WriteLine("SET IDENTITY_INSERT [tblfrapbs] ON");
            sr.WriteLine("GO");
            string[] p = new string[8];
            foreach (AccessDataSet.tblfrapbsRow h in this.dsAccess.tblfrapbs.Rows)
            {
                p[0] = qf(h.id);
                p[1] = h.IsdelsystemNull() ? "null" : qf(h.delsystem);
                p[2] = h.IsleverancetypeNull() ? "null" : qf(h.leverancetype);
                p[3] = h.IsudtrukketNull() ? "null" : qf(h.udtrukket);
                p[4] = h.IsbilagdatoNull() ? "null" : qf(h.bilagdato);
                p[5] = h.IspbsforsendelseidNull() ? "null" : qf(h.pbsforsendelseid);
                p[6] = h.IsleverancespecifikationNull() ? "null" : qf(h.leverancespecifikation);
                p[7] = h.IsleverancedannelsesdatoNull() ? "null" : qf(h.leverancedannelsesdato);

                string myString = string.Format(SQL, p[0], p[1], p[2], p[3], p[4], p[5], p[6], p[7]);

                sr.WriteLine(myString);
                sr.WriteLine("GO");
            }
            sr.WriteLine("SET IDENTITY_INSERT [tblfrapbs] OFF");
            sr.WriteLine("GO");
        }

        private void tblbet(StreamWriter sr)
        {
            this.taTblbet.Fill(this.dsAccess.tblbet);
            string SQL = "INSERT INTO [tblbet] ([id],[frapbsid],[pbssektionnr],[transkode],[bogforingsdato],[indbetalingsbelob]) VALUES ({0},{1},{2},{3},{4},{5});";
            sr.WriteLine("SET IDENTITY_INSERT [tblbet] ON");
            sr.WriteLine("GO");
            string[] p = new string[6];
            foreach (AccessDataSet.tblbetRow h in this.dsAccess.tblbet.Rows)
            {
                p[0] = qf(h.id);
                p[1] = h.IsfrapbsidNull() ? "null" : qf(h.frapbsid);
                p[2] = h.IspbssektionnrNull() ? "null" : qf(h.pbssektionnr);
                p[3] = h.IstranskodeNull() ? "null" : qf(h.transkode);
                p[4] = h.IsbogforingsdatoNull() ? "null" : qf(h.bogforingsdato);
                p[5] = h.IsindbetalingsbelobNull() ? "null" : qf(h.indbetalingsbelob);

                string myString = string.Format(SQL, p[0], p[1], p[2], p[3], p[4], p[5]);

                sr.WriteLine(myString);
                sr.WriteLine("GO");
            }
            sr.WriteLine("SET IDENTITY_INSERT [tblbet] OFF");
            sr.WriteLine("GO");
        }

        private void tblbetlin(StreamWriter sr)
        {
            this.taTblbetlin.Fill(this.dsAccess.tblbetlin);
            string SQL = "INSERT INTO [tblbetlin] ([id],[betid],[pbssektionnr],[pbstranskode],[Nr],[faknr],[debitorkonto],[aftalenr],[betalingsdato],[belob],[indbetalingsdato],[bogforingsdato],[indbetalingsbelob],[pbskortart],[pbsgebyrbelob],[pbsarkivnr]) VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15});";
            sr.WriteLine("SET IDENTITY_INSERT [tblbetlin] ON");
            sr.WriteLine("GO");
            string[] p = new string[16];
            foreach (AccessDataSet.tblbetlinRow h in this.dsAccess.tblbetlin.Rows)
            {
                p[0] = qf(h.id);
                p[1] = h.IsbetidNull() ? "null" : qf(h.betid);
                p[2] = h.IspbssektionnrNull() ? "null" : qf(h.pbssektionnr);
                p[3] = h.IspbstranskodeNull() ? "null" : qf(h.pbstranskode);
                p[4] = h.IsNrNull() ? "null" : qf(h.Nr);
                p[5] = h.IsfaknrNull() ? "null" : qf(h.faknr);
                p[6] = h.IsdebitorkontoNull() ? "null" : qf(h.debitorkonto);
                p[7] = h.IsaftalenrNull() ? "null" : qf(h.aftalenr);
                p[8] = h.IsbetalingsdatoNull() ? "null" : qf(h.betalingsdato);
                p[9] = h.IsbelobNull() ? "null" : qf(h.belob);
                p[10] = h.IsindbetalingsdatoNull() ? "null" : qf(h.indbetalingsdato);
                p[11] = h.IsbogforingsdatoNull() ? "null" : qf(h.bogforingsdato);
                p[12] = h.IsindbetalingsbelobNull() ? "null" : qf(h.indbetalingsbelob);
                p[13] = h.IspbskortartNull() ? "null" : qf(h.pbskortart);
                p[14] = h.IspbsgebyrbelobNull() ? "null" : qf(h.pbsgebyrbelob);
                p[15] = h.IspbsarkivnrNull() ? "null" : qf(h.pbsarkivnr);

                string myString = string.Format(SQL, p[0], p[1], p[2], p[3], p[4], p[5], p[6], p[7], p[8], p[9], p[10], p[11], p[12], p[13], p[14], p[15]);

                sr.WriteLine(myString);
                sr.WriteLine("GO");
            }
            sr.WriteLine("SET IDENTITY_INSERT [tblbetlin] OFF");
            sr.WriteLine("GO");
        }

        private string qf(object o)
        {
            switch (o.GetType().ToString().ToUpper())
            {
                case "SYSTEM.STRING":
                    return "N'" + ((string)o).ToString() + "'";

                case "SYSTEM.BOOLEAN":
                    if ((Boolean)o)
                    {
                        return "1";
                    }
                    else
                    {
                        return "0";
                    }

                case "SYSTEM.DECIMAL":
                    return ((decimal)o).ToString().Replace(',', '.');

                case "SYSTEM.DATETIME":
                    if (((DateTime)o).TimeOfDay == new TimeSpan(0, 0, 0))
                    {
                        return "{ts '" + ((DateTime)o).ToString("yyyy-MM-dd 00:00:00") + "'}";
                    }
                    else
                    {
                        return "{ts '" + ((DateTime)o).ToString("yyyy-MM-dd hh:mm:ss") + "'}";
                    }

                default:
                    return o.ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = tableAdapterManager1.Connection.ConnectionString;
        }

        private void accessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] s1 = tableAdapterManager1.Connection.ConnectionString.Split(';');
            string[] s2 = s1[1].Split('=');

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.DefaultExt = "mdb";
            openFileDialog1.FileName = s2[1].Trim('"');
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.Filter = "Database files (*.mdb)|*.mdb|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = false;
            openFileDialog1.Title = "Vælg Access Database";

            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                tableAdapterManager1.Connection.ConnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=""{0}""", openFileDialog1.FileName);
                toolStripStatusLabel1.Text = tableAdapterManager1.Connection.ConnectionString;
            }

        }

    }
}
