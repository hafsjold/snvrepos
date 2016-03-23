using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BJDoc2
{
    public partial class FrmFeatureType : Form
    {
        public FrmFeatureType()
        {
            InitializeComponent();
        }

        private void FrmFeatureType_Load(object sender, EventArgs e)
        {
            this.bsFeatureTypes.DataSource = Program.db.tblFeatureTypes.Local;
        }

        private void FrmFeatureType_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.db.SaveChanges();
        }
    }
}
