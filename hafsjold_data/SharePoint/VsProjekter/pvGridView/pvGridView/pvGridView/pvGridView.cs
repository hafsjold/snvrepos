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
        SPDataSource SPds;



        protected override void CreateChildControls()
        {
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

            //TestDatsSource();

            oGrid = new SPGridView();
            oGrid.AutoGenerateColumns = false;
            this.Controls.Add(oGrid);

            oGrid.DataKeyNames = new string[] { "ID" };
            oGrid.DataSource = SPds;

            BoundField colID = new BoundField();
            colID.DataField = "ID";
            colID.HeaderText = "ID";
            oGrid.Columns.Add(colID);


            BoundField colName = new BoundField();
            colName.DataField = "PresenterName";
            colName.HeaderText = "PresenterName";
            oGrid.Columns.Add(colName);

            BoundField colProgramme = new BoundField();
            colProgramme.DataField = "ProgrammeName";
            colProgramme.HeaderText = "ProgrammeName";
            oGrid.Columns.Add(colProgramme);

            CommandField colSelectButton = new CommandField();
            colSelectButton.HeaderText = "Action";
            colSelectButton.ControlStyle.Width = new Unit(75);
            colSelectButton.SelectText = "Select";
            colSelectButton.ShowDeleteButton = true;
            colSelectButton.DeleteText = "Delete";
            colSelectButton.ShowSelectButton = true;
            oGrid.Columns.Add(colSelectButton);

            
            oGrid.RowDeleting += new GridViewDeleteEventHandler(GridViewDeleting);
            oGrid.RowDeleted += new GridViewDeletedEventHandler(GridViewDeleted);
            oGrid.SelectedIndexChanged += new EventHandler(view_SelectedIndexChanged);


            //if (!this.Page.IsPostBack)
            //{
                oGrid.DataBind();
            //}
        }


        void GridViewDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 iKey = 3;
            string iValue = "ID";
            string name = "ProgrammeName";
            string val = "Morgen Kanalen";

            e.Keys.Add(iValue, iKey);
            e.Values.Add(name, val);
        }
 
        void GridViewDeleted(object sender, GridViewDeletedEventArgs e)
        {
            GridViewRow row = oGrid.SelectedRow;
        }

        void view_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = oGrid.SelectedRow;
            //this.xmlUrl = oGrid.SelectedValue.ToString();
        }

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

                Dictionary<string, object> keys = new Dictionary<string, object>();
                Int32 iKey = 3;
                string iValue = "ID";
                keys.Add("ID", iKey);

                Dictionary<string, object> values = new Dictionary<string, object>();
                values.Add("PresenterName", "Andreas Hafsjold");
                values.Add("ProgrammeName", "Det Store Morgen Show");

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