using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace BJDoc2
{
    public partial class FrmTest : Form
    {
        public FrmTest()
        {
            InitializeComponent();
         }

        private void FrmTest_Load(object sender, EventArgs e)
        {
           bsFeatures.DataSource = Program.db.tblFeatures.Local.ToBindingList();
           bsFeatureTypes.DataSource = Program.db.tblFeatureTypes.Local.ToBindingList();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            /*
            tblFeatures recFeatures = new tblFeatures
            {
                 navn = "ss",
                 tblFeatures_tblFeatureType = 2
            };
            Program.db.tblFeatures.Local.Add(recFeatures);
            */
        }

        private void tblFeaturesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            Program.db.SaveChanges();
        }
    }
}
