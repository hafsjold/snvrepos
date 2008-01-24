using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Xml;
using System.Reflection;

namespace spListDB
{
    public class clsData
    {
        private int _ID;
        private string _Title;

        public PropertyInfo[] GetProperties() { return this.GetType().GetProperties(); }

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

    }

    public class spListDB
    {
        private wsshost.Lists ls;

        public spListDB() { }

        private void wsslogin(string url, string login, string password, string domain)
        {
            ls = new wsshost.Lists();
            ls.Url = url + @"/_vti_bin/lists.asmx";
            ls.PreAuthenticate = true;
            ls.Credentials = new System.Net.NetworkCredential(login, password, domain);
        }

        public List<clsData> GetLists5()
        {

            wsslogin("http://hd16.hafsjold.dk", "administrator", "m733", "hd16");

            XmlNode list = this.ls.GetList("ProPurTypeColumn");
            XmlDocument docTEST3 = new XmlDocument();
            docTEST3.LoadXml("<?xml version='1.0' ?>" + list.OuterXml);
            XmlNamespaceManager nsMgr3 = new XmlNamespaceManager(docTEST3.NameTable);
            nsMgr3.AddNamespace("mha", "http://schemas.microsoft.com/sharepoint/soap/");
            XmlNode node3 = docTEST3.SelectSingleNode("//mha:List", nsMgr3);
            string ListName3 = node3.Attributes["Name"].Value;
            string ListWebId3 = node3.Attributes["WebId"].Value;



            XmlNode lists = this.ls.GetListCollection();
            XmlDocument docTEST = new XmlDocument();
            docTEST.LoadXml("<?xml version='1.0' ?>" + lists.OuterXml);
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(docTEST.NameTable);
            nsMgr.AddNamespace("mha", "http://schemas.microsoft.com/sharepoint/soap/");
            XmlNode node1 = docTEST.SelectSingleNode("//mha:Lists/mha:List[@Title=\"ProPurColumn\"]", nsMgr);
            string ListName = node1.Attributes["Name"].Value;
            string ListWebId = node1.Attributes["WebId"].Value;



            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<Document><Query /><ViewFields /><QueryOptions /></Document>");
            XmlNode listQuery = doc.SelectSingleNode("//Query");
            XmlNode listViewFields = doc.SelectSingleNode("//ViewFields");
            XmlNode listQueryOptions = doc.SelectSingleNode("//QueryOptions");
            string RowLimit = "50";

            XmlNode items = ls.GetListItems(ListName, string.Empty, listQuery, listViewFields, RowLimit, listQueryOptions, ListWebId);

            List<clsData> _rows = GetDataList(items);
            return _rows;
        }

        private List<clsData> GetDataList(XmlNode items)
        {
            XmlDocument docItems = new XmlDocument();
            docItems.LoadXml("<?xml version='1.0' ?>" + items.OuterXml);
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(docItems.NameTable);
            nsMgr.AddNamespace("s", "uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882");
            nsMgr.AddNamespace("dt", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882");
            nsMgr.AddNamespace("rs", "urn:schemas-microsoft-com:rowset");
            nsMgr.AddNamespace("z", "#RowsetSchema");
            nsMgr.AddNamespace("mha", "http://schemas.microsoft.com/sharepoint/soap/");

            XmlNodeList nodelist = docItems.SelectNodes("//mha:listitems/rs:data/z:row", nsMgr);

            List<clsData> _rows = new List<clsData>();
            foreach (XmlNode datanode in nodelist)
            {
                clsData _row = new clsData();
                foreach (PropertyInfo pi in _row.GetProperties())
                {
                    string _field = "ows_" + pi.Name;
                    string _type = pi.PropertyType.Name;
                    if (pi.CanWrite)
                    {
                        try
                        {
                            if (datanode.Attributes[_field] != null)
                            {
                                switch (_type)
                                {
                                    case "Int32":
                                        pi.SetValue(_row, int.Parse((string)datanode.Attributes[_field].Value), null);
                                        break;

                                    case "String":
                                        pi.SetValue(_row, (string)datanode.Attributes[_field].Value, null);
                                        break;

                                    default:
                                        pi.SetValue(_row, datanode.Attributes[_field].Value, null);
                                        break;
                                }
                            }
                            else
                            {
                                pi.SetValue(_row, null, null);
                            }
                        }
                        catch
                        {
                            pi.SetValue(_row, null, null);
                        }
                    }
                }
                _rows.Add(_row);
            }
            return _rows;
        }

        private List<clsData> FillDataList(SPListItemCollection collListItems)
        {
            List<clsData> _rows = new List<clsData>();
            foreach (SPListItem oListItem in collListItems)
            {

                clsData _row = new clsData();
                foreach (PropertyInfo pi in _row.GetProperties())
                {
                    string _field = pi.Name;
                    if (pi.CanWrite)
                    {
                        try
                        {
                            if (oListItem[_field] != null)
                            {
                                pi.SetValue(_row, oListItem[_field], null);
                            }
                            else
                            {
                                pi.SetValue(_row, null, null);
                            }
                        }
                        catch
                        {
                            pi.SetValue(_row, null, null);
                        }
                    }
                }
                _rows.Add(_row);
            }
            return _rows;
        }

        public List<clsData> GetLists()
        {
            SPWeb oWebsite = SPContext.Current.Web;
            SPList oList = oWebsite.Lists["Opgaver"];

            SPQuery oQuery = new SPQuery();
            oQuery.RowLimit = 10;
            int intIndex = 1;

            SPListItemCollection collListItems = oList.GetItems(oQuery);
            List<clsData> _rows = FillDataList(collListItems);

            return _rows;
        }

        public List<clsData> GetListsOld()
        {

            SPDataSource ds = new SPDataSource();

            ds.DataSourceMode = SPDataSourceMode.List;
            ds.UseInternalName = true;
            ds.Scope = SPViewScope.Recursive;
            ds.IncludeHidden = true;

            string queryString;
            //queryString = "<View><ViewFields><FieldRef Name='ID'/><FieldRef Name='Title'/><FieldRef Name='PermMask'/></ViewFields><Query><Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>5</Value></Eq></Where></Query></View>";
            //queryString = "<View><ViewFields><FieldRef Name='ID'/><FieldRef Name='Title'/><FieldRef Name='PermMask'/></View>";
            queryString = "<View></View>";

            ds.SelectCommand = queryString;

            Parameter dbParam_ListId = new Parameter("ListID");
            dbParam_ListId.DefaultValue = "DF806E7E-0073-4202-99E3-C4A269E2FA5D";
            ds.SelectParameters.Add(dbParam_ListId);


            SPDataSourceView dsw;
            List<clsData> dt = new List<clsData>();
            dsw = ds.GetView();

            SPList xlist = dsw.List;

            SPViewCollection colViews = xlist.Views;
            foreach (SPView vView in colViews)
            {

            }

            /*
            SPListItem newFolder = xlist.Items.Add(xlist.RootFolder.ServerRelativeUrl, SPFileSystemObjectType.Folder, null);
            if (newFolder != null)
            {
                newFolder["Title"] = "Min Nye Folder";
                newFolder.Update();
            }
            */
            System.Web.UI.DataSourceSelectArguments args = new System.Web.UI.DataSourceSelectArguments();

            SPDataSourceViewResultItem[] arrResult = (SPDataSourceViewResultItem[])dsw.Select(args);
            foreach (SPDataSourceViewResultItem Result in arrResult)
            {
                SPListItem oListItem = (SPListItem)Result.ResultItem;
                //SPField myField = xitem.Fields[new Guid("{FE7E79DD-DD68-438e-A960-E3686025D44B}")];

                clsData r = new clsData();
                foreach (SPField ofield in oListItem.Fields)
                {
                    string tekst;
                    try
                    {
                        tekst = ">" + ofield.Title + "< >" + ofield.InternalName + "<>" + oListItem[ofield.Id] + "<";
                    }
                    catch
                    {
                        //OK
                    }

                    switch (ofield.InternalName)
                    {
                        case "ID":
                            r.ID = (int)oListItem[ofield.Id];
                            break;

                        case "Title":
                            r.Title = (string)oListItem[ofield.Id];
                            break;
                    }
                }
                dt.Add(r);
            }

            return dt;
        }

        public int UpdateList(Int32 op_id, string op_title)
        {
            SPDataSource ds = new SPDataSource();

            ds.DataSourceMode = SPDataSourceMode.List;
            ds.UseInternalName = true;

            string queryString;
            queryString = "<View></View>";
            ds.UpdateCommand = queryString;

            Parameter dbParam_ListId = new Parameter("ListID");
            dbParam_ListId.DefaultValue = "DF806E7E-0073-4202-99E3-C4A269E2FA5D";
            ds.UpdateParameters.Add(dbParam_ListId);

            Dictionary<string, object> keys = new Dictionary<string, object>();
            keys.Add("ID", op_id);

            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("Title", op_title);

            SPDataSourceView dsw = ds.GetView();
            dsw.Update(keys, values, null, null);

            return 1;
        }




    }
}
