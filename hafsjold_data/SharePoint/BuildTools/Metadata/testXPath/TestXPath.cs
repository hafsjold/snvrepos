using System;
using System.Diagnostics;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using Microsoft.SharePoint;
using System.IO;
using System.Xml.Serialization;
using pvMetadata;
using System.Collections.Generic;


namespace testXPath
{
    class TestXPath
    {
        static string TESTFILEPATH1 = @"C:\_Provinsa\TEMPLATE\EventRecievers\TYPEItemEventReceiver.xml";
        static string TESTFILEPATH2 = @"C:\_Provinsa\ProvPur\ProvPur\TEMPLATE\FEATURES\ProvPurFields\fields.xml";
        static string TESTFILEPATH3 = @"C:\_BuildTools\CgenList\testXPath\list.xml";

        static void Main(string[] args)
        {
            Metadata myMetadata1 = new Metadata();
            System.Collections.Generic.SortedDictionary<string, ListtemplateColumn> sd;
            sd = myMetadata1.ListtemplateColumnsSort("ProductCatalog");
            sd = myMetadata1.ListtemplateColumnsSort("Rekvirent");

            //Test3();
        }

        private static void Test1()
        {
            System.Xml.XmlDocument docTEST = new System.Xml.XmlDocument();
            docTEST.PreserveWhitespace = true;
            docTEST.Load(TESTFILEPATH1);
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(docTEST.NameTable);
            nsMgr.AddNamespace("spe", "http://schemas.microsoft.com/sharepoint/events");

            System.Xml.XmlNode node1 = docTEST.SelectSingleNode("//XmlDocuments/XmlDocument/spe:Receivers/spe:Receiver/spe:Name", nsMgr);

            System.Xml.XmlNode node2 = docTEST.SelectSingleNode("//XmlDocuments/XmlDocument", nsMgr);

            //docTEST.Save(TESTFILEPATH1);
        }

        private static void Test2()
        {
            XmlNamespaceManager nsMgr1 = new XmlNamespaceManager(new NameTable());
            nsMgr1.AddNamespace("mha", "http://schemas.microsoft.com/sharepoint/");
            foreach (string pf in nsMgr1)
            {
                string ns = nsMgr1.LookupNamespace(pf);
            }

            System.Xml.XmlDocument docTEST = new System.Xml.XmlDocument();
            docTEST.PreserveWhitespace = true;
            docTEST.Load(TESTFILEPATH2);
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(docTEST.NameTable);
            nsMgr.AddNamespace("mha", "http://schemas.microsoft.com/sharepoint/");

            System.Xml.XmlNode node1 = docTEST.SelectSingleNode("//mha:Elements/mha:Field[@Name=\"deltelno\"]", nsMgr);

            System.Xml.XmlNode node2 = docTEST.SelectSingleNode("//XmlDocuments/XmlDocument", nsMgr);

            //docTEST.Save(TESTFILEPATH2);
        }

        private static void Test3()
        {
            System.Xml.XmlDocument docTEST = new System.Xml.XmlDocument();
            docTEST.PreserveWhitespace = true;
            docTEST.Load(TESTFILEPATH3);
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(docTEST.NameTable);
            nsMgr.AddNamespace("s", "uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882");
            nsMgr.AddNamespace("dt", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882");
            nsMgr.AddNamespace("rs", "urn:schemas-microsoft-com:rowset");
            nsMgr.AddNamespace("z", "#RowsetSchema");
            nsMgr.AddNamespace("mha", "http://schemas.microsoft.com/sharepoint/soap/");

            System.Xml.XmlNode node1 = docTEST.SelectSingleNode("//mha:listitems/rs:data/z:row", nsMgr);

            System.Xml.XmlNode node2 = docTEST.SelectSingleNode("//XmlDocuments/XmlDocument", nsMgr);

            //docTEST.Save(TESTFILEPATH2);
        }
    }
}
