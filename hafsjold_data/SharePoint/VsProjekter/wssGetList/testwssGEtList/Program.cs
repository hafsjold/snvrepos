using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;


namespace testwssGEtList
{
    class Program
    {
        static void Main(string[] args)
        {
            wssGetList.getList myList = new wssGetList.getList("http://hd16.hafsjold.dk", "administrator", "m733", "hd16");
            DataSet dsItems1 = myList.getListData("ProPurType");
            DataSet dsItems2 = myList.getListData("ProPurList");
        }
    }
}
