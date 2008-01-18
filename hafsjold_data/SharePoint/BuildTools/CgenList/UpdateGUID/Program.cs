using System;
using System.Xml;
using System.Xml.Schema;
using wssGetList;
using System.Data;

namespace UpdateGUID
{
    class Program
    {
        static void Main(string[] args)
        {
            wssGetList.getList myList = new wssGetList.getList("http://hd16.hafsjold.dk", "administrator", "m733", "hd16");
            string listName = myList.getListName("ProPurColumn");
            System.Data.DataSet dsFieldItems = myList.getListData("ProPurColumn");
            DataTable Field_rows = dsFieldItems.Tables["row"];

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            string xmlString = "<Batch OnError='Continue' ListVersion='1'>";
            foreach (DataRow Column_row in Field_rows.Rows)
            {
                string ows_ID = (string)Column_row["ows_ID"];
                string TestOpdat;
                try
                {
                    TestOpdat = (string)Column_row["ows_TestOpdat"];
                }
                catch
                {
                    TestOpdat = null;
                }
                if (TestOpdat == "1234")
                {
                    //update
                    xmlString += "<Method ID='" + ows_ID + "' Cmd='Update'>";
                    xmlString += "<Field Name='ID'>" + ows_ID + "</Field>";
                    xmlString += "<Field Name='TestOpdat'>Andreas Hafsjold</Field>";
                    xmlString += "</Method>";
                }
                else
                {
                    // do not update
                }
            }
            xmlString += "</Batch>";
            doc.LoadXml(xmlString);
            System.Xml.XmlNode myitems = doc.SelectSingleNode("//Batch");
            System.Xml.XmlNode myresult = myList.updateListData(listName, myitems);
        }


    }
}
