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
<<<<<<< .mine
        private DataView oView;
=======
        SPDataSource SPds;
>>>>>>> .r96
        private SPSite siteCollection = SPContext.Current.Site;
        private SPWeb web = SPContext.Current.Web;
        private SPList list;
        private DataTable oTable;
        string test;

<<<<<<< .mine
=======

>>>>>>> .r96

        protected override void CreateChildControls()
        {
<<<<<<< .mine
=======
            base.CreateChildControls();
            
            SPds = new SPDataSource();
            this.Controls.Add(SPds);
            
            SPds.DataSourceMode = SPDataSourceMode.List;
            SPds.UseInternalName = true;
            SPds.ID = "spdatasource1";
            SPds.EnableViewState = true;
            SPds.SelectCommand = "<View></View>";
            Parameter p1_select = new Parameter("ListName");
            p1_select.DefaultValue = "linier";
            SPds.SelectParameters.Add(p1_select);
>>>>>>> .r96

<<<<<<< .mine
            if (!this.Page.IsPostBack)
            {
            }
=======
            //TestDatsSource();

            oGrid = new SPGridView();
            oGrid.AutoGenerateColumns = false;
            this.Controls.Add(oGrid);
>>>>>>> .r96

<<<<<<< .mine
                siteCollection = SPContext.Current.Site;
                web = SPContext.Current.Web;
                list = web.Lists["linier"];
=======
            oGrid.DataKeyNames = new string[] { "ID" };
            oGrid.DataSource = SPds;
>>>>>>> .r96

<<<<<<< .mine
                //string querystring = "<WHERE><LE><FieldRef Name=\"Modified\"/>"
                //    + "<Value Type=\"DateTime\"><Today OffsetDays=\"5\" /></Value></LE></WHERE>";
                string querystring = "<WHERE><LE><FieldRef Name=\"Modified\"/>"
                + "<Value Type=\"DateTime\"><Today OffsetDays=\"5\" /></Value></LE></WHERE>";
=======
            BoundField colID = new BoundField();
            colID.DataField = "ID";
            colID.HeaderText = "ID";
            oGrid.Columns.Add(colID);
>>>>>>> .r96

<<<<<<< .mine
                SPQuery query = new SPQuery();
                query.Query = querystring;
                SPListItemCollection oItemColl = list.GetItems(query);
                oTable = oItemColl.GetDataTable();
                oView = oTable.DefaultView;
=======

            BoundField colName = new BoundField();
            colName.DataField = "PresenterName";
            colName.HeaderText = "PresenterName";
            oGrid.Columns.Add(colName);
>>>>>>> .r96

<<<<<<< .mine
=======
            BoundField colProgramme = new BoundField();
            colProgramme.DataField = "ProgrammeName";
            colProgramme.HeaderText = "ProgrammeName";
            oGrid.Columns.Add(colProgramme);
>>>>>>> .r96

<<<<<<< .mine
                oGrid = new SPGridView();
                oGrid.EnableViewState = true;
=======
            CommandField colSelectButton = new CommandField();
            colSelectButton.HeaderText = "Action";
            colSelectButton.ControlStyle.Width = new Unit(75);
            colSelectButton.SelectText = "Select";
            colSelectButton.ShowDeleteButton = true;
            colSelectButton.DeleteText = "Delete";
            colSelectButton.ShowSelectButton = true;
            oGrid.Columns.Add(colSelectButton);
>>>>>>> .r96

<<<<<<< .mine
                oGrid.DataSource = oView;
                oGrid.AutoGenerateColumns = false;
                oGrid.AllowSorting = true;
                oGrid.Sorting += new GridViewSortEventHandler(oGrid_Sorting);
=======
            
            oGrid.RowDeleting += new GridViewDeleteEventHandler(GridViewDeleting);
            oGrid.RowDeleted += new GridViewDeletedEventHandler(GridViewDeleted);
            oGrid.SelectedIndexChanged += new EventHandler(view_SelectedIndexChanged);
>>>>>>> .r96

<<<<<<< .mine
                oGrid.AllowGrouping = true;
                oGrid.AllowGroupCollapse = true;
                oGrid.GroupField = "ID";
                oGrid.GroupDescriptionField = "ID";
                oGrid.GroupFieldDisplayName = "ID";
=======
>>>>>>> .r96

<<<<<<< .mine
                oGrid.AllowGrouping = true;
                oGrid.AllowGroupCollapse = true;
                oGrid.GroupField = "ProgrammeName";
                oGrid.GroupDescriptionField = "ProgrammeName";
                oGrid.GroupFieldDisplayName = "Programme";
=======
            //if (!this.Page.IsPostBack)
            //{
                oGrid.DataBind();
            //}
        }
>>>>>>> .r96


<<<<<<< .mine
                /*
                BoundField colName = new BoundField();
                colName.DataField = "PresenterName";
                colName.HeaderText = "Presenter Name";
                colName.SortExpression = "PresenterName";
                oGrid.Columns.Add(colName);
                */
=======
        void GridViewDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 iKey = 3;
            string iValue = "ID";
            string name = "ProgrammeName";
            string val = "Morgen Kanalen";
>>>>>>> .r96

<<<<<<< .mine
                SPMenuField colMenu = new SPMenuField();
                colMenu.HeaderText = "Presenter Name";
                colMenu.TextFields = "PresenterName";
                colMenu.MenuTemplateId = "PresenterListMenu";
                colMenu.NavigateUrlFields = "ID,PresenterName";
                colMenu.NavigateUrlFormat = "do.aspx?p={0}&q={1}";
                colMenu.TokenNameAndValueFields = "EDIT=ID,NAME=PresenterName";
                colMenu.SortExpression = "PresenterName";
=======
            e.Keys.Add(iValue, iKey);
            e.Values.Add(name, val);
        }
 
        void GridViewDeleted(object sender, GridViewDeletedEventArgs e)
        {
            GridViewRow row = oGrid.SelectedRow;
        }
>>>>>>> .r96

<<<<<<< .mine
                MenuTemplate presenterListMenu = new MenuTemplate();
                presenterListMenu.ID = "PresenterListMenu";
                MenuItemTemplate biogMenu = new MenuItemTemplate("Read Biography", "/_layouts/images/EawfNewUser.gif");
                biogMenu.ClientOnClickNavigateUrl = "do.aspx?this=%EDIT%&that=%NAME%";
                //entry.ClientOnClickScript = "your javascript here";
                presenterListMenu.Controls.Add(biogMenu);
=======
        void view_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = oGrid.SelectedRow;
            //this.xmlUrl = oGrid.SelectedValue.ToString();
        }
>>>>>>> .r96

<<<<<<< .mine
                MenuItemTemplate broadcastMenu = new MenuItemTemplate("Recent Broadcasts", "/_layouts/images/ICWM.gif");
                presenterListMenu.Controls.Add(broadcastMenu);
=======
        private void TestDatsSource()
        {

            if (this.Page.IsPostBack)
            {
                SPDataSourceView mydsview = SPds.GetView();
                SPList xlist = mydsview.List;
                SPListItem xitem = xlist.GetItemById(3);
                foreach (SPField xfield in xitem.Fields)
                {
                    string tekst = ">" + xfield.Title + "< >" + xfield.InternalName + "<>" + xitem[xfield.Id] + "<";
                }
>>>>>>> .r96

<<<<<<< .mine
                MenuSeparatorTemplate sepMenu = new MenuSeparatorTemplate();
                presenterListMenu.Controls.Add(sepMenu);
=======
                Dictionary<string, object> keys = new Dictionary<string, object>();
                Int32 iKey = 3;
                string iValue = "ID";
                keys.Add("ID", iKey);
>>>>>>> .r96

<<<<<<< .mine
                MenuItemTemplate favMenu = new MenuItemTemplate("Add to Favorites", "/_layouts/images/addtofavorites.gif");
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
                oGrid.PageSize = 6;
                oGrid.AllowPaging = true;
                oGrid.PageIndexChanging += new GridViewPageEventHandler(oGrid_PageIndexChanging);
                oGrid.PagerTemplate = null;  // Must be called after Controls.Add(oGrid)

                // Recreate current sort if needed
                if (ViewState["SortDirection"] != null && ViewState["SortExpression"] != null)
                {
                    // We have an active sorting, so this need to be preserved
                    oView.Sort = ViewState["SortExpression"].ToString() + " " + ViewState["SortDirection"].ToString();
                }

                oGrid.DataKeyNames = new string[] { "ID" };
                oGrid.DataBind();


                base.CreateChildControls();
            
        }
=======
                Dictionary<string, object> values = new Dictionary<string, object>();
                values.Add("PresenterName", "Andreas Hafsjold");
                values.Add("ProgrammeName", "Det Store Morgen Show");
>>>>>>> .r96

                Dictionary<string, object> oldvalues = new Dictionary<string, object>();
                oldvalues.Add("PresenterName", "Mogens");

                mydsview.Insert(values, dscalback);
                //mydsview.Update(keys, values, oldvalues, dscalback);
                //mydsview.Delete(keys, oldkeys, dscalback);

                DataSourceSelectArguments arg = new DataSourceSelectArguments();
                arg.MaximumRows = 5;

                SPDataSourceViewResultItem[] emn = (SPDataSourceViewResultItem[])mydsview.Select(arg);
                SPListItem item0 = (SPListItem)emn[0].ResultItem;
            }


        }

        bool dscalback(int affectedRows, Exception ex)
        {
            return true;
        }
    }
}