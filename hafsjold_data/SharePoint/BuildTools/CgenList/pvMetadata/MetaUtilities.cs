using System;
using System.Diagnostics;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using Microsoft.SharePoint;
using System.IO;
using System.Xml.Serialization;


namespace pvMetadata
{
    public class MetaUtilities
    {

        public  static System.Data.DataSet OpenDataSet(string ListName)
        {
            System.Xml.Serialization.XmlSerializer ser = null;
            FileStream reader;
            string FileName = null;

            switch (ListName)
            {
                case "ProPurList":
                    FileName = @"C:\_Provinsa\DATASET\dsListItems.xml";
                    break;
                case "ProPurListType":
                    FileName = @"C:\_Provinsa\DATASET\dsListTypeItems.xml";
                    break;
                case "ProPurType":
                    FileName = @"C:\_Provinsa\DATASET\dsTypeItems.xml";
                    break;
                case "ProPurTypeColumn":
                    FileName = @"C:\_Provinsa\DATASET\dsTypeColumnItems.xml";
                    break;
                case "ProPurColumn":
                    FileName = @"C:\_Provinsa\DATASET\dsFieldItems.xml";
                    break;

            }
            System.Data.DataSet ds = new System.Data.DataSet();
            ser = new System.Xml.Serialization.XmlSerializer(ds.GetType());
            using (reader = new FileStream(FileName, FileMode.Open))
            {
                ds = (System.Data.DataSet)ser.Deserialize(reader);
                reader.Close();
            }
            return ds;
        }

        public static DataRowCollection OpenDataRows(string ListName)
        {
            DataTable tbl = OpenDataSet(ListName).Tables["row"];
            return tbl.Rows;
            
        }

    }
}
