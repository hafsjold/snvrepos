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
                var critDebtor = new List<PropValuePair>();
                var pairDebtor = PropValuePair.GenereteWhereElements("subscriberid", typeof(String), m.subscriber_id.ToString());
                critDebtor.Add(pairDebtor);
                var taskDebtor = m_api.Query<DebtorClientUser>(null, critDebtor);
                taskDebtor.Wait();
                var resultDebtor = taskDebtor.Result;
                var antalDebtor = resultDebtor.Count();

                if (antalDebtor == 1)
                {
                    var recDebtor = resultDebtor[0];

                    var masterDebtorOrder = new List<DebtorClientUser>();
                    masterDebtorOrder.Add(recDebtor);
                    var critDebtorOrder = new List<PropValuePair>();
                    var pairDebtorOrder = PropValuePair.GenereteWhereElements("Group", typeof(String), "Kont");
                    critDebtorOrder.Add(pairDebtorOrder);
                    var taskOrder = m_api.Query<DebtorOrderClientUser>(masterDebtorOrder, critDebtorOrder);
                    taskOrder.Wait();
                    var resultOrder = taskOrder.Result;
                    var antalOrder = resultOrder.Count();

                    if (antalOrder == 0)
                    {
                        DateTime root;
                        if ((m.kontingentBetaltTilDato.Month == 1) && (m.kontingentBetaltTilDato.Day == 1))
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

                        // Order Header
                        DebtorOrderClientUser rec_order = new DebtorOrderClientUser()
                        {
                            Account = recDebtor.Account,
                            medlemfra = fradato,
                            medlemtil = tildato,
                            InvoiceInterval = "Årlig",
                            NextInvoice = fakdato,
                            Group = "Kont",
                            DeleteLines = false,
                            DeleteOrder = false,

                        };
                        var taskInsert = m_api.Insert(rec_order);
                        taskInsert.Wait();
                        var Err = taskInsert.Result;

                        // Order Lines
                        List<UnicontaBaseEntity> new_recs = new List<UnicontaBaseEntity>();
                        DebtorOrderLineClientUser rec_line = new DebtorOrderLineClientUser()
                        {
                            Qty = 1,
                            Price = 250,
                            Text = string.Format("Puls 3060 Medlemskontingent for {0} for Perioden fra {1} til {2}", m.Navn, rec_order.medlemfra.ToShortDateString(), rec_order.medlemtil.ToShortDateString()),
                            lintype = "Kontingent"
                        };
                        rec_line.SetMaster(rec_order);
                        new_recs.Add(rec_line);

                        /*
                        rec_line = new DebtorOrderLineClient()
                        {
                            Qty = 1,
                            Price = 15,
                            Text ="Administrationsgebyr",
                            lintype = "Gebyr"
                        };
                        rec_line.SetMaster(rec_order);
                        new_recs.Add(rec_line);
                        */

                        var task2Insert = m_api.Insert(new_recs);
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
                var critDebtor = new List<PropValuePair>();
                var pairDebtor = PropValuePair.GenereteWhereElements("subscriberid", typeof(String), m.subscriber_id.ToString());
                critDebtor.Add(pairDebtor);
                var taskDebtor = m_api.Query<DebtorClientUser>(null, critDebtor);
                taskDebtor.Wait();
                var resultDebtor = taskDebtor.Result;
                var antalDebtor = resultDebtor.Count();
                if (antalDebtor == 0)
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
                    var taskDebtorInsert = m_api.Insert(rec_debtor);
                    taskDebtorInsert.Wait();
                    var Err = taskDebtorInsert.Result;
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
                DebtorInvoiceLines cc;
            }
        }

    }

}