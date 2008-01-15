using System;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using System.Diagnostics;

partial class genListMain
{
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
        string XMLFileLISTtext = myReader.ReadToEnd();
        myReader.Close();
        myStream.Close();

        System.Xml.XmlDocument docPROJECT = new System.Xml.XmlDocument();
        string strXML = Regex.Replace(XMLFileLISTtext, "xmlns=\"[^\"]*\"", "");
        docPROJECT.LoadXml(strXML);

        foreach (string strPath in ProjectFileList)
        {
            AddProjectContent(docPROJECT, strPath);

            string strFilename = System.IO.Path.GetFileName(strPath);
            strFilename.ToLower();
            if (strFilename == "schema.xml")
            {
                SCHEMA_FILE = PROJECT_DIR + strPath;
            }
        }

        strXML = Regex.Replace(docPROJECT.OuterXml, "<Project DefaultTargets=\"Build\"", "<Project DefaultTargets=\"Build\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\" ");

        myStream = new FileStream(PROJECT_FILE, FileMode.Truncate);
        StreamWriter myWriter = new StreamWriter(myStream);
        myWriter.Write(strXML);
        myWriter.Close();
        myStream.Close();

    }

    private static void AddProjectContent(XmlDocument docPROJECT, string strPath)
    {
        string filter = @"//Project/ItemGroup/Content[@Include=""" + strPath + @"""]";
        System.Xml.XmlNode field = docPROJECT.SelectSingleNode(filter);
        if (field == null)
        {
            string filter2 = @"//Project/ItemGroup";
            System.Xml.XmlNode ItemGroup = docPROJECT.SelectSingleNode(filter2);
            if (ItemGroup != null)
            {
                System.Xml.XmlElement Content = docPROJECT.CreateElement("Content");
                Content.SetAttribute("Include", strPath);
                ItemGroup.AppendChild(Content);
            }
        }
    }

    private static void UpdateManifestFile()
    {
        FileStream myStream = new FileStream(MANIFEST_FILE, FileMode.Open);
        StreamReader myReader = new StreamReader(myStream);
        string XMLFileLISTtext = myReader.ReadToEnd();
        myReader.Close();
        myStream.Close();

        System.Xml.XmlDocument docMANIFEST = new System.Xml.XmlDocument();
        string strXML = Regex.Replace(XMLFileLISTtext, "xmlns=\"[^\"]*\"", "");
        docMANIFEST.LoadXml(strXML);


        foreach (string strPath in ProjectFileList)
        {
            string strManifestPath = strPath.Replace("TEMPLATE\\FEATURES\\", "");
            string strFilename = System.IO.Path.GetFileName(strManifestPath).ToLower();
            if (strFilename == "feature.xml")
            {
                AddManifestContent(docMANIFEST, strManifestPath);
            }
        }
        //<Solution SolutionId="24493869-2DA2-49b5-AA30-67FE39550F1C"xmlns="http://schemas.microsoft.com/sharepoint/">
        strXML = Regex.Replace(docMANIFEST.OuterXml, "<Solution", "<Solution xmlns=\"http://schemas.microsoft.com/sharepoint/\" ");

        myStream = new FileStream(MANIFEST_FILE, FileMode.Truncate);
        StreamWriter myWriter = new StreamWriter(myStream);
        myWriter.Write(strXML);
        myWriter.Close();
        myStream.Close();

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

    private static void AddManifestContent(XmlDocument docMANIFEST, string strManifestPath)
    {
        //manifest-root: TARGET_DIR
        string filter = @"//Solution/FeatureManifests/FeatureManifest[@Location=""" + strManifestPath + @"""]";
        System.Xml.XmlNode field = docMANIFEST.SelectSingleNode(filter);
        if (field == null)
        {
            string filter2 = @"//Solution/FeatureManifests";
            System.Xml.XmlNode FeatureManifests = docMANIFEST.SelectSingleNode(filter2);
            if (FeatureManifests != null)
            {
                System.Xml.XmlElement FeatureManifest = docMANIFEST.CreateElement("FeatureManifest");
                FeatureManifest.SetAttribute("Location", strManifestPath);
                FeatureManifests.AppendChild(FeatureManifest);
            }
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
                string XMLFileLISTtext = myReader.ReadToEnd();
                myReader.Close();
                myStream.Close();

                System.Xml.XmlDocument docFEATURE = new System.Xml.XmlDocument();
                string strXML = Regex.Replace(XMLFileLISTtext, "xmlns=\"[^\"]*\"", "");
                docFEATURE.LoadXml(strXML);

                System.Data.DataSet dsListItems = OpenDataSet("ProPurList");
                DataTable List_rows = dsListItems.Tables["row"];

                foreach (DataRow List_row in List_rows.Rows)
                {
                    string List_SysName = (string)List_row["ows_Title"];
                    string List_FeatureGUID = (string)List_row["ows_FeatureGUID"];
                    string List_TypeIdentifier = (string)List_row["ows_TypeIdentifier"];

                    if (List_SysName == LISTNAME)
                    {
                        System.Xml.XmlNode Feature = docFEATURE.SelectSingleNode("//Feature");
                        Feature.Attributes["Id"].Value = List_FeatureGUID;
                        break;
                    }
                }
                //<Feature Id="24493869-2DA2-49b5-AA30-67FE39550F1C" xmlns="http://schemas.microsoft.com/sharepoint/">
                strXML = Regex.Replace(docFEATURE.OuterXml, "<Feature", "<Feature xmlns=\"http://schemas.microsoft.com/sharepoint/\" ");

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
                string XMLFileLISTtext = myReader.ReadToEnd();
                myReader.Close();
                myStream.Close();

                System.Xml.XmlDocument docELEMENTMANIFEST = new System.Xml.XmlDocument();
                string strXML = Regex.Replace(XMLFileLISTtext, "xmlns=\"[^\"]*\"", "");
                docELEMENTMANIFEST.LoadXml(strXML);

                System.Data.DataSet dsListItems = OpenDataSet("ProPurList");
                DataTable List_rows = dsListItems.Tables["row"];

                foreach (DataRow List_row in List_rows.Rows)
                {
                    string List_SysName = (string)List_row["ows_Title"];
                    string List_FeatureGUID = (string)List_row["ows_FeatureGUID"];
                    string List_TypeIdentifier = (string)List_row["ows_TypeIdentifier"];

                    if (List_SysName == LISTNAME)
                    {
                        System.Xml.XmlNode ListTemplate = docELEMENTMANIFEST.SelectSingleNode("//Elements/ListTemplate");
                        ListTemplate.Attributes["Type"].Value = List_TypeIdentifier;
                        break;
                    }
                }
                //<Feature Id="24493869-2DA2-49b5-AA30-67FE39550F1C" xmlns="http://schemas.microsoft.com/sharepoint/">
                strXML = Regex.Replace(docELEMENTMANIFEST.OuterXml, "<Elements", "<Elements xmlns=\"http://schemas.microsoft.com/sharepoint/\" ");

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
