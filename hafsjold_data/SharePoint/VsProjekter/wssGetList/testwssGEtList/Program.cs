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
            wssGetList.getList myList = new wssGetList.getList("http://localhost", "administrator", "m733", "hd19");
            DataSet dsItems = myList.getListData("TestListe");
        }
    }
}
