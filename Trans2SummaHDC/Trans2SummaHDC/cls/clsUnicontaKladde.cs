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
                                 orderby p.Nr
                                 select p;

            int antal = qryPosteringer.Count();

            foreach (var p in qryPosteringer)
            {
                GLDailyJournalLineClient jl = new GLDailyJournalLineClient()
                {
                    Date = (DateTime)p.Dato,
                    Voucher = (p.Bilag != null) ? (int)p.Bilag : 0,
                    Text = p.Tekst,
                    Account = KarNyKontoplan.NytKontonr(p.Konto)
                };

                if (p.Nettobeløb > 0)
                {
                    jl.Debit = (double)p.Nettobeløb;
                }
                else
                {
                    jl.Credit = -(double)p.Nettobeløb;
                }

                jl.SetMaster(rec_Master);
                var err = await api.Insert(jl);
            }
        }

        async public void InsertGLDailyJournalLines()
        {
            var api = UCInitializer.GetBaseAPI;
            var col3 = await api.Query<NumberSerieClient>();

            var crit = new List<PropValuePair>();
            var pair = PropValuePair.GenereteWhereElements("KeyStr", typeof(String), "Dag");
            crit.Add(pair);
            var col = await api.Query<GLDailyJournalClient>(null, crit);
            var rec_Master = col.FirstOrDefault();

            var qryKladder = from k in Program.karKladder orderby k.Id select k;
            int antal = qryKladder.Count();

            foreach (var k in qryKladder)
            {
                int refbilag = await InsertVouchersClients(k);

                GLDailyJournalLineClient jl = new GLDailyJournalLineClient()
                {
                    Date = (DateTime)k.Dato,
                    Voucher = (k.Bilag != null) ? (int)k.Bilag : 0,
                    Text = k.Tekst,
                    DocumentRef = refbilag,
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

        async public void InsertSalgsfakturaer()
        {
            int? lastFakid = null;
            tblfak recFak = null;
            var rec_regnskab = Program.qryAktivRegnskab();
            var qrySFak = from sfv in Program.karFakturavarer_s
                          join sf in Program.karFakturaer_s on new { fakid = sfv.Fakid } equals new { fakid = sf.fakid }
                          where sf.faknr != 0 && sf.faktype == 0
                          orderby sfv.Fakid, sfv.Line
                          select new
                          {
                              Regnskabid = rec_regnskab.Rid,
                              Sk = "S",
                              Fakid = sfv.Fakid,
                              Faknr = sf.faknr,
                              Dato = sf.dato,
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
            var col3 = await api.Query<DebtorOrderClient>();
            var col4 = await api.Query<DebtorOrderLineClient>();
            var col5 = await api.Query<DebtorInvoiceClient>();
            var col6 = await api.Query<DebtorInvoiceLines>();


            foreach (var s in qrySFak)
            {
                if ((!(s.Fakid == 0)) && (lastFakid != s.Fakid))
                {
                    try
                    {
                        DebtorOrderClient jl = new DebtorOrderClient()
                        {
                            Account = s.debitornr.ToString(),
                            InvoiceDate = s.Dato,

                        };

                        recFak = (from f in Program.dbDataTransSumma.tblfaks
                                  where f.regnskabid == rec_regnskab.Rid && f.sk == "S" && f.fakid == s.Fakid
                                  select f).First();
                    }
                    catch
                    {
                        recFak = new tblfak
                        {
                            udskriv = true,
                            regnskabid = s.Regnskabid,
                            sk = s.Sk,
                            fakid = s.Fakid,
                            faknr = s.Faknr,
                            dato = s.Dato,
                            konto = s.debitornr
                        };
                        Program.dbDataTransSumma.tblfaks.InsertOnSubmit(recFak);
                    }
                }


                tblfaklin recFaklin = new tblfaklin
                {
                    sk = s.Sk,
                    regnskabid = s.Regnskabid,
                    fakid = s.Fakid,
                    faklinnr = s.Faklinnr,
                    varenr = s.Varenr.ToString(),
                    tekst = s.Tekst,
                    konto = s.Konto,
                    momskode = s.Momskode,
                    antal = s.Antal,
                    enhed = s.Enhed,
                    pris = s.Pris,
                    rabat = s.Rabat,
                    moms = s.Moms,
                    nettobelob = s.Nettobelob,
                    bruttobelob = s.Bruttobelob
                };
                Program.dbDataTransSumma.tblfaklins.InsertOnSubmit(recFaklin);
                if (!(s.Fakid == 0)) recFak.tblfaklins.Add(recFaklin);
                lastFakid = s.Fakid;
            }
            //Program.dbDataTransSumma.SubmitChanges();

        }

        async public void InsertKøbsfakturaer()
        {
            int? lastFakid = null;
            tblfak recFak = null;
            var rec_regnskab = Program.qryAktivRegnskab();
            var qryKFak = from kfv in Program.karFakturavarer_k
                          join kf in Program.karFakturaer_k on new { fakid = kfv.Fakid } equals new { fakid = kf.fakid }
                          where kf.faknr != 0 && kf.faktype == 2
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
            var col3 = await api.Query<CreditorOrderClient>();
            var col4 = await api.Query<CreditorOrderLineClient>();
            var col5 = await api.Query<CreditorInvoiceClient>();
            var col6 = await api.Query<CreditorInvoiceLines>();


            foreach (var k in qryKFak)
            {
                if ((!(k.Fakid == 0)) && (lastFakid != k.Fakid))
                {
                    try
                    {
                        recFak = (from f in Program.dbDataTransSumma.tblfaks
                                  where f.regnskabid == rec_regnskab.Rid && f.sk == "K" && f.fakid == k.Fakid
                                  select f).First();
                    }
                    catch
                    {
                        recFak = new tblfak
                        {
                            udskriv = true,
                            regnskabid = k.Regnskabid,
                            sk = k.Sk,
                            fakid = k.Fakid,
                            faknr = k.Faknr,
                            dato = k.Dato,
                            konto = k.kreditornr
                        };
                        Program.dbDataTransSumma.tblfaks.InsertOnSubmit(recFak);
                    }
                }


                tblfaklin recFaklin = new tblfaklin
                {
                    sk = k.Sk,
                    regnskabid = k.Regnskabid,
                    fakid = k.Fakid,
                    faklinnr = k.Faklinnr,
                    varenr = k.Varenr.ToString(),
                    tekst = k.Tekst,
                    konto = k.Konto,
                    momskode = k.Momskode,
                    antal = k.Antal,
                    enhed = k.Enhed,
                    pris = k.Pris,
                    rabat = k.Rabat,
                    moms = k.Moms,
                    nettobelob = k.Nettobelob,
                    bruttobelob = k.Bruttobelob
                };
                Program.dbDataTransSumma.tblfaklins.InsertOnSubmit(recFaklin);
                if (!(k.Fakid == 0)) recFak.tblfaklins.Add(recFaklin);
                lastFakid = k.Fakid;
            }
            //Program.dbDataTransSumma.SubmitChanges();

        }
    }
}
