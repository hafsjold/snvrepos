
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
    public class RekvirentData
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

        //Aktivitetstype  
        //Rekvirent og indkøber/Aktiviteter: Aktivitetstype  
        private string _claacttype;
        public string claacttype
        {
            get { return _claacttype; }
            set { _claacttype = value; }
        }

        //Emne  
        //Rekvirent og indkøber/Aktiviteter: Emne  
        private string _claactsubject;
        public string claactsubject
        {
            get { return _claactsubject; }
            set { _claactsubject = value; }
        }

        //Filnavn  
        //Rekvirent og indkøber/Aktiviteter: Filnavn  
        private string _claactfilename;
        public string claactfilename
        {
            get { return _claactfilename; }
            set { _claactfilename = value; }
        }

        //Til  
        //Rekvirent og indkøber/Aktiviteter: Hvem dokumentet er til  
        private string _claactattention;
        public string claactattention
        {
            get { return _claactattention; }
            set { _claactattention = value; }
        }

        //Oprettet af    
        //Rekvirent og indkøber/Aktiviteter: Oprettet af    
        private string _claactwriter;
        public string claactwriter
        {
            get { return _claactwriter; }
            set { _claactwriter = value; }
        }

        //Oprettet dato  
        //Rekvirent og indkøber/Aktiviteter: Oprettet dato  
        private DateTime _claactsetupdate;
        public DateTime claactsetupdate
        {
            get { return _claactsetupdate; }
            set { _claactsetupdate = value; }
        }

        //Status  
        //Rekvirent og indkøber/Aktiviteter: Status  
        private string _claactstatus;
        public string claactstatus
        {
            get { return _claactstatus; }
            set { _claactstatus = value; }
        }

        //Bemærkninger   
        //Rekvirent og indkøber/Diverse: Bemærkninger   
        private string _clacomment;
        public string clacomment
        {
            get { return _clacomment; }
            set { _clacomment = value; }
        }

        //Brugerprofil  
        //Rekvirent og indkøber/Diverse: Brugerprofil  
        private string _clauserprofile;
        public string clauserprofile
        {
            get { return _clauserprofile; }
            set { _clauserprofile = value; }
        }

        //Login  
        //Rekvirent og indkøber/Diverse: Login  
        private string _clalogin;
        public string clalogin
        {
            get { return _clalogin; }
            set { _clalogin = value; }
        }

        //Password  
        //Rekvirent og indkøber/Diverse: Password  
        private string _clapassword;
        public string clapassword
        {
            get { return _clapassword; }
            set { _clapassword = value; }
        }

        //VIP status  
        //Rekvirent og indkøber/Diverse: VIP status  
        private string _clavipstatus;
        public string clavipstatus
        {
            get { return _clavipstatus; }
            set { _clavipstatus = value; }
        }

        //Adresse  
        //Rekvirent og indkøber/Firmaoplysninger: Adresse  
        private string _claaddress;
        public string claaddress
        {
            get { return _claaddress; }
            set { _claaddress = value; }
        }

        //Afdeling  
        //Rekvirent og indkøber/Firmaoplysninger: Afdeling  
        private string _cladepartment;
        public string cladepartment
        {
            get { return _cladepartment; }
            set { _cladepartment = value; }
        }

        //By  
        //Rekvirent og indkøber/Firmaoplysninger: By  
        private string _clacity;
        public string clacity
        {
            get { return _clacity; }
            set { _clacity = value; }
        }

        //Firmanavn  
        //Rekvirent og indkøber/Firmaoplysninger: Firmanavn  
        private string _clacomname;
        public string clacomname
        {
            get { return _clacomname; }
            set { _clacomname = value; }
        }

        //Postdistrikt  
        //Rekvirent og indkøber/Firmaoplysninger: Postdistrikt  
        private string _clapostaldis;
        public string clapostaldis
        {
            get { return _clapostaldis; }
            set { _clapostaldis = value; }
        }

        //Postnr.  
        //Indkøbskurv/Rekvirentoplysninger:  Postnr.  
        private int _clapostalcode;
        public int clapostalcode
        {
            get { return _clapostalcode; }
            set { _clapostalcode = value; }
        }

        //Ansat / Fratrådt  
        //Rekvirent og indkøber/Kontaktoplysninger: Ansat / Fratrådt  
        private string _claempres;
        public string claempres
        {
            get { return _claempres; }
            set { _claempres = value; }
        }

        //Direkte telefon-nr.  
        //Rekvirent og indkøber/Kontaktoplysninger: Direkte telefon-nr.  
        private string _cladirecttno;
        public string cladirecttno
        {
            get { return _cladirecttno; }
            set { _cladirecttno = value; }
        }

        //Efternavn  
        //Rekvirent og indkøber/Kontaktoplysninger: Efternavn  
        private string _clasurname;
        public string clasurname
        {
            get { return _clasurname; }
            set { _clasurname = value; }
        }

        //E-mail  
        //Rekvirent og indkøber/Kontaktoplysninger: E-mail  
        private string _clamail;
        public string clamail
        {
            get { return _clamail; }
            set { _clamail = value; }
        }

        //Fornavn  
        //Rekvirent og indkøber/Kontaktoplysninger: Fornavn  
        private string _clafirstname;
        public string clafirstname
        {
            get { return _clafirstname; }
            set { _clafirstname = value; }
        }

        //Initialer  
        //Rekvirent og indkøber/Kontaktoplysninger: Initialer  
        private string _clainitial;
        public string clainitial
        {
            get { return _clainitial; }
            set { _clainitial = value; }
        }

        //Leders initialer  
        //Rekvirent og indkøber/Kontaktoplysninger: Leders initialer  
        private string _clamangerinitial;
        public string clamangerinitial
        {
            get { return _clamangerinitial; }
            set { _clamangerinitial = value; }
        }

        //Medarbejder-nr.  
        //Rekvirent og indkøber/Kontaktoplysninger: Medarbejder-nr.  
        private int _claempno;
        public int claempno
        {
            get { return _claempno; }
            set { _claempno = value; }
        }

        //Leders navn  
        //Rekvirent og indkøber/Kontaktoplysninger: Leders navn  
        private string _clamanagername;
        public string clamanagername
        {
            get { return _clamanagername; }
            set { _clamanagername = value; }
        }

        //Mobil-nr.  
        //Rekvirent og indkøber/Kontaktoplysninger: Mobil-nr.  
        private string _clamobilno;
        public string clamobilno
        {
            get { return _clamobilno; }
            set { _clamobilno = value; }
        }

        //Lokal telefon-nr.  
        //Rekvirent og indkøber/Kontaktoplysninger: Lokal telefon-nr.  
        private string _clalocalno;
        public string clalocalno
        {
            get { return _clalocalno; }
            set { _clalocalno = value; }
        }

        //Titel  
        //Rekvirent og indkøber/Kontaktoplysninger: Titel  
        private string _clatitle;
        public string clatitle
        {
            get { return _clatitle; }
            set { _clatitle = value; }
        }

        //Lokale-nr.  
        //Rekvirent og indkøber/Kontaktoplysninger: Lokale-nr.  
        private string _claroomno;
        public string claroomno
        {
            get { return _claroomno; }
            set { _claroomno = value; }
        }

    }

    public class spRekvirentListDB
    {
        private wsshost.Lists ls;

        public spRekvirentListDB() { }

        private void wsslogin(string url, string login, string password, string domain)
        {
            ls = new wsshost.Lists();
            ls.Url = url + @"/_vti_bin/lists.asmx";
            ls.PreAuthenticate = true;
            ls.Credentials = new System.Net.NetworkCredential(login, password, domain);
        }

        public List<RekvirentData> GetLists5()
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

            List<RekvirentData> _rows = GetDataList(items);
            return _rows;
        }

        private List<RekvirentData> GetDataList(XmlNode items)
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

            List<RekvirentData> _rows = new List<RekvirentData>();
            foreach (XmlNode datanode in nodelist)
            {
                RekvirentData _row = new RekvirentData();
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

        private List<RekvirentData> FillDataList(SPListItemCollection collListItems)
        {
            List<RekvirentData> _rows = new List<RekvirentData>();
            foreach (SPListItem oListItem in collListItems)
            {

                RekvirentData _row = new RekvirentData();
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

        public List<RekvirentData> GetLists()
        {
            SPWeb oWebsite = SPContext.Current.Web;
            SPList oList = oWebsite.Lists["Opgaver"];

            SPQuery oQuery = new SPQuery();
            oQuery.RowLimit = 10;
            int intIndex = 1;

            SPListItemCollection collListItems = oList.GetItems(oQuery);
            List<RekvirentData> _rows = FillDataList(collListItems);

            return _rows;
        }

        public List<RekvirentData> GetListsOld()
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
            List<RekvirentData> dt = new List<RekvirentData>();
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

                RekvirentData r = new RekvirentData();
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
