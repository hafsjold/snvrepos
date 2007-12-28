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
using System.Collections.Generic;

namespace ListMenuSample
{
    [Guid("070a33ad-82d8-49cb-803a-d1bbe046a52b")]
    public class ListMenuSample : System.Web.UI.WebControls.WebParts.WebPart
    {
        private SPGridView oGrid;
        private pvGridView.TVData oDataset;
        private DataView oView;
        private SPSite siteCollection = SPContext.Current.Site;
        private SPWeb web = SPContext.Current.Web;
        private SPList list;
        SPDataSource dsSharePoint;
        SPNavigationManager test2;
        SPDataSourceView myView;
        SqlDataSource dsSQL;
        DataTable dvwtable;

        private void PopulateDataset()
        {
            SPList list = web.Lists["linier"];
            SPListItem realitem = list.GetItemById((int.Parse("1")));
            double ordnr = (double)realitem["ordernr"];
            realitem["ordernr"] = --ordnr;
            web.AllowUnsafeUpdates = true;
            realitem.Update();
            web.AllowUnsafeUpdates = false;
            string querystring = "<WHERE><LE><FieldRef Name=\"Modified\"/>"
                + "<Value Type=\"DateTime\"><Today OffsetDays=\"5\" /></Value></LE></WHERE>";
            SPQuery query = new SPQuery();
            query.Query = querystring;
            SPListItemCollection itemcoll = list.GetItems(query);
            SPListItem item = itemcoll.GetItemById(1);
            //string WebId = item["WebId"].ToString();
            //string ListId = item["ListId"].ToString();


            object ordernr = item["ordernr"];

            foreach (SPField field in item.Fields) { 
                if(!field.Hidden && !field.ReadOnlyField){
                    string test = field.Title + " = " + item[field.Id];
                }
            }
            
            
            dvwtable = itemcoll.GetDataTable(); 


            
            dsSQL = new SqlDataSource("Data Source=HD19\\SQLEXPRESS;Initial Catalog=SharePointDevelopment;Persist Security Info=True;User ID=sa;Password=mha733", "SELECT [id], [name] FROM [tblTypename]");
            
            DataSourceSelectArguments args = new DataSourceSelectArguments();
            DataView SQLview = (DataView)dsSQL.Select(args);
            DataTable table = SQLview.ToTable();
            
            dsSharePoint = new SPDataSource();
            dsSharePoint.DataSourceMode = SPDataSourceMode.List;
            dsSharePoint.UseInternalName = true;
            dsSharePoint.ID = "spdatasource1";
            dsSharePoint.EnableViewState = true;
            
            dsSharePoint.SelectCommand = "<View><Query><Where><Gt><FieldRef Name='Modified'/><Value Type='DateTime'><Today OffsetDays='-7' OffsetHoures='-5' /></Value></Gt></Where></Query></View>";
            Parameter p1_select = new Parameter("ListID");
            p1_select.DefaultValue = "17B3F9BC-5B07-431D-A876-5055040AD4E1";
            dsSharePoint.SelectParameters.Add(p1_select);

            dsSharePoint.InsertCommand = "<View></View>";
            Parameter p1_insert = new Parameter("ListID");
            p1_insert.DefaultValue = "17B3F9BC-5B07-431D-A876-5055040AD4E1";
            dsSharePoint.InsertParameters.Add(p1_insert);

            dsSharePoint.UpdateCommand = "<View></View>";
            Parameter p1_update = new Parameter("ListID");
            p1_update.DefaultValue = "17B3F9BC-5B07-431D-A876-5055040AD4E1";
            dsSharePoint.UpdateParameters.Add(p1_update);

            dsSharePoint.DeleteCommand = "<View></View>";
            Parameter p1_delete = new Parameter("ListID");
            p1_delete.DefaultValue = "17B3F9BC-5B07-431D-A876-5055040AD4E1";
            dsSharePoint.DeleteParameters.Add(p1_delete);

            SPDataSourceView mydsview = dsSharePoint.GetView();

            Dictionary<string, string> dic = mydsview.SelectParametersDictionary;
           
            DataSourceSelectArguments arg = new DataSourceSelectArguments();
            arg.MaximumRows = 5;
            
            SPDataSourceViewResultItem[] emn = (SPDataSourceViewResultItem[])mydsview.Select(arg);
            SPListItem item0 = (SPListItem)emn[0].ResultItem;

            //mydsview.Update(cc, cc, xx, bb);


            //DataView SP_view = (DataView)dsSharePoint.SelectParameters();
            //DataTable SP_table = SP_view.ToTable();

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
            oGrid.AutoGenerateDeleteButton = true;
            oGrid.DataSource = dvwtable;
            //////oGrid.DataSource = dsSharePoint;
            //oGrid.DataSource = dsSQL;
            oGrid.AutoGenerateColumns = false;
            //oGrid.AllowSorting = true;
            //oGrid.Sorting += new GridViewSortEventHandler(oGrid_Sorting);

            //oGrid.AllowGrouping = true;
            //oGrid.AllowGroupCollapse = true;
            //oGrid.GroupField = "ProgrammeName";
            //oGrid.GroupDescriptionField = "ProgrammeName";
            //oGrid.GroupFieldDisplayName = "Programme";


            
            BoundField colName = new BoundField();
            colName.DataField = "PresenterName";
            colName.HeaderText = "PresenterName";
            //colName.SortExpression = "PresenterName";
            oGrid.Columns.Add(colName);
            
            
            /*
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

            */
            // Add the menu control here

            BoundField colProgramme = new BoundField();
            colProgramme.DataField = "ProgrammeName";
            colProgramme.HeaderText = "ProgrammeName";
            //colProgramme.SortExpression = "ProgrammeName";
            oGrid.Columns.Add(colProgramme);

            Controls.Add(oGrid);
            
            /*
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
            */
            oGrid.DataBind();
            GridViewRowCollection myrows =  oGrid.Rows;
            TableCell mycell = oGrid.Rows[1].Cells[1];
            DataControlFieldCell cc = (DataControlFieldCell)mycell;
        
            base.CreateChildControls();
        }

        /*
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
    */
}
}