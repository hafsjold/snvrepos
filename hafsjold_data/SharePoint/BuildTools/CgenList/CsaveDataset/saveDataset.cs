using System;
using System.Xml;
using System.Xml.Schema;
using wssGetList;
using System.Data;

namespace CsaveDataset
{
    class saveDataset
    {
        static void Main(string[] args)
        {
            wssGetList.getList myList = new wssGetList.getList("http://hd16.hafsjold.dk", "administrator", "m733", "hd16");
            System.Xml.Serialization.XmlSerializer ser;
            XmlTextWriter writer;

            System.Data.DataSet dsListItems = myList.getListData("ProPurList");
            dsListItems.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
            ser = new System.Xml.Serialization.XmlSerializer(dsListItems.GetType());
            writer = new XmlTextWriter("C:\\_Provinsa\\DATASET\\dsListItems.xml", System.Text.Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            ser.Serialize(writer, dsListItems);
            writer.Close();

            System.Data.DataSet dsListTypeItems = myList.getListData("ProPurListType");
            dsListTypeItems.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
            ser = new System.Xml.Serialization.XmlSerializer(dsListTypeItems.GetType());
            writer = new XmlTextWriter("C:\\_Provinsa\\DATASET\\dsListTypeItems.xml", System.Text.Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            ser.Serialize(writer, dsListTypeItems);
            writer.Close();

            System.Data.DataSet dsTypeItems = myList.getListData("ProPurType");
            dsTypeItems.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
            ser = new System.Xml.Serialization.XmlSerializer(dsTypeItems.GetType());
            writer = new XmlTextWriter("C:\\_Provinsa\\DATASET\\dsTypeItems.xml", System.Text.Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            ser.Serialize(writer, dsTypeItems);
            writer.Close();

            System.Data.DataSet dsTypeColumnItems = myList.getListData("ProPurTypeColumn");
            dsTypeColumnItems.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
            ser = new System.Xml.Serialization.XmlSerializer(dsTypeColumnItems.GetType());
            writer = new XmlTextWriter("C:\\_Provinsa\\DATASET\\dsTypeColumnItems.xml", System.Text.Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            ser.Serialize(writer, dsTypeColumnItems);
            writer.Close();

            System.Data.DataSet dsFieldItems = myList.getListData("ProPurColumn");
            dsFieldItems.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
            ser = new System.Xml.Serialization.XmlSerializer(dsFieldItems.GetType());
            writer = new XmlTextWriter("C:\\_Provinsa\\DATASET\\dsFieldItems.xml", System.Text.Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            ser.Serialize(writer, dsFieldItems);
            writer.Close();

        }
    }
}
