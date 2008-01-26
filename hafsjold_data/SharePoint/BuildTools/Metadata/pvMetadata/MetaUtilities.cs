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
                    FileName = Metadata.DATASET_PATH + @"dsListItems.xml";
                    break;
                case "ProPurListType":
                    FileName = Metadata.DATASET_PATH + @"dsListTypeItems.xml";
                    break;
                case "ProPurType":
                    FileName = Metadata.DATASET_PATH + @"dsTypeItems.xml";
                    break;
                case "ProPurTypeColumn":
                    FileName = Metadata.DATASET_PATH + @"dsTypeColumnItems.xml";
                    break;
                case "ProPurColumn":
                    FileName = Metadata.DATASET_PATH + @"dsFieldItems.xml";
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
        
        public static string GetCType(string SPType)
        {
            string CType = "unknown";
            switch (SPType)
            {
                case "Text":
                    CType = @"string";
                    break;
                case "Note":
                    CType = @"string";
                    break;
                case "Choice":
                    CType = @"?Choice";
                    break;
                case "Number":
                    CType = @"int";
                    break;
                case "Percentage":
                    CType = @"Decimal";
                    break;
                case "Currency":
                    CType = @"Decimal";
                    break;
                case "DateTime":
                    CType = @"DateTime";
                    break;
                case "DateOnly":
                    CType = @"DateTime";
                    break;
                case "Boolean":
                    CType = @"Boolean";
                    break;
                case "Picture":
                    CType = @"?Picture";
                    break;
                case "Hyperlink":
                    CType = @"?Hyperlink";
                    break;
                case "Counter":
                    CType = @"int";
                    break;
            }
           
            return CType;
        }
    }
}
