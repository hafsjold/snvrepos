using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Medlem3060uc
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
            var qry = from p in Program.dbData3060.tblpbsfilenames
                      join f in Program.dbData3060.tblpbsforsendelses on p.pbsforsendelseid equals f.id into forsendelse
                      from f in forsendelse.DefaultIfEmpty()
                      select new
                      {
                          p.id,
                          p.path,
                          p.filename,
                          Size = (int?)p.size,
                          Atime = (DateTime?)p.atime,
                          Mtime = (DateTime?)p.mtime,
                          Pbsforsendelseid = (int?)f.id,
                          f.delsystem,
                          f.leverancetype,
                          f.oprettetaf,
                          Oprettet = (DateTime?)f.oprettet,
                          Leveranceid = (int?)f.leveranceid,
                          Transmittime = (DateTime?)p.transmittime
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
            var qry = from p in Program.dbData3060.tblpbsfiles
                      join f in Program.dbData3060.tblpbsfilenames on p.pbsfilesid equals f.id
                      orderby f.id descending, p.seqnr
                      select new
                      {
                          seq = p.id,
                          file = f.filename,
                          data = p.data
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
            bindingSource1.DataSource = Program.dbData3060.tblpbsforsendelses;
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
            var src = from d in Program.dbData3060.tblpbsfiles
                      where d.pbsfilesid == filesid
                      orderby d.seqnr
                      select d;
            int i = 0;
            int test = src.Count();
            string data = "";
            foreach (var d in src)
            {
                if (++i == 1)
                {
                    data = d.data;
                }
                else
                {
                    data += "\n" + d.data;
                }
            }
            this.richData.Text = data;

        }


    }
}
