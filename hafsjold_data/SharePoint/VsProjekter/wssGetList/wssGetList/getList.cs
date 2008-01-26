using System;
using System.Net;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Navigation;
using System.Data;
using System.Xml;
using System.Xml.Linq;
using System.Linq;


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
            {
                XElement root = XElement.Parse(ls.GetListCollection().OuterXml);
                IEnumerable<XElement> ListsList =
                    from el in root.Elements()
                    where el.Attribute("Title").Value != ListTitle
                    select new XElement("List",
                              new XAttribute("Desc", el.Attribute("Title").Value),
                              new XAttribute("Name", el.Attribute("Name").Value),
                              new XAttribute("WebId", el.Attribute("WebId").Value));
                
                foreach (XElement el in ListsList){
                    Console.WriteLine(el);
                    var Name = el.Attribute("Name").Value;
                    var WebId = el.Attribute("WebId").Value;
                    var Desc = el.Attribute("Desc").Value;

                }
            }
            
            XmlNode lists = ls.GetListCollection();

            DataSet dsLists = new DataSet();
            dsLists.ReadXml(new System.IO.StringReader("<?xml version='1.0' encoding='utf-8'?>" + lists.OuterXml));
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
            //doc.LoadXml("<Document><Query><Where><Lt><FieldRef Name='ID' /><Value Type='Counter'>3</Value></Lt></Where></Query><ViewFields><FieldRef Name='ID' /><FieldRef Name='Title' /></ViewFields><QueryOptions /></Document>");
            doc.LoadXml("<Document><Query /><ViewFields /><QueryOptions /></Document>");
            System.Xml.XmlNode listQuery = doc.SelectSingleNode("//Query");
            System.Xml.XmlNode listViewFields = doc.SelectSingleNode("//ViewFields");
            System.Xml.XmlNode listQueryOptions = doc.SelectSingleNode("//QueryOptions");
            string RowLimit = "500";

            System.Xml.XmlNode items = ls.GetListItems(ListName, string.Empty, listQuery, listViewFields, RowLimit, listQueryOptions, ListWebId);

            DataSet dsItems = new DataSet();
            dsItems.ReadXml(new System.IO.StringReader("<?xml version='1.0' encoding='utf-8'?>" + items.InnerXml));

            //DataTable rows = dsItems.Tables["row"];
            //DataRow row = rows.Rows[1];
            //object RowTitle = row["ows_Title"];

            return dsItems;
        }

        public string getListName (string ListTitle){
            XmlNode lists = ls.GetListCollection();
            DataSet dsLists = new DataSet();
            dsLists.ReadXml(new System.IO.StringReader("<?xml version='1.0' encoding='utf-8'?>" + lists.OuterXml));
            foreach (DataRow l in dsLists.Tables["List"].Rows)
            {
                if (((string)l["Title"]) == ListTitle)
                {
                    return (string)l["Name"];
                }
            }
            return null;
        }
        
        public System.Xml.XmlNode updateListData(string listName, System.Xml.XmlNode updates)
        {
            return ls.UpdateListItems(listName, updates);
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
