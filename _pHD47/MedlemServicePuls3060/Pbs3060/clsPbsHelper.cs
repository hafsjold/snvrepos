using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using Uniconta.API.System;

namespace Pbs3060
{
    public class clsPbsHelper
    {
        public void PbsAutoKontingent(dbData3060DataContext m_dbData3060, CrudAPI api)
        {
            DateTime Nu = new DateTime(2018, 8, 13); //<-------- DEBUG
            //DateTime Nu = DateTime.Now;
            int Dag = Nu.Day;
            if (Dag > 13)
                return;

            DateTime Nu_plus_1 = Nu.AddMonths(1);
            DateTime p_DatoKontingentForfald = new DateTime(Nu_plus_1.Year, Nu_plus_1.Month, 1);
            DateTime Nu_plus_2 = Nu.AddMonths(2);
            DateTime p_DatoBetaltKontingentTil = new DateTime(Nu_plus_2.Year, Nu_plus_2.Month, 12);

            clsPbs601 objPbs601d = new clsPbs601();
            List<string[]> items = objPbs601d.UniConta_KontingentForslag(p_DatoBetaltKontingentTil, m_dbData3060, api);
            int AntalForslag = items.Count();
            if (AntalForslag > 0)
            {
                Console.WriteLine(string.Format("Medlem3060Service {0} begin", "Send Kontingent File til PBS"));
                Memkontingentforslag memKontingentforslag = new Memkontingentforslag();
                foreach (string[] item in items)
                {
                    string user_id = item[0];
                    string name = item[1];
                    DateTime fradato = DateTime.Parse(item[4]);
                    double advisbelob = double.Parse(item[5]);
                    DateTime tildato = DateTime.Parse(item[6]);
                    bool indmeldelse = (item[7] == "J") ? true : false;
                    bool tilmeldtpbs = (item[8] == "J") ? true : false;
                    int subscriber_id = int.Parse(item[9]);
                    int memberid = (!string.IsNullOrEmpty(item[10])) ? int.Parse(item[10]) : m_dbData3060.nextval_v2("memberid");

                    recKontingentforslag rec_Kontingentforslag = new recKontingentforslag
                    {
                        betalingsdato = clsHelper.bankdageplus(p_DatoKontingentForfald, 0),
                        bsh = false,
                        user_id = int.Parse(user_id),
                        membership_id = 6,
                        advisbelob = (decimal)advisbelob,
                        fradato = fradato,
                        tildato = tildato,
                        indmeldelse = indmeldelse,
                        tilmeldtpbs = tilmeldtpbs,
                        subscriber_id = subscriber_id,
                        memberid = memberid,
                        name = name
                    };
                    memKontingentforslag.Add(rec_Kontingentforslag);
                }

                Tuple<int, int> tresulte = objPbs601d.UniConta_kontingent_fakturer_bs1(m_dbData3060, memKontingentforslag, pbsType.betalingsservice);
                int AntalFakturaer = tresulte.Item1;
                int lobnr = tresulte.Item2;
                if ((AntalFakturaer > 0))
                {
                    //pbsType.betalingsservice
                    objPbs601d.faktura_og_rykker_601_action_lobnr(m_dbData3060, lobnr, api);
                    clsSFTP objSFTPd = new clsSFTP(m_dbData3060);
                    objSFTPd.WriteTilSFtp(m_dbData3060, lobnr);
                    objSFTPd.DisconnectSFtp();
                    objSFTPd = null;
                }

                tresulte = objPbs601d.UniConta_kontingent_fakturer_bs1(m_dbData3060, memKontingentforslag, pbsType.indbetalingskort);
                AntalFakturaer = tresulte.Item1;
                lobnr = tresulte.Item2;
                if ((AntalFakturaer > 0))
                {
                    //pbsType.indbetalingskort
                    objPbs601d.faktura_og_rykker_601_action_lobnr(m_dbData3060, lobnr, api);
                    clsSFTP objSFTPd = new clsSFTP(m_dbData3060);
                    objSFTPd.WriteTilSFtp(m_dbData3060, lobnr);
                    objSFTPd.DisconnectSFtp();
                    objSFTPd = null;
                }

                Console.WriteLine(string.Format("Medlem3060Service {0} end", "Send Kontingent File til PBS"));
            }

        }
    }
}
