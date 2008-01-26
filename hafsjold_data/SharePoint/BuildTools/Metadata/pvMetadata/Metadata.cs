using System;
using System.Collections.Generic;
using System.Text;

namespace pvMetadata
{
    public class Metadata
    {
        private listtemplateCollection _listtemplates;
        private ListtemplateContenttypeCollection _ListtemplateContenttypes;
        private contenttypeCollection _contenttypes;
        private ContenttypeColumnCollection _ContenttypeColumns;
        private columnCollection _columns;
        private ListtemplateColumnCollection _ListtemplateColumns;

        public Metadata() {
            _columns = new columnCollection();
            _contenttypes = new contenttypeCollection();
            _listtemplates = new listtemplateCollection();
            _ContenttypeColumns = new ContenttypeColumnCollection();
            _ListtemplateContenttypes = new ListtemplateContenttypeCollection();
            _ListtemplateColumns = new ListtemplateColumnCollection();
        }

        public System.Collections.Generic.SortedDictionary<string, ListtemplateColumn> ListtemplateColumnsSort(string ListName)
        {
            System.Collections.Generic.SortedDictionary<string, ListtemplateColumn> scol = new System.Collections.Generic.SortedDictionary<string, ListtemplateColumn>();
            listtemplate list = this.listtemplates.getListtemplate(ListName);
            if (list != null)
            {
                foreach (ListtemplateColumn col in list.ListtemplateColumns)
                {
                    scol.Add(col.Seqnr, col);
                }
            }
            return scol;
        }

        public listtemplateCollection listtemplates
        {
            get { return _listtemplates; }
            set { _listtemplates = value; }
        }

        public ListtemplateContenttypeCollection ListtemplateContenttypes
        {
            get { return _ListtemplateContenttypes; }
            set { _ListtemplateContenttypes = value; }
        }

        public contenttypeCollection contenttypes
        {
            get { return _contenttypes; }
            set { _contenttypes = value; }
        }


        public ContenttypeColumnCollection ContenttypeColumns
        {
            get { return _ContenttypeColumns; }
            set { _ContenttypeColumns = value; }
        }

        public columnCollection columns
        {
            get { return _columns; }
            set { _columns = value; }
        }

        public ListtemplateColumnCollection ListtemplateColumns
        {
            get { return _ListtemplateColumns; }
            set { _ListtemplateColumns = value; }
        }

	
    }
}
