using System.Net.Sockets;
using System.Text;

namespace ObsidiaDebugger.Server
{
    // State object for reading client data asynchronously
    internal class StateObject
    {
        // Client  socket.
        public Socket WorkSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 1024;
        // Receive buffer.
        public byte[] Buffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder SBuilder = new StringBuilder ();
    }
}