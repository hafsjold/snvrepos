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

namespace Trans2Summa3060
{
    public class clsUnicontaKladde
    {
        async public void InsertAllVouchersClient()
        {
            var qryKladder = from k in Program.karKladder where k.Regnskabid == 10 orderby k.Bilag, k.Id select k;
            int antal = qryKladder.Count();
            foreach(var rec in qryKladder)
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

        async public void InsertGLDailyJournalLines()
        {
            var api = UCInitializer.GetBaseAPI;
            var col3 = await api.Query<NumberSerieClient>();

            var crit = new List<PropValuePair>();
            var pair = PropValuePair.GenereteWhereElements("KeyStr", typeof(String), "Dag");
            crit.Add(pair);
            var col = await api.Query<GLDailyJournalClient>(null, crit);
            var rec_Master = col.FirstOrDefault();

            var qryKladder = from k in Program.karKladder where k.Regnskabid == 10 orderby k.Id select k;
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
            string mask_bilag = @"C:\Users\regns\Documents\SummaSummarum\RegnskabsBilag\2017\Bogføringsbilag\Bilag {0}.pdf";
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
    }
}
