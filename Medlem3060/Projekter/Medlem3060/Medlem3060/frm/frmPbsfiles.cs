using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace nsPuls3060
{
    public partial class FrmPbsfiles : Form
    {
        public FrmPbsfiles()
        {
            InitializeComponent();
        }


        private void pbsfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Text = "Pbsfiles";
            var qry = from p in Program.dbData3060.Tblpbsfiles
                      join f in Program.dbData3060.Tblpbsforsendelse on p.Pbsforsendelseid equals f.Id into forsendelse
                      from f in forsendelse.DefaultIfEmpty()
                      select new
                      {
                          p.Path,
                          p.Filename,
                          Size = (int?)p.Size,
                          Atime = (DateTime?)p.Atime,
                          Mtime = (DateTime?)p.Mtime,
                          Pbsforsendelseid = (int?)f.Id,
                          f.Delsystem,
                          f.Leverancetype,
                          f.Oprettetaf,
                          Oprettet = (DateTime?)f.Oprettet,
                          Leveranceid = (int?)f.Leveranceid,
                          Transmittime = (DateTime?)p.Transmittime
                      };
            bindingSource1.DataSource = qry;
            dataGridView1.DataSource = bindingSource1;

            // Grid attributes 
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        }

        private void logToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Text = "Log";
            var qry = from i in Program.qryLog()
                       select new
                       {
                           Id = i.Id,
                           Logdato = i.Logdato,
                           Nr = i.Nr,
                           Akt_id = i.Akt_id,
                           Akt_dato = i.Akt_dato
                       };

            bindingSource1.DataSource = qry;
            dataGridView1.DataSource = bindingSource1;

            // Grid attributes 
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
 
        }

        private void afslutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbsfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Text = "Pbsfile";
            var qry = from p in Program.dbData3060.Tblpbsfile
                      join f in Program.dbData3060.Tblpbsfiles on p.Pbsfilesid equals f.Id
                      orderby f.Id descending , p.Seqnr  
                      select new
                      {
                          seq = p.Id,
                          file = f.Filename,
                          data =p.Data
                      };
            bindingSource1.DataSource = qry;
            dataGridView1.DataSource = bindingSource1;

            // Grid attributes 
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void pbsforsendelseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Text = "Pbsforsendelse";
            bindingSource1.DataSource = Program.dbData3060.Tblpbsforsendelse;
            dataGridView1.DataSource = bindingSource1;

            // Grid attributes 
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;


        }
    }
}
