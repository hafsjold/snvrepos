using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace nsPuls3060
{
    public class clsMedlemmer
    {
        public static dicMedlem dic = new dicMedlem();

        private Dictionary<int, clsMedlem> m_Medlemmer;
        private FileStream m_fs;
        private string m_kartotek_dat;

        public Dictionary<int, clsMedlem> Medlemmer {
            get
            {
                return m_Medlemmer;
            }
        }

        public clsMedlemmer() { }

        public void Open(string kartotek_dat)
        {
            m_kartotek_dat = kartotek_dat;
            m_Medlemmer = new Dictionary<int, clsMedlem>();
            m_fs = new FileStream(m_kartotek_dat, FileMode.Open, FileAccess.Read, FileShare.None);

            using (StreamReader sr = new StreamReader(m_fs, Encoding.Default))
            {
                string read = null;
                while ((read = sr.ReadLine()) != null)
                {
                    clsMedlem m = new clsMedlem(read);
                    m_Medlemmer.Add(m.Nr, m);
                }
            }
        }

        public void Update(Int32 p_Nr)
        {
            clsMedlem medlem;
            string wCsv;
            try
            {
                medlem = m_Medlemmer[p_Nr];
                wCsv = medlem.getUpdatedCvsString();
            }
            catch (KeyNotFoundException) { }
        }

        public void Save() {
            m_fs = new FileStream(m_kartotek_dat, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);

            using (StreamWriter sr = new StreamWriter(m_fs, Encoding.Default)) 
            {
                foreach(clsMedlem m in m_Medlemmer.Values){
                    sr.WriteLine(m.CsvString);
                }
            }       
        }
    }
}
