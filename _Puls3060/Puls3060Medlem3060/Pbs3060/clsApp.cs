using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nsPbs3060
{
    public class clsApp
    {
        public static string UniContaUser { get { return PropHelper("UniContaUser");}}
        public static string UniContaPW { get { return PropHelper("UniContaPW"); } }
        public static string UniContaCompanyId { get { return PropHelper("UniContaCompanyId"); } }

        private static string PropHelper(string key)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = ((AppSettingsSection)configFile.GetSection("PrivateAppSettings")).Settings;
            if (settings[key] != null)
                return settings[key].Value;
            else
                return null;
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
