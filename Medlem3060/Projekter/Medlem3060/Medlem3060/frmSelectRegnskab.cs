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
    public partial class frmSelectRegnskab : Form
    {
        public frmSelectRegnskab()
        {
            InitializeComponent();
        }

        private void frmSelectRegnskab_Load(object sender, EventArgs e)
        {
            (new clsPbs()).SetAktivRegnskaber();
            var rec_AktivRegnskab = (from r in Program.dbData3060.TblRegnskab
                                     join a in Program.dbData3060.TblAktivtRegnskab on r.Rid equals a.Rid
                                     select r).First();
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
            var rec_regnskab = (from r in Program.dbData3060.TblRegnskab
                                select r);
            foreach (var r in rec_regnskab)
            {
                this.listView1.Items.Add(r.Rid.ToString(), r.Navn, 0);
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
                    var rec_AktivRegnskab = (from a in Program.dbData3060.TblAktivtRegnskab select a).First();
                    rec_AktivRegnskab.Rid = int.Parse(key);
                }
                catch (System.InvalidOperationException)
                {
                    TblAktivtRegnskab rec_AktivRegnskab = new TblAktivtRegnskab
                    {
                        Rid = int.Parse(key)
                    };
                    Program.dbData3060.TblAktivtRegnskab.InsertOnSubmit(rec_AktivRegnskab);
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
