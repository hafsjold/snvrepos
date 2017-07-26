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
        private readonly NamedPipeClient<clsPipeData> _client = new NamedPipeClient<clsPipeData>("MyPipe");
        clsAppData m_appdata;

        public FrmPipeClient()
        {
            InitializeComponent();
        }
 
        private void OnLoad(object sender, EventArgs eventArgs)
        {
            _client.ServerMessage += OnServerMessage;
            _client.Disconnected += OnDisconnected;
            _client.Start();
            m_appdata = new clsAppData();
            dsAppData.DataSource = m_appdata;
            enablebuttoms();
        }
        
        private void OnDisconnected(NamedPipeConnection<clsPipeData, clsPipeData> connection)
        {
            richTextBoxMessages.Invoke(new Action(delegate
            {
                AddLine("Disconnected from server");
            }));
        }

        private void OnServerMessage(NamedPipeConnection<clsPipeData, clsPipeData> connection, clsPipeData message)
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
            clsPipeData pideData = new clsPipeData()
            {
                Id = new Random().Next(),
                cmd = clsPipeData.command.ProcessAppData,
                message = "Opdatering af markerede data",
                AppData = m_appdata
            };
            richTextBoxMessages.Invoke(new Action(delegate
            {
                AddLine("Client: " + pideData.ToString());
            }));
            _client.PushMessage(pideData);
            m_appdata = new clsAppData();
            dsAppData.DataSource = m_appdata;
        }

        private void EncryptAppconfig_Click(object sender, EventArgs e)
        {
            clsPipeData pideData = new clsPipeData()
            {
                Id = new Random().Next(),
                cmd = clsPipeData.command.ProcessAppData,
                message = "Encrypt App.config",
                AppData = new clsAppData() { bEncryptApp = true }
            };
            richTextBoxMessages.Invoke(new Action(delegate
            {
                AddLine("Client: " + pideData.ToString());
            }));
            _client.PushMessage(pideData);
            m_appdata = new clsAppData();
            dsAppData.DataSource = m_appdata;
        }

        private void btnLoadEncrypted_Click(object sender, EventArgs e)
        {
            string password = getPassword();
            if (!string.IsNullOrEmpty(password))
            {
                var res = clsPassword.CheckPassword(Program.User, password, false);
                if (res)
                {
                    var encryptData = (string)clsPassword.masterKey.GetValue("clsAppData", "");
                    m_appdata = new clsAppData(encryptData, password);
                    dsAppData.DataSource = m_appdata;
               }
            }
        }

        private void btnSaveEncrypted_Click(object sender, EventArgs e)
        {
            string password = getPassword();
            if (!string.IsNullOrEmpty(password))
            {
                var res = clsPassword.CheckPassword(Program.User, password, false);
                if (res)
                {
                    var encryptData = m_appdata.encryptClass(password);
                    clsPassword.masterKey.SetValue("clsAppData", encryptData, RegistryValueKind.String);
                }
            }
        }

        private void enablebuttoms()
        {
            if (Program.bLogedIn)
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

        public string getPassword()
        {
            FrmPassword frmPassword = new FrmPassword();
            frmPassword.txtBruger.Text = Program.User;
            while (true)
            {
                DialogResult res = frmPassword.ShowDialog(this);
                if (res == DialogResult.Cancel) return null;
             
                    var rxes = clsPassword.CheckPassword(frmPassword.txtBruger.Text, frmPassword.txtPassword.Text, false);
                    if (!rxes)
                    {
                    frmPassword.lblError.Text = "Forkert Bruger eller Password";
                    }
                    else
                    {
                        return frmPassword.txtPassword.Text;
                    }         
            }
        }
    }
}
