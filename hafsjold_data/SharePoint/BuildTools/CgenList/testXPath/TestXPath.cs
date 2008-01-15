using System;
using System.Diagnostics;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using Microsoft.SharePoint;
using System.IO;
using System.Xml.Serialization;


namespace testXPath
{
    class TestXPath
    {
        static string TESTFILEPATH1 = @"C:\_Provinsa\TEMPLATE\EventRecievers\TYPEItemEventReceiver.xml";
        static string TESTFILEPATH2 = @"C:\_Provinsa\ProvPur\ProvPur\TEMPLATE\FEATURES\ProvPurFields\fields.xml";

        static void Main(string[] args)
        {
            Test2();
        }

        private static void Test1()
        {
            string strFullPath = TESTFILEPATH1;
            string strFilename = System.IO.Path.GetFileName(strFullPath).ToLower();
            FileStream myStream = new FileStream(strFullPath, FileMode.Open);
            StreamReader myReader = new StreamReader(myStream);
            string strXMLFile = myReader.ReadToEnd();
            myReader.Close();
            myStream.Close();

            string strXML = strXMLFile;
            System.Xml.XmlDocument docTEST = new System.Xml.XmlDocument();
            docTEST.LoadXml(strXML);
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(docTEST.NameTable);
            nsMgr.AddNamespace("spe", "http://schemas.microsoft.com/sharepoint/events");

            System.Xml.XmlNode node1 = docTEST.SelectSingleNode("//XmlDocuments/XmlDocument/spe:Receivers/spe:Receiver/spe:Name", nsMgr);

            System.Xml.XmlNode node2 = docTEST.SelectSingleNode("//XmlDocuments/XmlDocument", nsMgr);

            strXML = docTEST.OuterXml;

            /*
            myStream = new FileStream(strFullPath, FileMode.Truncate);
            StreamWriter myWriter = new StreamWriter(myStream);
            myWriter.Write(strXML);
            myWriter.Close();
            myStream.Close();
            */
        }
        private static void Test2()
        {
            string strFullPath = TESTFILEPATH2;
            string strFilename = System.IO.Path.GetFileName(strFullPath).ToLower();
            FileStream myStream = new FileStream(strFullPath, FileMode.Open);
            StreamReader myReader = new StreamReader(myStream);
            string strXMLFile = myReader.ReadToEnd();
            myReader.Close();
            myStream.Close();

            string strXML = strXMLFile;
            //string strXML = Regex.Replace(strXMLFile, "xmlns=\"[^\"]*\"", "");
            System.Xml.XmlDocument docTEST = new System.Xml.XmlDocument();
            docTEST.LoadXml(strXML);

            XmlNamespaceManager nsMgr = new XmlNamespaceManager(docTEST.NameTable);
            nsMgr.AddNamespace("mha", "http://schemas.microsoft.com/sharepoint/");


            System.Xml.XmlNode node1 = docTEST.SelectSingleNode("//mha:Elements/mha:Field[@Name=\"deltelno\"]", nsMgr);

            System.Xml.XmlNode node2 = docTEST.SelectSingleNode("//XmlDocuments/XmlDocument", nsMgr);

            strXML = docTEST.OuterXml;

            /*
            myStream = new FileStream(strFullPath, FileMode.Truncate);
            StreamWriter myWriter = new StreamWriter(myStream);
            myWriter.Write(strXML);
            myWriter.Close();
            myStream.Close();
            */
        }

    }
}
