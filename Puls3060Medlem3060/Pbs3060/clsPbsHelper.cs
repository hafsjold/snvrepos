using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsPbs3060
{
    public class clsPbsHelper
    {
        public void PbsAutoKontingent(dbData3060DataContext m_dbData3060) 
        {
            DateTime Nu = new DateTime(2016, 7, 10);//DateTime.Now;
            int Dag = Nu.Day;
            if (Dag > 12)
                return;

            DateTime Nu_plus_1 = Nu.AddMonths(1);
            DateTime p_DatoKontingentForfald = new DateTime(Nu_plus_1.Year, Nu_plus_1.Month, 1);
            DateTime p_DatoBetaltKontingentTil = Nu.AddMonths(2);

            puls3060_dkEntities jdbd = new puls3060_dkEntities();
            clsPbs601 objPbs601d = new clsPbs601();
            List<string[]> items = objPbs601d.RSMembership_KontingentForslag(p_DatoBetaltKontingentTil, m_dbData3060);
            int AntalForslag = items.Count();
            if (AntalForslag > 0)
            {
                Program.Log(string.Format("Medlem3060Service {0} begin", "Send Kontingent File til PBS"));
                Memkontingentforslag memKontingentforslag = new Memkontingentforslag();
                foreach (string[] item in items)
                {
                    string user_id = item[0];
                    DateTime fradato = DateTime.Parse(item[4]);
                    double advisbelob = double.Parse(item[5]);
                    DateTime tildato = DateTime.Parse(item[6]);
                    bool indmeldelse = (item[7] == "J") ? true : false;
                    bool tilmeldtpbs = (item[8] == "J") ? true : false;

                    recKontingentforslag rec_Kontingentforslag = new recKontingentforslag
                    {
                        betalingsdato = clsOverfoersel.bankdageplus(p_DatoKontingentForfald, 0),
                        bsh = false,
                        user_id = int.Parse(user_id),
                        membership_id = 6,
                        advisbelob = (decimal)advisbelob,
                        fradato = fradato,
                        tildato = tildato,
                        indmeldelse = indmeldelse,
                        tilmeldtpbs = tilmeldtpbs,
                    };
                    memKontingentforslag.Add(rec_Kontingentforslag);
                }

                Tuple<int, int> tresulte = objPbs601d.rsmembeshhip_kontingent_fakturer_bs1(m_dbData3060, jdbd, memKontingentforslag);
                int AntalFakturaer = tresulte.Item1;
                int lobnr = tresulte.Item2;
                if ((AntalFakturaer > 0))
                {
                    objPbs601d.faktura_og_rykker_601_action(m_dbData3060, lobnr, fakType.fdrsmembership);
                    clsSFTP objSFTPd = new clsSFTP(m_dbData3060);
                    objSFTPd.WriteTilSFtp(m_dbData3060, lobnr);
                    objSFTPd.DisconnectSFtp();
                    objSFTPd = null;
                }
                Program.Log(string.Format("Medlem3060Service {0} end", "Send Kontingent File til PBS"));
            }

        }
    }
}
