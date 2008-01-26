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


class SPGenerate
{
    private static bool isGEN_XMLValid = true;
    private static string GEN_XML = null;
    private static string SVN = null;
    private static System.Xml.XmlDocument docGEN_XML = null;

    static void Main(string[] args)
    {
        InitParams(args);
        ReadandValidateGEN_XML();
        if (isGEN_XMLValid)
        {
            string l_ns = "http://www.hafsjold.dk/schema/hafsjold.xsd";
            XmlNamespaceManager l_nsMgr = new XmlNamespaceManager(docGEN_XML.NameTable);
            l_nsMgr.AddNamespace("mha", l_ns);

            System.Xml.XmlNode propertySets = docGEN_XML.SelectSingleNode("//mha:hafsjold/mha:propertySets", l_nsMgr);
            string SVNRootPath;
            if (SVN == "Empty")
            {
                SVNRootPath = propertySets.Attributes["SVNRootPath"].Value;
            }
            else
            {
                SVNRootPath = SVN;
            }
            string ProjectPath = propertySets.Attributes["ProjectPath"].Value;
            string ProjectName = propertySets.Attributes["ProjectName"].Value;
            System.Xml.XmlNodeList propertySetList = docGEN_XML.SelectNodes("//mha:hafsjold/mha:propertySets/mha:propertySet", l_nsMgr);
            foreach (XmlNode propertySet in propertySetList)
            {
                XmlNode ClonepropertySet = propertySet.CloneNode(true);
                string ClonepropertySetXML = ClonepropertySet.OuterXml;
                string CloneDocument =
                    string.Format(@"<?xml version='1.0' encoding='utf-8'?>
                    <hafsjold xmlns='http://www.hafsjold.dk/schema/hafsjold.xsd'>
                        <propertySets SVNRootPath='{0}' ProjectPath='{1}' ProjectName='{2}'>
                            {3}
                        </propertySets>
                    </hafsjold>", SVNRootPath, ProjectPath, ProjectName, ClonepropertySetXML);
                System.Xml.XmlDocument CloneXmlDocument = new System.Xml.XmlDocument();
                CloneXmlDocument.LoadXml(CloneDocument);
                MemoryStream sXML = new MemoryStream();
                CloneXmlDocument.Save(sXML);

                string Function = propertySet.Attributes["Function"].Value;
                string ProgPath = propertySet.Attributes["ProgPath"].Value;
                string ProgName = propertySet.Attributes["ProgName"].Value;

                switch (Function)
                {
                    case "SaveMetadata":
                        GenerateMetadata.GenerateMetadataMain(sXML);
                        break;

                    case "Column":
                        GenerateColumns.MainGenerateColumns(sXML);
                        break;

                    case "Type":
                        GenerateTypes.GenerateTypesMain(sXML);
                        break;

                    case "List":
                        GenerateList.MainGenerateList(sXML);
                        break;


                }

            }
        }
    }


    private static void InitParams(string[] args)
    {
        string ParmName = "Empty";
        GEN_XML = "Empty";
        SVN = "Empty";

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i].StartsWith("-"))
            {
                ParmName = args[i];
                ParmName = ParmName.ToUpper();
            }
            else
            {
                switch (ParmName)
                {
                    case "-GEN_XML":
                        if (GEN_XML == "Empty")
                            GEN_XML = args[i];
                        else
                            GEN_XML += " " + args[i];
                        break;

                    case "-SVN":
                        if (SVN == "Empty")
                            SVN = args[i];
                        else
                            SVN += " " + args[i];
                        break;


                    default:
                        Console.WriteLine("Default:");
                        Console.WriteLine(@"-GEN_XML  default: C:\_BuildTools\Generators\SPGenerator\genList.xml");
                        Console.WriteLine(@"-SVN      default: C:\_Provinsa\");
                        throw new Exception("The Param is not found.");
                }
            }
        }
        //Defaults
        if (GEN_XML == "Empty")
            GEN_XML = @"C:\_BuildTools\Generators\SPGenerator\genList.xml";

    }

    static void ReadandValidateGEN_XML()
    {
        {
            isGEN_XMLValid = true;
            XmlSchemaCollection myschemacoll = new XmlSchemaCollection();
            XmlValidatingReader vr;
            FileStream stream;
            try
            {
                stream = new FileStream(GEN_XML, FileMode.Open);
                //Load the XmlValidatingReader.
                vr = new XmlValidatingReader(stream, XmlNodeType.Element, null);

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
                stream.Position = 0;
                docGEN_XML.Load(stream);
            }
            catch
            {
                isGEN_XMLValid = false;
            }
            finally
            {
                //Clean up.
                vr = null;
                myschemacoll = null;
                stream = null;
            }
        }
    }
    public static void ShowCompileErrors(object sender, ValidationEventArgs args)
    {
        isGEN_XMLValid = false;
        Console.WriteLine("Validation Error: {0}", args.Message);
    }


}

