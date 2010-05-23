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

        private void pbs603ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Text = "Pbs603";

            var pbsfile1 = from d1 in Program.dbData3060.Tblpbsfile
                           join h1 in Program.dbData3060.Tblpbsfiles on d1.Pbsfilesid equals h1.Id
                           where d1.Seqnr == 1
                           select new
                           {
                               Pbsfilesid = d1.Pbsfilesid,
                               h1.Filename,
                               h1.Path,
                               h1.Pbsforsendelseid,
                               h1.Atime,
                               leverancetype = d1.Data.Substring(16, 4),
                               data1 = d1.Data
                           };

            var pbsfile0 = from d0 in Program.dbData3060.Tblpbsfile
                           join h0 in Program.dbData3060.Tblpbsfiles on d0.Pbsfilesid equals h0.Id
                           where d0.Seqnr == 0
                           select new 
                           {
                               Pbsfilesid = d0.Pbsfilesid,
                               recordtype = d0.Data.Substring(0, 8),
                               delsystem = d0.Data.Substring(8, 3),
                               dato_lev_id = d0.Data.Substring(12, 8),
                               data0 = d0.Data
                           };

            var qry = from d1 in pbsfile1
                      join d0 in pbsfile0 on d1.Pbsfilesid equals d0.Pbsfilesid into pbsfile0j
                      from d0 in pbsfile0j.DefaultIfEmpty()

                      select new
                      {
                        d1.Filename,
                        d1.Atime,
                        d0.dato_lev_id,
                        d0.delsystem,
                        d0.recordtype,
                        d1.leverancetype,
                        d0.data0,
                        d1.data1
                      };


            bindingSource1.DataSource = qry;
            dataGridView1.DataSource = bindingSource1;

            // Grid attributes 
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }


    }
}
