using nsPbs3060;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsPuls3060
{
    public class recRSMembershipTransactions
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string user_data { get; set; }
        public string type { get; set; }
        public string @params { get; set; }
        public DateTime date { get; set; }
        public Decimal price { get; set; }
        public string coupon { get; set; }
        public string currency { get; set; }
        public string hash { get; set; }
        public string custom { get; set; }
        public string gateway { get; set; }
        public string status { get; set; }
        public string response_log { get; set; }

        public string name { get; set; }
        public string username { get; set; }
        public string adresse { get; set; }
        public string postnr { get; set; }
        public string bynavn { get; set; }
        public string mobil { get; set; }
        public string memberid { get; set; }
        public string kon { get; set; }
        public string fodtaar { get; set; }
        public string message { get; set; }
        public string password { get; set; }
    }

    public class MemRSMembershipTransactions : List<recRSMembershipTransactions>
    {
        public MemRSMembershipTransactions()
        {
            puls3060_dkEntities jdb = new puls3060_dkEntities();

            var qrytrans = from t in jdb.ecpwt_rsmembership_transactions
                           where t.status == "pending"
                           select t;

            int antal = qrytrans.Count();
            foreach (var tr in qrytrans)
            {
                recRSMembershipTransactions rec = new recRSMembershipTransactions {
                    id = tr.id,
                     user_id = tr.user_id,
                     user_data = tr.user_data,
                     type = tr.type,
                     @params = tr.@params,
                     date = tr.date,
                     price = tr.price,
                     coupon = tr.coupon,
                     currency = tr.currency,
                     hash = tr.hash,
                     custom = tr.custom,
                     gateway = tr.gateway,
                     status = tr.status,
                     response_log = tr.response_log,
                };
    
                string st_php = "a" + tr.user_data.Substring(14);
                PHPSerializationLibrary.Serializer serializer = new PHPSerializationLibrary.Serializer();
                Hashtable php = (Hashtable)serializer.Deserialize(st_php);

                rec.name = (string)php["name"];
                rec.username = (string)php["username"];
                Hashtable fields = (Hashtable)php["fields"];
                rec.adresse = (string)fields["adresse"];
                rec.postnr = (string)fields["postnr"];
                rec.bynavn = (string)fields["bynavn"];
                rec.mobil = (string)fields["mobil"];
                rec.memberid = (string)fields["memberid"];
                Hashtable membership_fields = (Hashtable)php["membership_fields"];
                ArrayList arr_kon = (ArrayList)membership_fields["kon"];
                rec.kon = (string)arr_kon[0];
                ArrayList arr_fodtaar = (ArrayList)membership_fields["fodtaar"];
                rec.fodtaar = (string)arr_fodtaar[0];
                rec.message = (string)membership_fields["message"];
                rec.password = (string)php["password"];
                
                this.Add(rec);
            }
        }
    }
}
