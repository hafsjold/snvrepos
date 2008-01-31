using System;
using System.Diagnostics;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using Microsoft.SharePoint;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using pvMetadata;

namespace SPGenerateWithMSBuils
{
    public class GenColumns : Task
    {
        [Required]
        public ITaskItem SVNRootPath { get; set; }
        [Output]
        public ITaskItem FILENAME_FIELDS { get; set; }
        [Required]
        public ITaskItem COREFILE { get; set; }
        [Required]
        public ITaskItem FILENAME_DK { get; set; }
        [Required]
        public ITaskItem FILENAME_UK  { get; set; }

        private Metadata model { get; set; }
        private XmlNamespaceManager nsMgr { get; set; }
        private string ns { get; set; }

        public override bool Execute()
        {
            FileStream myStream;
            StreamReader myReader;
            StreamWriter myWriter;

            //Debug.Fail("Attach to MSBuild.exe and press Ignore");

            model = new Metadata(SVNRootPath.ItemSpec);

            string strXML = "<?xml version=\"1.0\" encoding=\"utf-8\"?><Elements xmlns=\"http://schemas.microsoft.com/sharepoint/\"></Elements>";
            System.Xml.XmlDocument docFIELDS = new System.Xml.XmlDocument();
            docFIELDS.LoadXml(strXML);
            ns = "http://schemas.microsoft.com/sharepoint/";
            nsMgr = new XmlNamespaceManager(docFIELDS.NameTable);
            nsMgr.AddNamespace("mha", "http://schemas.microsoft.com/sharepoint/");


            myStream = new FileStream(FILENAME_DK.ItemSpec, FileMode.Open);
            myReader = new StreamReader(myStream);
            string XMLFileDKtext = myReader.ReadToEnd();
            myReader.Close();
            myStream.Close();
            System.Xml.XmlDocument docDK = new System.Xml.XmlDocument();
            docDK.LoadXml(XMLFileDKtext);

            myStream = new FileStream(FILENAME_UK.ItemSpec, FileMode.Open);
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
                    createFieldElement(ref docFIELDS, col.colGUID, col.SysName, COREFILE.ItemSpec, col.KolonneType);
                    createDataElement(ref docDK, col.SysName, col.DisplayNameDK, col.Comment);
                    createDataElement(ref docUK, col.SysName, col.DisplayNameUK, col.Comment);
                }
            }

            strXML = docFIELDS.OuterXml;

            myStream = new FileStream(FILENAME_FIELDS.ItemSpec, FileMode.Truncate);
            myWriter = new StreamWriter(myStream);
            myWriter.Write(strXML);
            myWriter.Close();
            myStream.Close();

            myStream = new FileStream(FILENAME_DK.ItemSpec, FileMode.Truncate);
            myWriter = new StreamWriter(myStream);
            myWriter.Write(docDK.OuterXml);
            myWriter.Close();
            myStream.Close();

            myStream = new FileStream(FILENAME_UK.ItemSpec, FileMode.Truncate);
            myWriter = new StreamWriter(myStream);
            myWriter.Write(docUK.OuterXml);
            myWriter.Close();
            myStream.Close();

            return true;
        }


        private void createFieldElement(ref System.Xml.XmlDocument pdoc, string pid, string pname, string pcore, string KolonneType)
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

}
