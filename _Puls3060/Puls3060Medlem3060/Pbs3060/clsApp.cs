using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace nsPbs3060
{
    public class clsApp
    {
        public static string UniContaUser { get { return PH(); } }
        public static string UniContaPW { get { return PH(); } }
        public static string UniContaCompanyId { get { return PH(); } }
        public static string GigaHostImapUser { get { return PH(); } }
        public static string GigaHostImapPW { get { return PH(); } }
        public static string dbPuls3060MedlemPW { get { return PH(); } }
        public static string puls3060_dkPW { get { return PH(); } }

        private static string PH()
        {
            try
            {
                StackTrace stackTrace = new StackTrace();
                string calledfrom = stackTrace.GetFrame(1).GetMethod().Name;
                if (calledfrom.ToLower().StartsWith("get_"))
                {
                    string key = calledfrom.Substring(4);
                    var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    var settings = ((AppSettingsSection)configFile.GetSection("PrivateAppSettings")).Settings;
                    if (settings[key] != null)
                        return settings[key].Value;
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    

        public static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = ((AppSettingsSection)configFile.GetSection("PrivateAppSettings")).Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }
    }
}
