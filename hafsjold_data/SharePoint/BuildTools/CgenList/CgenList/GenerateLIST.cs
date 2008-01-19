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

partial class genListMain
{
    //
    // GenerateLIST
    //
    private static void GenerateLIST()
    {
        const string COREFILE = "ProvPur";
        string FILENAME_DK = PROJECT_DIR + @"Resources\" + COREFILE + ".da-dk.resx";
        string FILENAME_UK = PROJECT_DIR + @"Resources\" + COREFILE + ".resx";

        FileStream myStream;
        StreamReader myReader;
        StreamWriter myWriter;

        myStream = new FileStream(SCHEMA_FILE, FileMode.Open);
        myReader = new StreamReader(myStream);
        string XMLFileLISTtext = myReader.ReadToEnd();
        myReader.Close();
        myStream.Close();
        string strXML = Regex.Replace(XMLFileLISTtext, "xmlns=\"[^\"]*\"", "");
        System.Xml.XmlDocument docLIST = new System.Xml.XmlDocument();
        docLIST.PreserveWhitespace = true;
        docLIST.LoadXml(strXML);


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

        listtemplate list = model.listtemplates.getListtemplate(LISTNAME);
        if (list != null)
        {
            createListElement(ref docLIST, list.SysName, COREFILE, list.ListtemplateContenttypes, list.ListtemplateColumns);
            createDataElement(ref docDK, list.SysName, list.DisplayNameDK, list.Comment);
            createDataElement(ref docUK, list.SysName, list.DisplayNameUK, list.Comment);
        }

        strXML = Regex.Replace(docLIST.OuterXml, "<Elements>", "<Elements xmlns=\"http://schemas.microsoft.com/sharepoint/\">");

        myStream = new FileStream(SCHEMA_FILE, FileMode.Truncate);
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


    //
    //createDataElement
    //
    private static void createDataElement(
        ref System.Xml.XmlDocument pdoc,
        string pname, string pvalue,
        string pcomment)
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

    //
    //createListElement
    //
    private static void createListElement(
        ref System.Xml.XmlDocument pdoc,
        string pname,
        string pcore,
        System.Collections.Generic.List<pvMetadata.ListtemplateContenttype> ptypestable,
        System.Collections.Generic.List<pvMetadata.ListtemplateColumn> pfieldstable)
    {


        // 
        // Insert ContentTypeRef 
        // 
        System.Xml.XmlNode contenttypes = pdoc.SelectSingleNode("//List/MetaData/ContentTypes");
        foreach (ListtemplateContenttype typ in ptypestable)
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

            System.Xml.XmlElement contenttyperef = pdoc.CreateElement("ContentTypeRef");
            System.Xml.XmlComment contenttypescomment = pdoc.CreateComment(typ.SysName);
            contenttyperef.SetAttribute("ID", ContentTypeID);

            System.Xml.XmlNode ContentTypeRef0x01 = pdoc.SelectSingleNode("//List/MetaData/ContentTypes/ContentTypeRef[@ID=\"0x01\"]");
            if (ContentTypeRef0x01 == null)
            {
                System.Xml.XmlNode lastContentTypeRef = contenttypes.AppendChild(contenttyperef);
                contenttypes.InsertBefore(contenttypescomment, lastContentTypeRef);
            }
            else
            {
                System.Xml.XmlNode Folder = pdoc.SelectSingleNode("//List/MetaData/ContentTypes/ContentTypeRef[@ID=\"0x01\"]/Folder");
                if (Folder != null)
                {
                    System.Xml.XmlNode copyFolder = Folder.CloneNode(true);
                    contenttyperef.AppendChild(copyFolder);
                }
                contenttypes.InsertBefore(contenttypescomment, ContentTypeRef0x01);
                contenttypes.ReplaceChild(contenttyperef, ContentTypeRef0x01);
            }
        }


        // 
        // Insert Field in Fields 
        // 
        System.Xml.XmlNode fields = pdoc.SelectSingleNode("//List/MetaData/Fields");
        foreach (ListtemplateColumn col in pfieldstable)
        {
            System.Xml.XmlElement fieldref = pdoc.CreateElement("Field");
            fieldref.SetAttribute("Name", col.SysName);
            fieldref.SetAttribute("ID", col.colGUID);
            fieldref.SetAttribute("DisplayName", "$Resources:" + pcore + "," + col.SysName + ";");

            switch (col.KolonneType)
            {
                case "Text":
                    fieldref.SetAttribute("Type", "Text");
                    fieldref.SetAttribute("MaxLength", "255");
                    break;

                case "Note":
                    fieldref.SetAttribute("Type", "Note");
                    fieldref.SetAttribute("NumLines", "3");
                    fieldref.SetAttribute("RichText", "TRUE");
                    break;

                case "Choice":
                    fieldref.SetAttribute("Type", "Choice");
                    break;

                case "Number":
                    fieldref.SetAttribute("Type", "Number");
                    fieldref.SetAttribute("Decimals", "0");
                    break;

                case "Percentage":
                    fieldref.SetAttribute("Type", "Number");
                    fieldref.SetAttribute("Percentage", "TRUE");
                    fieldref.SetAttribute("Min", "0");
                    fieldref.SetAttribute("Max", "1");
                    break;

                case "Currency":
                    fieldref.SetAttribute("Type", "Currency");
                    fieldref.SetAttribute("Decimals", "2");
                    break;

                case "DateOnly":
                    fieldref.SetAttribute("Type", "DateTime");
                    fieldref.SetAttribute("Format", "DateOnly");
                    break;

                case "DateTime":
                    fieldref.SetAttribute("Type", "DateTime");
                    break;

                case "Boolean":
                    fieldref.SetAttribute("Type", "Boolean");
                    break;

                default:
                    break;

            }

            fields.AppendChild(fieldref);
        }

        // 
        // Insert FieldsRef in ViewFields 
        // 
        System.Xml.XmlNode viewfields = pdoc.SelectSingleNode("//List/MetaData/Views/View[@BaseViewID=\"1\"]/ViewFields");
        foreach (ListtemplateColumn col in pfieldstable)
        {
            System.Xml.XmlElement fieldref = pdoc.CreateElement("FieldRef");
            fieldref.SetAttribute("ID", col.colGUID);
            fieldref.SetAttribute("Name", col.SysName);
            viewfields.AppendChild(fieldref);
        }
    }
}