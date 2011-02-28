using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace nsPuls3060
{
    public partial class frmSelectRegnskab : Form
    {
        public frmSelectRegnskab()
        {
            InitializeComponent();
        }

        private void frmSelectRegnskab_Load(object sender, EventArgs e)
        {
            clsPbs objPbs = new clsPbs();
            objPbs.ReadRegnskaber();
            var rec_AktivRegnskab = Program.qryAktivRegnskab();
            if (rec_AktivRegnskab.Rid == 999) this.cmdSidstAnventeRegnskab.Enabled = false;
            this.Regnskab.Text = rec_AktivRegnskab.Navn;
        }

        private void cmdSidstAnventeRegnskab_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cmdOpenRegnskab_Click(object sender, EventArgs e)
        {
            this.cmdOpenRegnskab.Visible = false;
            this.cmdSidstAnventeRegnskab.Visible = false;
            this.Regnskab.Visible = false;
            var rec_regnskab = (from r in Program.memRegnskab select r);
            foreach (var r in rec_regnskab)
            {
                ListViewItem it = this.listView1.Items.Add(r.Rid.ToString(), r.Navn, 0);
                it.SubItems.Add((r.Afsluttet.Value == true) ? "Afsluttet" : "");

            }
            this.listView1.Visible = true;
            this.cmdOK.Visible = true;
            this.cmdCancel.Visible = true;
            this.label_listview.Visible = true;

        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 1)
            {
                string key = this.listView1.SelectedItems[0].Name;

                try
                {
                    var rec_AktivRegnskab = (from a in Program.memAktivRegnskab select a).First();
                    rec_AktivRegnskab.Rid = int.Parse(key);
                }
                catch (System.InvalidOperationException)
                {
                    recActivRegnskab rec_AktivRegnskab = new recActivRegnskab
                    {
                        Rid = int.Parse(key)
                    };
                    Program.memAktivRegnskab.Add(rec_AktivRegnskab);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
