using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace nsPbs3060
{
    [Serializable]
    public class clsAppData
    {
        public clsAppData() { }
        public clsAppData (string xml)
        {
            FromXml(xml);
        }
        public clsAppData(string encryptData, string password)
        {
            var xml = Decrypt(encryptData, password);
            FromXml(xml);
        }

        public string UniContaUser { get; set; }
        public string UniContaPW { get; set; }
        public string UniContaCompanyId { get; set; }
        public string GigaHostImapUser { get; set; }
        public string GigaHostImapPW { get; set; }
        public string dbPuls3060MedlemPW { get; set; }
        public string dbPuls3060MedlemUser { get; set; }
        public string puls3060_dkUser { get; set; }
        public string puls3060_dkPW { get; set; }

        public bool bUniContaUser { get; set; }
        public bool bUniContaPW { get; set; }
        public bool bUniContaCompanyId { get; set; }
        public bool bGigaHostImapUser { get; set; }
        public bool bGigaHostImapPW { get; set; }
        public bool bdbPuls3060MedlemPW { get; set; }
        public bool bdbPuls3060MedlemUser { get; set; }
        public bool bpuls3060_dkUser { get; set; }
        public bool bpuls3060_dkPW { get; set; }

        public bool bEncryptApp { get; set; }

        private string ToXml()
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(clsAppData));
            var subReq = this;
            var xml = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, subReq);
                    xml = sww.ToString(); // Your XML
                }
            }
            return xml;
        }

        private void FromXml(string xml)
        {
            try
            {
                clsAppData data;
                using (var stringReader = new StringReader(xml))
                {
                    var serializer = new XmlSerializer(typeof(clsAppData));
                    data = (clsAppData)serializer.Deserialize(stringReader);
                }
                UniContaUser = data.UniContaUser;
                UniContaPW = data.UniContaPW;
                UniContaCompanyId = data.UniContaCompanyId;
                GigaHostImapUser = data.GigaHostImapUser;
                GigaHostImapPW = data.GigaHostImapPW;
                dbPuls3060MedlemPW = data.dbPuls3060MedlemPW;
                dbPuls3060MedlemUser = data.dbPuls3060MedlemUser;
                puls3060_dkUser = data.puls3060_dkUser;
                puls3060_dkPW = data.puls3060_dkPW;
                bUniContaUser = data.bUniContaUser;
                bUniContaPW = data.bUniContaUser;
                bUniContaCompanyId = data.bUniContaCompanyId;
                bGigaHostImapUser = data.bGigaHostImapUser;
                bGigaHostImapPW = data.bGigaHostImapPW;
                bdbPuls3060MedlemPW = data.bdbPuls3060MedlemPW;
                bdbPuls3060MedlemUser = data.bdbPuls3060MedlemUser;
                bpuls3060_dkUser = data.bpuls3060_dkUser;
                bpuls3060_dkPW = data.bpuls3060_dkPW;
                bEncryptApp = data.bEncryptApp;
            }
            catch { }
        }

        public string encryptClass(string password)
        {
            return Encrypt(ToXml(), password);
        }

        private string Encrypt(string clearText, string Password)
        {
            byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            byte[] encryptedData = Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));
            return Convert.ToBase64String(encryptedData);
        }
        private byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV)
        {
            byte[] encryptedData;
            using (MemoryStream ms = new MemoryStream())
            {
                using (Rijndael alg = Rijndael.Create())
                {
                    alg.Key = Key;
                    alg.IV = IV;
                    using (CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        //Write all data to stream.
                        cs.Write(clearData, 0, clearData.Length);
                    }
                    encryptedData = ms.ToArray();
                }
            }
            return encryptedData;
        }
        private string Decrypt(string cipherText, string Password)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            byte[] decryptedData = Decrypt(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16));
            if (decryptedData != null)
                return Encoding.Unicode.GetString(decryptedData); 
            else
                return null;
        }

        private byte[] Decrypt(byte[] cipherData, byte[] Key, byte[] IV)
        {
            byte[] decryptedData;
            using (MemoryStream ms = new MemoryStream())
            {
                using (Rijndael alg = Rijndael.Create())
                {
                    try
                    {
                        alg.Key = Key;
                        alg.IV = IV;
                        using (CryptoStream cs = new CryptoStream(ms,
                           alg.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherData, 0, cipherData.Length);
                        }
                        decryptedData = ms.ToArray();
                    }
                    catch 
                    {
                        decryptedData = null;
                    }
                }
            }
            return decryptedData;
        }
    }
}
