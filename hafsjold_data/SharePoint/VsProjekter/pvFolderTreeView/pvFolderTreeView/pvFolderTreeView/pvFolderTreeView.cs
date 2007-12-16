using System;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Serialization;

using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.WebPartPages;
using System.Web.UI.WebControls;

using System.Web;



namespace pvFolderTreeView
{
    [Guid("3209722a-38a1-4baa-854a-6a1f1c085578")]
    public class pvFolderTreeView : System.Web.UI.WebControls.WebParts.WebPart
    {
        protected SPTreeView treeSitesFiles;

        const string SITE_IMG = @"\_layouts\images\FPWEB16.GIF";
        const string FOLDER_IMG = @"\_layouts\images\FOLDER16.GIF";
        const string GHOSTED_FILE_IMG = @"\_layouts\images\NEWDOC.GIF";
        const string UNGHOSTED_FILE_IMG = @"\_layouts\images\RAT16.GIF";

        public pvFolderTreeView()
        {
            this.ExportMode = WebPartExportMode.All;
        }

        protected override void CreateChildControls()
        {
            try
            {
                SPWeb site = SPContext.Current.Web;
                SPFolder rootFolder = site.RootFolder;
                TreeNode rootNode = new TreeNode(site.Url, site.Url, SITE_IMG);
                LoadFolderNodes(rootFolder, rootNode);
                
                treeSitesFiles = new SPTreeView();
                treeSitesFiles.Nodes.Add(rootNode);
                treeSitesFiles.ExpandDepth = 1;
                Controls.Add(treeSitesFiles);

                base.CreateChildControls();
            }
            catch (Exception e)
            {
                string exp = "e.ToString";
            }
        }

        protected void LoadFolderNodes(SPFolder folder, TreeNode folderNode)
        {

            foreach (SPFolder childFolder in folder.SubFolders)
            {
                TreeNode childFolderNode = new TreeNode(childFolder.Name, childFolder.Name, FOLDER_IMG);
                childFolderNode.NavigateUrl = SPControl.GetContextWeb(this.Context).Site.MakeFullUrl(childFolder.Url);
                LoadFolderNodes(childFolder, childFolderNode);
                folderNode.ChildNodes.Add(childFolderNode);
            }

            
            foreach (SPFile file in folder.Files)
            {
                TreeNode fileNode;
                if (file.CustomizedPageStatus == SPCustomizedPageStatus.Uncustomized)
                {
                    fileNode = new TreeNode(file.Name, file.Name, GHOSTED_FILE_IMG);
                }
                else
                {
                    fileNode = new TreeNode(file.Name, file.Name, UNGHOSTED_FILE_IMG);
                }
                fileNode.NavigateUrl = SPControl.GetContextWeb(this.Context).Site.MakeFullUrl(file.Url);
                folderNode.ChildNodes.Add(fileNode);
            }
           

        }

    }
}
