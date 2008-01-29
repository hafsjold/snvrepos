using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace pvMetadata
{
    public class contenttypeCollection
    {
        private static Dictionary<int, contenttype> _contenttypes;
        private static Dictionary<string, int> _contenttypes_name;

        public contenttypeCollection()
        {
            _contenttypes = null;
        }

        //Get All ContentTypes
        public Dictionary<int, contenttype> getAllContenttypes
        {
            get
            {
                if (_contenttypes == null) init();
                return _contenttypes;
            }
        }

        //Get a single ContentTypes on id
        public contenttype getContenttype(int Pid)
        {
            if (_contenttypes == null) init();
            return _contenttypes[Pid];
        }

        
        private void init()
        {
            DataTable tbl = pvMetadata.MetaUtilities.OpenDataSet("ProPurType").Tables["row"];
            _contenttypes = new Dictionary<int, contenttype>();
            _contenttypes_name = new Dictionary<string, int>();
            foreach (DataRow row in tbl.Rows)
            {
                contenttype val = new contenttype(row);
                int key = val.id;
                if (!_contenttypes_name.ContainsKey(val.SysName))
                {
                    _contenttypes.Add(key, val);
                    _contenttypes_name.Add(val.SysName, key);
                }
            }
        }
    }

    public class contenttype
    {
        private Dictionary<int, ContenttypeColumn> _ContenttypeColumns;
        private int _id;
        private string _SysName;
        private string _BasedOn;
        private string _DisplayNameDK;
        private string _DisplayNameUK;
        private string _Comment;
        private string _typeGUID;
        private Boolean _SysType;

        private contenttype() { }

        public contenttype(DataRow row)
        {
            _ContenttypeColumns = null;
            _id = int.Parse((string)row["ows_ID"]);
            _SysName = (string)row["ows_Title"];
            _BasedOn = (string)row["ows_BasedOn"];
            _DisplayNameDK = (string)row["ows_DisplayNameDK"];
            _DisplayNameUK = (string)row["ows_DisplayNameUK"];
            _Comment = (string)row["ows_Comment"];
            _typeGUID = (string)row["ows_Type_GUID"];
            try
            {
                _SysType = (Boolean)row["ows_SysType"];
            }
            catch
            {
                _SysType = false;
            }
        }

        private void init()
        {
            ContenttypeColumnCollection MASTERtypecolumns = new ContenttypeColumnCollection();
            _ContenttypeColumns = new Dictionary<int, ContenttypeColumn>();

            foreach (ContenttypeColumn MASTERtypecolumn in MASTERtypecolumns.getAllContenttypeColumns.Values)
            {
                if (MASTERtypecolumn.TypeName_id == _id)
                {
                    _ContenttypeColumns.Add(MASTERtypecolumn.Kolonne_id, MASTERtypecolumn);
                }
            }
        }

        //Get Columns for this ContentType
        public Dictionary<int, ContenttypeColumn> ContenttypeColumns
        {
            get
            {
                if (_ContenttypeColumns == null) init();
                return _ContenttypeColumns;
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

        public string BasedOn
        {
            get { return _BasedOn; }
            set { _BasedOn = value; }
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

        public string typeGUID
        {
            get { return _typeGUID; }
            set { _typeGUID = value; }
        }

        public Boolean SysType
        {
            get { return _SysType; }
            set { _SysType = value; }
        }

    }
}
