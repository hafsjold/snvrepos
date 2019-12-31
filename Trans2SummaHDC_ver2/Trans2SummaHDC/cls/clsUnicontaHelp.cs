using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Uniconta.API.GeneralLedger;
using Uniconta.API.System;
using Uniconta.ClientTools.DataModel;
using Uniconta.Common;
using Uniconta.DataModel;

namespace Trans2SummaHDC
{
    public class clsUnicontaHelp
    {
        public CrudAPI m_api;
        public CreditorClient[] m_Creditors;

        public clsUnicontaHelp(CrudAPI api)
        {
            m_api = api;
            var task = api.Query<CreditorClient>();
            task.Wait();
            m_Creditors = task.Result;
        }

        public int ImportEmailBilag()
        {
            MimeMessage message;
            int antalbilag = 0;
            clsParam objParam = null;
            using (var imap_client = new ImapClient())
            {
                imap_client.Connect("outlook.office365.com", 993, true);
                imap_client.AuthenticationMechanisms.Remove("XOAUTH");
                imap_client.Authenticate(clsApp.ImapUser, clsApp.ImapPW);
                var HafsjoldDataBilag = imap_client.GetFolder("_HafsjoldDataBilag");
                var HafsjoldDataBilagArkiv = imap_client.GetFolder("_HafsjoldDataBilagArkiv");
                HafsjoldDataBilag.Open(FolderAccess.ReadWrite);

                var results = HafsjoldDataBilag.Search(SearchQuery.All);
                antalbilag = results.Count();
                foreach (var result in results)
                {
                    message = HafsjoldDataBilag.GetMessage(result);

                    MemoryStream msMail = new MemoryStream();
                    message.WriteTo(msMail);

                    List<VouchersClient> documents = new List<VouchersClient>();
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
                    documents.Add(mail);

                    foreach (var msg_attachment in message.Attachments)
                    {
                        if (msg_attachment is MimePart) 
                        {
                            var bSaveAttachment = true;
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
                            part.Content.DecodeTo(msstream);
                            byte[] arrStream = msstream.ToArray();
                            if (type == FileextensionsTypes.UNK)
                            {
                                if (arrStream[0] == 0x25 && arrStream[1] == 0x50 && arrStream[2] == 0x44 && arrStream[3] == 0x46) // PDF Magic number
                                {
                                    type = FileextensionsTypes.PDF;
                                }
                                else if(Path.GetExtension(part.FileName.ToUpper()) == ".MSG" )
                                {
                                    type = FileextensionsTypes.MSG;
                                    using (var msg = new MsgReader.Outlook.Storage.Message(msstream))
                                    {
                                        var MessageBody = msg.BodyText;
                                        var msgtext = Regex.Replace(MessageBody, "<[^>]*>", String.Empty).Replace("&nbsp;", String.Empty).Trim();
                                        string[] splitstring = { "\r\n" };
                                        string[] arrParams = msgtext.Split(splitstring, StringSplitOptions.RemoveEmptyEntries);
                                        objParam = new clsParam(arrParams);
                                    }
                                    bSaveAttachment = false;
                                }
                            }
                            if (bSaveAttachment)
                            {
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
                                documents.Add(attm); 
                            }
                        }
                        else if (msg_attachment is MessagePart)
                        {
                            var msgpart = msg_attachment as MessagePart;
                            var MessageBody = msgpart.Message.HtmlBody;
                            if (string.IsNullOrEmpty(MessageBody))
                                MessageBody = msgpart.Message.TextBody;
                            var msgtext = Regex.Replace(MessageBody, "<[^>]*>", String.Empty).Replace("&nbsp;", String.Empty).Trim();
                            string[] splitstring = { "\r\n" };
                            string[] arrParams = msgtext.Split(splitstring, StringSplitOptions.RemoveEmptyEntries);
                            objParam = new clsParam(arrParams);
                        }
                    }

                    if (documents.Count > 0)
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
                        var task5 = docapi.CreateFolder(folder, documents);
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
                                Konto = "5820",
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
                        var newId = HafsjoldDataBilag.MoveTo(result, HafsjoldDataBilagArkiv);
                    }

                }

                HafsjoldDataBilag.Close();
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
                Account = objParam.Konto,
                Vat = objParam.Moms_Konto,
                OffsetAccountType = objParam.Modkontotype,
                OffsetAccount = objParam.Modkonto,
                OffsetVat = objParam.Moms_Modkonto,
                Debit = objParam.Debit,
                Credit = objParam.Kredit,
            };
            jl.SetMaster(rec_Master);
            var task2 = m_api.Insert(jl);
            task2.Wait();
            var err = task2.Result; 
            if (err != ErrorCodes.Succes)
            {
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
                //DeleteLines = true,
                //DeleteOrder = true

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
