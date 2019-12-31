using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trans2SummaHDC
{
    public partial class FrmSysInfo : Form
    {
        public FrmSysInfo()
        {
            InitializeComponent();
        }

        private void frmSysInfo_Load(object sender, EventArgs e)
        {
            var uri = new Uri(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase));
            tbExePath.Text = uri.LocalPath;
        }
    }
}
