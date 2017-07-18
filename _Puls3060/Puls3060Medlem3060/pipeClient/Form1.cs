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

namespace pipeClient
{
    public partial class Form1 : Form
    {
        private readonly NamedPipeClient<clsAppData> _client = new NamedPipeClient<clsAppData>("MyPipe");
        clsAppData _appdata;
        static string keyContainerName = "pipeClientContainerKeys";

        public Form1()
        {
            InitializeComponent();
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
            string password = "MyNewPassword";
            string data = _appdata.ToXml();
            clsCrypt crypt = new clsCrypt();
            var encryptData = crypt.Encrypt(data, password);
            var unencryptData = crypt.Decrypt(encryptData, password);
        }
    }
}
