using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NamedPipeWrapper;
using nsPbs3060;
using Microsoft.Win32;
using System.Security.Cryptography;

namespace pipeClient
{
    public partial class Form1 : Form
    {
        private readonly NamedPipeClient<clsAppData> _client = new NamedPipeClient<clsAppData>("MyPipe");
        clsAppData _appdata;
        private RegistryKey masterKey = null;
        private string regKey = @"Software\Hafsjold\pipeClient";

        public Form1()
        {
            InitializeComponent();
            masterKey = Registry.CurrentUser.OpenSubKey(regKey,true);
            if (masterKey == null)
            {
                RegistryKey masterKeyCreate = Registry.CurrentUser.OpenSubKey(@"Software", true);
                masterKey = masterKeyCreate.CreateSubKey(@"Hafsjold\pipeClient", RegistryKeyPermissionCheck.ReadWriteSubTree);
            }
        }

        private void OnLoad(object sender, EventArgs eventArgs)
        {
            _client.ServerMessage += OnServerMessage;
            _client.Disconnected += OnDisconnected;
            _client.Start();
            _appdata = new clsAppData();
            dsAppData.DataSource = _appdata;
        }
        
        private void OnDisconnected(NamedPipeConnection<clsAppData, clsAppData> connection)
        {
            richTextBoxMessages.Invoke(new Action(delegate
            {
                AddLine("Disconnected from server");
            }));
        }

        private void OnServerMessage(NamedPipeConnection<clsAppData, clsAppData> connection, clsAppData message)
        {
            richTextBoxMessages.Invoke(new Action(delegate
            {
                AddLine("Server: " + message.ToString());
            }));
        }

        private void AddLine(string textline)
        {
            richTextBoxMessages.Invoke(new Action(delegate
            {
                richTextBoxMessages.Text += Environment.NewLine + textline;
            }));
        }

        private void btnOpdatermarkerededata_Click(object sender, EventArgs e)
        {
            _appdata.Id = new Random().Next();
            _appdata.message = "Opdatering af markerede data";
            richTextBoxMessages.Invoke(new Action(delegate
            {
                AddLine("Client: " + _appdata.ToString());
            }));
            _client.PushMessage(_appdata);
            _appdata = new clsAppData();
            dsAppData.DataSource = _appdata;
        }

        private void EncryptAppconfig_Click(object sender, EventArgs e)
        {
            clsAppData data = new clsAppData()
            {
                bEncryptApp = true,
                Id = new Random().Next(),
                message = "Encrypt App.config"
            };

             richTextBoxMessages.Invoke(new Action(delegate
            {
                AddLine("Client: " + data.ToString());
            }));
            _client.PushMessage(data);
            _appdata = new clsAppData();
            dsAppData.DataSource = _appdata;
        }

        private void btnSaveEncrypted_Click(object sender, EventArgs e)
        {
            string password = txtPassword.Text;
            var res = CheckPassword(password);
            //var encryptData = _appdata.encryptClass(password);
            //masterKey.SetValue("clsAppData", encryptData, RegistryValueKind.String);

            var encryptData = (string)masterKey.GetValue("clsAppData", "");
            clsAppData udata = new clsAppData(encryptData, password);
        }

        bool CheckPassword(string password)
        {
            byte[] plainText = System.Text.Encoding.Unicode.GetBytes(password);
            bool newPassword = false;
            byte[] salt;
            var savedsalt = (string)masterKey.GetValue("salt", "");
            if (savedsalt == "")
            {
                newPassword = true;
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                salt = new byte[32];
                rng.GetBytes(salt);
                masterKey.SetValue("salt", Convert.ToBase64String(salt), RegistryValueKind.String);
            }
            else
            {
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
            byte[] hash =  algorithm.ComputeHash(plainTextWithSaltBytes);
            if (newPassword)
            {
                masterKey.SetValue("hash", Convert.ToBase64String(hash), RegistryValueKind.String);
                return true;
            }
            var savedhash = (string)masterKey.GetValue("hash", "");
            if (Convert.ToBase64String(hash) == savedhash)
                return true;
            else
                return false;
        }
    }
}
