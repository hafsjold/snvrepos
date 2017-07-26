using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace pipeClient
{
    static public class clsPassword
    {
        static public RegistryKey masterKey = null;

        static public bool CheckPassword(string pUser, string pPassword, bool newPassword)
        {
            bool check = setMasterKey(pUser, newPassword);
            if (!check) return false;

            byte[] plainText = System.Text.Encoding.Unicode.GetBytes(pPassword);

            byte[] salt;
            if (newPassword)
            {
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                salt = new byte[32];
                rng.GetBytes(salt);
                masterKey.SetValue("salt", Convert.ToBase64String(salt), RegistryValueKind.String);
            }
            else
            {
                var savedsalt = (string)masterKey.GetValue("salt", "");
                salt = Convert.FromBase64String(savedsalt);
            }

            byte[] plainTextWithSaltBytes = new byte[plainText.Length + salt.Length];
            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] hash = algorithm.ComputeHash(plainTextWithSaltBytes);
            if (newPassword)
            {
                masterKey.SetValue("hash", Convert.ToBase64String(hash), RegistryValueKind.String);
                return true;
            }
            var savedhash = (string)masterKey.GetValue("hash", "");
            if (Convert.ToBase64String(hash) == savedhash)
            {
                /*
                toolLogedInStatus.Text = string.Format("Er login som bruger {0}", pUser);
                enablebuttoms();
                */
                Program.User = pUser;
                Program.bLogedIn = true;
                return true;
            }
            else
            {
                /*
                toolLogedInStatus.Text = "Login med bruger og password for at få adgang";
                enablebuttoms();
                */
                Program.bLogedIn = false;
                return false;
            }
        }

        static private bool setMasterKey(string pUser, bool pNyUser)
        {
            string m_regKey = @"Software\Hafsjold\pipeClient\user\" + pUser;
            string m_regSubKey = @"Hafsjold\pipeClient\user\" + pUser;
            masterKey = Registry.CurrentUser.OpenSubKey(m_regKey, true);

            if ((masterKey == null) && pNyUser)
            {
                RegistryKey masterKeyCreate = Registry.CurrentUser.OpenSubKey(@"Software", true);
                masterKey = masterKeyCreate.CreateSubKey(m_regSubKey, RegistryKeyPermissionCheck.ReadWriteSubTree);
                return true;
            }
            else if ((masterKey == null) && !pNyUser)
            {
                return false;
            }
            else if ((masterKey != null) && pNyUser)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
