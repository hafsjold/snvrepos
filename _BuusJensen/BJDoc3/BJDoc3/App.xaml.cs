using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity;

namespace BJDoc3
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static dbBuusjensenEntities m_db;

        public static dbBuusjensenEntities db
        {
            get
            {
                if (m_db == null)
                {
                    m_db = new dbBuusjensenEntities();
                    m_db.ChangeDatabase
                        ( 
                            initialCatalog: "dbBuusjensen",
                            integratedSecuity: false
                        );
                    m_db.tblFeatures.Load();
                    m_db.tblFeatureTypes.Load();
                    m_db.tblBrugers.Load();
                }
                return m_db;
            }
            set
            {
                m_db = value;
            }
        }
    }
}
