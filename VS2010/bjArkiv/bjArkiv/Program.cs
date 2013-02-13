using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace bjArkiv
{
    static class Program
    {
        public const string BJARKIV = @"\.bja\bjArkiv.db3";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmbjArkiv());
        }
    }
}
