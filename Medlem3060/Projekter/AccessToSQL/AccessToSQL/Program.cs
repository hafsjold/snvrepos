using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace AccessToSQL
{
    static class Program
    {
        private static DbData3060 m_dbData3060;
        public static DbData3060 dbData3060
        {
            get
            {
                if (m_dbData3060 == null)
                {
                    if (!File.Exists(global::AccessToSQL.Properties.Settings.Default.DataBasePath))
                    {
                        OpenFileDialog openFileDialog1 = new OpenFileDialog();
                        openFileDialog1.DefaultExt = "sdf";
                        openFileDialog1.FileName = global::AccessToSQL.Properties.Settings.Default.DataBasePath;
                        openFileDialog1.CheckFileExists = true;
                        openFileDialog1.CheckPathExists = true;
                        openFileDialog1.Filter = "Database files (*.sdf)|*.sdf|All files (*.*)|*.*";
                        openFileDialog1.FilterIndex = 1;
                        openFileDialog1.Multiselect = false;
                        openFileDialog1.Title = "Vælg SQL Database";

                        DialogResult res = openFileDialog1.ShowDialog();
                        if (res == DialogResult.OK)
                        {
                            global::AccessToSQL.Properties.Settings.Default.DataBasePath = openFileDialog1.FileName;
                            global::AccessToSQL.Properties.Settings.Default.Save();
                        }
                    }
                    m_dbData3060 = new DbData3060(global::AccessToSQL.Properties.Settings.Default.DataBasePath);
                }
                return m_dbData3060;
            }
            set
            {
                m_dbData3060 = value;
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
