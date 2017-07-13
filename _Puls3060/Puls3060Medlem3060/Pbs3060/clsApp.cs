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
        public static string UniContaUser { get { return PH("UniContaUser"); } }
        public static string UniContaPW { get { return PH("UniContaPW"); } }
        public static string UniContaCompanyId { get { return PH("UniContaCompanyId"); } }
        public static string GigaHostImapUser { get { return PH("GigaHostImapUser"); } }
        public static string GigaHostImapPW { get { return PH("GigaHostImapPW"); } }
        public static string dbPuls3060MedlemPW { get { return PH("dbPuls3060MedlemPW"); } }
        public static string puls3060_dkUser { get { return PH("puls3060_dkUser"); } }
        public static string puls3060_dkPW { get { return PH("puls3060_dkPW"); } }

        private static string PH(string key)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = ((AppSettingsSection)configFile.GetSection("PrivateAppSettings")).Settings;
                if (settings[key] != null)
                    return settings[key].Value;
                else
                {
                    Program.Log(string.Format("Medlem3060Service clsApp() {0} NOT found", key));
                    return null;
                }
            }
            catch (Exception)
            {
                Program.Log(string.Format("Medlem3060Service clsApp() {0} NOT found", key));
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
