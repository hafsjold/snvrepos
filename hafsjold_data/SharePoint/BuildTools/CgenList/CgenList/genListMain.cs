using System;
using System.IO;
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
    private static System.Collections.Specialized.StringCollection excludedExtensions;
    private static System.Collections.Specialized.StringCollection ProjectFileList;

    static void Main(string[] args)
    {
        InitParams(args);
        InitCollections();

        CopyProjectFolder(FIND_DIR);
        UpdateProjectFile(PROJECT_FILE);
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

    private static void UpdateProjectFile(string ProjectFile)
    {

        FileStream myStream = new FileStream(ProjectFile, FileMode.Open);
        StreamReader myReader = new StreamReader(myStream);
        string XMLFileLISTtext = myReader.ReadToEnd();
        myReader.Close();
        myStream.Close();

        System.Xml.XmlDocument docPROJECT = new System.Xml.XmlDocument();
        string strXML = Regex.Replace(XMLFileLISTtext, "xmlns=\"[^\"]*\"", "");
        docPROJECT.LoadXml(strXML);

        System.Xml.XmlNode Project = docPROJECT.FirstChild;

        System.Xml.XmlElement ItemGroup = docPROJECT.CreateElement("ItemGroup");
        foreach (string strPath in ProjectFileList)
        {
            string strFilename = System.IO.Path.GetFileName(strPath);
            strFilename.ToLower();
            if (strFilename == "schema.xml")
            {
                SCHEMA_FILE = PROJECT_DIR + strPath;
            }
            System.Xml.XmlElement Content = docPROJECT.CreateElement("Content");
            Content.SetAttribute("Include", strPath);
            ItemGroup.AppendChild(Content);
        }
        Project.AppendChild(ItemGroup);

        strXML = Regex.Replace(docPROJECT.OuterXml, "<Project DefaultTargets=\"Build\"", "<Project DefaultTargets=\"Build\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\" ");

        myStream = new FileStream(ProjectFile, FileMode.Truncate);
        StreamWriter myWriter = new StreamWriter(myStream);
        myWriter.Write(strXML);
        myWriter.Close();
        myStream.Close();

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
                    System.IO.File.Copy(stFile, stToPath);
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

                Guid myGuid = System.Guid.NewGuid();
                string stMyGuid = myGuid.ToString();
                string s = s1.Replace("#GUID-FEATURE#", stMyGuid);

                myStream = new FileStream(stToPath, FileMode.Truncate);
                StreamWriter myWriter = new StreamWriter(myStream);
                myWriter.Write(s);
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
