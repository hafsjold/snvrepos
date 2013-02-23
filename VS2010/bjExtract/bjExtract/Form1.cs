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
            clsExportArkiv obj = new clsExportArkiv();
            obj.ExsportArkiv(@"C:\Users\mha\Documents\mha_test_arkiv25", @"C:\Users\mha\Documents\Visual Studio 2010\Projects\bjOutput\myArkiv.exe");
        }
     }
}
