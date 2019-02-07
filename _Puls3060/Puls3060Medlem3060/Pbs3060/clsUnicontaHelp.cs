using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Uniconta.API.GeneralLedger;
using Uniconta.API.System;
using Uniconta.ClientTools.DataModel;
using Uniconta.Common;
using Uniconta.DataModel;

namespace nsPbs3060
{
    public class clsUnicontaHelp
    {
        public CrudAPI m_api;
        public CreditorClient[] m_Creditors;
        public GLAccountClient[] m_GLAccounts;


        public clsUnicontaHelp(CrudAPI api)
        {
            m_api = api;
            var Creditor = api.Query<CreditorClient>();
            Creditor.Wait();
            m_Creditors = Creditor.Result;
            var GLAccount = api.Query<GLAccountClient>();
            GLAccount.Wait();
            m_GLAccounts = GLAccount.Result;
        }

        public int ImportEmailBilag()
        {
            int totalAntal = 0;
            totalAntal += _ImportEmailBilag();
            totalAntal += _ImportEmailTilBetaling();
            return totalAntal;
        }

        public int _ImportEmailBilag()
        {
            MimeMessage message;
            int antalbilag = 0;
            clsParam objParam = null;
            using (var imap_client = new ImapClient())
            {
                imap_client.Connect("imap.gigahost.dk", 993, true);
                imap_client.AuthenticationMechanisms.Remove("XOAUTH");
                imap_client.Authenticate(clsApp.GigaHostImapUser, clsApp.GigaHostImapPW);
                var Puls3060Bilag = imap_client.GetFolder("_Puls3060Bilag");
                var Puls3060BilagArkiv = imap_client.GetFolder("_Puls3060BilagArkiv");
                Puls3060Bilag.Open(FolderAccess.ReadWrite);

                var results = Puls3060Bilag.Search(SearchQuery.All);
                antalbilag = results.Count();
                foreach (var result in results)
                {
                    message = Puls3060Bilag.GetMessage(result);
                    List<VouchersClient> documentlist = new List<VouchersClient>();

                    if (message.Body.ContentType.MimeType == "application/ms-tnef")
                    {
                        antalbilag--;
                        continue;

                        foreach (var msg_attachment in message.Attachments)
                        {
                            var part = (TnefPart)msg_attachment;
                            //FileStream msstream = new FileStream(@"testfile.pdf", FileMode.CreateNew);
                            MemoryStream msstream = new MemoryStream();
                            part.ContentObject.DecodeTo(msstream);
                            msstream.Position = 0;
                            var parser = new MimeParser(msstream, MimeFormat.Default);
                            var xmessage = parser.ParseMessage();

                        }
                    }
                    MemoryStream msMail = new MemoryStream();
                    message.WriteTo(msMail);

                    VouchersClient mail = new VouchersClient()
                    {
                        Fileextension = FileextensionsTypes.EML,
                        Text = "e-Mail",
                        VoucherAttachment = msMail.ToArray(),
                        DocumentDate = DateTime.Now,
                    };
                    var task1 = m_api.Insert(mail);
                    task1.Wait();
                    var res1 = task1.Result;
                    documentlist.Add(mail);


                    foreach (var msg_attachment in message.Attachments)
                    {
                        if (msg_attachment is MimePart)
                        {

                            FileextensionsTypes type = FileextensionsTypes.PDF;
                            switch (msg_attachment.ContentType.MediaSubtype.ToUpper())
                            {
                                case "PDF":
                                    type = FileextensionsTypes.PDF;
                                    break;

                                case "JPEG":
                                    type = FileextensionsTypes.JPEG;
                                    break;

                                case "TXT":
                                    type = FileextensionsTypes.TXT;
                                    break;

                                case "PLAIN":
                                    type = FileextensionsTypes.TXT;
                                    break;

                                case "MSWORD":
                                    type = FileextensionsTypes.DOC;
                                    break;

                                case "VND.OPENXMLFORMATS-OFFICEDOCUMENT.SPREADSHEETML.SHEET":
                                    type = FileextensionsTypes.XLSX;
                                    break;

                                default:
                                    type = FileextensionsTypes.UNK;
                                    break;
                            }
                            var part = (MimePart)msg_attachment;
                            MemoryStream msstream = new MemoryStream();
                            part.ContentObject.DecodeTo(msstream);
                            byte[] arrStream = msstream.ToArray();
                            if (type == FileextensionsTypes.UNK)
                            {
                                if (arrStream[0] == 0x25 && arrStream[1] == 0x50 && arrStream[2] == 0x44 && arrStream[3] == 0x46) // PDF Magic number
                                {
                                    type = FileextensionsTypes.PDF;
                                }
                            }
                            VouchersClient attm = new VouchersClient()
                            {
                                Fileextension = type,
                                Text = (msg_attachment as MimePart).FileName,
                                VoucherAttachment = arrStream,
                                DocumentDate = DateTime.Now,
                            };
                            var task3 = m_api.Insert(attm);
                            task3.Wait();
                            var res3 = task3.Result;
                            documentlist.Add(attm);
                        }

                        else if (msg_attachment is MessagePart)
                        {
                            string wmsgtext;
                            var msgpart = msg_attachment as MessagePart;
                            if (string.IsNullOrEmpty(msgpart.Message.HtmlBody))
                            {
                                wmsgtext = msgpart.Message.TextBody;
                            }
                            else
                            {
                                wmsgtext = msgpart.Message.HtmlBody;
                            }
                            var msgtext = Regex.Replace(wmsgtext, "<[^>]*>", String.Empty).Replace("&nbsp;", String.Empty).Trim();
                            string[] splitstring = { "\r\n" };
                            string[] arrParams = msgtext.Split(splitstring, StringSplitOptions.RemoveEmptyEntries);
                            objParam = new clsParam(arrParams);
                        }
                    }
                    if (documentlist.Count > 0)
                    {

                        VouchersClient folder = new VouchersClient()
                        {
                            _Fileextension = FileextensionsTypes.DIR,
                            _Text = message.Subject,
                            _DocumentDate = DateTime.Now,

                        };
                        var ref3 = folder.PrimaryKeyId;
                        var task4 = m_api.Insert(folder);
                        task4.Wait();
                        var res4 = task4.Result;
                        var ref1 = folder.PrimaryKeyId;

                        DocumentAPI docapi = new DocumentAPI(m_api);
                        //TEST
                        //var task5 = docapi.CreateFolder(folder, null);
                        //var task5 = docapi.AppendToFolder(folder, documentlist);
                        var task5 = docapi.CreateFolder(folder, documentlist);
                        task5.Wait();
                        var res5 = task5.Result;
                        var ref2 = folder.PrimaryKeyId;

                        int DocumentRef = ref2;

                        if (ref1 != ref2) //Delete ref1
                        {
                            var crit = new List<PropValuePair>();
                            var pair = PropValuePair.GenereteWhereElements("PrimaryKeyId", typeof(int), ref1.ToString());
                            crit.Add(pair);
                            var task6 = m_api.Query<VouchersClient>(crit);
                            task6.Wait();
                            var col = task6.Result;
                            if (col.Count() == 1)
                            {
                                var rec = col[0];
                                m_api.DeleteNoResponse(rec);
                            }
                        }

                        if (objParam == null)
                        {
                            objParam = new clsParam()
                            {
                                 Delsystem = "Finans",
                                 Tekst = "Ukendt post",
                                 Kontotype = "Finans",
                                 Konto = "9900",
                                 Modkontotype = "Finans",
                                 Modkonto = "9900",
                                 Kredit = 0.00
                            };
                        }

                        switch (objParam.Delsystem.ToLower())
                        {
                            case "finans":
                                InsertFinansJournal(message, DocumentRef, objParam);
                                break;

                            case "kreditor":
                                InsertKøbsOrder(message, DocumentRef, objParam);
                                break;

                            default:
                                break;
                        }

                        // move email to arkiv
                        var newId = Puls3060Bilag.MoveTo(result, Puls3060BilagArkiv);
                    }

                }

                Puls3060Bilag.Close();
                imap_client.Disconnect(true);
            }
            return antalbilag;// message;
        }
        public int _ImportEmailTilBetaling()
        {
            MimeMessage message;
            int antalbilag = 0;
            clsParam objParam = null;
            using (var imap_client = new ImapClient())
            {
                imap_client.Connect("imap.gigahost.dk", 993, true);
                imap_client.AuthenticationMechanisms.Remove("XOAUTH");
                imap_client.Authenticate(clsApp.GigaHostImapUser, clsApp.GigaHostImapPW);
                var Puls3060TilBetaling = imap_client.GetFolder("_Puls3060TilBetaling");
                var Puls3060BilagArkiv = imap_client.GetFolder("_Puls3060BilagArkiv");
                Puls3060TilBetaling.Open(FolderAccess.ReadWrite);

                var results = Puls3060TilBetaling.Search(SearchQuery.All);
                antalbilag = results.Count();
                foreach (var result in results)
                {
                    message = Puls3060TilBetaling.GetMessage(result);
                    List<VouchersClient> documentlist = new List<VouchersClient>();

                    if (message.Body.ContentType.MimeType == "application/ms-tnef")
                    {
                        antalbilag--;
                        continue;

                        foreach (var msg_attachment in message.Attachments)
                        {
                            var part = (TnefPart)msg_attachment;
                            //FileStream msstream = new FileStream(@"testfile.pdf", FileMode.CreateNew);
                            MemoryStream msstream = new MemoryStream();
                            part.ContentObject.DecodeTo(msstream);
                            msstream.Position = 0;
                            var parser = new MimeParser(msstream, MimeFormat.Default);
                            var xmessage = parser.ParseMessage();

                        }
                    }
                    MemoryStream msMail = new MemoryStream();
                    message.WriteTo(msMail);

                    VouchersClient mail = new VouchersClient()
                    {
                        Fileextension = FileextensionsTypes.EML,
                        Text = "e-Mail",
                        VoucherAttachment = msMail.ToArray(),
                        DocumentDate = DateTime.Now,
                    };
                    var task1 = m_api.Insert(mail);
                    task1.Wait();
                    var res1 = task1.Result;
                    documentlist.Add(mail);


                    foreach (var msg_attachment in message.Attachments)
                    {
                        if (msg_attachment is MimePart)
                        {

                            FileextensionsTypes type = FileextensionsTypes.PDF;
                            switch (msg_attachment.ContentType.MediaSubtype.ToUpper())
                            {
                                case "PDF":
                                    type = FileextensionsTypes.PDF;
                                    break;

                                case "JPEG":
                                    type = FileextensionsTypes.JPEG;
                                    break;

                                case "TXT":
                                    type = FileextensionsTypes.TXT;
                                    break;

                                case "PLAIN":
                                    type = FileextensionsTypes.TXT;
                                    break;

                                case "MSWORD":
                                    type = FileextensionsTypes.DOC;
                                    break;

                                case "VND.OPENXMLFORMATS-OFFICEDOCUMENT.SPREADSHEETML.SHEET":
                                    type = FileextensionsTypes.XLSX;
                                    break;

                                default:
                                    type = FileextensionsTypes.UNK;
                                    break;
                            }
                            var part = (MimePart)msg_attachment;
                            MemoryStream msstream = new MemoryStream();
                            part.ContentObject.DecodeTo(msstream);
                            byte[] arrStream = msstream.ToArray();
                            if (type == FileextensionsTypes.UNK)
                            {
                                if (arrStream[0] == 0x25 && arrStream[1] == 0x50 && arrStream[2] == 0x44 && arrStream[3] == 0x46) // PDF Magic number
                                {
                                    type = FileextensionsTypes.PDF;
                                }
                            }
                            VouchersClient attm = new VouchersClient()
                            {
                                Fileextension = type,
                                Text = (msg_attachment as MimePart).FileName,
                                VoucherAttachment = arrStream,
                                DocumentDate = DateTime.Now,
                            };
                            var task3 = m_api.Insert(attm);
                            task3.Wait();
                            var res3 = task3.Result;
                            documentlist.Add(attm);
                        }

                        else if (msg_attachment is MessagePart)
                        {
                            string wmsgtext;
                            var msgpart = msg_attachment as MessagePart;
                            if (string.IsNullOrEmpty(msgpart.Message.HtmlBody))
                            {
                                wmsgtext = msgpart.Message.TextBody;
                            }
                            else
                            {
                                wmsgtext = msgpart.Message.HtmlBody;
                            }
                            var msgtext = Regex.Replace(wmsgtext, "<[^>]*>", String.Empty).Replace("&nbsp;", String.Empty).Trim();
                            string[] splitstring = { "\r\n" };
                            string[] arrParams = msgtext.Split(splitstring, StringSplitOptions.RemoveEmptyEntries);
                            objParam = new clsParam(arrParams);
                        }
                    }
                    if (documentlist.Count > 0)
                    {

                        VouchersClient folder = new VouchersClient()
                        {
                            _Fileextension = FileextensionsTypes.DIR,
                            _Text = message.Subject,
                            _DocumentDate = DateTime.Now,

                        };
                        var ref3 = folder.PrimaryKeyId;
                        var task4 = m_api.Insert(folder);
                        task4.Wait();
                        var res4 = task4.Result;
                        var ref1 = folder.PrimaryKeyId;

                        DocumentAPI docapi = new DocumentAPI(m_api);
                        //TEST
                        //var task5 = docapi.CreateFolder(folder, null);
                        //var task5 = docapi.AppendToFolder(folder, documentlist);
                        var task5 = docapi.CreateFolder(folder, documentlist);
                        task5.Wait();
                        var res5 = task5.Result;
                        var ref2 = folder.PrimaryKeyId;

                        int DocumentRef = ref2;

                        if (ref1 != ref2) //Delete ref1
                        {
                            var crit = new List<PropValuePair>();
                            var pair = PropValuePair.GenereteWhereElements("PrimaryKeyId", typeof(int), ref1.ToString());
                            crit.Add(pair);
                            var task6 = m_api.Query<VouchersClient>(crit);
                            task6.Wait();
                            var col = task6.Result;
                            if (col.Count() == 1)
                            {
                                var rec = col[0];
                                m_api.DeleteNoResponse(rec);
                            }
                        }

                        if (objParam == null)
                        {
                            objParam = new clsParam()
                            {
                                Delsystem = "Kreditor",
                                Tekst = "Ukendt post",
                                Kontotype = "Kreditor",
                                Konto = "100000",
                                Modkontotype = "Finans",
                                Modkonto = "9900",
                                Kredit = 0.00
                            };
                        }

                        switch (objParam.Delsystem.ToLower())
                        {
                            case "finans":
                                InsertFinansJournal(message, DocumentRef, objParam);
                                break;

                            case "kreditor":
                                InsertKøbsOrder(message, DocumentRef, objParam);
                                break;

                            default:
                                break;
                        }

                        // move email to arkiv
                        var newId = Puls3060TilBetaling.MoveTo(result, Puls3060BilagArkiv);
                    }

                }

                Puls3060TilBetaling.Close();
                imap_client.Disconnect(true);
            }
            return antalbilag;// message;
        }

        public void InsertFinansJournal(MimeMessage message, int DocumentRef, clsParam objParam)
        {
            var From = message.From.ToString();
            From = ExtractEmails(From);
            var Date = message.Date.DateTime;
            var Subject = objParam.Tekst;
            if (string.IsNullOrEmpty(Subject))
                Subject = message.Subject;

            string wAccount = null;
            if (!string.IsNullOrEmpty(objParam.Konto))
            {
                try
                {
                    wAccount = (from c in this.m_GLAccounts where c.Account == objParam.Konto select c.Account).First();
                }
                catch (Exception)
                {
                    wAccount = "9900"; //Fejlkonto
                }
            }

            string wOffsetAccount = null;
            if (!string.IsNullOrEmpty(objParam.Modkonto))
            {
                try
                {
                    wOffsetAccount = (from c in this.m_GLAccounts where c.Account == objParam.Modkonto select c.Account).First();
                }
                catch (Exception)
                {
                    wOffsetAccount = "9900"; //Fejlkonto
                }
            }

            var crit = new List<PropValuePair>();
            var pair = PropValuePair.GenereteWhereElements("KeyStr", typeof(String), "Dag");
            crit.Add(pair);
            var task = m_api.Query<GLDailyJournalClient>(crit);
            task.Wait();
            var col = task.Result;
            var rec_Master = col.FirstOrDefault();

            GLDailyJournalLineClient jl = new GLDailyJournalLineClient()
            {
                Date = Date,
                Text = Subject,
                DocumentRef = DocumentRef,
                AccountType = objParam.Kontotype,
                Account = wAccount,
                OffsetAccountType = objParam.Modkontotype,
                OffsetAccount = wOffsetAccount,
                Debit = objParam.Debit,
                Credit = objParam.Kredit,
            };
            jl.SetMaster(rec_Master);
            var task2 = m_api.Insert(jl);
            task2.Wait();
            var err = task2.Result;
            if (err != ErrorCodes.Succes)
            {
                int xx = 1;
            }
        }

        public void InsertKøbsOrder(MimeMessage message, int DocumentRef, clsParam objParam)
        {
            var From = message.From.ToString();
            From = ExtractEmails(From);
            var Date = message.Date.DateTime;
            var Subject = objParam.Tekst;
            if (string.IsNullOrEmpty(Subject))
                Subject = message.Subject;


            string wAccount = null;
            try
            {
                wAccount = (from c in this.m_Creditors where c.Account == objParam.Konto select c.Account).First();
            }
            catch (Exception)
            {
                try
                {
                    wAccount = (from c in this.m_Creditors where ((c.ContactEmail != null) && (c.ContactEmail.ToLower() == From.ToLower())) || ((c._InvoiceEmail != null) && (c._InvoiceEmail.ToLower() == From.ToLower())) select c.Account).First();
                }
                catch
                {
                    wAccount = "100000"; //Ukendt kreditor
                }
            }

            CreditorOrderClient recOrder = new CreditorOrderClient()
            {
                Account = wAccount,
                InvoiceDate = Date,
                DeliveryDate = Date,
                DocumentRef = DocumentRef,
                DeleteLines = true,
                DeleteOrder = true
            };
            var taskInsertCreditorOrder = m_api.Insert(recOrder);
            taskInsertCreditorOrder.Wait();
            var err1 = taskInsertCreditorOrder.Result;

            CreditorOrderLineClient recOrderLine = new CreditorOrderLineClient()
            {
                Text = Subject,
                Qty = 1,
                Price = objParam.Kredit == null ? 0 : (double)objParam.Kredit,
                PostingAccount = objParam.Modkonto,
            };
            recOrderLine.SetMaster(recOrder);
            var taskInsertCreditorOrderLine = m_api.Insert(recOrderLine);
            taskInsertCreditorOrderLine.Wait();
            var err2 = taskInsertCreditorOrderLine.Result;
        }

        public string ExtractEmails(string data)
        {
            Regex emailRegex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
            MatchCollection emailMatches = emailRegex.Matches(data);
            foreach (Match emailMatch in emailMatches)
            {
                return emailMatch.Value;
            }
            return "";
        }

    }
}
