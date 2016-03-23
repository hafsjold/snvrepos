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

        private static dbBuusjensenEntities m_db;

        public static dbBuusjensenEntities db
        {
            get
            {
                if (m_db == null)
                {
                    m_db = new dbBuusjensenEntities();
                    m_db.tblFeatureTypes.Load();
                    m_db.tblFeatures.Load();
                    m_db.tblHWtypes.Load();
                    m_db.tblHWs.Load();
                    m_db.tblComputers.Load();
                    m_db.tblFeatureRelations.Load();
                    m_db.tblBrugers.Load();
                    m_db.tblLokales.Load();
                    m_db.tblBrugerRelations.Load();
                    m_db.tblIps.Load();
                    m_db.tblIpRelations.Load();
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
