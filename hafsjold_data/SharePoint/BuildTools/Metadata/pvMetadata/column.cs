using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace pvMetadata
{
    public class columnCollection
    {
        private static Dictionary<int, column> _columns;
        private static Dictionary<string, int> _columns_name;
        private static Dictionary<Guid, int> _columns_guid;

        public columnCollection()
        {
            _columns = null;
        }

        //Get All Columns
        public Dictionary<int, column> getAllColumns
        {
            get
            {
                if (_columns == null) init();
                return _columns;
            }
        }

        //Get a single Column on id
        public column getColumn(int Pid)
        {
            if (_columns == null) init();
            return _columns[Pid];
        }

        private void init()
        {
            DataTable tbl = pvMetadata.MetaUtilities.OpenDataSet("ProPurColumn").Tables["row"];
            _columns = new Dictionary<int, column>();
            _columns_name = new Dictionary<string, int>();
            _columns_guid = new Dictionary<Guid, int>();
            foreach (DataRow row in tbl.Rows)
            {
                column val = new column(row);
                int key = val.id;
                if ((!_columns_name.ContainsKey(val.SysName)) && (!_columns_guid.ContainsKey(new Guid(val.colGUID))))
                {
                    _columns.Add(key, val);
                    _columns_name.Add(val.SysName, key);
                    _columns_guid.Add(new Guid(val.colGUID), key);
                }
                else
                {
                    string dupkey = "DUP";
                }
            }
        }

    }

    public class column
    {
        private int _id;
        private string _SysName;
        private string _KolonneType;
        private string _DisplayNameDK;
        private string _DisplayNameUK;
        private string _Comment;
        private string _colGUID;
        private Boolean _SysCol;


        private column() { }

        public column(DataRow row)
        {
            _id = int.Parse((string)row["ows_ID"]);
            _SysName = (string)row["ows_Title"];
            _KolonneType = (string)row["ows_KolonneType"];
            _DisplayNameDK = (string)row["ows_DisplayNameDK"];
            _DisplayNameUK = (string)row["ows_DisplayNameUK"];
            _Comment = (string)row["ows_FieldName"];
            _colGUID = (string)row["ows_GUID0"];
            try
            {
                if ((string)row["ows_SysCol"] == "1")
                {
                    _SysCol = true;
                }
                else _SysCol = false;
            }
            catch
            {
                _SysCol = false;
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

        public string KolonneType
        {
            get { return _KolonneType; }
            set { _KolonneType = value; }
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

        public string colGUID
        {
            get { return _colGUID; }
            set { _colGUID = value; }
        }

        public Boolean SysCol
        {
            get { return _SysCol; }
            set { _SysCol = value; }
        }

        public string CType
        {
            get { return MetaUtilities.GetCType(_KolonneType); }
        }

    }
}
