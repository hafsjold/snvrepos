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

        private void tblMedlemToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.taTblMedlem.Fill(this.dsAccess.tblMedlem);
            var qry = from h in this.dsAccess.tblMedlem.AsQueryable()
                      orderby h.Nr
                      select new
                      {
                          h.Nr,
                          h.Knr,
                          h.Kon,
                          h.FodtDato
                      };
            FileStream ts = new FileStream(m_path + "tblMedlem.sqlce", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            string SQL = "INSERT INTO [tblMedlem] ([Nr],[Knr],[Kon],[FodtDato]) VALUES ({0},{1},'{2}',{3});";
            using (StreamWriter sr = new StreamWriter(ts, Encoding.Default))
            {
                foreach (var h in qry)
                {
                    string FodtDato = "{ts '" + h.FodtDato.ToString("yyyy-MM-dd") + " 00:00:00'}";
                    string myString = string.Format(SQL, h.Nr, h.Knr, h.Kon, FodtDato);
                    sr.WriteLine(myString);
                    sr.WriteLine("GO");
                }
            }
        }

        private void tblMedlemLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.taTblMedlemLog.Fill(this.dsAccess.tblMedlemLog);
            var qry = from h in this.dsAccess.tblMedlemLog.AsQueryable()
                      orderby h.id
                      select h;
            FileStream ts = new FileStream(m_path + "tblMedlemLog.sqlce", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            string SQL = "INSERT INTO [tblMedlemLog] ([id],[Nr],[logdato],[akt_id],[akt_dato]) VALUES ({0},{1},{2},{3},{4});";
            using (StreamWriter sr = new StreamWriter(ts, Encoding.Default))
            {
                sr.WriteLine("SET IDENTITY_INSERT [tblMedlemLog] ON");
                sr.WriteLine("GO");
                foreach (var h in qry)
                {
                    string logdato = "{ts '" + h.logdato.ToString("yyyy-MM-dd hh:mm:ss") + "'}";
                    string akt_dato = "{ts '" + h.akt_dato.ToString("yyyy-MM-dd hh:mm:ss") + "'}";
                    string myString = string.Format(SQL, h.id, h.Nr, logdato, h.akt_id, akt_dato);
                    sr.WriteLine(myString);
                    sr.WriteLine("GO");
                }
                sr.WriteLine("SET IDENTITY_INSERT [tblMedlemLog] OFF");
                sr.WriteLine("GO");
            }
        }
    }
}
