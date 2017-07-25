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
    public partial class FrmPipeClient : Form
    {
        private readonly NamedPipeClient<clsAppData> _client = new NamedPipeClient<clsAppData>("MyPipe");
        clsAppData m_appdata;
        private RegistryKey m_masterKey = null;
        private string m_regKey = null;
        private bool m_bLogedIn = false;
        private string m_user = null;

        public FrmPipeClient()
        {
            InitializeComponent();
        }

        private bool setMasterKey(string pUser, bool pNyUser)
        {
            string m_regKey = @"Software\Hafsjold\pipeClient\user\" + pUser;
            string m_regSubKey = @"Hafsjold\pipeClient\user\" + pUser;
            m_masterKey = Registry.CurrentUser.OpenSubKey(m_regKey, true);

            if ((m_masterKey == null) && pNyUser)
            {
                RegistryKey masterKeyCreate = Registry.CurrentUser.OpenSubKey(@"Software", true);
                m_masterKey = masterKeyCreate.CreateSubKey(m_regSubKey, RegistryKeyPermissionCheck.ReadWriteSubTree);
                return true;
            }
            else if ((m_masterKey == null) && !pNyUser)
            {
                return false;
            }
            else if ((m_masterKey != null) && pNyUser)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void OnLoad(object sender, EventArgs eventArgs)
        {
            _client.ServerMessage += OnServerMessage;
            _client.Disconnected += OnDisconnected;
            _client.Start();
            m_appdata = new clsAppData();
            dsAppData.DataSource = m_appdata;
            toolLogedInStatus.Text = "Login med bruger og password for at få adgang";
            m_bLogedIn = true;
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
            m_appdata.Id = new Random().Next();
            m_appdata.message = "Opdatering af markerede data";
            richTextBoxMessages.Invoke(new Action(delegate
            {
                AddLine("Client: " + m_appdata.ToString());
            }));
            _client.PushMessage(m_appdata);
            m_appdata = new clsAppData();
            dsAppData.DataSource = m_appdata;
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
            m_appdata = new clsAppData();
            dsAppData.DataSource = m_appdata;
        }

        private void btnLoadEncrypted_Click(object sender, EventArgs e)
        {
            string password = login(m_user); 
            var res = CheckPassword(m_user,password, false);
            
            var encryptData = (string)m_masterKey.GetValue("clsAppData", "");
            m_appdata = new clsAppData(encryptData, password);
            dsAppData.DataSource = m_appdata;

        }

        private void btnSaveEncrypted_Click(object sender, EventArgs e)
        {
            string password = login(m_user);
            var res = CheckPassword(m_user, password, false);

            var encryptData = m_appdata.encryptClass(password);
            m_masterKey.SetValue("clsAppData", encryptData, RegistryValueKind.String);
        }

        bool CheckPassword(string pUser, string pPassword, bool newPassword)
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
                m_masterKey.SetValue("salt", Convert.ToBase64String(salt), RegistryValueKind.String);
            }
            else
            {
                var savedsalt = (string)m_masterKey.GetValue("salt", "");
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
                m_masterKey.SetValue("hash", Convert.ToBase64String(hash), RegistryValueKind.String);
                return true;
            }
            var savedhash = (string)m_masterKey.GetValue("hash", "");
            if (Convert.ToBase64String(hash) == savedhash)
            {
                toolLogedInStatus.Text = string.Format("Er login som bruger {0}", pUser);
                m_user = pUser;
                m_bLogedIn = true;
                enablebuttoms();
                return true;
            }
            else
            {
                toolLogedInStatus.Text = "Login med bruger og password for at få adgang";
                m_bLogedIn = false;
                enablebuttoms();
                return false;
            }
        }

        private void enablebuttoms()
        {
            if (m_bLogedIn)
            {
                btnEncryptAppconfig.Enabled = true;
                btnOpdatermarkerededata.Enabled = true;
                btnSaveEncrypted.Enabled = true;
                btnLoadEncrypted.Enabled = true;
            }
            else
            {
                btnEncryptAppconfig.Enabled = false;
                btnOpdatermarkerededata.Enabled = false;
                btnSaveEncrypted.Enabled = false;
                btnLoadEncrypted.Enabled = false;
            }
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            login("");
        }

        private string login(string user)
        {
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.txtBruger.Text = user;
            while (true)
            {
                DialogResult res = frmLogin.ShowDialog(this);
                if (res == DialogResult.Cancel) return null;
                if (!frmLogin.bNyBruger)
                {
                    var rxes = CheckPassword(frmLogin.txtBruger.Text, frmLogin.txtPassword.Text, false);
                    if (!rxes)
                    {
                        frmLogin.lblError.Text = "Forkert Bruger eller Password";
                    }
                    else
                    {
                        return frmLogin.txtPassword.Text;
                    }
                }
                else
                {
                    var ryes = CheckPassword(frmLogin.txtBruger.Text, frmLogin.txtPassword.Text, true);
                    return frmLogin.txtPassword.Text;
                }
            }
        }


    }
}
