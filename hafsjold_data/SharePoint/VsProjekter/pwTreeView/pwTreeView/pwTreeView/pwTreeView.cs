using System;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Serialization;

using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.WebPartPages;

namespace pwTreeView
{

    [Guid("6e6bacbb-c2fe-4dc7-8e5a-cd8e9879f2f2")]
    public class pwTreeView : System.Web.UI.WebControls.WebParts.WebPart, INamingContainer
    {
        private SPTreeView tvw;
        private SPHierarchyDataSourceControl ds;

        public pwTreeView()
        {
            this.ExportMode = WebPartExportMode.All;
        }

        protected override void CreateChildControls()
        {
            try
            {
                if (!this.Page.IsPostBack)
                {
                    string test = "Test";
                }

                SPWeb topWeb = SPControl.GetContextWeb(this.Context).Site.RootWeb;

                ds = new SPHierarchyDataSourceControl();
                
                ds.Web = topWeb;
                ds.RootWebId = topWeb.Site.RootWeb.ID.ToString();
                ds.RootContextObject = null; 
                //ds.RootContextObject = "Web";
                
                ds.ID = "TreeViewDataSource";
                ds.IncludeDiscussionFolders = true;
                ds.ShowListChildren = true;
                ds.ShowDocLibChildren = true;
                ds.ShowFolderChildren = true;
                ds.DataBind();
                Controls.Add(ds);

                tvw = new SPTreeView();
                tvw.ID = "WebTreeView";
                tvw.ShowLines = false;
                tvw.DataSourceID = "TreeViewDataSource";
                tvw.ExpandDepth = 1;
                tvw.EnableClientScript = true;
                tvw.EnableViewState = true;
                tvw.NodeStyle.CssClass = "ms-navitem";
                tvw.NodeStyle.HorizontalPadding = 2;
                tvw.SelectedNodeStyle.CssClass = "ms-tvselected";
                tvw.SkipLinkText = "";
                tvw.NodeIndent = 12;
                tvw.ExpandImageUrl = "/_layouts/images/tvplus.gif";
                tvw.CollapseImageUrl = "/_layouts/images/tvminus.gif";
                tvw.NoExpandImageUrl = "/_layouts/images/tvblank.gif";
                tvw.TreeNodeExpanded += Node_Expand;
                Controls.Add(tvw);
                //tvw.DataBind();

                base.CreateChildControls();

            }
            catch (Exception e)
            {
                string exp = "e.ToString";
            }
        }
        void Node_Expand(Object sender, System.Web.UI.WebControls.TreeNodeEventArgs e)
        {

            string Message = "Test";

        }


    }
}
