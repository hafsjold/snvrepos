using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniconta.API.System;
using Uniconta.ClientTools.DataModel;
using Uniconta.Common;
using Uniconta.DataModel;

namespace nsPbs3060
{
    public class clsRSMembership2UniConta
    {
        private CrudAPI m_api { get; set; }
        private CompanyFinanceYear m_CurrentCompanyFinanceYear { get; set; }
        private dbData3060DataContext m_dbData3060 { get; set; }
        private puls3060_nyEntities m_dbPuls3060_dk { get; set; }

        public clsRSMembership2UniConta(dbData3060DataContext p_dbData3060, puls3060_nyEntities p_dbPuls3060_dk, CrudAPI api)
        {
            m_dbData3060 = p_dbData3060;
            m_dbPuls3060_dk = p_dbPuls3060_dk;
            m_api = api;
            var taskFinanceYear = api.Query<CompanyFinanceYear>();
            taskFinanceYear.Wait();
            var resultFinanceYear = taskFinanceYear.Result;
            foreach (var rfy in resultFinanceYear)
            {
                if (rfy._Current)
                {
                    m_CurrentCompanyFinanceYear = rfy;
                }
            }
        }


        public void Subscriber2Invoice()
        {
            var qry_rsmembership = from s in m_dbPuls3060_dk.ecpwt_rsmembership_membership_subscribers
                                   where s.membership_id == 6 && s.status != 2 && s.status != 3
                                   join u in m_dbPuls3060_dk.ecpwt_users on s.user_id equals u.id
                                   select new
                                   {
                                       Navn = u.name,
                                       kontingentBetaltTilDato = s.membership_end,
                                       Kontingent = 250,
                                       subscriber_id = s.id
                                   };

            var antal = qry_rsmembership.Count();
            var rsm = qry_rsmembership.ToArray();
            foreach (var m in rsm)
            {
                var crit = new List<PropValuePair>();
                var pair = PropValuePair.GenereteWhereElements("subscriberid", typeof(String), m.subscriber_id.ToString());
                crit.Add(pair);
                var taskDebtor = m_api.Query<DebtorClientUser>(null, crit);
                taskDebtor.Wait();
                var resultDebtor = taskDebtor.Result;
                var xantal = resultDebtor.Count();
                if (xantal == 1)
                {
                    List<DebtorClientUser> master = new List<DebtorClientUser>();
                    master.Add(resultDebtor[0]);
                    var recDebtor = resultDebtor[0];
                    var crit2 = new List<PropValuePair>();
                    var pair2 = PropValuePair.GenereteWhereElements("Group", typeof(String), "Kont");
                    crit2.Add(pair2);
                    var taskOrder = m_api.Query<DebtorOrderClientUser>(master, crit);
                    taskOrder.Wait();
                    var resultOrder = taskOrder.Result;
                    var yantal = resultOrder.Count();
                    if (yantal == 0)
                    {
                        DateTime root;
                        if ((m.kontingentBetaltTilDato.Month == 1)&& (m.kontingentBetaltTilDato.Day ==1))
                        {
                            root = m.kontingentBetaltTilDato.AddDays(-1);
                        }
                        else
                        {
                            root = m.kontingentBetaltTilDato;
                        }

                        DateTime fradato = new DateTime(root.Year - 1, root.Month, root.Day).AddDays(1);
                        DateTime tildato = new DateTime(root.Year, root.Month, root.Day);
                        DateTime wfakdato = tildato.AddMonths(-1);
                        DateTime fakdato = new DateTime(wfakdato.Year, wfakdato.Month, 12);

                        DebtorOrderClientUser rec_order = new DebtorOrderClientUser()
                        {
                            Account = recDebtor.Account,
                            medlemfra = fradato,
                            medlemtil = tildato,
                            InvoiceInterval = "Årlig",
                            NextInvoice = fakdato,
                            Group = "Kont",
                            DeleteLines = false,
                            DeleteOrder = false
                        };
                        var taskInsert = m_api.Insert(rec_order);
                        taskInsert.Wait();
                        var Err = taskInsert.Result;

                        DebtorOrderLineClient rec_line = new DebtorOrderLineClient()
                        {
                            Qty = 1,
                            Price = 250,
                            Text = string.Format("Puls 3060 Medlemskontingent for {0} for Perioden fra {1} til {2}", m.Navn, rec_order.medlemfra, rec_order.medlemtil),

                        };
                        rec_line.SetMaster(rec_order);
                        var task2Insert = m_api.Insert(rec_line);
                        task2Insert.Wait();
                        Err = task2Insert.Result; 
                    }
                }
            }

        }

        public void Subscriber2Debtor()
        {
            int iNr;

            //s.status == 0 Activ
            //s.status == 1 Pending
            //s.status == 2 Expired
            //s.status == 3 Cancelled
            var qry_rsmembership = from s in m_dbPuls3060_dk.ecpwt_rsmembership_membership_subscribers
                                   where s.membership_id == 6 && s.status != 2 && s.status != 3
                                   join tf in m_dbPuls3060_dk.ecpwt_rsmembership_transactions on s.from_transaction_id equals tf.id
                                   join tl in m_dbPuls3060_dk.ecpwt_rsmembership_transactions on s.last_transaction_id equals tl.id
                                   join m in m_dbPuls3060_dk.ecpwt_rsmembership_subscribers on s.user_id equals m.user_id
                                   join u in m_dbPuls3060_dk.ecpwt_users on s.user_id equals u.id
                                   select new
                                   {
                                       Nr = m.f14,
                                       Navn = u.name,
                                       Adresse = m.f1,
                                       Postnr = m.f4,
                                       Bynavn = m.f2,
                                       telefon = m.f6,
                                       indmeldelsesDato = tf.date,
                                       kontingentBetaltTilDato = s.membership_end,
                                       Kontingent = tl.price,
                                       s.user_id,
                                       tl.user_data,
                                       subscriber_id = s.id
                                   };

            var antal = qry_rsmembership.Count();
            var rsm = qry_rsmembership.ToArray();
            foreach (var m in rsm)
            {
                // opdater Nr fra m.user_data 
                User_data recud = clsHelper.unpack_UserData(m.user_data);
                if (!string.IsNullOrEmpty(recud.memberid))
                {
                    iNr = int.Parse(recud.memberid);
                }
                else
                {
                    if (!string.IsNullOrEmpty(m.Nr))
                        iNr = int.Parse(m.Nr);
                    else
                        iNr = (int)(from r in m_dbData3060.nextval("memberid") select r.id).First();
                }
                var email = recud.email;

                DebtorClientUser rec_debtor = new DebtorClientUser()
                {
                    Account = iNr.ToString(),
                    Name = m.Navn,
                    Address1 = m.Adresse,
                    ZipCode = m.Postnr,
                    City = m.Bynavn,
                    subscriberid = m.subscriber_id.ToString(),
                    Phone = m.telefon,
                    Group = "Medlem",
                    ContactEmail = email,
                    InvoiceEmail = email,
                };

                var crit = new List<PropValuePair>();
                var pair = PropValuePair.GenereteWhereElements("subscriberid", typeof(String), rec_debtor.subscriberid);
                crit.Add(pair);
                var taskDebtor = m_api.Query<DebtorClientUser>(null, crit);
                taskDebtor.Wait();
                var resultDebtor = taskDebtor.Result;
                var xantal = resultDebtor.Count();
                if (xantal == 0)
                {
                    var taskInsert = m_api.Insert(rec_debtor);
                    taskInsert.Wait();
                    var Err = taskInsert.Result;
                }
            }

        }

        public void testDebtor()
        {
            var taskDebtor = m_api.Query<DebtorClientUser>();
            taskDebtor.Wait();
            var resultDebtor = taskDebtor.Result;
            foreach (var xx in resultDebtor)
            {

            }


        }
    }




    public class DebtorClientUser : DebtorClient
    {
        public string subscriberid
        {
            get { return this.GetUserFieldString(0); }
            set { this.SetUserFieldString(0, value); }
        }

    }

    public class DebtorOrderClientUser : DebtorOrderClient
    {
        public DateTime medlemfra
        {
            get { return this.GetUserFieldDateTime(0); }
            set { this.SetUserFieldDateTime(0, value); }
        }

        public DateTime medlemtil
        {
            get { return this.GetUserFieldDateTime(1); }
            set { this.SetUserFieldDateTime(1, value); }
        }

    }
}