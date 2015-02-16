using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MailKit.Net.Imap;
using MailKit.Search;
using HtmlAgilityPack;
using System.Globalization;
using System.Reflection;
using System.Diagnostics;
using Microsoft.WindowsAzure;


namespace MailWebJob
{
    public class Functions
    {
        [NoAutomaticTrigger]
        public static void ProcessEmails(TextWriter log, string MailFolder) 
        {
            //object cc = CloudConfigurationManager.GetSetting("aaa");
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.ProductVersion;
            log.WriteLine("ProductVersion: " + version);

            log.WriteLine("Start ProcessEmails: " + MailFolder);
            dbPuls3060MedlemEntities db = new dbPuls3060MedlemEntities();

            //var qry = from m in db.tblNytMedlem select m;
            //int antal = qry.Count();
            //log.WriteLine("qry.Count()++: " + antal.ToString());

            using (var client = new ImapClient())
            {
                client.Connect("imap.gigahost.dk", 993, true);
                client.AuthenticationMechanisms.Remove("XOAUTH");
                client.Authenticate("regnskab@puls3060.dk", "1234West+");

                // The Inbox folder is always available on all IMAP servers...
                var Inbox = client.Inbox;
                var Indmeldelser = client.GetFolder("INBOX.Indmeldelser");

                Inbox.Open(FolderAccess.ReadWrite);

                var query = SearchQuery.DeliveredAfter(DateTime.Now.AddDays(-2)).And(SearchQuery.SubjectContains("Ny besvaret formular fra 'Medlem Puls 3060'!"));
                foreach (var uid in Inbox.Search(query))
                {
                    var message = Inbox.GetMessage(uid);
                    string htmlbody = message.HtmlBody;

                    if (htmlbody == null) continue;

                    string DecodedString = System.Web.HttpUtility.HtmlDecode(htmlbody);
                    HtmlAgilityPack.HtmlDocument document = new HtmlDocument();
                    document.LoadHtml(DecodedString);
                    HtmlNode rootNode = document.DocumentNode;
                    IEnumerable<HtmlNode> allp = rootNode.Descendants("p");
                    foreach (var p in allp)
                    {
                        var lin = p.InnerText;
                    }

                    DateTime Date;
                    try
                    {
                        Date = DateTime.Parse(message.Headers["Date"]);
                    }
                    catch
                    {
                        Date = DateTime.Now; 
                    }
                    
                    tblNytMedlem medlem = new tblNytMedlem 
                    {
                        MessageDate = Date,
                        MessageID = message.Headers["Message-ID"],
                        MessageFrom = message.Headers["From"]

                    };

                    IEnumerable<HtmlNode> allpre = rootNode.Descendants("pre");
                    foreach (var pre in allpre)
                    {
                        var lin = pre.InnerText;
                        char[] sp = { ':' };
                        string[] arr = lin.Split(sp,2);
                        if (arr.Length == 2)
                        {
                            string name = arr[0].Trim().ToUpper();
                            string value = arr[1].Trim();
                            switch (name)
                            {
                                case "FORNAVN":
                                    medlem.Fornavn = value;
                                    break;

                                case "EFTERNAVN":
                                    medlem.Efternavn = value;
                                    break;

                                case "ADRESSE":
                                    medlem.Adresse = value;
                                    break;

                                case "POST NR.":
                                    medlem.Postnr = value;
                                    break;

                                case "BY":
                                    medlem.Bynavn = value;
                                    break;

                                case "MAIL":
                                    medlem.Email = value;
                                    break;

                                case "TELEFON":
                                    medlem.Telefon = value;
                                    break;

                                case "MOBIL":
                                    medlem.Mobil = value;
                                    break;

                                case "FØDSELSDAG":
                                    try
                                    {
                                        medlem.FodtDato = DateTime.ParseExact(value, "d -M -yyyy", CultureInfo.InvariantCulture);
                                    }
                                    catch
                                    {
                                        medlem.FodtDato = null;
                                    }
                                    break;

                                case "BESKED":
                                    if (value == "Skriv en besked til Puls 3060 her")
                                        medlem.Besked = "";
                                    else
                                        medlem.Besked = value;
                                    break;

                                case "KØN":
                                    medlem.Kon = value;
                                    break;

                                default:
                                    break;
                            }

                        }
                    }
                    db.tblNytMedlem.Add(medlem);
                    db.SaveChanges();
                    Inbox.MoveTo(uid, Indmeldelser);
                    SendSMS(log);
                    SendEmail(log, client, medlem);
                    log.WriteLine("Slut ProcessEmails: " + medlem.MessageID);
                }

                client.Disconnect(true);
            }
        }

        [NoAutomaticTrigger]
        public static void SendEmail(TextWriter log, ImapClient imap_client, tblNytMedlem medlem) 
        {
            var message = new MimeMessage();
            string Navn = medlem.Fornavn + " " + medlem.Efternavn;
            message.To.Add(new MailboxAddress(Navn, medlem.Email));
            message.From.Add(new MailboxAddress("Regnskab Puls3060", "regnskab@puls3060.dk"));
            message.Subject = "Tak for din tilmelding til løbeklubben Puls 3060";

            var builder = new BodyBuilder();
            builder.HtmlBody = string.Format(emailTemplates.KviteringIndmeldelse, medlem.Fornavn);
            message.Body = builder.ToMessageBody();
            using (var smtp_client = new SmtpClient())
            {
                smtp_client.Connect("smtp.gigahost.dk", 587, false);
                smtp_client.AuthenticationMechanisms.Remove("XOAUTH2");
                smtp_client.Authenticate("regnskab@puls3060.dk", "1234West+");
                smtp_client.Send(message);
                smtp_client.Disconnect(true);
            }

            var SendtPost = imap_client.GetFolder("Sendt post");
            SendtPost.Open(FolderAccess.ReadWrite);
            SendtPost.Append(message);
            SendtPost.Close();

            log.WriteLine("SendEmail: " + medlem.Email);
        }

        [NoAutomaticTrigger]
        public static void SendSMS(TextWriter log)
        {
            var sms = new MimeMessage();
            sms.To.Add(new MailboxAddress("Puls3060 SMS", "40133540.Puls3060.Puls3060@mail.suresms.com"));
            sms.From.Add(new MailboxAddress("Regnskab Puls3060", "regnskab@puls3060.dk"));
            sms.Subject = "sms";

            var builder = new BodyBuilder();
            builder.TextBody = "Ny indmeldelse i Puls3060";
            sms.Body = builder.ToMessageBody();
            using (var smtp_client = new SmtpClient())
            {
                smtp_client.Connect("smtp.gigahost.dk", 587, false);
                smtp_client.AuthenticationMechanisms.Remove("XOAUTH2");
                smtp_client.Authenticate("regnskab@puls3060.dk", "1234West+");
                smtp_client.Send(sms);
                smtp_client.Disconnect(true);
            }

            log.WriteLine("SendSMS: Ny indmeldelse i Puls3060");
        }
    }
}
