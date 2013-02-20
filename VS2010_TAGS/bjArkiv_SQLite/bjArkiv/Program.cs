using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LogicNP.ShellObjects;

namespace bjArkiv
{
    static class Program
    {
        public const string BJARKIV = @"\.bja\bjArkiv.db3";
        public static System.IO.FileSystemWatcher bjArkivWatcher;
        private static Columns m_customColumns;
        private static Columns m_explorerColumns;

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
                    m_customColumns["Beskrivelse"] = new Column { Name = "Beskrivelse", ColumnGuid = Guid.Empty, ColumnPid = 0, Width = 250, ColumnDisplayIndex = 6 };
                }
                return m_customColumns;
            }
        }

        public static Columns explorerColumns
        {
            get
            {
                if (m_explorerColumns == null)
                {
                    m_explorerColumns = new Columns();
                    m_explorerColumns["Name"] = new Column { Name = "Name", ColumnGuid = new Guid("b725f130-47ef-101a-a5f1-02608c9eebac"), ColumnPid = 10, Width = 322, ColumnDisplayIndex = 0 };
                    m_explorerColumns["Date Modified"] = new Column { Name = "Date Modified", ColumnGuid = new Guid("b725f130-47ef-101a-a5f1-02608c9eebac"), ColumnPid = 14, Width = 120, ColumnDisplayIndex = 1 };
                    m_explorerColumns["Type"] = new Column { Name = "Size", ColumnGuid = new Guid("28636aa6-953d-11d2-b5d6-00c04fd918d0"), ColumnPid = 11, Width = 120, ColumnDisplayIndex = 2 };
                    m_explorerColumns["Size"] = new Column { Name = "Size", ColumnGuid = new Guid("b725f130-47ef-101a-a5f1-02608c9eebac"), ColumnPid = 12, Width = 90, ColumnDisplayIndex = 3 };
                }
                return m_explorerColumns;
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
            if (SingleInstanceComponent.NotifyExistingInstance(null))
            {
                Application.Run(new frmFileView());
            }
        }
    }
}
