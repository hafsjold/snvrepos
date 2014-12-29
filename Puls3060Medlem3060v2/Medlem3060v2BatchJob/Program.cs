using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace nsMedlem3060v2BatchJob
{
    class Program
    {
        private static dbPuls3060Medlem m_dbJobQ;
        private static dbPuls3060Medlem dbJobQContextFactory()
        {
            string constr = clsConnectionstringManager.EFConnection;
            return new dbPuls3060Medlem(constr);
        }
        public static dbPuls3060Medlem dbJobQ
        {
            get
            {
                if (m_dbJobQ == null)
                {
                    m_dbJobQ = dbJobQContextFactory();
                }
                return m_dbJobQ;
            }
            set
            {
                m_dbJobQ = value;
            }
        }

        static void Main(string[] args)
        {
            
            clsMedlem3060v2BatchJob job = new clsMedlem3060v2BatchJob();
            job.Scheduler();
        }
    }
}
