using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniconta.ClientTools.DataModel;
using Uniconta.DataModel;
using Uniconta.Common;
using Uniconta.API.GeneralLedger;
using System.IO;
using System.Security.Cryptography;

namespace Trans2SummaHDC
{
    public class clsUnicontaKladde
    {
        string m_BilagPath;

        public clsUnicontaKladde()
        {
            KarTrans2Summa obj = new KarTrans2Summa();
            m_BilagPath = obj.BilagPath();
        }
        async public void InsertAllVouchersClient()
        {
            var qryKladder = from k in Program.karKladder orderby k.Bilag, k.Id select k;
            int antal = qryKladder.Count();
            foreach (var rec in qryKladder)
            {
                int refbilag = await InsertVouchersClients(rec);
            }
        }

        async public void DeleteVouchersClients()
        {
            var api = UCInitializer.GetBaseAPI;
            var collection = await api.Query<VouchersClient>();
            foreach (var vc in collection)
            {
                if (vc.Voucher == 0)
                {
                    var err = await api.Delete(vc);
                }
            }
        }

        async public void InsertGLDailyJournalLinesYearEnd()
        {
            var api = UCInitializer.GetBaseAPI;
            var col3 = await api.Query<NumberSerieClient>();

            var crit = new List<PropValuePair>();
            var pair = PropValuePair.GenereteWhereElements("KeyStr", typeof(String), "Dag");
            crit.Add(pair);
            var col = await api.Query<GLDailyJournalClient>(null, crit);
            var rec_Master = col.FirstOrDefault();


            var qryPosteringer = from p in Program.karPosteringer
                                 where p.Bilag == 0 && (p.Tekst.StartsWith("ÅP:") || p.Tekst.StartsWith("EP:"))
                                 orderby p.Konto, p.Nr
                                 select p;

            int antal = qryPosteringer.Count();

            DateTime Dato_last = DateTime.Today;
            int Konto_last = 0;
            decimal Nettobeløb_sum = 0;

            foreach (var p in qryPosteringer)
            {
                if (p.Konto != Konto_last)
                {
                    if (Konto_last != 0)
                    {
                        GLDailyJournalLineClient jl = new GLDailyJournalLineClient()
                        {
                            Date = Dato_last,
                            Voucher = 9999,
                            Text = "Primo SummaSummarum",
                            Account = KarNyKontoplan.NytKontonr(Konto_last)
                        };

                        if (Nettobeløb_sum > 0)
                        {
                            jl.Debit = (double)Nettobeløb_sum;
                        }
                        else
                        {
                            jl.Credit = -(double)Nettobeløb_sum;
                        }

                        jl.SetMaster(rec_Master);
                        var err = await api.Insert(jl);

                    }
                    Nettobeløb_sum = 0;
                }
                Dato_last = p.Dato;
                Konto_last = p.Konto;
                Nettobeløb_sum += p.Nettobeløb;
            }

            if (antal > 0)
            {
                GLDailyJournalLineClient jl = new GLDailyJournalLineClient()
                {
                    Date = Dato_last,
                    Voucher = 9999,
                    Text = "Primo SummaSummarum",
                    Account = KarNyKontoplan.NytKontonr(Konto_last)
                };

                if (Nettobeløb_sum > 0)
                {
                    jl.Debit = (double)Nettobeløb_sum;
                }
                else
                {
                    jl.Credit = -(double)Nettobeløb_sum;
                }

                jl.SetMaster(rec_Master);
                var err = await api.Insert(jl);
            }
        }

        async public void InsertGLDailyJournalLines()
        {
            var api = UCInitializer.GetBaseAPI;
            //var col3 = await api.Query<NumberSerieClient>();

            var crit = new List<PropValuePair>();
            var pair = PropValuePair.GenereteWhereElements("KeyStr", typeof(String), "Dag");
            crit.Add(pair);
            var col = await api.Query<GLDailyJournalClient>(null, crit);
            var rec_Master = col.FirstOrDefault();

            var qryKladder = from k in Program.karKladder
                                 //where k.Bilag > 183 // <-----------------------
                             orderby k.Bilag, k.Id
                             select k;
            int antal = qryKladder.Count();

            foreach (var k in qryKladder)
            {
                //if (k.Bilag > 3)
                //    break;
                int refbilag = await InsertVouchersClients(k);

                GLDailyJournalLineClient jl = new GLDailyJournalLineClient()
                {
                    Date = (DateTime)k.Dato,
                    Voucher = (k.Bilag != null) ? (int)k.Bilag : 0,
                    Text = k.Tekst,
                    DocumentRef = refbilag,
                    Vat = MomsKodeKonvertering(k.Momskode)
                };


                if (!String.IsNullOrWhiteSpace(k.Afstemningskonto)) //Afstemningskonto er udfyldt
                {
                    if (k.Belob > 0)
                    {
                        jl.Debit = (double)k.Belob;
                    }
                    else
                    {
                        jl.Credit = -(double)k.Belob;
                    }
                }
                else //Afstemningskonto er ikke udfyldt
                {
                    if (k.Belob > 0)
                    {
                        jl.Credit = (double)k.Belob;
                    }
                    else
                    {
                        jl.Debit = -(double)k.Belob;
                    }
                }

                if (KarNyKontoplan.NytKontonr(k.Afstemningskonto) != string.Empty)
                {
                    jl.Account = KarNyKontoplan.NytKontonr(k.Afstemningskonto);

                    if (KarNyKontoplan.NytKontonr(k.Konto) != string.Empty)
                    {
                        jl.OffsetAccount = KarNyKontoplan.NytKontonr(k.Konto);
                    }
                }
                else
                {
                    if (KarNyKontoplan.NytKontonr(k.Konto) != string.Empty)
                    {
                        jl.Account = KarNyKontoplan.NytKontonr(k.Konto);
                    }
                }
                jl.SetMaster(rec_Master);
                var err = await api.Insert(jl);
                if (err != ErrorCodes.Succes)
                {
                    int xx = 1;
                }
            }
        }

        async public Task<int> InsertVouchersClients(recKladder rec)
        {
            string mask_bilag = m_BilagPath + @"\Bilag {0}.pdf";
            var api = UCInitializer.GetBaseAPI;
            string hash = null;
            byte[] attachment = null;
            DateTime file_date = DateTime.Now;
            try
            {
                string file_bilag = string.Format(mask_bilag, rec.Bilag);
                var fileinfo = new FileInfo(file_bilag);
                int file_bilag_length = (int)fileinfo.Length;
                file_date = fileinfo.CreationTime;
                attachment = new byte[file_bilag_length];
                FileStream ts = new FileStream(file_bilag, FileMode.Open, FileAccess.Read, FileShare.None);
                ts.Read(attachment, 0, file_bilag_length);
                ts.Close();

                using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
                {
                    hash = Convert.ToBase64String(sha1.ComputeHash(attachment));
                }
                var crit = new List<PropValuePair>();
                var pair = PropValuePair.GenereteWhereElements("PostingInstruction", typeof(String), hash);
                crit.Add(pair);
                var collection = await api.Query<VouchersClient>(null, crit);
                if (collection.Count() > 0)
                    return collection[0].PrimaryKeyId;
            }
            catch
            {
                return 0;
            }

            VouchersClient vc = new VouchersClient()
            {
                Text = string.Format("Bilag {0}", rec.Bilag),
                DocumentDate = file_date,
                Fileextension = FileextensionsTypes.PDF,
                VoucherAttachment = attachment,
                PostingInstruction = hash
            };
            var err = await api.Insert(vc);
            return vc.PrimaryKeyId;
        }

        public void InsertSalgsfakturaer()
        {
            int? lastFakid = null;
            DebtorOrderClient recOrder = null;

            var rec_regnskab = Program.qryAktivRegnskab();
            var qrySFak = from sfv in Program.karFakturavarer_s
                          join sf in Program.karFakturaer_s on new { fakid = sfv.Fakid } equals new { fakid = sf.fakid }
                         // where sf.faknr != 0 && sf.faktype == 0
                          where sf.faknr > 28 && sf.faktype == 0
                          orderby sfv.Fakid, sfv.Line
                          select new
                          {
                              Regnskabid = rec_regnskab.Rid,
                              Sk = "S",
                              Fakid = sfv.Fakid,
                              Faknr = sf.faknr,
                              Dato = sf.dato,
                              forfdato = sf.forfdato,
                              debitornr = sf.debitornr,
                              Faklinnr = sfv.Line,
                              Varenr = sfv.Varenr,
                              Tekst = sfv.VareTekst,
                              Konto = sfv.Bogfkonto,
                              Momskode = KarKontoplan.getMomskode(sfv.Bogfkonto),
                              Antal = sfv.Antal,
                              Enhed = sfv.Enhed,
                              Pris = sfv.Pris,
                              Rabat = sfv.Rabat,
                              Moms = sfv.Moms,
                              Nettobelob = sfv.Nettobelob,
                              Bruttobelob = sfv.Bruttobelob,
                          };

            int antal = qrySFak.Count();

            var api = UCInitializer.GetBaseAPI;
            //var col3 = await api.Query<DebtorOrderClient>();
            //var col4 = await api.Query<DebtorOrderLineClient>();

            foreach (var s in qrySFak)
            {
                if ((!(s.Fakid == 0)) && (lastFakid != s.Fakid))
                {
                    try
                    {
                        var crit = new List<PropValuePair>();
                        var pair = PropValuePair.GenereteWhereElements("OrderNumber", typeof(int), s.Fakid.ToString());
                        crit.Add(pair);
                        var taskDebtorOrder = api.Query<DebtorOrderClient>(null, crit);
                        taskDebtorOrder.Wait();
                        var col = taskDebtorOrder.Result;
                        if (col.Count() == 0)
                        {
                            recOrder = new DebtorOrderClient()
                            {
                                OrderNumber = s.Fakid,
                                Account = s.debitornr.ToString(),
                                InvoiceDate = s.Dato,
                                DeliveryDate = s.Dato,


                            };
                            var taskInsertDebtorOrder = api.Insert(recOrder);
                            taskInsertDebtorOrder.Wait();
                            var err = taskInsertDebtorOrder.Result;
                        }
                        else
                        {
                            recOrder = col[0];
                        }
                    }
                    catch { }
                }

                DebtorOrderLineClient recOrderLine = new DebtorOrderLineClient()
                {
                    Text = s.Tekst,
                    Qty = (double)s.Antal,
                    Price = (double)s.Pris,
                    PostingAccount = KarNyKontoplan.NytKontonr(s.Konto),
                    Vat = MomsKodeKonvertering(s.Momskode)
                };
                recOrderLine.SetMaster(recOrder);
                var taskInsertDebtorOrderLine = api.Insert(recOrderLine);
                taskInsertDebtorOrderLine.Wait();
                var err1 = taskInsertDebtorOrderLine.Result;
            }

        }

        public void InsertKøbsfakturaer()
        {
            int? lastFakid = null;
            CreditorOrderClient recOrder = null;

            var rec_regnskab = Program.qryAktivRegnskab();
            var qryKFak = from kfv in Program.karFakturavarer_k
                          join kf in Program.karFakturaer_k on new { fakid = kfv.Fakid } equals new { fakid = kf.fakid }
                          where kf.faknr != 0 && (kf.faktype == 2 || kf.faktype == 3)
                          orderby kfv.Fakid, kfv.Line
                          select new
                          {
                              Regnskabid = rec_regnskab.Rid,
                              Sk = "K",
                              Fakid = kfv.Fakid,
                              Faknr = kf.faknr,
                              Dato = kf.dato,
                              kreditornr = kf.kreditornr,
                              Faklinnr = kfv.Line,
                              Varenr = kfv.Varenr,
                              Tekst = kfv.VareTekst,
                              Konto = kfv.Bogfkonto,
                              Momskode = KarKontoplan.getMomskode(kfv.Bogfkonto),
                              Faktype = kf.faktype,
                              Antal = kfv.Antal,
                              Enhed = kfv.Enhed,
                              Pris = kfv.Pris,
                              Rabat = kfv.Rabat,
                              Moms = kfv.Moms,
                              Nettobelob = kfv.Nettobelob,
                              Bruttobelob = kfv.Bruttobelob,
                          };

            int antal = qryKFak.Count();

            var api = UCInitializer.GetBaseAPI;
            //var col3 = await api.Query<CreditorOrderClient>();
            //var col4 = await api.Query<CreditorOrderLineClient>();

            foreach (var k in qryKFak)
            {
                if ((!(k.Fakid == 0)) && (lastFakid != k.Fakid))
                {
                    try
                    {
                        var crit = new List<PropValuePair>();
                        var pair = PropValuePair.GenereteWhereElements("OrderNumber", typeof(int), k.Fakid.ToString());
                        crit.Add(pair);
                        var taskCreditorOrder = api.Query<CreditorOrderClient>(null, crit);
                        taskCreditorOrder.Wait();
                        var col = taskCreditorOrder.Result;
                        if (col.Count() == 0)
                        {
                            recOrder = new CreditorOrderClient()
                            {
                                OrderNumber = k.Fakid,
                                Account = k.kreditornr.ToString(),
                                InvoiceDate = k.Dato,
                                DeliveryDate = k.Dato,


                            };
                            var taskInsertCreditorOrder = api.Insert(recOrder);
                            taskInsertCreditorOrder.Wait();
                            var err = taskInsertCreditorOrder.Result;
                        }
                        else
                        {
                            recOrder = col[0];
                        }
                    }
                    catch { }
                }

                double wAntal = 0;
                if (k.Faktype == 2) //Købsfaktura
                {
                    if (k.Antal != null)
                    {
                        wAntal = (double)k.Antal;
                    }
                    else
                        wAntal = 1;
                }
                else if (k.Faktype == 3) //Købskreditnota
                {
                    if (k.Antal != null)
                    {
                        wAntal = -(double)k.Antal;
                    }
                    else
                        wAntal = -11;

                }

                CreditorOrderLineClient recOrderLine = new CreditorOrderLineClient()
                {
                    Text = k.Tekst,
                    Qty = k.Antal != null ? (double)k.Antal : 1 ,
                    Price = (double)k.Pris,
                    PostingAccount = KarNyKontoplan.NytKontonr(k.Konto),
                    Vat = MomsKodeKonvertering(k.Momskode)
                };
                recOrderLine.SetMaster(recOrder);
                var taskInsertCreditorOrderLine = api.Insert(recOrderLine);
                taskInsertCreditorOrderLine.Wait();
                var err1 = taskInsertCreditorOrderLine.Result;
            }

        }

        public string MomsKodeKonvertering(string momskode)
        {
            if (!string.IsNullOrEmpty(momskode)){
                switch (momskode.ToUpper())
                {
                    case "S25":  //Standard Salg udgående moms
                        return "U25";

                    case "K25":  //Standard Køb indgående moms
                        return "I25";

                    case "Y25": //Udlandsmoms ydelser Køb indgående moms
                        return "IY25";

                    case "U25": //Udlandsmoms varer Køb indgående moms
                        return "IY25";  //rettet fra "IV25";

                    default:
                        return null;
                }
            }
            return null;
        }
    }
}
