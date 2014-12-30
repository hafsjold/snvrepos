using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace nsPuls3060v2
{
    public class KarKartotek : List<clsKartotek>
    {
        private string m_kartotek_dat;

        public KarKartotek()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
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
                    clsKartotek m = new clsKartotek(read);
                    this.Add(m);
                }
            }
        }
    }
}
