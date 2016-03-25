using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;


namespace BJDoc2
{
    static class Program
    {
        public static FrmMain frmMain { get; set; }
        
        private static dbBuusjensenEntities1 m_db;

        public static dbBuusjensenEntities1 db
        {
            get
            {
                if (m_db == null)
                {
                    m_db = new dbBuusjensenEntities1();
                    m_db.tblFeatures.Load();
                    m_db.tblFeatureTypes.Load();
                }
                return m_db;
            }
            set
            {
                m_db = value;
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
            Application.Run(new FrmMain());
        }
    }
}
