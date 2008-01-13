using System;
using System.Diagnostics;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using Microsoft.SharePoint;
using System.IO;
using System.Xml.Serialization;

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

        System.Data.DataSet dsListItems = OpenDataSet("ProPurList");
        DataTable List_rows = dsListItems.Tables["row"];
        System.Data.DataSet dsTypeItems = OpenDataSet("ProPurType");
        DataTable Type_rows = dsTypeItems.Tables["row"];
        System.Data.DataSet dsFieldItems = OpenDataSet("ProPurColumn");
        DataTable Field_rows = dsFieldItems.Tables["row"];

        foreach (DataRow List_row in List_rows.Rows)
        {


            int List_Id = int.Parse((string)List_row["ows_ID"]);
            string List_SysName = (string)List_row["ows_Title"];
            string[] List_Types = Regex.Split((string)List_row["ows_BasedOn"], ";#");
            string List_DisplayNameDK = (string)List_row["ows_DisplayNameDK"];
            string List_DisplayNameUK = (string)List_row["ows_DisplayNameUK"];
            string List_Comment = (string)List_row["ows_Comment"];

            if (List_SysName == LISTNAME)
            {
                createListElement(ref docLIST, List_SysName, COREFILE, List_Types, ref Type_rows, ref Field_rows);
                createDataElement(ref docDK, List_SysName, List_DisplayNameDK, List_Comment);
                createDataElement(ref docUK, List_SysName, List_DisplayNameDK, List_Comment);
                break;
            }
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
    // OpenDataSet
    //
    private static System.Data.DataSet OpenDataSet(string ListName)
    {
        System.Xml.Serialization.XmlSerializer ser = null;
        FileStream reader;
        string FileName = null;

        switch (ListName)
        {
            case "ProPurList":
                FileName = @"C:\_Provinsa\DATASET\dsListItems.xml";
                break;
            case "ProPurType":
                FileName = @"C:\_Provinsa\DATASET\dsTypeItems.xml";
                break;
            case "ProPurColumn":
                FileName = @"C:\_Provinsa\DATASET\dsFieldItems.xml";
                break;

        }

        System.Data.DataSet ds = new System.Data.DataSet();
        ser = new System.Xml.Serialization.XmlSerializer(ds.GetType());
        reader = new FileStream(FileName, FileMode.Open);
        ds = (System.Data.DataSet)ser.Deserialize(reader);
        return ds;
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
        string[] ptypes, 
        ref DataTable ptypestable, 
        ref DataTable pfieldstable)
    {

        // 
        // Insert ContentTypeRef 
        // 
        System.Xml.XmlNode contenttypes = pdoc.SelectSingleNode("//List/MetaData/ContentTypes");
        foreach (DataRow Type_row in ptypestable.Rows)
        {
            int Type_Id = int.Parse((string)Type_row["ows_ID"]);
            for (int i = 0; i <= ptypes.Length - 1; i += 2)
            {
                if (Type_Id == int.Parse(ptypes[i]))
                {
                    string Type_SysName = (string)Type_row["ows_Title"];
                    string[] Type_Fields = Regex.Split((string)Type_row["ows_Felter"], ";#");
                    string Type_BasedOn = (string)Type_row["ows_BasedOn"];
                    string Type_DisplayNameDK = (string)Type_row["ows_DisplayNameDK"];
                    string Type_DisplayNameUK = (string)Type_row["ows_DisplayNameUK"];
                    string Type_Comment = (string)Type_row["ows_Comment"];
                    string Type_Guid = (string)Type_row["ows_Type_GUID"];
                    string ContentTypeID;
                    switch (Type_BasedOn)
                    {
                        case "Element":
                            ContentTypeID = "0x01" + "00" + Type_Guid;
                            break;
                        case "Annoncering":
                            ContentTypeID = "0x0104" + "00" + Type_Guid;
                            break;
                        case "Hyperlink":
                            ContentTypeID = "0x0105" + "00" + Type_Guid;
                            break;
                        case "'Kontaktperson":
                            ContentTypeID = "0x0106" + "00" + Type_Guid;
                            break;
                        case "'Meddelelse":
                            ContentTypeID = "0x0107" + "00" + Type_Guid;
                            break;
                        case "'Opgave":
                            ContentTypeID = "0x0108" + "00" + Type_Guid;
                            break;
                        case "'Problem":
                            ContentTypeID = "0x0103" + "00" + Type_Guid;
                            break;
                        default:
                            ContentTypeID = "Error" + "00" + Type_Guid;
                            break;
                    }

                    System.Xml.XmlElement contenttyperef = pdoc.CreateElement("ContentTypeRef");
                    System.Xml.XmlComment contenttypescomment = pdoc.CreateComment(Type_SysName);
                    contenttyperef.SetAttribute("ID", ContentTypeID);
                    contenttypes.AppendChild(contenttypescomment);
                    contenttypes.AppendChild(contenttyperef);
                    break; // TODO: might not be correct. Was : Exit For 
                }
            }
        }

        // 
        // Insert FieldsRef in Fields 
        // 
        System.Xml.XmlNode fields = pdoc.SelectSingleNode("//List/MetaData/Fields");
        foreach (DataRow Type_row in ptypestable.Rows)
        {
            int Type_Id = int.Parse((string)Type_row["ows_ID"]);
            for (int i = 0; i <= ptypes.Length - 1; i += 2)
            {
                if (Type_Id == int.Parse(ptypes[i]))
                {
                    string[] Type_Fields = Regex.Split((string)Type_row["ows_Felter"], ";#");
                    foreach (DataRow Field_row in pfieldstable.Rows)
                    {
                        int Field_Id = int.Parse((string)Field_row["ows_ID"]);
                        for (int j = 0; j <= Type_Fields.Length - 1; j += 2)
                        {
                            if (Field_Id == int.Parse(Type_Fields[j]))
                            {
                                string Field_SysName = (string)Field_row["ows_Title"];
                                string Field_KolonneType = (string)Field_row["ows_KolonneType"];
                                string Field_DisplayNameDK = (string)Field_row["ows_DisplayNameDK"];
                                string Field_DisplayNameUK = (string)Field_row["ows_DisplayNameUK"];
                                string Field_Comment = (string)Field_row["ows_FieldName"];
                                string Guid = (string)Field_row["ows_GUID0"];

                                System.Xml.XmlElement fieldref = pdoc.CreateElement("FieldRef");
                                fieldref.SetAttribute("ID", Guid);
                                fieldref.SetAttribute("Name", Field_SysName);
                                fields.AppendChild(fieldref);
                                break; // TODO: might not be correct. Was : Exit For 
                            }
                        }
                    }
                    break; // TODO: might not be correct. Was : Exit For 
                }
            }
        }

        // 
        // Insert FieldsRef in ViewFields 
        // 
        System.Xml.XmlNode viewfields = pdoc.SelectSingleNode("//List/MetaData/Views/View[@BaseViewID=\"1\"]/ViewFields");
        foreach (DataRow Type_row in ptypestable.Rows)
        {
            int Type_Id = int.Parse((string)Type_row["ows_ID"]);
            for (int i = 0; i <= ptypes.Length - 1; i += 2)
            {
                if (Type_Id == int.Parse(ptypes[i]))
                {
                    string[] Type_Fields = Regex.Split((string)Type_row["ows_Felter"], ";#");
                    foreach (DataRow Field_row in pfieldstable.Rows)
                    {
                        int Field_Id = int.Parse((string)Field_row["ows_ID"]);
                        for (int j = 0; j <= Type_Fields.Length - 1; j += 2)
                        {
                            if (Field_Id == int.Parse(Type_Fields[j]))
                            {
                                string Field_SysName = (string)Field_row["ows_Title"];
                                string Field_KolonneType = (string)Field_row["ows_KolonneType"];
                                string Field_DisplayNameDK = (string)Field_row["ows_DisplayNameDK"];
                                string Field_DisplayNameUK = (string)Field_row["ows_DisplayNameUK"];
                                string Field_Comment = (string)Field_row["ows_FieldName"];
                                string Guid = (string)Field_row["ows_GUID0"];

                                System.Xml.XmlElement fieldref = pdoc.CreateElement("FieldRef");
                                fieldref.SetAttribute("ID", Guid);
                                fieldref.SetAttribute("Name", Field_SysName);
                                viewfields.AppendChild(fieldref);
                                break; // TODO: might not be correct. Was : Exit For 
                            }
                        }
                    }
                    break; // TODO: might not be correct. Was : Exit For 
                }
            }
        }
    }
}