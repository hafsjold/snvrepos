using System;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using System.Diagnostics;
using pvMetadata;


partial class genListMain
{
    private static string GEN_XML;
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

    private static bool isValid = true;      // If a validation error occurs,
    // set this flag to false in the
    // validation event handler. 

    static void Main(string[] args)
    {
        InitParams(args);
        InitCollections();
        CopyProjectFolder(FIND_DIR);
        UpdateProjectFile();
        UpdateManifestFile();
        UpdateDFFFile();
        UpdateFeatureFile();
        UpdateelementManifest();
        GenerateLIST();
    }


    private static void InitParams(string[] args)
    {
        string ParmName = "Empty";
        LISTNAME = "Empty";
        SOURCE_TEMPLATE_DIR = "Empty";
        TARGET_DIR = "Empty";
        PROJECT_DIR = "Empty";
        PROJECT_FILE = "Empty";
        GEN_XML = "Empty";

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

                    case "-LISTNAME":
                        if (LISTNAME == "Empty")
                            LISTNAME = args[i];
                        else
                            LISTNAME += " " + args[i];
                        break;

                    case "-PROJECT_FILE":
                        if (PROJECT_FILE == "Empty")
                            PROJECT_FILE = args[i];
                        else
                            PROJECT_FILE += " " + args[i];
                        break;

                    case "-SOURCE_TEMPLATE_DIR":
                        if (SOURCE_TEMPLATE_DIR == "Empty")
                            SOURCE_TEMPLATE_DIR = args[i];
                        else
                            SOURCE_TEMPLATE_DIR += " " + args[i];
                        break;

                    case "-TARGET_DIR":
                        if (TARGET_DIR == "Empty")
                            TARGET_DIR = args[i];
                        else
                            TARGET_DIR += " " + args[i];
                        break;

                    default:
                        Console.WriteLine(@"-GEN_XML                default: C:\_BuildTools\CgenList\CgenList\genList.xml");
                        Console.WriteLine("Default:");
                        Console.WriteLine(@"-LISTNAME               default: Test");
                        Console.WriteLine(@"-PROJECT_FILE           default: C:\_Provinsa\ProvPur\ProvPur\ProvPur.csproj");
                        Console.WriteLine(@"-SOURCE_TEMPLATE_DIR    default: C:\_Provinsa\TEMPLATE\FEATURES\");
                        Console.WriteLine(@"-TARGET_DIR             default: C:\_Provinsa\ProvPur\ProvPur\TEMPLATE\FEATURES\");
                        throw new Exception("The Param is not found.");
                }
            }
        }

        //Defaults
        if (GEN_XML == "Empty")
            GEN_XML = @"C:\_BuildTools\CgenList\CgenList\genList.xml";

        System.Xml.XmlDocument docGEN_XML = null;
        {
            isValid = true;
            XmlSchemaCollection myschemacoll = new XmlSchemaCollection();
            XmlValidatingReader vr;
            FileStream stream;
            try
            {
                stream = new FileStream(GEN_XML, FileMode.Open);
                //Load the XmlValidatingReader.
                vr = new XmlValidatingReader(stream, XmlNodeType.Element, null);

                //Add the schemas to the XmlSchemaCollection object.
                myschemacoll.Add("http://www.hafsjold.dk/schema/hafsjold.xsd", @"C:\_BuildTools\CgenList\CgenList\hafsjold.xsd");
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
            }
            finally
            {
                //Clean up.
                vr = null;
                myschemacoll = null;
                stream = null;
            }
        }

        {

            string ns = "http://www.hafsjold.dk/schema/hafsjold.xsd";
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(docGEN_XML.NameTable);
            nsMgr.AddNamespace("mha", ns);

            System.Xml.XmlNode propertySets = docGEN_XML.SelectSingleNode("//mha:hafsjold/mha:propertySets", nsMgr);
            string SVNRootPath = propertySets.Attributes["SVNRootPath"].Value;
            string ProjectPath = propertySets.Attributes["ProjectPath"].Value;
            string ProjectName = propertySets.Attributes["ProjectName"].Value;
            PROJECT_FILE = SVNRootPath + ProjectPath + ProjectName;
            System.Xml.XmlNode propertySet = docGEN_XML.SelectSingleNode("//mha:hafsjold/mha:propertySets/mha:propertySet", nsMgr);
            string ProgPath = propertySet.Attributes["ProgPath"].Value;
            string ProgName = propertySet.Attributes["ProgName"].Value;
            System.Xml.XmlNode pLISTNAME = docGEN_XML.SelectSingleNode("//mha:hafsjold/mha:propertySets/mha:propertySet/mha:property[@name=\"LISTNAME\"]", nsMgr);
            LISTNAME = pLISTNAME.Attributes["value"].Value;
            System.Xml.XmlNode pSOURCE_TEMPLATE_DIR = docGEN_XML.SelectSingleNode("//mha:hafsjold/mha:propertySets/mha:propertySet/mha:property[@name=\"SOURCE_TEMPLATE_DIR\"]", nsMgr);
            SOURCE_TEMPLATE_DIR = SVNRootPath + pSOURCE_TEMPLATE_DIR.Attributes["value"].Value;
            System.Xml.XmlNode pTARGET_DIR = docGEN_XML.SelectSingleNode("//mha:hafsjold/mha:propertySets/mha:propertySet/mha:property[@name=\"TARGET_DIR\"]", nsMgr);
            TARGET_DIR = SVNRootPath + pTARGET_DIR.Attributes["value"].Value;
        }
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
        model = new Metadata();
    }

    public static void ShowCompileErrors(object sender, ValidationEventArgs args)
    {
        isValid = false;
        Console.WriteLine("Validation Error: {0}", args.Message);
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
