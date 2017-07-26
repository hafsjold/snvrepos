using System;
using NamedPipeWrapper;
using System.Threading;
using nsPbs3060;

namespace nsMedlem3060Service
{
    public class pipeServer
    {
        NamedPipeServer<clsPipeData> server = null;

        public pipeServer(string pipeName)
        {
            Program.Log("Medlem3060Service pipeServer() start");
            server = new NamedPipeServer<clsPipeData>(pipeName);
            server.ClientConnected += OnClientConnected;
            server.ClientDisconnected += OnClientDisconnected;
            server.ClientMessage += OnClientMessage;
            server.Error += OnError;
            server.Start();
            mcMedlem3060Service.Service_waitStopHandle.WaitOne();
            Program.Log("Medlem3060Service pipeServer() Stop");
            server.Stop();
        }

        private void OnClientConnected(NamedPipeConnection<clsPipeData, clsPipeData> connection)
        {
            Program.Log(string.Format("Medlem3060Service OnClientConnected(): Client {0} is now connected!", connection.Id));
            connection.PushMessage(new clsPipeData { Id = new Random().Next(), message = "Welcome!  You are now connected to the server."});
        }

        private void OnClientMessage(NamedPipeConnection<clsPipeData, clsPipeData> connection, clsPipeData pipedata)
        {
            try
            {
                Program.Log(string.Format("Medlem3060Service OnClientMessage(): Client {0} send {1}", connection.Id, pipedata.ToString()));
                if (pipedata.cmd == clsPipeData.command.ProcessAppData)
                {
                    clsApp.AddUpdateAppSettings(pipedata.AppData);
                    pipedata.message = "Your request has been executed on the server.";
                    connection.PushMessage(pipedata);
                }
            }
            catch (Exception exception)
            {
                Program.Log(string.Format("Medlem3060Service OnClientMessage() ERROR: {0}", exception));
            }
        }

        private void OnClientDisconnected(NamedPipeConnection<clsPipeData, clsPipeData> connection)
        {
            Program.Log(string.Format("Medlem3060Service OnClientDisconnected(): Client {0} disconnected", connection.Id));
        }

        private void OnError(Exception exception)
        {
            Program.Log(string.Format("Medlem3060Service pipeServer() OnError() ERROR: {0}", exception));
        }

    }
}
       