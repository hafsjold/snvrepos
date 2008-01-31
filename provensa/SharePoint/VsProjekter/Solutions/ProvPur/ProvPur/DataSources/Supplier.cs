
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


namespace provpur
{
    public class SupplierData
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

        //Fornavn  
        //Leverandør/Kontaktoplysninger: Fornavn  
        private string _supfirstname;
        public string supfirstname
        {
            get { return _supfirstname; }
            set { _supfirstname = value; }
        }

        //Efternavn  
        //Leverandør/Kontaktoplysninger: Efternavn  
        private string _supsurname;
        public string supsurname
        {
            get { return _supsurname; }
            set { _supsurname = value; }
        }

        //Initialer  
        //Leverandør/Kontaktoplysninger: Initialer  
        private string _supinitial;
        public string supinitial
        {
            get { return _supinitial; }
            set { _supinitial = value; }
        }

        //Titel  
        //Leverandør/Kontaktoplysninger: Titel  
        private string _suptitle;
        public string suptitle
        {
            get { return _suptitle; }
            set { _suptitle = value; }
        }

        //Direkte telefon-nr.  
        //Leverandør/Kontaktoplysninger: Direkte telefon-nr.  
        private string _supdirecttno;
        public string supdirecttno
        {
            get { return _supdirecttno; }
            set { _supdirecttno = value; }
        }

        //Mobil-nr.  
        //Leverandør/Kontaktoplysninger: Mobil-nr.  
        private string _supmobilno;
        public string supmobilno
        {
            get { return _supmobilno; }
            set { _supmobilno = value; }
        }

        //E-mail  
        //Leverandør/Kontaktoplysninger: E-mail  
        private string _supmail;
        public string supmail
        {
            get { return _supmail; }
            set { _supmail = value; }
        }

        //Firmanavn  
        //Leverandør/Firmaoplysninger: Firmanavn  
        private string _supcomname;
        public string supcomname
        {
            get { return _supcomname; }
            set { _supcomname = value; }
        }

        //Afdeling  
        //Leverandør/Firmaoplysninger: Afdeling  
        private string _supdepartment;
        public string supdepartment
        {
            get { return _supdepartment; }
            set { _supdepartment = value; }
        }

        //Adresse  
        //Leverandør/Firmaoplysninger: Adresse  
        private string _supaddress;
        public string supaddress
        {
            get { return _supaddress; }
            set { _supaddress = value; }
        }

        //Postnr.  
        //Leverandør/Firmaoplysninger: Postnr.  
        private int _suppostalcode;
        public int suppostalcode
        {
            get { return _suppostalcode; }
            set { _suppostalcode = value; }
        }

        //Postnr. /postdistrikt  
        //Leverandør/Firmaoplysninger: Postdistrikt  
        private string _suppostaldis;
        public string suppostaldis
        {
            get { return _suppostaldis; }
            set { _suppostaldis = value; }
        }

        //By  
        //Leverandør/Firmaoplysninger: By  
        private string _supcity;
        public string supcity
        {
            get { return _supcity; }
            set { _supcity = value; }
        }

        //Telefonnr.  
        //Leverandør/Firmaoplysninger: Telefonnr.  
        private string _suptelno;
        public string suptelno
        {
            get { return _suptelno; }
            set { _suptelno = value; }
        }

        //Brugerprofil  
        //Leverandør/Diverse: Brugerprofil  
        private string _supuserprofile;
        public string supuserprofile
        {
            get { return _supuserprofile; }
            set { _supuserprofile = value; }
        }

        //Login  
        //Leverandør/Diverse: Login  
        private string _suplogin;
        public string suplogin
        {
            get { return _suplogin; }
            set { _suplogin = value; }
        }

        //Password  
        //Leverandør/Diverse: Password  
        private string _suppassword;
        public string suppassword
        {
            get { return _suppassword; }
            set { _suppassword = value; }
        }

        //Bemærkninger   
        //Leverandør/Diverse: Bemærkninger   
        private string _supcomment;
        public string supcomment
        {
            get { return _supcomment; }
            set { _supcomment = value; }
        }

        //Aktivitetstype  
        //Leverandør/Aktiviteter: Aktivitetstype  
        private string _supacttype;
        public string supacttype
        {
            get { return _supacttype; }
            set { _supacttype = value; }
        }

        //Filnavn  
        //Leverandør/Aktiviteter: Filnavn  
        private string _supactfilename;
        public string supactfilename
        {
            get { return _supactfilename; }
            set { _supactfilename = value; }
        }

        //Emne  
        //Leverandør/Aktiviteter: Emne  
        private string _supactsubject;
        public string supactsubject
        {
            get { return _supactsubject; }
            set { _supactsubject = value; }
        }

        //Til  
        //Leverandør/Aktiviteter: Hvem dokumentet er til  
        private string _supactattention;
        public string supactattention
        {
            get { return _supactattention; }
            set { _supactattention = value; }
        }

        //Oprettet af    
        //Leverandør/Aktiviteter: Oprettet af    
        private string _supactwriter;
        public string supactwriter
        {
            get { return _supactwriter; }
            set { _supactwriter = value; }
        }

        //Oprettet dato  
        //Leverandør/Aktiviteter: Oprettet dato  
        private DateTime _supactsetupdate;
        public DateTime supactsetupdate
        {
            get { return _supactsetupdate; }
            set { _supactsetupdate = value; }
        }

        //Status  
        //Leverandør/Aktiviteter: Status  
        private string _supactstatus;
        public string supactstatus
        {
            get { return _supactstatus; }
            set { _supactstatus = value; }
        }

    }

    public class spSupplierListDB
    {
        private ProvPur.wsshost.Lists ls;

        public spSupplierListDB() { }

        private void wsslogin(string url, string login, string password, string domain)
        {
            ls = new ProvPur.wsshost.Lists();
            ls.Url = url + @"/_vti_bin/lists.asmx";
            ls.PreAuthenticate = true;
            ls.Credentials = new System.Net.NetworkCredential(login, password, domain);
        }

        public List<SupplierData> GetLists5()
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

            List<SupplierData> _rows = GetDataList(items);
            return _rows;
        }

        private List<SupplierData> GetDataList(XmlNode items)
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

            List<SupplierData> _rows = new List<SupplierData>();
            foreach (XmlNode datanode in nodelist)
            {
                SupplierData _row = new SupplierData();
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

        private List<SupplierData> FillDataList(SPListItemCollection collListItems)
        {
            List<SupplierData> _rows = new List<SupplierData>();
            foreach (SPListItem oListItem in collListItems)
            {

                SupplierData _row = new SupplierData();
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

        public List<SupplierData> GetLists()
        {
            SPWeb oWebsite = SPContext.Current.Web;
            SPList oList = oWebsite.Lists["Opgaver"];

            SPQuery oQuery = new SPQuery();
            oQuery.RowLimit = 10;
            int intIndex = 1;

            SPListItemCollection collListItems = oList.GetItems(oQuery);
            List<SupplierData> _rows = FillDataList(collListItems);

            return _rows;
        }

        public List<SupplierData> GetListsOld()
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
            List<SupplierData> dt = new List<SupplierData>();
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

                SupplierData r = new SupplierData();
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
