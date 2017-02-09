using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ObcidiaNetwork.Client
{
    internal class RestClient
    {
        // The port number for the remote device.
        private const int Port = 11000;

        private Socket _client;
        private IPEndPoint _remoteEp;

        public void StartClient ()
        {
            try
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry (Dns.GetHostName ());
                IPAddress ipAddress = ipHostInfo.AddressList[3];
                _remoteEp = new IPEndPoint (ipAddress, Port);
            }
            catch (Exception e)
            {
                Console.WriteLine (e.ToString ());
            }
        }

        public void SendMessage(string message)
        {
            // Create a TCP/IP socket.
            _client = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Connect to the remote endpoint.
            _client.Connect (_remoteEp);
            // Send test data to the remote device.
            Send (_client, message + "<EOF>");
            // Release the socket.
            _client.Shutdown (SocketShutdown.Both);
            _client.Close ();
        }

        private void Send (Socket client, String data)
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes (data);

            // Begin sending the data to the remote device.
            client.Send (byteData);
        }
    }
}