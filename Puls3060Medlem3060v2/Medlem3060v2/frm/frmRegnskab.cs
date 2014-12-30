using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace nsPuls3060v2
{
    public partial class FrmRegnskab : Form
    {
        public FrmRegnskab()
        {
            InitializeComponent();
        }

        private void FrmRegnskab_Load(object sender, EventArgs e)
        {
            this.bsRegnskab.DataSource = Program.memRegnskaber;
        }

        private void FrmRegnskab_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void Navn_Validating(object sender, CancelEventArgs e)
        {
            if (Navn.Text.Length > 15)
            {
                e.Cancel = true;
                Navn.Select(0, Navn.Text.Length);

                // Set the ErrorProvider error with the text to display. 
                this.epRegnskab.SetError(Navn, "Dette er en fejl");
            }
        }

        private void Navn_Validated(object sender, EventArgs e)
        {
            this.epRegnskab.SetError(Navn, "");
        }
    }
}
