using System;
using System.Net;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Navigation;
using System.Data;
using System.Xml;

namespace wssGetList
{
    public class getList
    {
        private wssGetList.wsshost.Lists ls;
        
        private getList() { }
        
        public getList(string url, string login, string password, string domain)
        {
            ls = new wssGetList.wsshost.Lists();
            ls.Url = url + @"/_vti_bin/lists.asmx";
            ls.PreAuthenticate = true;
            ls.Credentials = new System.Net.NetworkCredential(login, password, domain);
        }

        public DataSet getListData(string ListTitle)
        {
            XmlNode lists = ls.GetListCollection();
            DataSet dsLists = new DataSet();
            dsLists.ReadXml(new System.IO.StringReader("<?xml version='1.0' ?>" + lists.OuterXml));
            string ListName = null;
            string ListWebId = null;
            foreach (DataRow l in dsLists.Tables["List"].Rows)
            {
                if (((string)l["Title"]) == ListTitle)
                {
                    ListName = (string)l["Name"];
                    ListWebId = (string)l["WebId"];
                    break;
                }
            }

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml("<Document><Query><Where><Lt><FieldRef Name='ID' /><Value Type='Counter'>3</Value></Lt></Where></Query><ViewFields><FieldRef Name='ID' /><FieldRef Name='Title' /></ViewFields><QueryOptions /></Document>");
            System.Xml.XmlNode listQuery = doc.SelectSingleNode("//Query");
            System.Xml.XmlNode listViewFields = doc.SelectSingleNode("//ViewFields");
            System.Xml.XmlNode listQueryOptions = doc.SelectSingleNode("//QueryOptions");

            System.Xml.XmlNode items = ls.GetListItems(ListName, string.Empty, listQuery, listViewFields, string.Empty, listQueryOptions, ListWebId);
            
            DataSet dsItems = new DataSet();
            dsItems.ReadXml(new System.IO.StringReader("<?xml version='1.0' ?>" + items.InnerXml));

            DataTable rows = dsItems.Tables["row"];
            DataRow row = rows.Rows[1];
            object RowTitle = row["ows_Title"];

            return dsItems;
        }
        
        public void test() { 
            System.Xml.XmlDocument doc2 = new System.Xml.XmlDocument();
            string xmlString = "<Batch OnError='Continue' ListVersion='1'>";
            xmlString += "<Method ID='1' Cmd='Update'><Field Name='ID'>1</Field><Field Name='Title'>Mogens Hafsjold</Field></Method>";
            xmlString += "<Method ID='2' Cmd='Update'><Field Name='ID'>2</Field><Field Name='Title'>Julie Hafsjold</Field></Method>";
            xmlString += "</Batch>";
            doc2.LoadXml(xmlString);
            System.Xml.XmlNode myitems = doc2.SelectSingleNode("//Batch");
            System.Xml.XmlNode myresult = ls.UpdateListItems("EE24CD1A-B465-4CA7-8F79-96F0BAB67247", myitems);
        }
    }
}
