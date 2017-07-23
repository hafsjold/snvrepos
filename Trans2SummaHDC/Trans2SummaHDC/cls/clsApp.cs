using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Trans2SummaHDC
{
    public class clsApp
    {
        public static string UniContaUser { get { return PH("UniContaUser"); } }
        public static string UniContaPW { get { return PH("UniContaPW"); } }
        public static string UniContaCompanyId { get { return PH("UniContaCompanyId"); } }
        public static string ImapUser { get { return PH("ImapUser"); } }
        public static string ImapPW { get { return PH("ImapPW"); } }
        public static string puls3060_dkUser { get { return PH("puls3060_dkUser"); } }
        public static string puls3060_dkPW { get { return PH("puls3060_dkPW"); } }

        private static string PH(string key)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = ((AppSettingsSection)configFile.GetSection("PrivateHDCAppSettings")).Settings;
                if (settings[key] != null)
                    return settings[key].Value;
                else
                {
                    Program.Log(string.Format("Trans2SummaHDC clsApp() key {0} NOT found", key));
                    return null;
                }
            }
            catch (Exception)
            {
                Program.Log(string.Format("Trans2SummaHDC clsApp() key {0} NOT found", key));
                return null;
            }
        }

       public static void AddUpdateAppSettings(clsAppData recAppData)
        {
            if (recAppData.bUniContaUser) AddUpdateAppSettings("UniContaUser", recAppData.UniContaUser);
            if (recAppData.bUniContaPW) AddUpdateAppSettings("UniContaPW", recAppData.UniContaPW);
            if (recAppData.bUniContaCompanyId) AddUpdateAppSettings("UniContaCompanyId", recAppData.UniContaCompanyId);
            if (recAppData.bImapUser) AddUpdateAppSettings("ImapUser", recAppData.ImapUser);
            if (recAppData.bImapPW) AddUpdateAppSettings("ImapPW", recAppData.ImapPW);
            if (recAppData.bpuls3060_dkUser) AddUpdateAppSettings("puls3060_dkUser", recAppData.puls3060_dkUser);
            if (recAppData.bpuls3060_dkPW) AddUpdateAppSettings("puls3060_dkPW", recAppData.puls3060_dkPW);

            if (recAppData.bEncryptApp) EncryptAppSettings(recAppData);
        }

        private static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = ((AppSettingsSection)configFile.GetSection("PrivateHDCAppSettings")).Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                    Program.Log(string.Format("Trans2SummaHDC clsApp() key {0} Tilføjet", key));
                }
                else
                {
                    settings[key].Value = value;
                    Program.Log(string.Format("Trans2SummaHDC clsApp() key {0} Opdateret", key));
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Program.Log(string.Format("Trans2SummaHDC clsApp() key {0} IKKE Opdateret", key));
            }
        }

        private static void EncryptAppSettings(clsAppData recAppData)
        {
            var processName = Process.GetCurrentProcess().ProcessName;
            var strExeConfigFilename = string.Format("{0}.exe.config", processName).Replace(@".vshost","");
            Program.Log(string.Format("DEBUG1 EncryptAppSettings() strExeConfigFilename={0}", strExeConfigFilename));

            var uri = new Uri(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase));
            ExeConfigurationFileMap configurationFileMap = new ExeConfigurationFileMap { ExeConfigFilename = Path.Combine(uri.LocalPath, strExeConfigFilename) };
            Program.Log(string.Format("DEBUG2 EncryptAppSettings() ExeConfigFilename={0}, RoamingUserConfigFilename={1}, LocalUserConfigFilename={2} ", configurationFileMap.ExeConfigFilename, configurationFileMap.RoamingUserConfigFilename, configurationFileMap.LocalUserConfigFilename));

            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configurationFileMap, ConfigurationUserLevel.None, true);
            Program.Log(string.Format("DEBUG3 EncryptAppSettings() FilePath={0}", config.FilePath));

            ConfigurationSection sectionToEncrypt = config.GetSection("PrivateHDCAppSettings");

            if (sectionToEncrypt == null)
            {
                Program.Log("Trans2SummaHDC clsApp() EncryptAppSettings Error - unable to find section to encrypt: PrivateAppSetting");
                return;
            }

            if (sectionToEncrypt.SectionInformation.IsProtected == false)
            {
                sectionToEncrypt.SectionInformation.ProtectSection("MyHDCProvider");
                config.Save(ConfigurationSaveMode.Full);
            }
        }
     }
}
