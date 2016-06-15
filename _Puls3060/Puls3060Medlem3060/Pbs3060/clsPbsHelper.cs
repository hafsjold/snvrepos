using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Collections;

namespace nsPbs3060
{
    public class clsPbsHelper
    {
        public void PbsAutoKontingent(dbData3060DataContext m_dbData3060)
        {
            //DateTime Nu = new DateTime(2016, 7, 10);
            DateTime Nu = DateTime.Now;
            int Dag = Nu.Day;
            if (Dag > 13)
                return;

            DateTime Nu_plus_1 = Nu.AddMonths(1);
            DateTime p_DatoKontingentForfald = new DateTime(Nu_plus_1.Year, Nu_plus_1.Month, 1);
            DateTime Nu_plus_2 = Nu.AddMonths(2);
            DateTime p_DatoBetaltKontingentTil = new DateTime(Nu_plus_2.Year, Nu_plus_2.Month, 12);

            puls3060_nyEntities jdbd = new puls3060_nyEntities(true);
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
                    string name = item[1];
                    DateTime fradato = DateTime.Parse(item[4]);
                    double advisbelob = double.Parse(item[5]);
                    DateTime tildato = DateTime.Parse(item[6]);
                    bool indmeldelse = (item[7] == "J") ? true : false;
                    bool tilmeldtpbs = (item[8] == "J") ? true : false;
                    int subscriber_id = int.Parse(item[9]);
                    int memberid = (!string.IsNullOrEmpty(item[10])) ? int.Parse(item[10]) : (int)(from r in m_dbData3060.nextval("memberid") select r.id).First();

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
                        subscriber_id = subscriber_id,
                        memberid = memberid,
                        name = name
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

        public int OpdateringAfSlettet_rsmembership_transaction(int p_trans_id, dbData3060DataContext p_dbData3060)
        {
            int out_trans_id = p_trans_id;
            puls3060_nyEntities dbPuls3060_dk = new puls3060_nyEntities(true);

            var qry1 = from s1 in dbPuls3060_dk.ecpwt_rsmembership_transactions where s1.id == p_trans_id select s1;
            int c1 = qry1.Count();
            if (c1 == 0)
            {
                var qry2 = from s2 in p_dbData3060.tblrsmembership_transactions where s2.trans_id == p_trans_id select s2;
                try
                {
                    tblrsmembership_transaction t2 = qry2.First();
                    //*****
                    User_data recud = clsHelper.unpack_UserData(t2.user_data);
                    if (string.IsNullOrEmpty(recud.memberid))
                    {
                        recud.memberid = t2.memberid.ToString();
                        t2.user_data = clsHelper.pack_UserData(recud);
                    }
                    //*****
                    int? subscriber_id = t2.subscriber_id;
                    var qry3 = from s3 in dbPuls3060_dk.ecpwt_rsmembership_membership_subscribers where s3.id == subscriber_id select s3;
                    int c3 = qry3.Count();
                    if (c3 == 1)
                    {
                        ecpwt_rsmembership_transactions t1 = new ecpwt_rsmembership_transactions
                        {
                            user_id = t2.user_id,
                            user_email = t2.user_email,
                            user_data = t2.user_data,
                            type = t2.type,
                            @params = t2.@params,
                            date = t2.date,
                            ip = t2.ip,
                            price = t2.price,
                            coupon = t2.coupon,
                            currency = t2.currency,
                            hash = t2.hash,
                            custom = t2.custom,
                            gateway = t2.gateway,
                            status = t2.status,
                            response_log = t2.response_log
                        };
                        dbPuls3060_dk.ecpwt_rsmembership_transactions.Add(t1);
                        dbPuls3060_dk.SaveChanges();

                        ecpwt_rsmembership_transactions rec_trans = (from t in dbPuls3060_dk.ecpwt_rsmembership_transactions where t.custom == t2.custom select t).First();
                        out_trans_id = rec_trans.id;
                    }
                    else
                    {
                        out_trans_id = 0;
                    }
                }
                catch
                {
                    out_trans_id = 0;
                }
            }
            return out_trans_id;
        }

        public void opdaterKanSlettes()
        {
            puls3060_nyEntities jdb = new puls3060_nyEntities(true);
            jdb.Database.ExecuteSqlCommand(@"
INSERT INTO ecpwt_user_usergroup_map  (user_id, group_id) 
SELECT u.id, 17   
FROM ecpwt_users u 
JOIN ecpwt_rsmembership_membership_subscribers m ON u.id = m.user_id 
JOIN ecpwt_rsmembership_transactions t ON m.last_transaction_id = t.id 
JOIN ecpwt_rsmembership_subscribers a ON m.user_id = a.user_id 
WHERE 17 NOT IN (SELECT ugm.group_id FROM ecpwt_user_usergroup_map ugm WHERE ugm.user_id = u.id) 
  AND (m.membership_id = 6 
  AND (m.status = 2 AND m.membership_end < DATE_ADD(  NOW(), INTERVAL -30 DAY ))
  OR  (m.status = 3 AND m.membership_end < NOW() ) );
            ");
        }

        public void JobQMaintenance(dbData3060DataContext p_dbData3060)
        {
            var result = p_dbData3060.ExecuteQuery<dynamic>(@"DELETE FROM dbo.tblJobqueue WHERE starttime < DATEADD(DAY,-2,GETDATE())");
        }

        public static void Update_memberid_in_rsmembership_transaction(recRSMembershipTransactions t1)
        {
            puls3060_nyEntities dbPuls3060_dk = new puls3060_nyEntities(true);

            var qry2 = from s2 in dbPuls3060_dk.ecpwt_rsmembership_transactions where s2.id == t1.id select s2;
            int c2 = qry2.Count();
            if (c2 == 1)
            {
                ecpwt_rsmembership_transactions t2 = qry2.First();
                User_data recud = clsHelper.unpack_UserData(t2.user_data);
                recud.memberid = t1.memberid.ToString();
                t2.user_data = clsHelper.pack_UserData(recud);
                dbPuls3060_dk.SaveChanges();
            }
            return;
        }

        public static void Update_rsmembership_transactions(dbData3060DataContext p_dbData3060)
        {
            puls3060_nyEntities dbPuls3060_dk = new puls3060_nyEntities(true);
            var qry1 = from s1 in p_dbData3060.tblrsmembership_transactions where s1.type == "new" select s1;
            foreach (var t1 in qry1)
            {
                var qry2 = from s2 in dbPuls3060_dk.ecpwt_rsmembership_transactions where s2.id == t1.trans_id select s2;
                int c2 = qry2.Count();
                if (c2 == 1)
                {
                    ecpwt_rsmembership_transactions t2 = qry2.First();
                    User_data recud = clsHelper.unpack_UserData(t2.user_data);
                    if (string.IsNullOrEmpty(recud.memberid))
                    {
                        recud.memberid = t1.memberid.ToString();
                        t2.user_data = clsHelper.pack_UserData(recud);
                        dbPuls3060_dk.SaveChanges();
                    }
                }
            }
            return;
        }

        public static void update_rsmembership_transactions_user_data()
        {
            puls3060_nyEntities db = new puls3060_nyEntities(true);
            db.ecpwt_rsmembership_membership_subscribers.Load();
            db.ecpwt_rsmembership_transactions.Load();
            db.ecpwt_rsmembership_transactions_user_data.Load();

            var qry = from s in db.ecpwt_rsmembership_membership_subscribers.Local
                      where s.membership_id == 6
                      join t in db.ecpwt_rsmembership_transactions.Local on s.last_transaction_id equals t.id
                      join ud in db.ecpwt_rsmembership_transactions_user_data.Local on t.id equals ud.id into data
                      from a in data.DefaultIfEmpty(new ecpwt_rsmembership_transactions_user_data { id = -1, adresse = "", bynavn = "", email = "", fodtaar = "", kon = "", memberid = "", mobil = "", name = "", postnr = "" })
                      where a.id == -1
                      select new
                      {
                          t.id,
                          t.user_data,
                          t.user_email,
                          s.user_id
                      };

            int n = 0;
            foreach (var trn in qry)
            {
                n++;
                try
                {
                    ecpwt_rsmembership_transactions_user_data rec = unpack_UserData(trn.id, trn.user_data, trn.user_email);
                    db.ecpwt_rsmembership_transactions_user_data.Local.Add(rec);
                }
                catch {  }
                //if (n > 5) break;
            }
            db.SaveChanges();
        }

        public static ecpwt_rsmembership_transactions_user_data unpack_UserData(int id, string user_data, string user_email)
        {
            ecpwt_rsmembership_transactions_user_data rec = new ecpwt_rsmembership_transactions_user_data();
            rec.id = id;
            string st_php = "a" + user_data.Substring(14);
            PHPSerializationLibrary.Serializer serializer = new PHPSerializationLibrary.Serializer();
            Hashtable php = (Hashtable)serializer.Deserialize(st_php);

            rec.name = (string)php["name"];
            if (rec.name == null) rec.name = "";

            rec.email = (string)php["email"];
            if (String.IsNullOrEmpty(rec.email)) rec.email = user_email;
            if (rec.email == null) rec.email = "";

            Hashtable fields = (Hashtable)php["fields"];
            rec.adresse = (string)fields["adresse"];
            if (rec.adresse == null) rec.adresse = "";

            rec.postnr = (string)fields["postnr"];
            if (rec.postnr == null) rec.postnr = "";

            rec.bynavn = (string)fields["bynavn"];
            if (rec.bynavn == null) rec.bynavn = "";

            rec.mobil = (string)fields["mobil"];
            if (rec.mobil == null) rec.mobil = "";

            rec.memberid = (string)fields["memberid"];
            if (String.IsNullOrEmpty(rec.memberid)) rec.memberid = "";

            rec.kon = (string)((ArrayList)((Hashtable)php["membership_fields"])["kon"])[0];
            if (rec.kon == null) rec.kon = "";

            rec.fodtaar = (string)((ArrayList)((Hashtable)php["membership_fields"])["fodtaar"])[0];
            if (rec.fodtaar == null) rec.fodtaar = "";

            return rec;
        }
    }
}
