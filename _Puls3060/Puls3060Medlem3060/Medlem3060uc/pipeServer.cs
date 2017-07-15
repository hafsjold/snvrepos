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
        private readonly NamedPipeServer<clsAppData> _server = new NamedPipeServer<clsAppData>("MyPipe");

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
 
        private void OnClientConnected(NamedPipeConnection<clsAppData, clsAppData> connection)
        {
            //connection.PushMessage("Welcome!  You are now connected to the server.");
            Program.Log(string.Format("Medlem306uc OnClientConnected(): Client {0} is now connected!", connection.Id));
            connection.PushMessage(new clsAppData { Id = new Random().Next(), message = "Welcome!  You are now connected to the server." });
        }

        private void OnClientMessage(NamedPipeConnection<clsAppData, clsAppData> connection, clsAppData appdata)
        {
            try
            {
                Program.Log(string.Format("Medlem306uc OnClientMessage(): Client {0} send {1}", connection.Id, appdata.ToString()));
                clsApp.AddUpdateAppSettings(appdata);
                appdata.message = "Your request has been executed on the server.";
                connection.PushMessage(appdata);
            }
            catch (Exception exception)
            {
                Program.Log(string.Format("Medlem306uc OnClientMessage() ERROR: {0}", exception));
            }
        }

        private void OnClientDisconnected(NamedPipeConnection<clsAppData, clsAppData> connection)
        {
            Program.Log(string.Format("Medlem306uc OnClientDisconnected(): Client {0} disconnected", connection.Id));
        }

        private void OnError(Exception exception)
        {
            Program.Log(string.Format("Medlem306uc pipeServer() OnError() ERROR: {0}", exception));
        }
    }
}
