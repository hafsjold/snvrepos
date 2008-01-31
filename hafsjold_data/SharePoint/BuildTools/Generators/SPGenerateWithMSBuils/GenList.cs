using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
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
    public class GenList : Task
    {
        [Required]
        public ITaskItem SVNRootPath { get; set; }
        [Required]
        public ITaskItem LISTNAME { get; set; }
        [Required]
        public ITaskItem COREFILE { get; set; }
        [Required]
        public ITaskItem FILENAME_DK { get; set; }
        [Required]
        public ITaskItem FILENAME_UK { get; set; }
        [Required]
        public ITaskItem SOURCE_TEMPLATE_DIR { get; set; }
        [Required]
        public ITaskItem TARGET_DIR { get; set; }
        [Required]
        public ITaskItem PROJECT_FILE { get; set; }

        private string PROJECT_DIR { get; set; }
        private string SOURCE_TEMPLATE_NAME { get; set; }
        private string TARGET_TEMPLATE_NAME { get; set; }
        private string FIND_DIR { get; set; }
        private string REPLACE_DIR { get; set; }
        private string SCHEMA_FILE { get; set; }
        private string MANIFEST_FILE { get; set; }
        private string MANIFEST_ROOT { get; set; }
        private string DFF_FILE { get; set; }
        private string DFF_ROOT { get; set; }
        private string DFF_FROM_ROOT { get; set; }
        private string DFF_TO_ROOT { get; set; }
        private string FEATURE_FILE { get; set; }

        private  System.Collections.Specialized.StringCollection excludedExtensions { get; set; }
        private  System.Collections.Specialized.StringCollection ProjectFileList { get; set; }
        private Metadata model { get; set; }

        private XmlNamespaceManager s_nsMgr { get; set; }
        private string s_ns { get; set; }

        public override bool Execute()
        {
            model = new Metadata(SVNRootPath.ItemSpec);
            
            //Debug.Fail("Attach to MSBuild.exe and press Ignore");
            
            InitParams();
            InitCollections();
            CopyProjectFolder(FIND_DIR);
            UpdateProjectFile();
            UpdateManifestFile();
            UpdateDFFFile();
            UpdateFeatureFile();
            UpdateelementManifest();
            GenerateLIST();
            return true;
        }

        private void InitParams()
        {
            PROJECT_DIR = PROJECT_FILE.GetMetadata("RootDir") + PROJECT_FILE.GetMetadata("Directory");
            SOURCE_TEMPLATE_NAME = "#NAME#List";
            TARGET_TEMPLATE_NAME = Name_Substitute(SOURCE_TEMPLATE_NAME);
            FIND_DIR = SOURCE_TEMPLATE_DIR.ItemSpec + SOURCE_TEMPLATE_NAME;
            REPLACE_DIR = TARGET_DIR.ItemSpec + TARGET_TEMPLATE_NAME;
            MANIFEST_FILE = PROJECT_DIR + "manifest.xml";
            MANIFEST_ROOT = TARGET_DIR.ItemSpec;
            DFF_FILE = PROJECT_DIR + "wsp.ddf";
            DFF_FROM_ROOT = PROJECT_DIR;
            DFF_TO_ROOT = TARGET_DIR.ItemSpec;
        }

        //
        // GenerateLIST
        //
        private void GenerateLIST()
        {
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

            listtemplate list = model.listtemplates.getListtemplate(LISTNAME.ItemSpec);
            if (list != null)
            {
                createListElement(ref docLIST, list.SysName, COREFILE.ItemSpec, list.ListtemplateContenttypes, list.ListtemplateColumns);
                createDataElement(ref docDK, list.SysName, list.DisplayNameDK, list.Comment);
                createDataElement(ref docUK, list.SysName, list.DisplayNameUK, list.Comment);
            }

            strXML = docLIST.OuterXml;

            myStream = new FileStream(SCHEMA_FILE, FileMode.Truncate);
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
        }


        //
        //createDataElement
        //
        private void createDataElement(
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
        private void createListElement(
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




        private string Name_Substitute(string s)
        {
            string s1 = s.Replace("#NAME#", LISTNAME.ItemSpec);
            return s1;
        }

        private string FolderPath_Substitute(string s)
        {
            string s1 = s.Replace(FIND_DIR, REPLACE_DIR);
            string s2 = s1.Replace("#NAME#", LISTNAME.ItemSpec);
            return s2;
        }

        private string ProjectPath_Substitute(string s)
        {
            string s1 = s.Replace(FIND_DIR, REPLACE_DIR);
            string s2 = s1.Replace("#NAME#", LISTNAME.ItemSpec);
            string s3 = s2.Replace(PROJECT_DIR, "");
            return s3;
        }

        private void InitCollections()
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

        private void UpdateProjectFile()
        {

            FileStream myStream = new FileStream(PROJECT_FILE.ItemSpec, FileMode.Open);
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

            myStream = new FileStream(PROJECT_FILE.ItemSpec, FileMode.Truncate);
            StreamWriter myWriter = new StreamWriter(myStream);
            myWriter.Write(strXML);
            myWriter.Close();
            myStream.Close();

        }

        private void AddProjectContent(XmlDocument docPROJECT, string strPath, XmlNamespaceManager nsMgr, string ns)
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

        private void UpdateManifestFile()
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

        private void AddManifestContent(XmlDocument docMANIFEST, string strManifestPath, XmlNamespaceManager nsMgr, string ns)
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

        private void UpdateDFFFile()
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

        private void AddDFFContent(System.Collections.Specialized.StringCollection DFFFiletext, string strPath)
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

        private void UpdateFeatureFile()
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

                    listtemplate list = model.listtemplates.getListtemplate(LISTNAME.ItemSpec);
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

        private void UpdateelementManifest()
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

                    listtemplate list = model.listtemplates.getListtemplate(LISTNAME.ItemSpec);
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

        private void CopyProjectFolder(string currentPath)
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

                    string s1 = tmpFiletextSel.Replace("#NAME#", LISTNAME.ItemSpec);

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
        private bool IsFileExcluded(string filePath)
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

        private void AddFolder2Folder(string ProjectPath, Boolean bFile)
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
}
