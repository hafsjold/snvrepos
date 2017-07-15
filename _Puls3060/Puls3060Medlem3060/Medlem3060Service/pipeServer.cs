using System;
using NamedPipeWrapper;
using System.Threading;
using nsPbs3060;

namespace nsMedlem3060Service
{
    public class pipeServer
    {
        NamedPipeServer<clsAppData> server = null;

        public pipeServer(string pipeName)
        {
            Program.Log("Medlem3060Service pipeServer() start");
            server = new NamedPipeServer<clsAppData>(pipeName);
            server.ClientConnected += OnClientConnected;
            server.ClientDisconnected += OnClientDisconnected;
            server.ClientMessage += OnClientMessage;
            server.Error += OnError;
            server.Start();
            mcMedlem3060Service.Service_waitStopHandle.WaitOne();
            Program.Log("Medlem3060Service pipeServer() Stop");
            server.Stop();
        }

        private void OnClientConnected(NamedPipeConnection<clsAppData, clsAppData> connection)
        {
            Program.Log(string.Format("Medlem3060Service OnClientConnected(): Client {0} is now connected!", connection.Id));
            connection.PushMessage(new clsAppData { });
        }

        private void OnClientDisconnected(NamedPipeConnection<clsAppData, clsAppData> connection)
        {
            Program.Log(string.Format("Medlem3060Service OnClientDisconnected(): Client {0} disconnected", connection.Id));
        }

        private void OnClientMessage(NamedPipeConnection<clsAppData, clsAppData> connection, clsAppData message)
        {
            Program.Log(string.Format("Medlem3060Service OnClientMessage(): Client {0} send {1}", connection.Id, message.ToString()));
        }

        private void OnError(Exception exception)
        {
            Program.Log(string.Format("Medlem3060Service pipeServer() OnError() ERROR: {0}", exception));
        }

    }
}
