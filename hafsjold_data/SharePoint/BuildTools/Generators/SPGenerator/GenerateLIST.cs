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

class GenerateList
{
    private static string GEN_XML;
    private static bool isGEN_XMLValid = true;
    private static string LISTNAME;
    private static string SOURCE_TEMPLATE_DIR;
    private static string TARGET_DIR;
    private static string PROJECT_DIR;
    private static string PROJECT_FILE;
    private static string SOURCE_TEMPLATE_NAME;
    private static string TARGET_TEMPLATE_NAME;
    private static string FIND_DIR;
    private static string REPLACE_DIR;
    private static string SCHEMA_FILE;
    private static string MANIFEST_FILE;
    private static string MANIFEST_ROOT;
    private static string DFF_FILE;
    private static string DFF_ROOT;
    private static string DFF_FROM_ROOT;
    private static string DFF_TO_ROOT;
    private static string FEATURE_FILE;
    private static System.Collections.Specialized.StringCollection excludedExtensions;
    private static System.Collections.Specialized.StringCollection ProjectFileList;
    static Metadata model;

    static XmlNamespaceManager s_nsMgr;
    static string s_ns;


    public static void MainGenerateList(MemoryStream sXML)
    {
        InitParams(sXML);
        InitCollections();
        CopyProjectFolder(FIND_DIR);
        UpdateProjectFile();
        UpdateManifestFile();
        UpdateDFFFile();
        UpdateFeatureFile();
        UpdateelementManifest();
        GenerateLIST();
    }

    private static void InitParams(MemoryStream sXML)
    {
        LISTNAME = "Empty";
        SOURCE_TEMPLATE_DIR = "Empty";
        TARGET_DIR = "Empty";
        PROJECT_DIR = "Empty";
        PROJECT_FILE = "Empty";
        GEN_XML = "Empty";

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

        string l_ns = "http://www.hafsjold.dk/schema/hafsjold.xsd";
        XmlNamespaceManager l_nsMgr = new XmlNamespaceManager(docGEN_XML.NameTable);
        l_nsMgr.AddNamespace("mha", l_ns);

        System.Xml.XmlNode propertySets = docGEN_XML.SelectSingleNode("//mha:hafsjold/mha:propertySets", l_nsMgr);
        string SVNRootPath = propertySets.Attributes["SVNRootPath"].Value;
        string ProjectPath = propertySets.Attributes["ProjectPath"].Value;
        string ProjectName = propertySets.Attributes["ProjectName"].Value;
        PROJECT_FILE = SVNRootPath + ProjectPath + ProjectName;
        System.Xml.XmlNode propertySet = docGEN_XML.SelectSingleNode("//mha:hafsjold/mha:propertySets/mha:propertySet", l_nsMgr);
        string ProgPath = propertySet.Attributes["ProgPath"].Value;
        string ProgName = propertySet.Attributes["ProgName"].Value;
        System.Xml.XmlNode pLISTNAME = docGEN_XML.SelectSingleNode("//mha:hafsjold/mha:propertySets/mha:propertySet/mha:property[@name=\"LISTNAME\"]", l_nsMgr);
        LISTNAME = pLISTNAME.Attributes["value"].Value;
        System.Xml.XmlNode pSOURCE_TEMPLATE_DIR = docGEN_XML.SelectSingleNode("//mha:hafsjold/mha:propertySets/mha:propertySet/mha:property[@name=\"SOURCE_TEMPLATE_DIR\"]", l_nsMgr);
        SOURCE_TEMPLATE_DIR = SVNRootPath + pSOURCE_TEMPLATE_DIR.Attributes["value"].Value;
        System.Xml.XmlNode pTARGET_DIR = docGEN_XML.SelectSingleNode("//mha:hafsjold/mha:propertySets/mha:propertySet/mha:property[@name=\"TARGET_DIR\"]", l_nsMgr);
        TARGET_DIR = SVNRootPath + pTARGET_DIR.Attributes["value"].Value;

        if (LISTNAME == "Empty")
            LISTNAME = "Test";
        if (PROJECT_FILE == "Empty")
            PROJECT_FILE = @"C:\_Provinsa\ProvPur\ProvPur\ProvPur.csproj";
        if (SOURCE_TEMPLATE_DIR == "Empty")
            SOURCE_TEMPLATE_DIR = @"C:\_Provinsa\TEMPLATE\FEATURES\";
        if (TARGET_DIR == "Empty")
            TARGET_DIR = @"C:\_Provinsa\ProvPur\ProvPur\TEMPLATE\FEATURES\";

        PROJECT_DIR = System.IO.Path.GetDirectoryName(PROJECT_FILE) + @"\";
        SOURCE_TEMPLATE_NAME = "#NAME#List";
        TARGET_TEMPLATE_NAME = Name_Substitute(SOURCE_TEMPLATE_NAME);
        FIND_DIR = SOURCE_TEMPLATE_DIR + SOURCE_TEMPLATE_NAME;
        REPLACE_DIR = TARGET_DIR + TARGET_TEMPLATE_NAME;
        MANIFEST_FILE = PROJECT_DIR + "manifest.xml";
        MANIFEST_ROOT = TARGET_DIR;
        DFF_FILE = PROJECT_DIR + "wsp.ddf";
        DFF_FROM_ROOT = PROJECT_DIR;
        DFF_TO_ROOT = TARGET_DIR;
        model = new Metadata(SVNRootPath);
    }

    public static void ShowCompileErrors(object sender, ValidationEventArgs args)
    {
        isGEN_XMLValid = false;
        Console.WriteLine("Validation Error: {0}", args.Message);
    }

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
        string strXML = myReader.ReadToEnd();
        myReader.Close();
        myStream.Close();
        System.Xml.XmlDocument docLIST = new System.Xml.XmlDocument();
        docLIST.PreserveWhitespace = true;
        docLIST.LoadXml(strXML);
        s_ns = "http://schemas.microsoft.com/sharepoint/";
        s_nsMgr = new XmlNamespaceManager(docLIST.NameTable);
        s_nsMgr.AddNamespace("mha", "http://schemas.microsoft.com/sharepoint/");



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

        strXML = docLIST.OuterXml;

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
        System.Xml.XmlNode contenttypes = pdoc.SelectSingleNode("//mha:List/mha:MetaData/mha:ContentTypes", s_nsMgr);
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

            System.Xml.XmlElement contenttyperef = pdoc.CreateElement("", "ContentTypeRef", s_ns);
            System.Xml.XmlComment contenttypescomment = pdoc.CreateComment(typ.SysName);
            contenttyperef.SetAttribute("ID", ContentTypeID);

            System.Xml.XmlNode ContentTypeRef0x01 = pdoc.SelectSingleNode("//mha:List/mha:MetaData/mha:ContentTypes/mha:ContentTypeRef[@ID=\"0x01\"]", s_nsMgr);
            if (ContentTypeRef0x01 == null)
            {
                System.Xml.XmlNode lastContentTypeRef = contenttypes.AppendChild(contenttyperef);
                contenttypes.InsertBefore(contenttypescomment, lastContentTypeRef);
            }
            else
            {
                System.Xml.XmlNode Folder = pdoc.SelectSingleNode("//mha:List/mha:MetaData/mha:ContentTypes/mha:ContentTypeRef[@ID=\"0x01\"]/mha:Folder", s_nsMgr);
                if (Folder != null)
                {
                    System.Xml.XmlNode copyFolder = Folder.CloneNode(true);
                    contenttyperef.AppendChild(copyFolder);
                }
                contenttypes.InsertBefore(contenttypescomment, ContentTypeRef0x01);
                contenttypes.ReplaceChild(contenttyperef, ContentTypeRef0x01);
            }
        }


        System.Collections.Generic.SortedDictionary<string, ListtemplateColumn> scol = new System.Collections.Generic.SortedDictionary<string, ListtemplateColumn>();
        foreach (ListtemplateColumn col in pfieldstable)
        {
            scol.Add(col.Seqnr, col);
        }

        // 
        // Insert Field in Fields 
        // 
        System.Xml.XmlNode fields = pdoc.SelectSingleNode("//mha:List/mha:MetaData/mha:Fields", s_nsMgr);
        foreach (ListtemplateColumn col in scol.Values)
        {
            if (!col.SysCol)
            {
                System.Xml.XmlElement fieldref = pdoc.CreateElement("", "Field", s_ns);
                fieldref.SetAttribute("Name", col.SysName);
                fieldref.SetAttribute("ID", col.colGUID);

                fieldref.SetAttribute("DisplayName", "$Resources:" + pcore + "," + col.SysName + ";");
                switch (col.KolonneType)
                {
                    case "Text":
                        fieldref.SetAttribute("Type", "Text");
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

                    case "Counter":
                        fieldref.SetAttribute("Type", "Counter");
                        break;

                    case "Picture":
                        fieldref.SetAttribute("Type", "Picture");
                        break;
                    case "Hyperlink":
                        fieldref.SetAttribute("Type", "Hyperlink");
                        break;

                    default:
                        break;

                }

                fields.AppendChild(fieldref);
            }
        }


        // 
        // Insert FieldsRef in ViewFields 
        // 
        System.Xml.XmlNode viewfields = pdoc.SelectSingleNode("//mha:List/mha:MetaData/mha:Views/mha:View[@BaseViewID=\"1\"]/mha:ViewFields", s_nsMgr);
        foreach (ListtemplateColumn col in scol.Values)
        {
            if (!col.SysCol)
            {
                System.Xml.XmlElement fieldref = pdoc.CreateElement("", "FieldRef", s_ns);
                fieldref.SetAttribute("ID", col.colGUID);
                fieldref.SetAttribute("Name", col.SysName);
                viewfields.AppendChild(fieldref);
            }
        }
    }




    private static string Name_Substitute(string s)
    {
        string s1 = s.Replace("#NAME#", LISTNAME);
        return s1;
    }

    private static string FolderPath_Substitute(string s)
    {
        string s1 = s.Replace(FIND_DIR, REPLACE_DIR);
        string s2 = s1.Replace("#NAME#", LISTNAME);
        return s2;
    }

    private static string ProjectPath_Substitute(string s)
    {
        string s1 = s.Replace(FIND_DIR, REPLACE_DIR);
        string s2 = s1.Replace("#NAME#", LISTNAME);
        string s3 = s2.Replace(PROJECT_DIR, "");
        return s3;
    }

    private static void InitCollections()
    {
        ProjectFileList = new System.Collections.Specialized.StringCollection();

        // If you do not want a file with a particular extension or name 
        // to be added, then add that extension or name to this list: 
        excludedExtensions = new System.Collections.Specialized.StringCollection();
        excludedExtensions.Add(".obj");
        excludedExtensions.Add(".ilk");
        excludedExtensions.Add(".pch");
        excludedExtensions.Add(".pdb");
        excludedExtensions.Add(".exe");
        excludedExtensions.Add(".dll");
        excludedExtensions.Add(".sbr");
        excludedExtensions.Add(".lib");
        excludedExtensions.Add(".exp");
        excludedExtensions.Add(".bsc");
        excludedExtensions.Add(".tlb");
        excludedExtensions.Add(".ncb");
        excludedExtensions.Add(".sln");
        excludedExtensions.Add(".suo");
        excludedExtensions.Add(".vcproj");
        excludedExtensions.Add(".vbproj");
        excludedExtensions.Add(".csproj");
        excludedExtensions.Add(".vjsproj");
        excludedExtensions.Add(".msi");
        excludedExtensions.Add("_svn");
    }

    private static void UpdateProjectFile()
    {

        FileStream myStream = new FileStream(PROJECT_FILE, FileMode.Open);
        StreamReader myReader = new StreamReader(myStream);
        string strXML = myReader.ReadToEnd();
        myReader.Close();
        myStream.Close();

        System.Xml.XmlDocument docPROJECT = new System.Xml.XmlDocument();
        docPROJECT.LoadXml(strXML);
        string ns = "http://schemas.microsoft.com/developer/msbuild/2003";
        XmlNamespaceManager nsMgr = new XmlNamespaceManager(docPROJECT.NameTable);
        nsMgr.AddNamespace("mha", ns);

        foreach (string strPath in ProjectFileList)
        {
            AddProjectContent(docPROJECT, strPath, nsMgr, ns);

            string strFilename = System.IO.Path.GetFileName(strPath);
            strFilename.ToLower();
            if (strFilename == "schema.xml")
            {
                SCHEMA_FILE = PROJECT_DIR + strPath;
            }
        }

        strXML = docPROJECT.OuterXml;

        myStream = new FileStream(PROJECT_FILE, FileMode.Truncate);
        StreamWriter myWriter = new StreamWriter(myStream);
        myWriter.Write(strXML);
        myWriter.Close();
        myStream.Close();

    }

    private static void AddProjectContent(XmlDocument docPROJECT, string strPath, XmlNamespaceManager nsMgr, string ns)
    {
        string filter = @"//mha:Project/mha:ItemGroup/mha:Content[@Include=""" + strPath + @"""]";
        System.Xml.XmlNode field = docPROJECT.SelectSingleNode(filter, nsMgr);
        if (field == null)
        {
            string filter2 = @"//mha:Project/mha:ItemGroup";
            System.Xml.XmlNode ItemGroup = docPROJECT.SelectSingleNode(filter2, nsMgr);
            if (ItemGroup != null)
            {
                System.Xml.XmlElement Content = docPROJECT.CreateElement("", "Content", ns);
                Content.SetAttribute("Include", strPath);
                ItemGroup.AppendChild(Content);
            }
        }
    }

    private static void UpdateManifestFile()
    {
        FileStream myStream = new FileStream(MANIFEST_FILE, FileMode.Open);
        StreamReader myReader = new StreamReader(myStream);
        string strXML = myReader.ReadToEnd();
        myReader.Close();
        myStream.Close();

        System.Xml.XmlDocument docMANIFEST = new System.Xml.XmlDocument();
        docMANIFEST.LoadXml(strXML);
        string ns = "http://schemas.microsoft.com/sharepoint/";
        XmlNamespaceManager nsMgr = new XmlNamespaceManager(docMANIFEST.NameTable);
        nsMgr.AddNamespace("mha", ns);

        foreach (string strPath in ProjectFileList)
        {
            string strManifestPath = strPath.Replace("TEMPLATE\\FEATURES\\", "");
            string strFilename = System.IO.Path.GetFileName(strManifestPath).ToLower();
            if (strFilename == "feature.xml")
            {
                AddManifestContent(docMANIFEST, strManifestPath, nsMgr, ns);
            }
        }
        strXML = docMANIFEST.OuterXml;

        myStream = new FileStream(MANIFEST_FILE, FileMode.Truncate);
        StreamWriter myWriter = new StreamWriter(myStream);
        myWriter.Write(strXML);
        myWriter.Close();
        myStream.Close();

    }

    private static void AddManifestContent(XmlDocument docMANIFEST, string strManifestPath, XmlNamespaceManager nsMgr, string ns)
    {
        //manifest-root: TARGET_DIR
        string filter = @"//mha:Solution/mha:FeatureManifests/mha:FeatureManifest[@Location=""" + strManifestPath + @"""]";
        System.Xml.XmlNode field = docMANIFEST.SelectSingleNode(filter, nsMgr);
        if (field == null)
        {
            string filter2 = @"//mha:Solution/mha:FeatureManifests";
            System.Xml.XmlNode FeatureManifests = docMANIFEST.SelectSingleNode(filter2, nsMgr);
            if (FeatureManifests != null)
            {
                System.Xml.XmlElement FeatureManifest = docMANIFEST.CreateElement("", "FeatureManifest", ns);
                FeatureManifest.SetAttribute("Location", strManifestPath);
                FeatureManifests.AppendChild(FeatureManifest);
            }
        }
    }

    private static void UpdateDFFFile()
    {
        FileStream myStream = new FileStream(DFF_FILE, FileMode.Open);
        StreamReader myReader = new StreamReader(myStream);
        System.Collections.Specialized.StringCollection DFFFiletext = new System.Collections.Specialized.StringCollection();
        while (!myReader.EndOfStream)
            DFFFiletext.Add(myReader.ReadLine());
        myReader.Close();
        myStream.Close();

        foreach (string strPath in ProjectFileList)
            AddDFFContent(DFFFiletext, strPath);

        myStream = new FileStream(DFF_FILE, FileMode.Truncate);
        StreamWriter myWriter = new StreamWriter(myStream);
        foreach (string s in DFFFiletext)
            myWriter.WriteLine(s);
        myWriter.Close();
        myStream.Close();

    }

    private static void AddDFFContent(System.Collections.Specialized.StringCollection DFFFiletext, string strPath)
    {
        Boolean bFound = false;
        foreach (string s in DFFFiletext)
            if (s.Contains(strPath))
            {
                bFound = true;
                break;
            }
        if (bFound)
        {

        }
        else
        {
            DFFFiletext.Add(strPath + "\t" + strPath.Replace("TEMPLATE\\FEATURES\\", ""));
        }
    }

    private static void UpdateFeatureFile()
    {
        foreach (string strPath in ProjectFileList)
        {
            string strFullPath = PROJECT_DIR + strPath;
            string strFilename = System.IO.Path.GetFileName(strFullPath).ToLower();
            if (strFilename == "feature.xml")
            {
                FileStream myStream = new FileStream(strFullPath, FileMode.Open);
                StreamReader myReader = new StreamReader(myStream);
                string strXML = myReader.ReadToEnd();
                myReader.Close();
                myStream.Close();

                System.Xml.XmlDocument docFEATURE = new System.Xml.XmlDocument();
                docFEATURE.LoadXml(strXML);
                string ns = "http://schemas.microsoft.com/sharepoint/";
                XmlNamespaceManager nsMgr = new XmlNamespaceManager(docFEATURE.NameTable);
                nsMgr.AddNamespace("mha", ns);

                listtemplate list = model.listtemplates.getListtemplate(LISTNAME);
                if (list != null)
                {
                    System.Xml.XmlNode Feature = docFEATURE.SelectSingleNode("//mha:Feature", nsMgr);
                    Feature.Attributes["Id"].Value = list.FeatureGUID;
                }

                strXML = docFEATURE.OuterXml;

                myStream = new FileStream(strFullPath, FileMode.Truncate);
                StreamWriter myWriter = new StreamWriter(myStream);
                myWriter.Write(strXML);
                myWriter.Close();
                myStream.Close();
            }
        }

    }

    private static void UpdateelementManifest()
    {
        foreach (string strPath in ProjectFileList)
        {
            string strFullPath = PROJECT_DIR + strPath;
            string strFilename = System.IO.Path.GetFileName(strFullPath).ToLower();
            if (strFilename == "elementmanifest.xml")
            {
                FileStream myStream = new FileStream(strFullPath, FileMode.Open);
                StreamReader myReader = new StreamReader(myStream);
                string strXML = myReader.ReadToEnd();
                myReader.Close();
                myStream.Close();

                System.Xml.XmlDocument docELEMENTMANIFEST = new System.Xml.XmlDocument();
                docELEMENTMANIFEST.LoadXml(strXML);
                string ns = "http://schemas.microsoft.com/sharepoint/";
                XmlNamespaceManager nsMgr = new XmlNamespaceManager(docELEMENTMANIFEST.NameTable);
                nsMgr.AddNamespace("mha", ns);

                listtemplate list = model.listtemplates.getListtemplate(LISTNAME);
                if (list != null)
                {
                    System.Xml.XmlNode ListTemplate = docELEMENTMANIFEST.SelectSingleNode("//mha:Elements/mha:ListTemplate", nsMgr);
                    ListTemplate.Attributes["Type"].Value = list.TypeIdentifier;

                    System.Xml.XmlNode ListInstance = docELEMENTMANIFEST.SelectSingleNode("//mha:Elements/mha:ListInstance", nsMgr);
                    ListInstance.Attributes["TemplateType"].Value = list.TypeIdentifier;
                }
                strXML = docELEMENTMANIFEST.OuterXml;

                myStream = new FileStream(strFullPath, FileMode.Truncate);
                StreamWriter myWriter = new StreamWriter(myStream);
                myWriter.Write(strXML);
                myWriter.Close();
                myStream.Close();
            }
        }

    }

    private static void CopyProjectFolder(string currentPath)
    {
        string[] stFolders = System.IO.Directory.GetDirectories(currentPath);
        string[] stFiles = System.IO.Directory.GetFiles(currentPath);

        // Examine all the files within the folder. 
        foreach (string stFile in stFiles)
        {
            if ((!IsFileExcluded(stFile)))
            {
                string stToPath = FolderPath_Substitute(stFile);
                try
                {
                    AddFolder2Folder(stToPath, true);
                    System.IO.File.Copy(stFile, stToPath, true);
                    ProjectFileList.Add(ProjectPath_Substitute(stFile));
                }
                catch
                {
                    //'Error 
                }

                FileStream myStream = new FileStream(stToPath, FileMode.Open);
                StreamReader myReader = new StreamReader(myStream);
                string tmpFiletextSel = myReader.ReadToEnd();
                myReader.Close();
                myStream.Close();

                string s1 = tmpFiletextSel.Replace("#NAME#", LISTNAME);

                /*
                 * Guid myGuid = System.Guid.NewGuid();
                string stMyGuid = myGuid.ToString();
                string s = s1.Replace("#GUID-FEATURE#", stMyGuid);
                */

                myStream = new FileStream(stToPath, FileMode.Truncate);
                StreamWriter myWriter = new StreamWriter(myStream);
                myWriter.Write(s1);
                myWriter.Close();
                myStream.Close();

            }

        }

        // Examine all the subfolders. 
        foreach (string stFolder in stFolders)
        {
            if ((!IsFileExcluded(stFolder)))
            {
                CopyProjectFolder(stFolder);
            }
        }
    }

    // Function to filter out folder names, file names, and extensions that we do not 
    // want to add to the solution. 
    private static bool IsFileExcluded(string filePath)
    {
        string extension;
        string fileName;

        extension = System.IO.Path.GetExtension(filePath);
        extension = extension.ToLower();

        fileName = System.IO.Path.GetFileName(filePath);
        fileName = fileName.ToLower();

        if ((excludedExtensions.Contains(extension)))
        {
            return true;
        }
        else
        {
            if ((excludedExtensions.Contains(fileName)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private static void AddFolder2Folder(string ProjectPath, Boolean bFile)
    {
        string[] folders = Regex.Split(ProjectPath, @"\\");
        string stPath = "";
        int iLops;
        if (bFile)
            iLops = folders.Length - 2;
        else
            iLops = folders.Length - 1;

        for (int i = 0; i <= iLops; i++)
        {
            if (stPath.Length > 0)
            {
                stPath += @"\";
            }
            stPath = stPath + folders[i];
            if (!System.IO.Directory.Exists(stPath))
            {
                System.IO.Directory.CreateDirectory(stPath);
            }
        }
    }

}