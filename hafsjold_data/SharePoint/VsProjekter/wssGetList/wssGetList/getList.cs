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

        public void test(string ListTitle){
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
            
            System.Xml.XmlDocument doc2 = new System.Xml.XmlDocument();
            string xmlString = "<Batch OnError='Continue' ListVersion='1'>";
            xmlString += "<Method ID='1' Cmd='Update'><Field Name='ID'>1</Field><Field Name='Title'>Mogens Hafsjold</Field></Method>";
            xmlString += "<Method ID='2' Cmd='Update'><Field Name='ID'>2</Field><Field Name='Title'>Julie Hafsjold</Field></Method>";
            xmlString += "</Batch>";
            doc2.LoadXml(xmlString);
            System.Xml.XmlNode myitems = doc2.SelectSingleNode("//Batch");

            System.Xml.XmlNode myresult = ls.UpdateListItems("EE24CD1A-B465-4CA7-8F79-96F0BAB67247", myitems);

            /*
  Name="{EE24CD1A-B465-4CA7-8F79-96F0BAB67247}"
  DocTemplateUrl=""
  DefaultViewUrl="/Lists/TestListe/AllItems.aspx"
  MobileDefaultViewUrl=""
  ID="{EE24CD1A-B465-4CA7-8F79-96F0BAB67247}"
  Title="TestListe"
  Description=""
  ImageUrl="/_layouts/images/itgen.gif"
  BaseType="0"
  FeatureId="00bfea71-de22-43b2-a848-c05709900100"
  ServerTemplate="100"
  Created="20071125 01:18:24"
  Modified="20071125 02:42:00"
  LastDeleted="20071125 01:18:24"
  Version="1"
  Direction="none"
  ThumbnailSize=""
  WebImageWidth=""
  WebImageHeight=""
  Flags="545263616"
  ItemCount="3"
  AnonymousPermMask="0"
  RootFolder=""
  ReadSecurity="1"
  WriteSecurity="1"
  Author="1"
  EventSinkAssembly=""
  EventSinkClass=""
  EventSinkData=""
  EmailInsertsFolder=""
  EmailAlias=""
  WebFullUrl="/"
  WebId="6dc093fc-61fd-4253-a02c-4e3ac727135d"
  SendToLocation=""
  ScopeId="4b85c08e-eafc-4a36-96a2-d6772463ede0"
  MajorVersionLimit="0"
  MajorWithMinorVersionsLimit="0"
  WorkFlowId=""
  HasUniqueScopes="False"
  AllowDeletion="True"
  AllowMultiResponses="False"
  EnableAttachments="True"
  EnableModeration="False"
  EnableVersioning="False"
  Hidden="False"
  MultipleDataList="False"
  Ordered="False"
  ShowUser="True"
  EnableMinorVersion="False"
  RequireCheckout="False"
*/
        }


    }
}
