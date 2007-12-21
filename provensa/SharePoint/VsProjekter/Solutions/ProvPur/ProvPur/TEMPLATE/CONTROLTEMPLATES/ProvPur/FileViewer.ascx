<%@ Assembly Name="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" %>
<%@ Register Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c"
    Namespace="Microsoft.SharePoint.WebControls" TagPrefix="SharePoint" %>
<%@ Import Namespace="Microsoft.SharePoint" %>


<script runat="server">

  protected override void OnLoad(EventArgs e) {
    SPWeb site = SPContext.Current.Web;
    SPFolder rootFolder = site.RootFolder;
    TreeNode rootNode = new TreeNode(site.Title, "", @"\_layouts\images\FPWEB16.GIF");
    rootNode.NavigateUrl = site.ServerRelativeUrl;
    LoadFolderNodes(rootFolder, rootNode);
    treeSitesFiles.Nodes.Add(rootNode);
    treeSitesFiles.ExpandDepth = 1;
  }

  protected void LoadFolderNodes(SPFolder folder, TreeNode folderNode)  {
    
    foreach (SPFolder childFolder in folder.SubFolders) {
        TreeNode childFolderNode = new TreeNode(childFolder.Name, "", @"\_layouts\images\FOLDER16.GIF");
      childFolderNode.NavigateUrl = childFolder.ServerRelativeUrl;
      LoadFolderNodes(childFolder, childFolderNode);
      folderNode.ChildNodes.Add(childFolderNode);
    }
    
    foreach (SPFile file in folder.Files){
      TreeNode fileNode;
      if (file.CustomizedPageStatus == SPCustomizedPageStatus.Uncustomized) {
          fileNode = new TreeNode(file.Name, "", @"\_layouts\images\NEWDOC.GIF");
        fileNode.NavigateUrl = file.ServerRelativeUrl;
      }        
      else {
          fileNode = new TreeNode(file.Name, "", @"\_layouts\images\RAT16.GIF");
        fileNode.NavigateUrl = file.ServerRelativeUrl;
      }
      folderNode.ChildNodes.Add(fileNode);
    }


  }
  
  
</script>

<asp:TreeView  ID="treeSitesFiles" runat="server" EnableViewState="False"  CssClass="ms-treeviewouter" ImageSet="XPFileExplorer" NodeIndent="15">
    <ParentNodeStyle Font-Bold="False" />
    <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
    <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px" VerticalPadding="0px" />
    <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px" />
</asp:TreeView>
