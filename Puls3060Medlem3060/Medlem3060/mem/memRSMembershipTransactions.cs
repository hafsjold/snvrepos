using nsPbs3060;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsPuls3060
{
    public class WMemRSMembershipTransactions : List<recRSMembershipTransactions>
    {
        public WMemRSMembershipTransactions()
        {
            puls3060_dkEntities jdb = new puls3060_dkEntities();

            var qrytrans = from t in jdb.ecpwt_rsmembership_transactions
                           where t.status == "pending" && t.type == "new"
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
                     ip = tr.ip,
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
                rec.memberid = ((string)fields["memberid"] != "") ? (string)fields["memberid"] : (10000 + tr.user_id).ToString();
                rec.kon = (string)((ArrayList)((Hashtable)php["membership_fields"])["kon"])[0];
                rec.fodtaar = (string)((ArrayList)((Hashtable)php["membership_fields"])["fodtaar"])[0];
                rec.message = (string)((Hashtable)php["membership_fields"])["message"];
                rec.password = (string)php["password"];
                
                this.Add(rec);
            }
        }
    }
}
