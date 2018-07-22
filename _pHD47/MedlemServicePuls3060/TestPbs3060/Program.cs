using System;
using System.Collections.Generic;
using System.Linq;
using Pbs3060;
using Uniconta.API.System;
using Uniconta.Common;

namespace TestPbs3060
{
    class Program
    {
        static void Main(string[] args)
        {
            Uniconta.ClientTools.Localization.SetLocalizationStrings(System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
            Uniconta.WindowsAPI.Startup.OnLoad();
            UCInitializer.InitUniconta();
            UCInitializer.UnicontaLogin();

            CrudAPI api = UCInitializer.GetBaseAPI;
            dbData3060DataContext p_dbData3060 = new dbData3060DataContext();
            puls3060_nyEntities p_dbPuls3060_dk = new puls3060_nyEntities();

            //clsRSMembership2UniConta obj = new clsRSMembership2UniConta(p_dbData3060, p_dbPuls3060_dk, api);
            //obj.Subscriber2Debtor();
            //obj.EngangsSletningAfDebitor();
            //obj.Subscriber2Medlem();
            //obj.Subscriber2Medlem();
            //clsPbs601 obj2 = new clsPbs601();
            //obj2.pending_rsform_indmeldelser(p_dbData3060, p_dbPuls3060_dk, api);

            /*
            var Nr = 20407;
            var critMedlem = new List<PropValuePair>();
            var pairMedlem = PropValuePair.GenereteWhereElements("KeyStr", typeof(String),Nr.ToString());
            critMedlem.Add(pairMedlem);
            var taskMedlem = api.Query<Medlem>(critMedlem);
            taskMedlem.Wait();
            var resultMedlem = taskMedlem.Result;
            var antalMedlem = resultMedlem.Count();
            */
            //string SQL = @"status == ""NytMedlem""";
            //var pairMedlem = PropValuePair.GenereteWhere(SQL);
            //var critMedlem = new List<PropValuePair>();
            //var pairMedlem = PropValuePair.GenereteWhereElements("status", typeof(String), "NytMedlem");
            //critMedlem.Add(pairMedlem);
            //var xdt = new DateTime(1, 1, 1, 0, 0, 1).ToString();
            //var ydt = DateTime.MinValue.ToString();
            //var pairMedlem = PropValuePair.GenereteWhereElements("sidstfaktureret", typeof(DateTime),ydt);
            //critMedlem.Add(pairMedlem);
            //var taskMedlem = api.Query<Medlem>(critMedlem);
            //taskMedlem.Wait();
            //var resultMedlem = taskMedlem.Result;
            //var antalMedlem = resultMedlem.Count();

            //**************************************
            /*
            List<Medlem> qry5List = null;
            var critMedlem = new List<PropValuePair>();
            var pairMedlem = PropValuePair.GenereteWhereElements("status", typeof(String), "NytMedlem");
            critMedlem.Add(pairMedlem);
            var taskMedlem = api.Query<Medlem>(critMedlem);
            taskMedlem.Wait();
            var resultMedlem = taskMedlem.Result;
            var antalMedlem = resultMedlem.Count();
            if (antalMedlem > 0)
            {
                qry5List = (from w in resultMedlem
                            where w.sidstfaktureret == DateTime.MinValue
                            && w.medlemtil == DateTime.MinValue
                            select w
                           ).ToList();
            }
            */
            clsUniconta objUniconta = new clsUniconta(p_dbData3060, api);
            objUniconta.BogforIndBetalinger();

            //clsHelp.konverterNytMedlem(p_dbData3060, api);
            //clsHelp.update_betlin(p_dbData3060);
        }
    }
}
