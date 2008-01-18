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


namespace testXPath
{
    class TestXPath
    {
        static string TESTFILEPATH1 = @"C:\_Provinsa\TEMPLATE\EventRecievers\TYPEItemEventReceiver.xml";
        static string TESTFILEPATH2 = @"C:\_Provinsa\ProvPur\ProvPur\TEMPLATE\FEATURES\ProvPurFields\fields.xml";

        static void Main(string[] args)
        {
            Metadata myMetadata1 = new Metadata();
            listtemplate test1 = myMetadata1.listtemplates.getAllListtemplates[1];
            object test2 = test1.ListtemplateContenttypes;
            object test3 = test1.ListtemplateColumns;
            //Test2();
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

    }
}
