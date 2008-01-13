using System;
using System.Diagnostics;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using Microsoft.SharePoint;
using System.IO;
using System.Xml.Serialization;

namespace GenerateColumns
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerateColumns();

        }

        private static void GenerateColumns()
        {
            const string PROJECT_DIR = @"C:\_Provinsa\ProvPur\ProvPur\";
            const string COREFILE = "ProvPur";
            const string FILENAME_FIELDS = PROJECT_DIR + @"TEMPLATE\FEATURES\ProvPurFields\fields.xml";
            const string FILENAME_DK = PROJECT_DIR + @"Resources\" + COREFILE + ".da-dk.resx";
            const string FILENAME_UK = PROJECT_DIR + @"Resources\" + COREFILE + ".resx";

            FileStream myStream;
            StreamReader myReader;
            StreamWriter myWriter;

            myStream = new FileStream(FILENAME_FIELDS, FileMode.Open);
            myReader = new StreamReader(myStream);
            string XMLFileFIELDStext = myReader.ReadToEnd();
            myReader.Close();
            myStream.Close();
            string strXML = Regex.Replace(XMLFileFIELDStext, "xmlns=\"[^\"]*\"", "");
            System.Xml.XmlDocument docFIELDS = new System.Xml.XmlDocument();
            docFIELDS.LoadXml(strXML);

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

            System.Data.DataSet dsItems = OpenDataSet("ProPurColumn");
            DataTable rows = dsItems.Tables["row"];

            foreach (DataRow row in rows.Rows)
            {
                int Id = int.Parse((string)row["ows_ID"]);
                string SysName = (string)row["ows_Title"];
                string KolonneType = (string)row["ows_KolonneType"];
                string DisplayNameDK = (string)row["ows_DisplayNameDK"];
                string DisplayNameUK = (string)row["ows_DisplayNameUK"];
                string FieldComment = (string)row["ows_FieldName"];
                string Guid = (string)row["ows_GUID0"];

                createFieldElement(ref docFIELDS, Guid, SysName, COREFILE, KolonneType);
                createDataElement(ref docDK, SysName, DisplayNameDK, FieldComment);
                createDataElement(ref docUK, SysName, DisplayNameUK, FieldComment);
            }

            strXML = Regex.Replace(docFIELDS.OuterXml, "<Elements>", "<Elements xmlns=\"http://schemas.microsoft.com/sharepoint/\">");

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
    
            System.Xml.XmlElement field = pdoc.CreateElement("Field"); 
            field.SetAttribute("ID", pid); 
            field.SetAttribute("Name", pname); 
            field.SetAttribute("DisplayName", "$Resources:" + pcore + "," + pname + ";"); 
    
            switch (KolonneType) { 
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
    
            System.Xml.XmlNode elements = pdoc.SelectSingleNode("//Elements"); 
            string filter = "//Field[@ID=\"" + pid + "\"]"; 
            System.Xml.XmlNode old_field = elements.SelectSingleNode(filter); 
    
            if (old_field == null) { 
                elements.AppendChild(field); 
            } 
            else { 
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
