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

namespace Medlem3060uc
{
    public partial class pipeServer : Form
    {
        private readonly NamedPipeServer<clsPipeData> _server = new NamedPipeServer<clsPipeData>("MyPipe");

        public pipeServer()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, EventArgs eventArgs)
        {
            _server.ClientConnected += OnClientConnected;
            _server.ClientDisconnected += OnClientDisconnected;
            _server.ClientMessage += OnClientMessage;
            _server.Error += OnError;
            _server.Start();
        }
 
        private void OnClientConnected(NamedPipeConnection<clsPipeData, clsPipeData> connection)
        {
            //connection.PushMessage("Welcome!  You are now connected to the server.");
            Program.Log(string.Format("Medlem306uc OnClientConnected(): Client {0} is now connected!", connection.Id));
            connection.PushMessage(new clsPipeData { Id = new Random().Next(), message = "Welcome!  You are now connected to the server." });
        }

        private void OnClientMessage(NamedPipeConnection<clsPipeData, clsPipeData> connection, clsPipeData pipedata)
        {
            try
            {
                Program.Log(string.Format("Medlem306uc OnClientMessage(): Client {0} send {1}", connection.Id, pipedata.ToString()));
                if (pipedata.cmd == clsPipeData.command.ProcessAppData)
                {
                    clsApp.AddUpdateAppSettings(pipedata.AppData);
                    pipedata.message = "Your request has been executed on the server.";
                    connection.PushMessage(pipedata);
                }
            }
            catch (Exception exception)
            {
                Program.Log(string.Format("Medlem306uc OnClientMessage() ERROR: {0}", exception));
            }
        }

        private void OnClientDisconnected(NamedPipeConnection<clsPipeData, clsPipeData> connection)
        {
            Program.Log(string.Format("Medlem306uc OnClientDisconnected(): Client {0} disconnected", connection.Id));
        }

        private void OnError(Exception exception)
        {
            Program.Log(string.Format("Medlem306uc pipeServer() OnError() ERROR: {0}", exception));
        }
    }
}
