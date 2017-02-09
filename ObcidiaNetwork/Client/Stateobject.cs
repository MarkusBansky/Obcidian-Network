using System.Net.Sockets;
using System.Text;

namespace ObcidiaNetwork.Client
{
    // State object for receiving data from remote device.
    internal class StateObject
    {
        // Client socket.
        public Socket WorkSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 256;
        // Receive buffer.
        public byte[] Buffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder SBuilder = new StringBuilder ();
    }
}