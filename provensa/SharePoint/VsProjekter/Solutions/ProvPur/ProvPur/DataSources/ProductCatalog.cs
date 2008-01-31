
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
    public class ProductCatalogData
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

        //Produktkatalog  
        //Produkrkatalog/Produktkatalog/-gruppe: Produktkatalog  
        private string _Procat;
        public string Procat
        {
            get { return _Procat; }
            set { _Procat = value; }
        }

        //Produktgruppe  
        //Produkrkatalog/Produktkatalog/-gruppe: Produktgruppe  
        private string _Progroup;
        public string Progroup
        {
            get { return _Progroup; }
            set { _Progroup = value; }
        }

        //Ajourføring er godkendt af  
        //Produkrkatalog/Produktoplysninger: Ajourføring er godkendt af  
        private string _proapproveupdateperson;
        public string proapproveupdateperson
        {
            get { return _proapproveupdateperson; }
            set { _proapproveupdateperson = value; }
        }

        //Ajourføringsdato  
        //Produkrkatalog/Produktoplysninger: Ajourføringsdato  
        private DateTime _proupdatedate;
        public DateTime proupdatedate
        {
            get { return _proupdatedate; }
            set { _proupdatedate = value; }
        }

        //Ajourført af  
        //Produkrkatalog/Produktoplysninger: Ajourført af  
        private string _proupdateperson;
        public string proupdateperson
        {
            get { return _proupdateperson; }
            set { _proupdateperson = value; }
        }

        //Besked til varemodtagelse om levering  
        //Produkrkatalog/Produktoplysninger: Besked til varemodtagelse om levering  
        private string _promessagegoodsrecieve;
        public string promessagegoodsrecieve
        {
            get { return _promessagegoodsrecieve; }
            set { _promessagegoodsrecieve = value; }
        }

        //Billed 1  
        //Indkøbskurv/Produktoplysninger:  Billed 1  
        private int _prodpicture1;
        public int prodpicture1
        {
            get { return _prodpicture1; }
            set { _prodpicture1 = value; }
        }

        //Billed 2  
        //Indkøbskurv/Produktoplysninger:  Billed 2  
        private int _prodpicture2;
        public int prodpicture2
        {
            get { return _prodpicture2; }
            set { _prodpicture2 = value; }
        }

        //Billed 3  
        //Indkøbskurv/Produktoplysninger:  Billed 3  
        private int _prodpicture3;
        public int prodpicture3
        {
            get { return _prodpicture3; }
            set { _prodpicture3 = value; }
        }

        //Billed 4  
        //Indkøbskurv/Produktoplysninger:  Billed 4  
        private int _prodpicture4;
        public int prodpicture4
        {
            get { return _prodpicture4; }
            set { _prodpicture4 = value; }
        }

        //Bruttopris (DKK excl. moms)  
        //Produkrkatalog/Produktoplysninger: Bruttopris (DKK excl. moms)  
        private string _prodgrossprice;
        public string prodgrossprice
        {
            get { return _prodgrossprice; }
            set { _prodgrossprice = value; }
        }

        //Enhedsbetegnelse  
        //Produkrkatalog/Produktoplysninger: Enhedsbetegnelse  
        private string _produnitdesc;
        public string produnitdesc
        {
            get { return _produnitdesc; }
            set { _produnitdesc = value; }
        }

        //Godkendelsesdato for ajourføring  
        //Produkrkatalog/Produktoplysninger: Godkendelsesdato for ajourføring  
        private DateTime _proapproveupdatedate;
        public DateTime proapproveupdatedate
        {
            get { return _proapproveupdatedate; }
            set { _proapproveupdatedate = value; }
        }

        //Godkendelsesflow  
        //Produkrkatalog/Produktoplysninger: Godkendelsesflow  
        private string _approveflow;
        public string approveflow
        {
            get { return _approveflow; }
            set { _approveflow = value; }
        }

        //Indkøbspraktik  
        //Produkrkatalog/Produktoplysninger: Indkøbspraktik  
        private string _prodpurpractice;
        public string prodpurpractice
        {
            get { return _prodpurpractice; }
            set { _prodpurpractice = value; }
        }

        //Leverandørnavn  
        //Produkrkatalog/Produktoplysninger: Leverandørnavn  
        private string _prodsupname;
        public string prodsupname
        {
            get { return _prodsupname; }
            set { _prodsupname = value; }
        }

        //Leveringstid  
        //Produkrkatalog/Produktoplysninger: Leveringstid  
        private string _proddeltime;
        public string proddeltime
        {
            get { return _proddeltime; }
            set { _proddeltime = value; }
        }

        //Læse / skriveadgang  
        //Produkrkatalog/Produktoplysninger: Læse / skriveadgang  
        private string _proreadwriter;
        public string proreadwriter
        {
            get { return _proreadwriter; }
            set { _proreadwriter = value; }
        }

        //Nettopris (DKK excl. moms)  
        //Produkrkatalog/Produktoplysninger: Nettopris (DKK excl. moms)  
        private string _prodnetprice;
        public string prodnetprice
        {
            get { return _prodnetprice; }
            set { _prodnetprice = value; }
        }

        //Produktbeskrivelse   
        //Produkrkatalog/Produktoplysninger: Produktbeskrivelse   
        private string _proddesc;
        public string proddesc
        {
            get { return _proddesc; }
            set { _proddesc = value; }
        }

        //Produktkategori/-gruppe  
        //Indkøbskurv/Produktoplysninger: Produktkategori/-gruppe  
        private string _prodcatgro;
        public string prodcatgro
        {
            get { return _prodcatgro; }
            set { _prodcatgro = value; }
        }

        //Produktnavn  
        //Produkrkatalog/Produktoplysninger: Produktnavn  
        private string _prodname;
        public string prodname
        {
            get { return _prodname; }
            set { _prodname = value; }
        }

        //Produktnavn - alias  
        //Produkrkatalog/Produktoplysninger: Produktnavn - alias  
        private string _prodalias;
        public string prodalias
        {
            get { return _prodalias; }
            set { _prodalias = value; }
        }

        //Rabat sats (pct.)  
        //Produkrkatalog/Produktoplysninger: Rabat sats (pct.)  
        private string _proddiscountpct;
        public string proddiscountpct
        {
            get { return _proddiscountpct; }
            set { _proddiscountpct = value; }
        }

        //Valgfri felt til bestillingsformular 1:  
        //Produkrkatalog/Produktoplysninger: Valgfri felt til bestillingsformular 1:  
        private string _prodoptfield1;
        public string prodoptfield1
        {
            get { return _prodoptfield1; }
            set { _prodoptfield1 = value; }
        }

        //Valgfri felt til bestillingsformular 2:  
        //Produkrkatalog/Produktoplysninger: Valgfri felt til bestillingsformular 2:  
        private string _prodoptfield2;
        public string prodoptfield2
        {
            get { return _prodoptfield2; }
            set { _prodoptfield2 = value; }
        }

        //Valgfri felt til bestillingsformular 3:  
        //Produkrkatalog/Produktoplysninger: Valgfri felt til bestillingsformular 3:  
        private string _prodoptfield3;
        public string prodoptfield3
        {
            get { return _prodoptfield3; }
            set { _prodoptfield3 = value; }
        }

        //Valgfri felt til bestillingsformular 4:  
        //Produkrkatalog/Produktoplysninger: Valgfri felt til bestillingsformular 4:  
        private string _prodoptfield4;
        public string prodoptfield4
        {
            get { return _prodoptfield4; }
            set { _prodoptfield4 = value; }
        }

        //Valgfri felt til bestillingsformular 5:  
        //Produkrkatalog/Produktoplysninger: Valgfri felt til bestillingsformular 5:  
        private string _prodoptfield5;
        public string prodoptfield5
        {
            get { return _prodoptfield5; }
            set { _prodoptfield5 = value; }
        }

        //Valgfri felt til bestillingsformular 6:  
        //Produkrkatalog/Produktoplysninger: Valgfri felt til bestillingsformular 6:  
        private string _prodoptfield6;
        public string prodoptfield6
        {
            get { return _prodoptfield6; }
            set { _prodoptfield6 = value; }
        }

        //Valgfri felt til bestillingsformular 7:  
        //Produkrkatalog/Produktoplysninger: Valgfri felt til bestillingsformular 7:  
        private string _prodoptfield7;
        public string prodoptfield7
        {
            get { return _prodoptfield7; }
            set { _prodoptfield7 = value; }
        }

        //Varenummer  
        //Produkrkatalog/Produktoplysninger: Varenummer  
        private string _proditemno;
        public string proditemno
        {
            get { return _proditemno; }
            set { _proditemno = value; }
        }

        //Valgfri felt til bestillingsformular xx:  
        //Produkrkatalog/Produktoplysninger: Valgfri felt til bestillingsformular xx:  
        private string _prodoptfieldxx;
        public string prodoptfieldxx
        {
            get { return _prodoptfieldxx; }
            set { _prodoptfieldxx = value; }
        }

    }

    public class spProductCatalogListDB
    {
        private ProvPur.wsshost.Lists ls;

        public spProductCatalogListDB() { }

        private void wsslogin(string url, string login, string password, string domain)
        {
            ls = new ProvPur.wsshost.Lists();
            ls.Url = url + @"/_vti_bin/lists.asmx";
            ls.PreAuthenticate = true;
            ls.Credentials = new System.Net.NetworkCredential(login, password, domain);
        }

        public List<ProductCatalogData> GetLists5()
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

            List<ProductCatalogData> _rows = GetDataList(items);
            return _rows;
        }

        private List<ProductCatalogData> GetDataList(XmlNode items)
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

            List<ProductCatalogData> _rows = new List<ProductCatalogData>();
            foreach (XmlNode datanode in nodelist)
            {
                ProductCatalogData _row = new ProductCatalogData();
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

        private List<ProductCatalogData> FillDataList(SPListItemCollection collListItems)
        {
            List<ProductCatalogData> _rows = new List<ProductCatalogData>();
            foreach (SPListItem oListItem in collListItems)
            {

                ProductCatalogData _row = new ProductCatalogData();
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

        public List<ProductCatalogData> GetLists()
        {
            SPWeb oWebsite = SPContext.Current.Web;
            SPList oList = oWebsite.Lists["Opgaver"];

            SPQuery oQuery = new SPQuery();
            oQuery.RowLimit = 10;
            int intIndex = 1;

            SPListItemCollection collListItems = oList.GetItems(oQuery);
            List<ProductCatalogData> _rows = FillDataList(collListItems);

            return _rows;
        }

        public List<ProductCatalogData> GetListsOld()
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
            List<ProductCatalogData> dt = new List<ProductCatalogData>();
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

                ProductCatalogData r = new ProductCatalogData();
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
