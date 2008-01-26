using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;

namespace pvMetadata
{
    public class ContenttypeColumnCollection
    {
        private static Dictionary<int, ContenttypeColumn> _ContenttypeColumns;

        public ContenttypeColumnCollection()
        {
            _ContenttypeColumns = null;
        }

        //Get All ContenttypeColumns
        public Dictionary<int, ContenttypeColumn> getAllContenttypeColumns
        {
            get
            {
                if (_ContenttypeColumns == null) init();

                return _ContenttypeColumns;
            }
        }

        private void init()
        {
            DataTable tbl = pvMetadata.MetaUtilities.OpenDataSet("ProPurTypeColumn").Tables["row"];
            _ContenttypeColumns = new Dictionary<int, ContenttypeColumn>();
            foreach (DataRow row in tbl.Rows)
            {
                ContenttypeColumn val = new ContenttypeColumn(row);
                int key = val.TypeName_id * 10000 + val.Kolonne_id;
                _ContenttypeColumns.Add(key, val);
            }
        }
    }

    public class ContenttypeColumn
    {
        private column _col;
        private int _id;
        private string _Seqnr;
        private int _TypeName_id;
        private int _Kolonne_id;
        private string _DisplayNameDK;
        private string _DisplayNameUK;
        private Boolean _SkalUdfyldes;

        private ContenttypeColumn() { }


        public ContenttypeColumn(DataRow row)
        {
            _col = null;
            _id = int.Parse((string)row["ows_ID"]);
            _Seqnr = (string)row["ows_Title"];
            string[] ID_TypeName = Regex.Split((string)row["ows_TypeName"], ";#");
            _TypeName_id = int.Parse((string)ID_TypeName[0]);
            string[] ID_Kolonne = Regex.Split((string)row["ows_Kolonne"], ";#");
            _Kolonne_id = int.Parse((string)ID_Kolonne[0]);
            try { _DisplayNameDK = (string)row["ows_DisplayNameDK"]; }
            catch { _DisplayNameDK = null; }
            try { _DisplayNameUK = (string)row["ows_DisplayNameUK"]; }
            catch { _DisplayNameUK = null; }
            try { _SkalUdfyldes = (Boolean)row["ows_SkalUdfyldes"]; }
            catch { _SkalUdfyldes = false; }
        }
        
        private void init(){
            columnCollection cols = new columnCollection();
            _col = cols.getColumn(Kolonne_id);
        }
        
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Seqnr
        {
            get { return _Seqnr; }
            set { _Seqnr = value; }
        }

        public string DisplayNameDK
        {
            get
            {
                if (_DisplayNameDK == null)
                {
                    if (_col == null) init();
                    return _col.DisplayNameDK;
                }
                else
                {
                    return _DisplayNameDK;
                }
            }

            set { _DisplayNameDK = value; }
        }

        public string DisplayNameUK
        {
            get
            {
                if (_DisplayNameUK == null)
                {
                    if (_col == null) init();
                    return _col.DisplayNameUK;
                }
                else
                {
                    return _DisplayNameUK;
                }
            }
            set { _DisplayNameUK = value; }
        }

        public int TypeName_id
        {
            get { return _TypeName_id; }
            set { _TypeName_id = value; }
        }

        public int Kolonne_id
        {
            get { return _Kolonne_id; }
            set { _Kolonne_id = value; }
        }

        public Boolean SkalUdfyldes
        {
            get { return _SkalUdfyldes; }
            set { _SkalUdfyldes = value; }
        }

        public string SysName
        {
            get
            {
                if (_col == null) init();
                return _col.SysName;
            }
        }

        public string KolonneType
        {
            get
            {
                if (_col == null) init();
                return _col.KolonneType;
            }
        }
        public string Comment
        {
            get
            {
                if (_col == null) init();
                return _col.Comment;
            }
        }

        public string colGUID
        {
            get
            {
                if (_col == null) init();
                return _col.colGUID;
            }
        }

        public Boolean SysCol
        {
            get
            {
                if (_col == null) init();
                return _col.SysCol;
            }
        }

        public string CType
        {
            get
            {
                if (_col == null) init();
                return _col.CType;
            }
        }

    }
}

