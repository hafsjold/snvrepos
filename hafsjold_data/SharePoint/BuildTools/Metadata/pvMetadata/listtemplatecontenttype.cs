using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;

namespace pvMetadata
{
    public class ListtemplateContenttypeCollection
    {
        private static Dictionary<int, ListtemplateContenttype> _ListtemplateContenttypes;

        public ListtemplateContenttypeCollection()
        {
            _ListtemplateContenttypes = null;
        }

        //Get All ListtemplateContenttypes
        public Dictionary<int, ListtemplateContenttype> getAllListtemplateContenttypes
        {
            get
            {
                if (_ListtemplateContenttypes == null) init();
                return _ListtemplateContenttypes;
            }
        }

        private void init()
        {
            DataTable tbl = pvMetadata.MetaUtilities.OpenDataSet("ProPurListType").Tables["row"];
            _ListtemplateContenttypes = new Dictionary<int, ListtemplateContenttype>();
            foreach (DataRow row in tbl.Rows)
            {
                ListtemplateContenttype val = new ListtemplateContenttype(row);
                int key = val.ListNavn_id * 10000 + val.TypeNavn_id;
                _ListtemplateContenttypes.Add(key, val);
            }
        }
    }

    public class ListtemplateContenttype
    {
        private contenttype _contenttype;
        private int _id;
        private string _Seqnr;
        private int _ListNavn_id;
        private int _TypeNavn_id;

        private ListtemplateContenttype() { }


        public ListtemplateContenttype(DataRow row)
        {
            _contenttype = null;
            _id = int.Parse((string)row["ows_ID"]);
            _Seqnr = (string)row["ows_Title"];
            string[] ID_ListNavn = Regex.Split((string)row["ows_ListName"], ";#");
            _ListNavn_id = int.Parse((string)ID_ListNavn[0]);
            string[] ID_TypeName = Regex.Split((string)row["ows_TypeNavn"], ";#");
            _TypeNavn_id = int.Parse((string)ID_TypeName[0]);
        }

        private void init_contenttype()
        {
            contenttypeCollection wcontenttypeCollection = new contenttypeCollection();
            _contenttype = wcontenttypeCollection.getContenttype(_TypeNavn_id);
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

        public string SysName
        {
            get
            {
                if (_contenttype == null) init_contenttype();
                return _contenttype.SysName;
            }
        }

        public string BasedOn
        {
            get
            {
                if (_contenttype == null) init_contenttype();
                return _contenttype.BasedOn;
            }
        }

        public string DisplayNameDK
        {
            get
            {
                if (_contenttype == null) init_contenttype();
                return _contenttype.DisplayNameDK;
            }
        }

        public string DisplayNameUK
        {
            get
            {
                if (_contenttype == null) init_contenttype();
                return _contenttype.DisplayNameUK;
            }
        }

        public string Comment
        {
            get
            {
                if (_contenttype == null) init_contenttype();
                return _contenttype.Comment;
            }
        }

        public string typeGUID
        {
            get
            {
                if (_contenttype == null) init_contenttype();
                return _contenttype.typeGUID;
            }
        }

        public Boolean SysType
        {
            get
            {
                if (_contenttype == null) init_contenttype();
                return _contenttype.SysType;
            }
        }
    }
}
