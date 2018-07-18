using System;
using Pbs3060;
using Uniconta.API.System;

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
            clsRSMembership2UniConta obj = new clsRSMembership2UniConta(p_dbData3060, p_dbPuls3060_dk, api);
            obj.Subscriber2Debtor();
        }
    }
}
