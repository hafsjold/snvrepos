using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace nsPuls3060
{
    public class clsMedlemmer : List<clsMedlem>
    {
        public static dicMedlem dic = new dicMedlem();

        private string m_kartotek_dat;
        private DbData3060 m_dbData3060;

        public clsMedlemmer(DbData3060 pdbData3060)
        {
            m_dbData3060 = pdbData3060;
            var rec_regnskab = (from r in m_dbData3060.TblRegnskab
                                join a in m_dbData3060.TblAktivtRegnskab on r.Rid equals a.Rid
                                select r).First();

            m_kartotek_dat = rec_regnskab.Placering + "kartotek.dat";
            Open();
        }

        public void Open()
        {
            FileStream fs = new FileStream(m_kartotek_dat, FileMode.Open, FileAccess.Read, FileShare.None);

            using (StreamReader sr = new StreamReader(fs, Encoding.Default))
            {
                string read = null;
                while ((read = sr.ReadLine()) != null)
                {
                    clsMedlem m = new clsMedlem(read);
                    this.Add(m);
                }
            }
        }

        public void Update(Int32 p_Nr)
        {
            clsMedlem medlem;
            string wCsv;
            try
            {
                medlem = (from d in this
                          where d.Nr == p_Nr
                          select d).First();

                wCsv = medlem.getUpdatedCvsString();
            }
            catch (KeyNotFoundException) { }
        }

        public void Save()
        {
            FileStream fs = new FileStream(m_kartotek_dat, FileMode.Truncate, FileAccess.Write, FileShare.None);

            using (StreamWriter sr = new StreamWriter(fs, Encoding.Default))
            {
                var rec = from d in this
                          orderby d.Nr
                          select d;

                foreach (clsMedlem m in rec)
                {
                    sr.WriteLine(m.CsvString);
                }
            }
        }
    }
}
