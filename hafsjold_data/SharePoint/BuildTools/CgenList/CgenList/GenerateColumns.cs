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



class GenerateColumns
{

    static Metadata model;
    static XmlNamespaceManager nsMgr;
    static string ns;

    public static void MainGenerateColumns(string[] args)
    {
        model = new Metadata();
        Generate();

    }

    private static void Generate()
    {
        const string PROJECT_DIR = @"C:\_Provinsa\ProvPur\ProvPur\";
        const string COREFILE = "ProvPur";
        const string FILENAME_FIELDS = PROJECT_DIR + @"TEMPLATE\FEATURES\ProvPurFields\fields.xml";
        const string FILENAME_DK = PROJECT_DIR + @"Resources\" + COREFILE + ".da-dk.resx";
        const string FILENAME_UK = PROJECT_DIR + @"Resources\" + COREFILE + ".resx";

        FileStream myStream;
        StreamReader myReader;
        StreamWriter myWriter;

        string strXML = "<?xml version=\"1.0\" encoding=\"utf-8\"?><Elements xmlns=\"http://schemas.microsoft.com/sharepoint/\"></Elements>";
        System.Xml.XmlDocument docFIELDS = new System.Xml.XmlDocument();
        docFIELDS.LoadXml(strXML);
        ns = "http://schemas.microsoft.com/sharepoint/";
        nsMgr = new XmlNamespaceManager(docFIELDS.NameTable);
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

        foreach (column col in model.columns.getAllColumns.Values)
        {
            if (!col.SysCol)
            {
                createFieldElement(ref docFIELDS, col.colGUID, col.SysName, COREFILE, col.KolonneType);
                createDataElement(ref docDK, col.SysName, col.DisplayNameDK, col.Comment);
                createDataElement(ref docUK, col.SysName, col.DisplayNameUK, col.Comment);
            }
        }

        strXML = docFIELDS.OuterXml;

        myStream = new FileStream(FILENAME_FIELDS, FileMode.Truncate);
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


    private static void createFieldElement(ref System.Xml.XmlDocument pdoc, string pid, string pname, string pcore, string KolonneType)
    {

        System.Xml.XmlElement field = pdoc.CreateElement("", "Field", ns);
        field.SetAttribute("ID", pid);
        field.SetAttribute("Name", pname);
        field.SetAttribute("DisplayName", "$Resources:" + pcore + "," + pname + ";");

        switch (KolonneType)
        {
            case "Text":
                field.SetAttribute("Type", "Text");
                field.SetAttribute("MaxLength", "255");
                break;

            case "Note":
                field.SetAttribute("Type", "Note");
                field.SetAttribute("NumLines", "3");
                field.SetAttribute("RichText", "TRUE");
                break;

            case "Choice":
                field.SetAttribute("Type", "Choice");
                break;

            case "Number":
                field.SetAttribute("Type", "Number");
                field.SetAttribute("Decimals", "0");
                break;

            case "Percentage":
                field.SetAttribute("Type", "Number");
                field.SetAttribute("Percentage", "TRUE");
                field.SetAttribute("Min", "0");
                field.SetAttribute("Max", "1");
                break;

            case "Currency":
                field.SetAttribute("Type", "Currency");
                field.SetAttribute("Decimals", "2");
                break;

            case "DateOnly":
                field.SetAttribute("Type", "DateTime");
                field.SetAttribute("Format", "DateOnly");
                break;

            case "DateTime":
                field.SetAttribute("Type", "DateTime");
                break;

            case "Boolean":
                field.SetAttribute("Type", "Boolean");
                break;

            default:
                break;

        }

        field.SetAttribute("Group", "$Resources:" + pcore + ",FieldsGroupName;");

        System.Xml.XmlNode elements = pdoc.SelectSingleNode("//mha:Elements", nsMgr);
        string filter = "//mha:Field[@ID=\"" + pid + "\"]";
        System.Xml.XmlNode old_field = elements.SelectSingleNode(filter, nsMgr);

        if (old_field == null)
        {
            elements.AppendChild(field);
        }
        else
        {
            elements.ReplaceChild(field, old_field);
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
