using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace BJDoc2
{
    public partial class FrmFeatureType : Form
    {
        public FrmFeatureType()
        {
            InitializeComponent();
            //Program.db.tblFeatureTypes.Load();
        }

        private void FrmFeatureType_Load(object sender, EventArgs e)
        {
            //tblFeatureTypesBindingSource.DataSource = Program.db.tblFeatureTypes;
        }

        private void FrmFeatureType_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Program.db.SubmitChanges();
        }
    }
}
