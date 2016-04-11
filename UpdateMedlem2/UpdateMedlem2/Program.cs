using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Collections;

namespace UpdateMedlem2
{
    class Program
    {
        static void Main(string[] args)
        {
            dbPuls3060 db = new dbPuls3060();
            db.ChangeDatabase
                (
                    initialCatalog: "puls3060_dk",
                    userId: "puls3060",
                    password: "tasja123",
                    integratedSecuity: false,
                    dataSource: @"mysql3.gigahost.dk"
                );
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
                ecpwt_rsmembership_transactions_user_data rec = unpack_UserData(trn.id, trn.user_data, trn.user_email, trn.user_id);
                db.ecpwt_rsmembership_transactions_user_data.Local.Add(rec);
                //if (n > 5) break;
            }
            db.SaveChanges();
        }

        public static ecpwt_rsmembership_transactions_user_data unpack_UserData(int id, string user_data, string user_email, int user_id)
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

    public class User_data
    {
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string adresse { get; set; }
        public string postnr { get; set; }
        public string bynavn { get; set; }
        public string mobil { get; set; }
        public string memberid { get; set; }
        public string kon { get; set; }
        public string fodtaar { get; set; }
        public string message { get; set; }
        public string fiknr { get; set; }
        public string password { get; set; }
    }
}
