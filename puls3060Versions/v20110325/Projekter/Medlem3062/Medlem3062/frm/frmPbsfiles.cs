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
        private bool m_listfile = false;

        public FrmPbsfiles()
        {
            InitializeComponent();
        }


        private void pbsfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_listfile = true;
            this.Text = "Pbsfiles";
            var qry = from p in Program.dbData3060.Tblpbsfiles
                      join f in Program.dbData3060.Tblpbsforsendelse on p.Pbsforsendelseid equals f.Id into forsendelse
                      from f in forsendelse.DefaultIfEmpty()
                      select new
                      {
                          p.Id,
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
            m_listfile = false;
            this.Text = "Log";
            var qry = from i in Program.qryLog()
                      select new
                      {
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
            m_listfile = false;
            this.Text = "Pbsfile";
            var qry = from p in Program.dbData3060.Tblpbsfile
                      join f in Program.dbData3060.Tblpbsfiles on p.Pbsfilesid equals f.Id
                      orderby f.Id descending, p.Seqnr
                      select new
                      {
                          seq = p.Id,
                          file = f.Filename,
                          data = p.Data
                      };
            bindingSource1.DataSource = qry;
            dataGridView1.DataSource = bindingSource1;

            // Grid attributes 
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void pbsforsendelseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_listfile = false;
            this.Text = "Pbsforsendelse";
            bindingSource1.DataSource = Program.dbData3060.Tblpbsforsendelse;
            dataGridView1.DataSource = bindingSource1;

            // Grid attributes 
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;


        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (m_listfile)
            {
                int row = e.RowIndex;
                DataGridView dg = (DataGridView)sender;
                try
                {

                    string filesid = dg.Rows[row].Cells["Id"].Value.ToString();
                    fillrichData(int.Parse(filesid));
                }
                catch
                {
                    this.richData.Text = "";
                }
            }
            else 
            {
                this.richData.Text = "";
            }
        }

        private void fillrichData(int filesid)
        {
            var src = from d in Program.dbData3060.Tblpbsfile
                      where d.Pbsfilesid == filesid
                      orderby d.Seqnr
                      select d;
            int i = 0;
            int test = src.Count();
            string data = "";
            foreach (var d in src)
            {
                if (++i == 1)
                {
                    data = d.Data;
                }
                else
                {
                    data += "\n" + d.Data;
                }
            }
            this.richData.Text = data;

        }


    }
}
