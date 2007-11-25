using System;
using System.Net;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Navigation;
using System.Data;
using System.Xml;


namespace WebServiceList
{
    class Program
    {
        static void Main(string[] args)
        {
            //GetSites("http://localhost", "Administrator", "m733", "hd19");
            GetLists("http://hd19", "Administrator", "m733", "hd19");
        }
        //
        // Get Web Sites
        //
        private static void GetSites(string url, string login, string password, string domain)
        {
            WebServiceList.Webs.Webs service = new WebServiceList.Webs.Webs();
            service.PreAuthenticate = true;
            service.Credentials = new System.Net.NetworkCredential(login, password, domain);

            XmlNode sites = null;

            try
            {
                sites = service.GetWebCollection();
            }
            catch
            {
                return;
            }

            foreach (System.Xml.XmlNode site in sites.ChildNodes)
            {
                Console.WriteLine(site.Attributes["Url"].Value);

                GetLists(site.Attributes["Url"].Value, login, password, domain);
                GetSites(site.Attributes["Url"].Value, login, password, domain);
            }
        }

        //Get Web Site Lists  
        //
        private static void GetLists(string url, string login, string password, string domain)
        {
            //List WebService 
            WebServiceList.Lists.Lists ls = new WebServiceList.Lists.Lists();
            ls.PreAuthenticate = true;

            ls.Credentials = new System.Net.NetworkCredential(login, password, domain);
            ls.Url = url + @"/_vti_bin/lists.asmx";


            foreach (XmlNode list in ls.GetListCollection().ChildNodes)
            {
                //Check whether list is document library
                /*
                if (Convert.ToInt32(list.Attributes["ServerTemplate"].Value) != 0x65)
                {
                    continue;
                }
                */
                string title = list.Attributes["Title"].Value;
                string listUrl = list.Attributes["DefaultViewUrl"].Value.Replace("/Forms/AllItems.aspx", string.Empty);

                char[] separator = new char[] { '/' };
                string listPath = url.Substring(0, url.LastIndexOf('/'));

                Console.WriteLine(listPath + listUrl + "/" + title);
            }
        }
    }
}
