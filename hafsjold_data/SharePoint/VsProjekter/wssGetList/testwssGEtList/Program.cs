using System;
using System.Collections.Generic;
using System.Text;

namespace testwssGEtList
{
    class Program
    {
        static void Main(string[] args)
        {
            wssGetList.getList myList = new wssGetList.getList("http://localhost", "administrator", "m733", "hd19");
            myList.test("TestListe");
        }
    }
}
