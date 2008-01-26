using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using Microsoft.SharePoint;
using System.IO;
using System.Xml.Serialization;
using pvMetadata;

class GenerateTypes
{
    static Metadata model;
    static XmlNamespaceManager nsMgr;
    static string ns;

    public static void GenerateTypesMain(string[] args)
    {
        model = new Metadata();
        GenerateTypes1();
    }

    private static void GenerateTypes1()
    {
        const string PROJECT_DIR = @"C:\_Provinsa\ProvPur\ProvPur\";
        const string COREFILE = "ProvPur";
        const string FILENAME_TYPES = PROJECT_DIR + @"TEMPLATE\FEATURES\ProvPurTypes\types.xml";
        const string FILENAME_DK = PROJECT_DIR + @"Resources\" + COREFILE + ".da-dk.resx";
        const string FILENAME_UK = PROJECT_DIR + @"Resources\" + COREFILE + ".resx";

        FileStream myStream;
        StreamReader myReader;
        StreamWriter myWriter;

        string strXML = "<?xml version=\"1.0\" encoding=\"utf-8\"?><Elements xmlns=\"http://schemas.microsoft.com/sharepoint/\"></Elements>";
        System.Xml.XmlDocument docTYPES = new System.Xml.XmlDocument();
        docTYPES.LoadXml(strXML);
        ns = "http://schemas.microsoft.com/sharepoint/";
        nsMgr = new XmlNamespaceManager(docTYPES.NameTable);
        nsMgr.AddNamespace("mha", "http://schemas.microsoft.com/sharepoint/");


        myStream = new FileStream(FILENAME_DK, FileMode.Open);
        myReader = new StreamReader(myStream);
        string XMLFileDKtext = myReader.ReadToEnd();
        myReader.Close();
        myStream.Close();
        System.Xml.XmlDocument docDK = new System.Xml.XmlDocument();
        docDK.LoadXml(XMLFileDKtext);

        myStream = new FileStream(FILENAME_UK, FileMode.Open);
        myReader = new StreamReader(myStream);
        string XMLFileUKtext = myReader.ReadToEnd();
        myReader.Close();
        myStream.Close();
        System.Xml.XmlDocument docUK = new System.Xml.XmlDocument();
        docUK.LoadXml(XMLFileUKtext);

        foreach (contenttype typ in model.contenttypes.getAllContenttypes.Values)
        {
            string ContentTypeID;
            switch (typ.BasedOn)
            {
                case "Element":
                    ContentTypeID = "0x01" + "00" + typ.typeGUID;
                    break;
                case "Annoncering":
                    ContentTypeID = "0x0104" + "00" + typ.typeGUID;
                    break;
                case "Hyperlink":
                    ContentTypeID = "0x0105" + "00" + typ.typeGUID;
                    break;
                case "'Kontaktperson":
                    ContentTypeID = "0x0106" + "00" + typ.typeGUID;
                    break;
                case "'Meddelelse":
                    ContentTypeID = "0x0107" + "00" + typ.typeGUID;
                    break;
                case "'Opgave":
                    ContentTypeID = "0x0108" + "00" + typ.typeGUID;
                    break;
                case "'Problem":
                    ContentTypeID = "0x0103" + "00" + typ.typeGUID;
                    break;
                default:
                    ContentTypeID = "Error" + "00" + typ.typeGUID;
                    break;
            }
            createTypeElement(ref docTYPES, ContentTypeID, typ.SysName, COREFILE, typ.ContenttypeColumns);
            createDataElement(ref docDK, typ.SysName, typ.DisplayNameDK, typ.Comment);
            createDataElement(ref docUK, typ.SysName, typ.DisplayNameUK, typ.Comment);

        }

        strXML = docTYPES.OuterXml;

        myStream = new FileStream(FILENAME_TYPES, FileMode.Truncate);
        myWriter = new StreamWriter(myStream);
        myWriter.Write(strXML);
        myWriter.Close();
        myStream.Close();

        myStream = new FileStream(FILENAME_DK, FileMode.Truncate);
        myWriter = new StreamWriter(myStream);
        myWriter.Write(docDK.OuterXml);
        myWriter.Close();
        myStream.Close();

        myStream = new FileStream(FILENAME_UK, FileMode.Truncate);
        myWriter = new StreamWriter(myStream);
        myWriter.Write(docUK.OuterXml);
        myWriter.Close();
        myStream.Close();
    }

    private static void createTypeElement(ref System.Xml.XmlDocument pdoc, string pid, string pname, string pcore, System.Collections.Generic.Dictionary<int, ContenttypeColumn> pfieldstable)
    {

        System.Xml.XmlElement contenttype = pdoc.CreateElement("", "ContentType", ns);
        contenttype.SetAttribute("ID", pid);
        contenttype.SetAttribute("Name", pname);
        contenttype.SetAttribute("Group", "$Resources:" + pcore + ",TypesGroupName;");
        contenttype.SetAttribute("Description", "$Resources:" + pcore + "," + pname + ";");
        contenttype.SetAttribute("Version", "0");

        System.Xml.XmlElement fieldrefs = pdoc.CreateElement("", "FieldRefs", ns);
        contenttype.AppendChild(fieldrefs);

        System.Collections.Generic.SortedDictionary<string, ContenttypeColumn> scol = new System.Collections.Generic.SortedDictionary<string, ContenttypeColumn>();
        foreach (ContenttypeColumn col in pfieldstable.Values)
        {
            scol.Add(col.Seqnr, col);
        }

        foreach (ContenttypeColumn col in scol.Values)
        {
            if (!col.SysCol)
            {
                System.Xml.XmlElement fieldref = pdoc.CreateElement("", "FieldRef", ns);
                fieldref.SetAttribute("ID", col.colGUID);
                fieldref.SetAttribute("Name", col.SysName);
                fieldrefs.AppendChild(fieldref);
            }
        }

        System.Xml.XmlNode elements = pdoc.SelectSingleNode("//mha:Elements", nsMgr);
        string filter = "//mha:ContentType[@ID=\"" + pid + "\"]";
        System.Xml.XmlNode old_contenttype = elements.SelectSingleNode(filter, nsMgr);

        if (old_contenttype == null)
        {
            elements.AppendChild(contenttype);
        }
        else
        {
            elements.ReplaceChild(contenttype, old_contenttype);
        }
    }

    private static void createDataElement(ref System.Xml.XmlDocument pdoc, string pname, string pvalue, string pcomment)
    {

        System.Xml.XmlElement data = pdoc.CreateElement("data");
        data.SetAttribute("name", pname);
        data.SetAttribute("xml:space", "preserve");

        System.Xml.XmlElement value = pdoc.CreateElement("value");
        value.InnerText = pvalue;
        data.AppendChild(value);

        if (!(pcomment == null))
        {
            System.Xml.XmlElement comment = pdoc.CreateElement("comment");
            comment.InnerText = pcomment;
            data.AppendChild(comment);
        }

        System.Xml.XmlNode root = pdoc.SelectSingleNode("//root");
        System.Xml.XmlNode old_data = root.SelectSingleNode("//data[@name='" + pname + "']");
        if (old_data == null)
        {
            root.AppendChild(data);
        }
        else
        {
            root.ReplaceChild(data, old_data);
        }
    }
}

