using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace pvMetadata
{
    public class listtemplateCollection
    {
        private static Dictionary<int, listtemplate> _listtemplates;
        private static Dictionary<string, int> _listtemplates_name;

        public listtemplateCollection()
        {
            _listtemplates = null;
        }

        //Get All ListTemplates
        public Dictionary<int, listtemplate> getAllListtemplates
        {
            get
            {
                if (_listtemplates == null) init();
                return _listtemplates;
            }
        }

        //Get ListTemplate by name
        public listtemplate getListtemplate(string templateName)
        {
            if (_listtemplates == null) init();
            if (_listtemplates_name.ContainsKey(templateName))
            {
                int key = _listtemplates_name[templateName];
                return _listtemplates[key];
            }
            return null;
        }

        private void init()
        {
            DataTable tbl = pvMetadata.MetaUtilities.OpenDataSet("ProPurList").Tables["row"];
            _listtemplates = new Dictionary<int, listtemplate>();
            _listtemplates_name = new Dictionary<string, int>();
            foreach (DataRow row in tbl.Rows)
            {
                listtemplate val = new listtemplate(row);
                int key = val.id;
                if (!_listtemplates_name.ContainsKey(val.SysName))
                {
                    _listtemplates.Add(key, val);
                    _listtemplates_name.Add(val.SysName, key);
                }
            }
        }
    }

    public class listtemplate
    {
        private List<ListtemplateContenttype> _ListtemplateContenttypes;
        private List<ListtemplateColumn> _ListtemplateColumns;
        private int _id;
        private string _SysName;
        private string _DisplayNameDK;
        private string _DisplayNameUK;
        private string _Comment;
        private string _FeatureGUID;
        private string _TypeIdentifier;

        private listtemplate() { }


        public listtemplate(DataRow row)
        {
            _ListtemplateContenttypes = null;
            _ListtemplateColumns = null;
            _id = int.Parse((string)row["ows_ID"]);
            _SysName = (string)row["ows_Title"];
            _DisplayNameDK = (string)row["ows_DisplayNameDK"];
            _DisplayNameUK = (string)row["ows_DisplayNameUK"];
            _Comment = (string)row["ows_Comment"];
            _FeatureGUID = (string)row["ows_FeatureGUID"];
            _TypeIdentifier = (string)row["ows_TypeIdentifier"];
        }

        private void inittype()
        {
            ListtemplateContenttypeCollection MASTERlisttypes = new ListtemplateContenttypeCollection();
            _ListtemplateContenttypes = new List<ListtemplateContenttype>();

            foreach (ListtemplateContenttype MASTERlisttype in MASTERlisttypes.getAllListtemplateContenttypes.Values)
            {
                if (MASTERlisttype.ListNavn_id == _id)
                {
                    _ListtemplateContenttypes.Add(MASTERlisttype);
                }
            }
        }

        //Get Contenttypes for this ListTemplate
        public List<ListtemplateContenttype> ListtemplateContenttypes
        {
            get
            {
                if (_ListtemplateContenttypes == null) inittype();
                return _ListtemplateContenttypes;
            }
        }


        private void initcolum()
        {
            ListtemplateColumnCollection MASTERlistcolumns = new ListtemplateColumnCollection();
            _ListtemplateColumns = new List<ListtemplateColumn>();

            foreach (ListtemplateColumn MASTERlistcolumn in MASTERlistcolumns.getAllListtemplateColumns)
            {
                if (MASTERlistcolumn.ListNavn_id == _id)
                {
                    _ListtemplateColumns.Add(MASTERlistcolumn);
                }
            }
        }

        //Get Columns for this ListTemplate
        public List<ListtemplateColumn> ListtemplateColumns
        {
            get
            {
                if (_ListtemplateColumns == null) initcolum();
                return _ListtemplateColumns;
            }
        }

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string SysName
        {
            get { return _SysName; }
            set { _SysName = value; }
        }

        public string DisplayNameDK
        {
            get { return _DisplayNameDK; }
            set { _DisplayNameDK = value; }
        }

        public string DisplayNameUK
        {
            get { return _DisplayNameUK; }
            set { _DisplayNameUK = value; }
        }

        public string Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }

        public string FeatureGUID
        {
            get { return _FeatureGUID; }
            set { _FeatureGUID = value; }
        }

        public string TypeIdentifier
        {
            get { return _TypeIdentifier; }
            set { _TypeIdentifier = value; }
        }

    }
}
