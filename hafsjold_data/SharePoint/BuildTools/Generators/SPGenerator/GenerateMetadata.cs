using System;
using System.Xml;
using System.Xml.Schema;
using wssGetList;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.SharePoint;
using System.IO;
using System.Xml.Serialization;

    class GenerateMetadata
    {
        private static string DATASET_PATH = null;
        private static bool isGEN_XMLValid = true;

        public static void GenerateMetadataMain(MemoryStream sXML)
        {
            InitParams(sXML);

            wssGetList.getList myList = new wssGetList.getList("http://hd16.hafsjold.dk", "administrator", "m733", "hd16");
            System.Xml.Serialization.XmlSerializer ser;
            XmlTextWriter writer;

            System.Data.DataSet dsListItems = myList.getListData("ProPurList");
            dsListItems.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
            ser = new System.Xml.Serialization.XmlSerializer(dsListItems.GetType());
            writer = new XmlTextWriter(DATASET_PATH + "dsListItems.xml", System.Text.Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            ser.Serialize(writer, dsListItems);
            writer.Close();

            System.Data.DataSet dsListTypeItems = myList.getListData("ProPurListType");
            dsListTypeItems.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
            ser = new System.Xml.Serialization.XmlSerializer(dsListTypeItems.GetType());
            writer = new XmlTextWriter(DATASET_PATH + "dsListTypeItems.xml", System.Text.Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            ser.Serialize(writer, dsListTypeItems);
            writer.Close();

            System.Data.DataSet dsTypeItems = myList.getListData("ProPurType");
            dsTypeItems.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
            ser = new System.Xml.Serialization.XmlSerializer(dsTypeItems.GetType());
            writer = new XmlTextWriter(DATASET_PATH + "dsTypeItems.xml", System.Text.Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            ser.Serialize(writer, dsTypeItems);
            writer.Close();

            System.Data.DataSet dsTypeColumnItems = myList.getListData("ProPurTypeColumn");
            dsTypeColumnItems.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
            ser = new System.Xml.Serialization.XmlSerializer(dsTypeColumnItems.GetType());
            writer = new XmlTextWriter(DATASET_PATH + "dsTypeColumnItems.xml", System.Text.Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            ser.Serialize(writer, dsTypeColumnItems);
            writer.Close();

            System.Data.DataSet dsFieldItems = myList.getListData("ProPurColumn");
            dsFieldItems.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
            ser = new System.Xml.Serialization.XmlSerializer(dsFieldItems.GetType());
            writer = new XmlTextWriter(DATASET_PATH + "dsFieldItems.xml", System.Text.Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            ser.Serialize(writer, dsFieldItems);
            writer.Close();

        }

        private static void InitParams(MemoryStream sXML)
        {

            System.Xml.XmlDocument docGEN_XML = null;
            {
                sXML.Position = 0;
                isGEN_XMLValid = true;
                XmlSchemaCollection myschemacoll = new XmlSchemaCollection();
                XmlValidatingReader vr;

                try
                {
                    //Load the XmlValidatingReader.
                    vr = new XmlValidatingReader(sXML, XmlNodeType.Element, null);

                    //Add the schemas to the XmlSchemaCollection object.
                    myschemacoll.Add("http://www.hafsjold.dk/schema/hafsjold.xsd", @"C:\_BuildTools\Generators\SPGenerator\hafsjold.xsd");
                    vr.Schemas.Add(myschemacoll);
                    vr.ValidationType = ValidationType.Schema;
                    vr.ValidationEventHandler += new ValidationEventHandler(ShowCompileErrors);

                    while (vr.Read())
                    {
                    }
                    Console.WriteLine("Validation completed");
                    docGEN_XML = new System.Xml.XmlDocument();
                    sXML.Position = 0;
                    docGEN_XML.Load(sXML);
                }
                catch
                {
                }
                finally
                {
                    //Clean up.
                    vr = null;
                    myschemacoll = null;
                    sXML = null;
                }
            }

            {
                string l_ns = "http://www.hafsjold.dk/schema/hafsjold.xsd";
                XmlNamespaceManager l_nsMgr = new XmlNamespaceManager(docGEN_XML.NameTable);
                l_nsMgr.AddNamespace("mha", l_ns);

                System.Xml.XmlNode propertySets = docGEN_XML.SelectSingleNode("//mha:hafsjold/mha:propertySets", l_nsMgr);
                string SVNRootPath = propertySets.Attributes["SVNRootPath"].Value;
                DATASET_PATH = SVNRootPath + @"DATASET\";
            }

        }
        public static void ShowCompileErrors(object sender, ValidationEventArgs args)
        {
            isGEN_XMLValid = false;
            Console.WriteLine("Validation Error: {0}", args.Message);
        }

    }
