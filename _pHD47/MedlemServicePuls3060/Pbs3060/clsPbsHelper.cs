﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Pbs3060
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

            puls3060_nyEntities jdbd = new puls3060_nyEntities();
            clsPbs601 objPbs601d = new clsPbs601();
            List<string[]> items = objPbs601d.RSMembership_KontingentForslag(p_DatoBetaltKontingentTil, m_dbData3060);
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

                Tuple<int, int> tresulte = objPbs601d.rsmembeshhip_kontingent_fakturer_bs1(m_dbData3060, jdbd, memKontingentforslag, pbsType.betalingsservice);
                int AntalFakturaer = tresulte.Item1;
                int lobnr = tresulte.Item2;
                if ((AntalFakturaer > 0))
                {
                    //pbsType.indbetalingskort
                    objPbs601d.faktura_og_rykker_601_action_lobnr(m_dbData3060, lobnr);
                    clsSFTP objSFTPd = new clsSFTP(m_dbData3060);
                    objSFTPd.WriteTilSFtp(m_dbData3060, lobnr);
                    objSFTPd.DisconnectSFtp();
                    objSFTPd = null;
                }

                tresulte = objPbs601d.rsmembeshhip_kontingent_fakturer_bs1(m_dbData3060, jdbd, memKontingentforslag, pbsType.indbetalingskort);
                AntalFakturaer = tresulte.Item1;
                lobnr = tresulte.Item2;
                if ((AntalFakturaer > 0))
                {
                    //pbsType.indbetalingskort
                    objPbs601d.faktura_og_rykker_601_action_lobnr(m_dbData3060, lobnr);
                    clsSFTP objSFTPd = new clsSFTP(m_dbData3060);
                    objSFTPd.WriteTilSFtp(m_dbData3060, lobnr);
                    objSFTPd.DisconnectSFtp();
                    objSFTPd = null;
                }

                Console.WriteLine(string.Format("Medlem3060Service {0} end", "Send Kontingent File til PBS"));
            }

        }

        public void opdaterKanSlettes()

        {
            puls3060_nyEntities jdb = new puls3060_nyEntities();
            jdb.Database.ExecuteSqlCommand(@"
INSERT INTO ecpwt_user_usergroup_map  (user_id, group_id) 
SELECT DISTINCT u.id, 20   
FROM ecpwt_users u 
JOIN ecpwt_rsmembership_membership_subscribers m ON u.id = m.user_id 
JOIN ecpwt_rsmembership_transactions t ON m.last_transaction_id = t.id 
JOIN ecpwt_rsmembership_subscribers a ON m.user_id = a.user_id 
WHERE 20 NOT IN (SELECT ugm.group_id FROM ecpwt_user_usergroup_map ugm WHERE ugm.user_id = u.id) 
  AND (m.membership_id = 6 
  AND (m.status = 2 AND m.membership_end < DATE_ADD(  NOW(), INTERVAL -100 DAY ))
  OR  (m.status = 3 AND m.membership_end < NOW() ) );
            ");
        }

        public int OpdateringAfSlettet_rsmembership_transaction(int p_trans_id, dbData3060DataContext p_dbData3060)
        {
            int out_trans_id = p_trans_id;
            puls3060_nyEntities dbPuls3060_dk = new puls3060_nyEntities();

            var qry1 = from s1 in dbPuls3060_dk.ecpwt_rsmembership_transactions where s1.id == p_trans_id select s1;
            int c1 = qry1.Count();
            if (c1 == 0)
            {
                var qry2 = from s2 in p_dbData3060.TblrsmembershipTransactions where s2.TransId == p_trans_id select s2;
                try
                {
                    TblrsmembershipTransactions t2 = qry2.First();
                    //*****
                    User_data recud = clsHelper.unpack_UserData(t2.UserData);
                    if (string.IsNullOrEmpty(recud.memberid))
                    {
                        recud.memberid = t2.Memberid.ToString();
                        t2.UserData = clsHelper.pack_UserData(recud);
                    }
                    //*****
                    int? subscriber_id = t2.SubscriberId;
                    var qry3 = from s3 in dbPuls3060_dk.ecpwt_rsmembership_membership_subscribers where s3.id == subscriber_id select s3;
                    int c3 = qry3.Count();
                    if (c3 == 1)
                    {
                        DateTime wDate = t2.Date;
                        int alder = DateTime.Now.Subtract(t2.Date).Days;
                        if (alder > 80) wDate = DateTime.Now.AddDays(-60);

                        ecpwt_rsmembership_transactions t1 = new ecpwt_rsmembership_transactions
                        {
                            id = p_trans_id,
                            user_id = t2.UserId,
                            user_email = t2.UserEmail,
                            user_data = t2.UserData,
                            type = t2.Type,
                            @params = t2.Params,
                            date = wDate,
                            ip = t2.Ip,
                            price = t2.Price,
                            coupon = t2.Coupon,
                            currency = t2.Currency,
                            hash = t2.Hash,
                            custom = t2.Custom,
                            gateway = t2.Gateway,
                            status = t2.Status,
                            response_log = t2.ResponseLog
                        };
                        dbPuls3060_dk.ecpwt_rsmembership_transactions.Add(t1);
                        dbPuls3060_dk.SaveChanges();

                        ecpwt_rsmembership_transactions rec_trans = (from t in dbPuls3060_dk.ecpwt_rsmembership_transactions where t.custom == t2.Custom select t).First();

                        string sql = string.Format(@"UPDATE ecpwt_rsmembership_transactions SET id = {0} WHERE id = {1}", p_trans_id, rec_trans.id);
                        dbPuls3060_dk.Database.ExecuteSqlCommand(sql);

                        rec_trans = (from t in dbPuls3060_dk.ecpwt_rsmembership_transactions where t.custom == t2.Custom select t).First();
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

        public static void update_rsmembership_transactions_user_data()
        {
            puls3060_nyEntities db = new puls3060_nyEntities();
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

            int antal = qry.Count();
            int n = 0;
            foreach (var trn in qry)
            {
                n++;
                try
                {
                    ecpwt_rsmembership_transactions_user_data rec = unpack_UserData(trn.id, trn.user_data, trn.user_email);
                    db.ecpwt_rsmembership_transactions_user_data.Local.Add(rec);
                }
                catch { }
                //if (n > 5) break;
            }
            db.SaveChanges();
        }

        public static ecpwt_rsmembership_transactions_user_data unpack_UserData(int id, string user_data, string user_email)
        {
            ecpwt_rsmembership_transactions_user_data rec = new ecpwt_rsmembership_transactions_user_data();
            rec.id = id;
            string st_php = "a" + user_data.Substring(14);
            Pbs3060.Serializer serializer = new Pbs3060.Serializer();
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