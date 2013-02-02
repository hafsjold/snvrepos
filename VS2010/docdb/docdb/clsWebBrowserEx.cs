using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading;

namespace docdb
{
    public class WebBrowserEx : System.Windows.Forms.WebBrowser
    {
        string _Html = String.Empty;
        string _BaseDirectory = String.Empty;
        byte[] _bytes = null;
        string _ContentType = String.Empty;

        public void LoadHtml(string html, string baseDirectory)
        {
            if (SynchronizationContext.Current != _UI)
                throw new ApplicationException(
                  "WebBrowserEx.LoadHtml must be called on the UI thread.");

            InitServer();

            _Html = html;
            _BaseDirectory = baseDirectory;

            if (_Server == null) return;

            _Server.SetHtml(html, baseDirectory);

            Navigate(_Server.Url);
        }

        public void LoadBytes(byte[] bytes, string ContentType)
        {
            if (SynchronizationContext.Current != _UI)
                throw new ApplicationException(
                  "WebBrowserEx.LoadHtml must be called on the UI thread.");

            InitServer();

            _bytes = bytes;
 
            if (_Server == null) return;

            _Server.SetBytes(bytes, ContentType);

            Navigate(_Server.Url);
        }

        SynchronizationContext _UI = null;
        bool _TriedToStartServer = false;
        Server _Server = null;

        public WebBrowserEx()
        {
            _UI = SynchronizationContext.Current;
        }

        void InitServer()
        {
            if (_TriedToStartServer) return;

            _TriedToStartServer = true;

            _Server = new Server();
        }

        public class Server
        {
            string _Html = String.Empty;
            string _BaseDirectory = String.Empty;
            byte[] _bytes = null;
            string _ContentType = String.Empty;

            public void SetBytes(byte[] bytes, string ContentType)
            {
                _bytes = bytes;
                _ContentType = ContentType;
            }

            public void SetHtml(string html, string baseDirectory)
            {
                _Html = html;
                _BaseDirectory = baseDirectory;
            }

            public Uri Url
            {
                get
                {
                    return new Uri(
                        "http://" + "localhost" + ":" + _Port + "/");
                }
            }

            HttpListener _Listener = null;
            int _Port = -1;

            public Server()
            {
                var rnd = new Random();

                for (int i = 0; i < 100; i++)
                {
                    int port = rnd.Next(49152, 65536);

                    try
                    {
                        _Listener = new HttpListener();
                        _Listener.Prefixes.Add("http://localhost:" + port + "/");
                        _Listener.Start();

                        _Port = port;
                        _Listener.BeginGetContext(ListenerCallback, null);
                        return;
                    }
                    catch (Exception x)
                    {
                        _Listener.Close();
                        Debug.WriteLine("HttpListener.Start:\n" + x);
                    }
                }

                throw new ApplicationException("Failed to start HttpListener");
            }

            public void ListenerCallback(IAsyncResult ar)
            {
                _Listener.BeginGetContext(ListenerCallback, null);

                var context = _Listener.EndGetContext(ar);
                var request = context.Request;
                var response = context.Response;

                Debug.WriteLine("SERVER: " + _BaseDirectory + " " + request.Url);

                response.AddHeader("Cache-Control", "no-cache");

                try
                {
                    if (request.Url.AbsolutePath == "/")
                    {
                        if (_bytes != null)
                        {
                            response.ContentType = _ContentType;
                            response.ContentLength64 = _bytes.Length;
                            using (var s = response.OutputStream) s.Write(_bytes, 0, _bytes.Length);
                            return;
                        }
                        else
                        {
                            response.ContentType = MediaTypeNames.Text.Html;
                            response.ContentEncoding = Encoding.UTF8;
                            var buffer = Encoding.UTF8.GetBytes(_Html);
                            response.ContentLength64 = buffer.Length;
                            using (var s = response.OutputStream) s.Write(buffer, 0, buffer.Length);
                            return;
                        }
                    }

                    var filepath = Path.Combine(_BaseDirectory,
                      request.Url.AbsolutePath.Substring(1));

                    Debug.WriteLine("--FILE: " + filepath);

                    if (!File.Exists(filepath))
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound; // 404
                        response.StatusDescription = response.StatusCode + " Not Found";

                        response.ContentType = MediaTypeNames.Text.Html;
                        response.ContentEncoding = Encoding.UTF8;

                        var buffer = Encoding.UTF8.GetBytes(
                          "<html><body>404 Not Found</body></html>");
                        response.ContentLength64 = buffer.Length;
                        using (var s = response.OutputStream) s.Write(buffer, 0, buffer.Length);

                        return;
                    }

                    byte[] entity = null;
                    try
                    {
                        entity = File.ReadAllBytes(filepath);
                    }
                    catch (Exception x)
                    {
                        Debug.WriteLine("Exception reading file: " + filepath + "\n" + x);

                        response.StatusCode = (int)HttpStatusCode.InternalServerError; // 500
                        response.StatusDescription = response.StatusCode + " Internal Server Error";

                        response.ContentType = MediaTypeNames.Text.Html;
                        response.ContentEncoding = Encoding.UTF8;

                        var buffer = Encoding.UTF8.GetBytes(
                          "<html><body>500 Internal Server Error</body></html>");
                        response.ContentLength64 = buffer.Length;
                        using (var s = response.OutputStream) s.Write(buffer, 0, buffer.Length);

                        return;
                    }


                    response.ContentLength64 = entity.Length;

                    switch (Path.GetExtension(request.Url.AbsolutePath).ToLowerInvariant())
                    {
                        //images
                        case ".gif": response.ContentType = MediaTypeNames.Image.Gif; break;
                        case ".jpg":
                        case ".jpeg": response.ContentType = MediaTypeNames.Image.Jpeg; break;
                        case ".tiff": response.ContentType = MediaTypeNames.Image.Tiff; break;
                        case ".png": response.ContentType = "image/png"; break;

                        // application
                        case ".pdf": response.ContentType = MediaTypeNames.Application.Pdf; break;
                        case ".zip": response.ContentType = MediaTypeNames.Application.Zip; break;

                        // text
                        case ".htm":
                        case ".html": response.ContentType = MediaTypeNames.Text.Html; break;
                        case ".txt": response.ContentType = MediaTypeNames.Text.Plain; break;
                        case ".xml": response.ContentType = MediaTypeNames.Text.Xml; break;

                        // let the user agent work it out
                        default: response.ContentType = MediaTypeNames.Application.Octet; break;
                    }

                    using (var s = response.OutputStream) s.Write(entity, 0, entity.Length);
                }
                catch (Exception x)
                {
                    Debug.WriteLine("Unexpected exception. Aborting...\n" + x);

                    response.Abort();
                }
            }
        }
    }
}