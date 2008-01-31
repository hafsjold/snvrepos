
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Xml;
using System.Reflection;


namespace ProvPur
{
    public class OrderCaseData
    {
        public PropertyInfo[] GetProperties() { return this.GetType().GetProperties(); }

        //ID  
        //System ID Field  
        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        //Titel  
        //System Title Field  
        private string _Title;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        //Sagsnummer  
        //Indkøbssag:  Sagsnummer  
        private int _pono;
        public int pono
        {
            get { return _pono; }
            set { _pono = value; }
        }

        //Ordreen vedrører køb af  
        //Indkøbssag:  Orderen vedrører køb af  
        private string _orderconcern;
        public string orderconcern
        {
            get { return _orderconcern; }
            set { _orderconcern = value; }
        }

        //Status på indkøbssag  
        //Indkøbssag:  Status på indkøbssag  
        private int _Orderstatus;
        public int Orderstatus
        {
            get { return _Orderstatus; }
            set { _Orderstatus = value; }
        }

        //Aktivitetstype  
        //Indkøbssag/Aktiviteter: Aktivitetstype  
        private string _acttype;
        public string acttype
        {
            get { return _acttype; }
            set { _acttype = value; }
        }

        //Filnavn  
        //Indkøbssag/Aktiviteter: Filnavn  
        private string _actfilename;
        public string actfilename
        {
            get { return _actfilename; }
            set { _actfilename = value; }
        }

        //Emne  
        //Indkøbssag/Aktiviteter: Emne  
        private string _actsubject;
        public string actsubject
        {
            get { return _actsubject; }
            set { _actsubject = value; }
        }

        //Til  
        //Indkøbssag/Aktiviteter: Til (person)  
        private string _actattention;
        public string actattention
        {
            get { return _actattention; }
            set { _actattention = value; }
        }

        //Oprettet af    
        //Indkøbssag/Aktiviteter: Oprettet af  
        private string _actwriter;
        public string actwriter
        {
            get { return _actwriter; }
            set { _actwriter = value; }
        }

        //Oprettet dato  
        //Indkøbssag/Aktiviteter: Oprettet dato  
        private DateTime _actsetupdate;
        public DateTime actsetupdate
        {
            get { return _actsetupdate; }
            set { _actsetupdate = value; }
        }

        //Status  
        //Indkøbssag/Aktiviteter: Status  
        private string _actstatus;
        public string actstatus
        {
            get { return _actstatus; }
            set { _actstatus = value; }
        }

        //Filnavn  
        //Indkøbssag/Ordrebekræftelse: Filnavn  
        private string _conorderfilename;
        public string conorderfilename
        {
            get { return _conorderfilename; }
            set { _conorderfilename = value; }
        }

        //Vedrørende  
        //Indkøbssag/Ordrebekræftelse: Vedrørende  
        private string _conorderregard;
        public string conorderregard
        {
            get { return _conorderregard; }
            set { _conorderregard = value; }
        }

        //Til  
        //Indkøbssag/Ordrebekræftelse: Til (person)  
        private string _conorderattention;
        public string conorderattention
        {
            get { return _conorderattention; }
            set { _conorderattention = value; }
        }

        //Oprettet af    
        //Indkøbssag/Ordrebekræftelse: Oprettet af    
        private string _conorderwriter;
        public string conorderwriter
        {
            get { return _conorderwriter; }
            set { _conorderwriter = value; }
        }

        //Oprettet dato  
        //Indkøbssag/Ordrebekræftelse: Oprettet dato  
        private DateTime _conordersetupdate;
        public DateTime conordersetupdate
        {
            get { return _conordersetupdate; }
            set { _conordersetupdate = value; }
        }

        //Filnavn  
        //Indkøbssag/Følgeseddel:  Filnavn  
        private string _dnotefilename;
        public string dnotefilename
        {
            get { return _dnotefilename; }
            set { _dnotefilename = value; }
        }

        //Vedrørende  
        //Indkøbssag/Følgeseddel:  Vedrørende  
        private string _dnoteregard;
        public string dnoteregard
        {
            get { return _dnoteregard; }
            set { _dnoteregard = value; }
        }

        //Til  
        //Indkøbssag/Følgeseddel:  Eventuelt hvem dokumentet er til  
        private string _dnoteattention;
        public string dnoteattention
        {
            get { return _dnoteattention; }
            set { _dnoteattention = value; }
        }

        //Oprettet af    
        //Indkøbssag/Følgeseddel:  Oprettet af    
        private string _dnotewriter;
        public string dnotewriter
        {
            get { return _dnotewriter; }
            set { _dnotewriter = value; }
        }

        //Oprettet dato  
        //Indkøbssag/Følgeseddel:  Oprettet dato  
        private DateTime _dnotesetupdate;
        public DateTime dnotesetupdate
        {
            get { return _dnotesetupdate; }
            set { _dnotesetupdate = value; }
        }

        //Filnavn  
        //Indkøbssag/Faktura:  Filnavn  
        private string _invfilename;
        public string invfilename
        {
            get { return _invfilename; }
            set { _invfilename = value; }
        }

        //Vedrørende  
        //Indkøbssag/Faktura:  Vedrørende  
        private string _invregard;
        public string invregard
        {
            get { return _invregard; }
            set { _invregard = value; }
        }

        //Til  
        //Indkøbssag/Faktura:  Eventuelt hvem dokumentet er til  
        private string _invattention;
        public string invattention
        {
            get { return _invattention; }
            set { _invattention = value; }
        }

        //Oprettet af    
        //Indkøbssag/Faktura:  Oprettet af    
        private string _invwriter;
        public string invwriter
        {
            get { return _invwriter; }
            set { _invwriter = value; }
        }

        //Oprettet dato  
        //Indkøbssag/Faktura:  Oprettet dato  
        private DateTime _invsetupdate;
        public DateTime invsetupdate
        {
            get { return _invsetupdate; }
            set { _invsetupdate = value; }
        }

    }

    public class spOrderCaseListDB
    {
        private wsshost.Lists ls;

        public spOrderCaseListDB() { }

        private void wsslogin(string url, string login, string password, string domain)
        {
            ls = new wsshost.Lists();
            ls.Url = url + @"/_vti_bin/lists.asmx";
            ls.PreAuthenticate = true;
            ls.Credentials = new System.Net.NetworkCredential(login, password, domain);
        }

        public List<OrderCaseData> GetLists5()
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

            List<OrderCaseData> _rows = GetDataList(items);
            return _rows;
        }

        private List<OrderCaseData> GetDataList(XmlNode items)
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

            List<OrderCaseData> _rows = new List<OrderCaseData>();
            foreach (XmlNode datanode in nodelist)
            {
                OrderCaseData _row = new OrderCaseData();
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

        private List<OrderCaseData> FillDataList(SPListItemCollection collListItems)
        {
            List<OrderCaseData> _rows = new List<OrderCaseData>();
            foreach (SPListItem oListItem in collListItems)
            {

                OrderCaseData _row = new OrderCaseData();
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

        public List<OrderCaseData> GetLists()
        {
            SPWeb oWebsite = SPContext.Current.Web;
            SPList oList = oWebsite.Lists["Opgaver"];

            SPQuery oQuery = new SPQuery();
            oQuery.RowLimit = 10;
            int intIndex = 1;

            SPListItemCollection collListItems = oList.GetItems(oQuery);
            List<OrderCaseData> _rows = FillDataList(collListItems);

            return _rows;
        }

        public List<OrderCaseData> GetListsOld()
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
            List<OrderCaseData> dt = new List<OrderCaseData>();
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

                OrderCaseData r = new OrderCaseData();
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
