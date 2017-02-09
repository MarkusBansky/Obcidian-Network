using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using ObsidiaDebugger.Content;
using ObsidiaDebugger.Helpers;

namespace ObsidiaDebugger.Server
{
    internal class RestServer
    {
        private ObsidianDebugger _form;

        public RestServer (ObsidianDebugger form)
        {
            _form = form;
        }

        public void StartListening ()
        {
            // Establish the local endpoint for the socket.
            // The DNS name of the computer
            // running the listener is "host.contoso.com".
            IPHostEntry ipHostInfo = Dns.GetHostEntry (Dns.GetHostName ());
            IPAddress ipAddress = ipHostInfo.AddressList[3];
            IPEndPoint localEndPoint = new IPEndPoint (ipAddress, 11000);

            // Create a TCP/IP socket.
            Socket listener = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.
            try
            {
                listener.Bind (localEndPoint);
                listener.Listen (100);

                while (true)
                {
                    // Start socket to listen for connections.
                    Socket handler = listener.Accept ();

                    string data = "";
                    while (true)
                    {
                        byte[] bytes = new byte[1024];
                        int bytesRec = handler.Receive (bytes);
                        data += Encoding.ASCII.GetString (bytes, 0, bytesRec);
                        if (data.IndexOf ("<EOF>", StringComparison.Ordinal) > -1)
                        {
                            break;
                        }
                    }

                    _form.Update(ResponseParser.Parse(data.Replace("<EOF>", "")));

                    handler.Shutdown (SocketShutdown.Both);
                    handler.Close ();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}