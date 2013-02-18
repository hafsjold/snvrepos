using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace bjArkiv
{
    static class Program
    {
        public const string BJARKIV = @"\.bja\bjArkiv.db3";
        public static System.IO.FileSystemWatcher bjArkivWatcher;
        private static Columns m_customColumns;

        public static Columns customColumns
        {
            get 
            {
                if (m_customColumns == null) 
                {
                    m_customColumns = new Columns();
                    m_customColumns["Virksomhed"] = new Column { Name = "Virksomhed", ColumnGuid = Guid.Empty, ColumnPid = 0, Width = 200, ColumnDisplayIndex = 1 };
                    m_customColumns["Emne"] = new Column { Name = "Emne", ColumnGuid = Guid.Empty, ColumnPid = 0, Width = 100, ColumnDisplayIndex = 2 };
                    m_customColumns["Doktype"] = new Column { Name = "Doktype", ColumnGuid = Guid.Empty, ColumnPid = 0, Width = 150, ColumnDisplayIndex = 3 };
                    m_customColumns["År"] = new Column { Name = "År", ColumnGuid = Guid.Empty, ColumnPid = 0, Width = 60, ColumnDisplayIndex = 4 };
                    m_customColumns["Ekstern kilde"] = new Column { Name = "Ekstern kilde", ColumnGuid = Guid.Empty, ColumnPid = 0, Width = 100, ColumnDisplayIndex = 5 };
                }
                return m_customColumns; 
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
            Application.Run(new frmFileView());
        }
    }
}
