using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CitySimNetworkService
{
    /// <summary>
    /// Contains socket information.
    /// </summary>
    public class SocketState
    {
        public Socket socket = null;
        public const int BufferSize = 1024;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder receivedData = new StringBuilder();
    }
}
