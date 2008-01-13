using System;
using System.Diagnostics;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using Microsoft.SharePoint;
using System.IO;
using System.Xml.Serialization;

namespace GenerateTypes
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        private static void GenerateTypes()
        {
            const string PROJECT_DIR = @"C:\_Provinsa\ProvPur\ProvPur\";
            const string COREFILE = "ProvPur";
            const string FILENAME_TYPES = PROJECT_DIR + @"TEMPLATE\FEATURES\ProvPurTypes\types.xml";
            const string FILENAME_DK = PROJECT_DIR + @"Resources\" + COREFILE + ".da-dk.resx";
            const string FILENAME_UK = PROJECT_DIR + @"Resources\" + COREFILE + ".resx";

            FileStream myStream;
            StreamReader myReader;
            StreamWriter myWriter;

            myStream = new FileStream(FILENAME_TYPES, FileMode.Open);
            myReader = new StreamReader(myStream);
            string XMLFileTYPEStext = myReader.ReadToEnd();
            myReader.Close();
            myStream.Close();
            string strXML = Regex.Replace(XMLFileTYPEStext, "xmlns=\"[^\"]*\"", "");
            System.Xml.XmlDocument docTYPES = new System.Xml.XmlDocument();
            docTYPES.LoadXml(strXML);

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

            System.Data.DataSet dsTypeItems = OpenDataSet("ProPurType");
            DataTable Type_rows = dsTypeItems.Tables["row"];
            System.Data.DataSet dsFieldItems = OpenDataSet("ProPurColumn");
            DataTable Field_rows = dsFieldItems.Tables["row"];

            foreach (DataRow Type_row in Type_rows.Rows)
            {

                int Type_Id = int.Parse((string)Type_row["ows_ID"]);
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

                createTypeElement(ref docTYPES, ContentTypeID, Type_SysName, COREFILE, Type_Fields, ref Field_rows);
                createDataElement(ref docDK, Type_SysName, Type_DisplayNameDK, Type_Comment);
                createDataElement(ref docUK, Type_SysName, Type_DisplayNameUK, Type_Comment);

            }

            strXML = Regex.Replace(docTYPES.OuterXml, "<Elements>", "<Elements xmlns=\"http://schemas.microsoft.com/sharepoint/\">");
/*
            EditPoint starteditPntTYPES = XMLFileTYPES.StartPoint.CreateEditPoint();
            EditPoint endeditPntTYPES = XMLFileTYPES.StartPoint.CreateEditPoint;
            starteditPntTYPES.StartOfDocument();
            endeditPntTYPES.EndOfDocument();
            starteditPntTYPES.Delete(endeditPntTYPES);
            starteditPntTYPES.Insert(strXML);

            EditPoint starteditPntDK = XMLFileDK.StartPoint.CreateEditPoint();
            EditPoint endeditPntDK = XMLFileDK.StartPoint.CreateEditPoint;
            starteditPntDK.StartOfDocument();
            endeditPntDK.EndOfDocument();
            starteditPntDK.Delete(endeditPntDK);
            starteditPntDK.Insert(docDK.OuterXml);

            EditPoint starteditPntUK = XMLFileUK.StartPoint.CreateEditPoint();
            EditPoint endeditPntUK = XMLFileUK.StartPoint.CreateEditPoint;
            starteditPntUK.StartOfDocument();
            endeditPntUK.EndOfDocument();
            starteditPntUK.Delete(endeditPntUK);
            starteditPntUK.Insert(docUK.OuterXml);
*/
        }


        private static void createTypeElement(ref System.Xml.XmlDocument pdoc, string pid, string pname, string pcore, string[] pfields, ref DataTable pfieldstable)
        {

            System.Xml.XmlElement contenttype = pdoc.CreateElement("ContentType");
            contenttype.SetAttribute("ID", pid);
            contenttype.SetAttribute("Name", pname);
            contenttype.SetAttribute("Group", "$Resources:" + pcore + ",TypesGroupName;");
            contenttype.SetAttribute("Description", "$Resources:" + pcore + "," + pname + ";");
            contenttype.SetAttribute("Version", "0");

            System.Xml.XmlElement fieldrefs = pdoc.CreateElement("FieldRefs");
            contenttype.AppendChild(fieldrefs);


            foreach (DataRow Field_row in pfieldstable.Rows)
            {
                int Field_Id = int.Parse((string)Field_row["ows_ID"]);
                for (int i = 0; i <= pfields.Length - 1; i += 2)
                {
                    if (Field_Id == int.Parse(pfields[i]))
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
                        fieldrefs.AppendChild(fieldref);
                        break; // TODO: might not be correct. Was : Exit For 
                    }
                }
            }

            System.Xml.XmlNode elements = pdoc.SelectSingleNode("//Elements");
            string filter = "//ContentType[@ID=\"" + pid + "\"]";
            System.Xml.XmlNode old_contenttype = elements.SelectSingleNode(filter);

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
    }
}
