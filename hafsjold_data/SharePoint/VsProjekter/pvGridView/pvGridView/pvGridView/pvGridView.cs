using System;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Serialization;

using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.WebPartPages;

using System.Data;
using System.Web.UI.WebControls;

namespace ListMenuSample
{
    [Guid("070a33ad-82d8-49cb-803a-d1bbe046a52b")]
    public class ListMenuSample : System.Web.UI.WebControls.WebParts.WebPart
    {
        private SPGridView oGrid;
        private pvGridView.TVData oDataset;
        private DataView oView;
        private SPSite siteCollection = SPContext.Current.Site;
        private SPWeb site = SPContext.Current.Web;
        SPDataSource test;
        SPNavigationManager test2;

        private void PopulateDataset()
        {
            oDataset = new pvGridView.TVData();
            oDataset.Presenters.AddPresentersRow(1, "Jeremy Paxman", "Newsnight");
            oDataset.Presenters.AddPresentersRow(2, "Kirsty Wark", "Newsnight");
            oDataset.Presenters.AddPresentersRow(6, "Bill Turnbull", "Breakfast");
            oDataset.Presenters.AddPresentersRow(7, "Sian Williams", "Breakfast");
            oDataset.Presenters.AddPresentersRow(8, "Søren Olsen", "Evning");
            oDataset.Presenters.AddPresentersRow(9, "Ole Andersen", "Evning");
            oDataset.Presenters.AddPresentersRow(10, "Peter Pedersen", "Breakfast");
            oDataset.Presenters.AddPresentersRow(11, "Jens Hansen", "Newsnight");
            // plus a few more entries
        }

        protected override void CreateChildControls()
        {
            PopulateDataset();
            oView = new DataView(oDataset.Presenters);

            oGrid = new SPGridView();
            oGrid.DataSource = oView;
            oGrid.AutoGenerateColumns = false;
            oGrid.AllowSorting = true;
            oGrid.Sorting += new GridViewSortEventHandler(oGrid_Sorting);

            oGrid.AllowGrouping = true;
            oGrid.AllowGroupCollapse = true;
            oGrid.GroupField = "ProgrammeName";
            oGrid.GroupDescriptionField = "ProgrammeName";
            oGrid.GroupFieldDisplayName = "Programme";


            /*
            BoundField colName = new BoundField();
            colName.DataField = "PresenterName";
            colName.HeaderText = "Presenter Name";
            colName.SortExpression = "PresenterName";
            oGrid.Columns.Add(colName);
            */
            
            SPMenuField colMenu = new SPMenuField();
            colMenu.HeaderText = "Presenter Name";
            colMenu.TextFields = "PresenterName";
            colMenu.MenuTemplateId = "PresenterListMenu";
            colMenu.NavigateUrlFields = "ID,PresenterName";
            colMenu.NavigateUrlFormat = "do.aspx?p={0}&q={1}";
            colMenu.TokenNameAndValueFields = "EDIT=ID,NAME=PresenterName";
            colMenu.SortExpression = "PresenterName";

            MenuTemplate presenterListMenu = new MenuTemplate();
            presenterListMenu.ID = "PresenterListMenu";
            MenuItemTemplate biogMenu = new MenuItemTemplate(
                "Read Biography", "/_layouts/images/EawfNewUser.gif");
            biogMenu.ClientOnClickNavigateUrl = "do.aspx?this=%EDIT%&that=%NAME%";
            //entry.ClientOnClickScript = "your javascript here";
            presenterListMenu.Controls.Add(biogMenu);

            MenuItemTemplate broadcastMenu = new MenuItemTemplate(
                    "Recent Broadcasts", "/_layouts/images/ICWM.gif");
            presenterListMenu.Controls.Add(broadcastMenu);

            MenuSeparatorTemplate sepMenu = new MenuSeparatorTemplate();
            presenterListMenu.Controls.Add(sepMenu);

            MenuItemTemplate favMenu = new MenuItemTemplate(
                   "Add to Favorites", "/_layouts/images/addtofavorites.gif");
            presenterListMenu.Controls.Add(favMenu);

            this.Controls.Add(presenterListMenu);
            oGrid.Columns.Add(colMenu);


            // Add the menu control here

            BoundField colProgramme = new BoundField();
            colProgramme.DataField = "ProgrammeName";
            colProgramme.HeaderText = "Programme";
            colProgramme.SortExpression = "ProgrammeName";
            oGrid.Columns.Add(colProgramme);

            Controls.Add(oGrid);
            
            // Turn on paging and add event handler
            oGrid.PageSize = 3;
            oGrid.AllowPaging = true;
            oGrid.PageIndexChanging +=
                new GridViewPageEventHandler(oGrid_PageIndexChanging);
            oGrid.PagerTemplate = null;  // Must be called after Controls.Add(oGrid)

            // Recreate current sort if needed
            if (ViewState["SortDirection"] != null && ViewState["SortExpression"] != null)
            {
                // We have an active sorting, so this need to be preserved
                oView.Sort = ViewState["SortExpression"].ToString()
                    + " " + ViewState["SortDirection"].ToString();
            }

            oGrid.DataBind();

            base.CreateChildControls();
        }

        void oGrid_Sorting(object sender, GridViewSortEventArgs e)
        {
            string lastExpression = "";
            if (ViewState["SortExpression"] != null)
                lastExpression = ViewState["SortExpression"].ToString();

            string lastDirection = "asc";
            if (ViewState["SortDirection"] != null)
                lastDirection = ViewState["SortDirection"].ToString();

            string newDirection = "asc";
            if (e.SortExpression == lastExpression)
                newDirection = (lastDirection == "asc") ? "desc" : "asc";

            ViewState["SortExpression"] = e.SortExpression;
            ViewState["SortDirection"] = newDirection;

            oView.Sort = e.SortExpression + " " + newDirection;
            oGrid.DataBind();
        }

        void oGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            oGrid.PageIndex = e.NewPageIndex;
            oGrid.DataBind();
        }
    }
}