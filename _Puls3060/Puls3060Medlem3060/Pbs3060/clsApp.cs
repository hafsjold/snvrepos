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
        public static string dbPuls3060MedlemUser { get { return PH("dbPuls3060MedlemUser"); } }
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
                    Program.Log(string.Format("Medlem3060Service clsApp() key {0} NOT found", key));
                    return null;
                }
            }
            catch (Exception)
            {
                Program.Log(string.Format("Medlem3060Service clsApp() key {0} NOT found", key));
                return null;
            }
        }

       public static void AddUpdateAppSettings(clsAppData recAppData)
        {
            if (recAppData.bUniContaUser) AddUpdateAppSettings("UniContaUser", recAppData.UniContaUser);
            if (recAppData.bUniContaPW) AddUpdateAppSettings("UniContaPW", recAppData.UniContaPW);
            if (recAppData.bUniContaCompanyId) AddUpdateAppSettings("UniContaCompanyId", recAppData.UniContaCompanyId);
            if (recAppData.bGigaHostImapUser) AddUpdateAppSettings("GigaHostImapUser", recAppData.GigaHostImapUser);
            if (recAppData.bGigaHostImapPW) AddUpdateAppSettings("GigaHostImapPW", recAppData.GigaHostImapPW);
            if (recAppData.bdbPuls3060MedlemUser) AddUpdateAppSettings("dbPuls3060MedlemUser", recAppData.dbPuls3060MedlemUser);
            if (recAppData.bdbPuls3060MedlemPW) AddUpdateAppSettings("dbPuls3060MedlemPW", recAppData.dbPuls3060MedlemPW);
            if (recAppData.bpuls3060_dkUser) AddUpdateAppSettings("puls3060_dkUser", recAppData.puls3060_dkUser);
            if (recAppData.bpuls3060_dkPW) AddUpdateAppSettings("puls3060_dkPW", recAppData.puls3060_dkPW);

            if (recAppData.bEncryptApp) EncryptAppSettings(recAppData);
        }

        private static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = ((AppSettingsSection)configFile.GetSection("PrivateAppSettings")).Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                    Program.Log(string.Format("Medlem3060Service clsApp() key {0} Tilføjet", key));
                }
                else
                {
                    settings[key].Value = value;
                    Program.Log(string.Format("Medlem3060Service clsApp() key {0} Opdateret", key));
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Program.Log(string.Format("Medlem3060Service clsApp() key {0} IKKE Opdateret", key));
            }
        }

        private static void EncryptAppSettings(clsAppData recAppData)
        {
            ExeConfigurationFileMap configurationFileMap = new ExeConfigurationFileMap { ExeConfigFilename = "Medlem3060uc.exe.config" };
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configurationFileMap, ConfigurationUserLevel.None, true);
            ConfigurationSection sectionToEncrypt = config.GetSection("PrivateAppSettings");

            if (sectionToEncrypt == null)
            {
                Program.Log("Medlem3060Service clsApp() EncryptAppSettings Error - unable to find section to encrypt: PrivateAppSetting");
                return;
            }

            if (sectionToEncrypt.SectionInformation.IsProtected == false)
            {
                sectionToEncrypt.SectionInformation.ProtectSection("MyProvider");
                config.Save(ConfigurationSaveMode.Modified);
            }
        }
     }
}
