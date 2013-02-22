using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace bjExtract
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SelfExtractor objSelfExtractor = new SelfExtractor();
            objSelfExtractor.AddFile(@"C:\Users\mha\Documents\mha_test_arkiv25\.bja\bjArkiv.xml");
            objSelfExtractor.AddFile(@"C:\Users\mha\Documents\mha_test_arkiv25\Svampeangreb0001 - Kopi - Kopi.pdf");
            objSelfExtractor.AddFile(@"C:\Users\mha\Documents\mha_test_arkiv25\Svampeangreb0001 - Kopi.pdf");
            objSelfExtractor.AddFile(@"C:\Users\mha\Documents\mha_test_arkiv25\Svampeangreb0001.pdf");
            objSelfExtractor.AddFile(@"C:\Users\mha\Documents\mha_test_arkiv25\System.Data.SQLite.dll");

            objSelfExtractor.CompileArchive(@"C:\Users\mha\Documents\Visual Studio 2010\Projects\bjOutput\bjViewer.exe");


        }
    }
}
