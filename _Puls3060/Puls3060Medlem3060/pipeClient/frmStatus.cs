using NamedPipeWrapper;
using nsPbs3060;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pipeClient
{
    public partial class FrmStatus : Form
    {
        private readonly NamedPipeClient<clsPipeData> _client = new NamedPipeClient<clsPipeData>("MyPipe");
        clsStatusData m_statusdata;

        public FrmStatus()
        {
            InitializeComponent();
        }

        private void FrmStatus_Load(object sender, EventArgs e)
        {
            _client.ServerMessage += OnServerMessage;
            _client.Disconnected += OnDisconnected;
            _client.Start();
            m_statusdata = new clsStatusData();
            dsStatusData.DataSource = m_statusdata;
        }

        private void OnDisconnected(NamedPipeConnection<clsPipeData, clsPipeData> connection)
        {
            richTextBoxMessages.Invoke(new Action(delegate
            {
                AddLine("Disconnected from server");
            }));
        }

        private void OnServerMessage(NamedPipeConnection<clsPipeData, clsPipeData> connection, clsPipeData pipedata)
        {
            richTextBoxMessages.Invoke(new Action(delegate
            {
                AddLine("Server: " + pipedata.ToString());
            }));
            switch (pipedata.cmd)
            {
                case clsPipeData.command.ProcessAppData:
                    break;
                case clsPipeData.command.ResponseAppData:
                    break;
                case clsPipeData.command.ProcessStatusData:
                    break;
                case clsPipeData.command.ResponseStatusData:
                    m_statusdata = pipedata.StatusData;
                    dsStatusData.DataSource = m_statusdata;
                    RefreshScreen();
                    break;
                default:
                    break;
            }
        }

        private void RefreshScreen()
        {
            this.Invoke(new Action(delegate { Refresh(); }));
        }

        private void AddLine(string textline)
        {
            richTextBoxMessages.Invoke(new Action(delegate { richTextBoxMessages.Text += Environment.NewLine + textline; }));
        }

        private void btnServerStatus_Click(object sender, EventArgs e)
        {
            clsPipeData pideData = new clsPipeData()
            {
                Id = new Random().Next(),
                cmd = clsPipeData.command.ProcessStatusData,
                message = "Get Status Data",
                StatusData = new clsStatusData(),
            };
            richTextBoxMessages.Invoke(new Action(delegate
            {
                AddLine("Client: " + pideData.ToString());
            }));
            _client.PushMessage(pideData);
            m_statusdata = new clsStatusData();
        }
    }
}
