using System; 
using System.IO; 
using System.Text.RegularExpressions; 
using System.Xml; 
using System.Xml.Schema; 
using System.Diagnostics; 

class genListMain 
{ 
    static System.Collections.Specialized.StringCollection excludedExtensions;
    static System.Collections.Specialized.StringCollection ProjectFileList;

    static void Main(string[] args)
    { 
        string LISTNAME = "TestList"; 
        
        ProjectFileList = new System.Collections.Specialized.StringCollection(); 
        const string FIND_DIR0 = @"C:\_Provinsa\TEMPLATE\FEATURES\";
        const string REPLACE_DIR0 = @"C:\_Provinsa\ProvPur\ProvPur\TEMPLATE\FEATURES\";
        const string PROJECT_DIR = @"C:\_Provinsa\ProvPur\ProvPur\";
        const string PROJECT_FILE = @"C:\_Provinsa\ProvPur\ProvPur\ProvPur.csproj"; 
        
        string TEMP_NAME = "#NAME#List"; 
        string PROJ_NAME = FolderPathSubstitute(TEMP_NAME, LISTNAME, "xx", "xx", PROJECT_DIR); 
        string FIND_DIR = FIND_DIR0 + TEMP_NAME; 
        string REPLACE_DIR = REPLACE_DIR0 + PROJ_NAME; 
        
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
        
        CopyProjectFolder(FIND_DIR, LISTNAME, FIND_DIR, REPLACE_DIR, PROJECT_DIR); 
        
        UpdateProjectFile(PROJECT_FILE); 
        
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
        foreach (string strPath in ProjectFileList) { 
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
    
    private static string FolderPathSubstitute(string s, string LISTNAME, string FIND_DIR, string REPLACE_DIR, string PROJECT_DIR) 
    {
        string s1 = s.Replace(FIND_DIR, REPLACE_DIR);
        string s2 = s1.Replace("#NAME#", LISTNAME); 
        return s2; 
    }
    private static string ProjectPathSubstitute(string s, string LISTNAME, string FIND_DIR, string REPLACE_DIR, string PROJECT_DIR) 
    {
        string s1 = s.Replace(FIND_DIR, REPLACE_DIR);
        string s2 = s1.Replace("#NAME#", LISTNAME);
        string s3 = s2.Replace(PROJECT_DIR, ""); 
        return s3; 
    }


    private static void CopyProjectFolder(string currentPath, string LISTNAME, string FIND_DIR, string REPLACE_DIR, string PROJECT_DIR) 
    { 
        string[] stFolders; 
        string[] stFiles; 
        
        stFolders = System.IO.Directory.GetDirectories(currentPath); 
        stFiles = System.IO.Directory.GetFiles(currentPath); 
        
        // Examine all the files within the folder. 
        foreach ( string stFile in stFiles) {
            if ((!IsFileExcluded(stFile)))
            {
                string stToPath = FolderPathSubstitute(stFile, LISTNAME, FIND_DIR, REPLACE_DIR, PROJECT_DIR); 
                try { 
                    AddFolder2Folder(stToPath);
                    System.IO.File.Copy(stFile, stToPath);
                    ProjectFileList.Add(ProjectPathSubstitute(stFile, LISTNAME, FIND_DIR, REPLACE_DIR, PROJECT_DIR)); 
                } 
                catch { 
                    //'Error 
                } 
                
                FileStream myStream = new FileStream(stToPath, FileMode.Open); 
                StreamReader myReader = new StreamReader(myStream); 
                string tmpFiletextSel = myReader.ReadToEnd(); 
                myReader.Close(); 
                myStream.Close(); 
                
                string s1 = tmpFiletextSel.Replace("#NAME#", LISTNAME);

                Guid myGuid= System.Guid.NewGuid();
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
        foreach ( string stFolder in stFolders) {
            if ((!IsFileExcluded(stFolder)))
            {
                CopyProjectFolder(stFolder, LISTNAME, FIND_DIR, REPLACE_DIR, PROJECT_DIR); 
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
        
        if ((excludedExtensions.Contains(extension))) { 
            return true; 
        } 
        else { 
            if ((excludedExtensions.Contains(fileName))) { 
                return true; 
            } 
            else { 
                return false; 
            } 
        } 
    }

    private static void AddFolder2Folder(string ProjectPath) 
    { 
        string[] folders = Regex.Split(ProjectPath, @"\\"); 
        string stPath = ""; 
        
        for (int i = 0; i <= folders.Length - 2; i++) { 
            if (stPath.Length > 0) { 
                stPath += @"\"; 
            } 
            stPath = stPath + folders[i]; 
            if (!System.IO.Directory.Exists(stPath)) { 
                System.IO.Directory.CreateDirectory(stPath); 
            } 
        } 
    } 
    
} 
