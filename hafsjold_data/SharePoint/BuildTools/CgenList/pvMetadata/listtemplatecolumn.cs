using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;

namespace pvMetadata
{
    public class ListtemplateColumnCollection
    {
        private static List<ListtemplateColumn> _ListtemplateColumns;

        public ListtemplateColumnCollection()
        {
            _ListtemplateColumns = null;
        }

        //Get All ListtemplateColumns
        public List<ListtemplateColumn> getAllListtemplateColumns
        {
            get
            {
                if (_ListtemplateColumns == null) init();
                return _ListtemplateColumns;
            }
        }

        private void init()
        {
            _ListtemplateColumns = new List<ListtemplateColumn>();
            listtemplateCollection wlisttemplates = new listtemplateCollection();
            foreach (listtemplate wlisttemplate in wlisttemplates.getAllListtemplates.Values)
            {
                foreach (ListtemplateContenttype wListtemplateContenttype in wlisttemplate.ListtemplateContenttypes)
                {
                    contenttypeCollection wcontenttypes = new contenttypeCollection();
                    contenttype wcontenttype = wcontenttypes.getContenttype(wListtemplateContenttype.TypeNavn_id);
                    foreach (ContenttypeColumn wContenttypeColumn in wcontenttype.ContenttypeColumns.Values)
                    {
                        ListtemplateColumn wListtemplateColumn = new ListtemplateColumn(wlisttemplate.id, wContenttypeColumn);
                        _ListtemplateColumns.Add(wListtemplateColumn);
                    }
                }
            }
        }
    }

    public class ListtemplateColumn
    {
        private static Dictionary<int, ListtemplateColumn> _ListtemplateColumns;
        private ContenttypeColumn _col;
        private int _Kolonne_id;
        private int _ListNavn_id;
        private int _TypeNavn_id;

        private ListtemplateColumn() { }

        public ListtemplateColumn(int PListNavn_id, ContenttypeColumn Pcol)
        {
            _ListtemplateColumns = null;
            _col = null;
            _ListNavn_id = PListNavn_id;
            _TypeNavn_id = Pcol.TypeName_id;
            _Kolonne_id = Pcol.Kolonne_id;
        }

        private void init_columns()
        {
            ListtemplateColumnCollection MASTERlistcolumns = new ListtemplateColumnCollection();
            _ListtemplateColumns = new Dictionary<int, ListtemplateColumn>();

            foreach (ListtemplateColumn MASTERlistcolumn in MASTERlistcolumns.getAllListtemplateColumns)
            {
                if (MASTERlistcolumn.ListNavn_id == _ListNavn_id)
                {
                    if (!_ListtemplateColumns.ContainsKey(MASTERlistcolumn.Kolonne_id))
                    {
                        _ListtemplateColumns.Add(MASTERlistcolumn.Kolonne_id, MASTERlistcolumn);
                    }
                }
            }
        }

        //Get Columns for this Listtemplate
        public Dictionary<int, ListtemplateColumn> ListtemplateColumns
        {
            get
            {
                if (_ListtemplateColumns == null) init_columns();
                return _ListtemplateColumns;
            }
        }



        private void init()
        {
            ContenttypeColumnCollection wContenttypeColumnCollection = new ContenttypeColumnCollection();
            int key = _TypeNavn_id * 10000 + _Kolonne_id;
            ContenttypeColumn _col = wContenttypeColumnCollection.getAllContenttypeColumns[key];
        }

        public int Kolonne_id
        {
            get { return _Kolonne_id; }
            set { _Kolonne_id = value; }
        }

        public int ListNavn_id
        {
            get { return _ListNavn_id; }
            set { _ListNavn_id = value; }
        }

        public int TypeNavn_id
        {
            get { return _TypeNavn_id; }
            set { _TypeNavn_id = value; }
        }

        public string Seqnr
        {
            get
            {
                if (_col == null) init();
                return _col.Seqnr;
            }
        }

        public string DisplayNameDK
        {
            get
            {
                if (_col == null) init();
                return _col.DisplayNameDK;
            }
        }

        public string DisplayNameUK
        {
            get
            {
                if (_col == null) init();
                return _col.DisplayNameUK;
            }
        }

        public Boolean SkalUdfyldes
        {
            get
            {
                if (_col == null) init();
                return _col.SkalUdfyldes;
            }
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
    }
}

