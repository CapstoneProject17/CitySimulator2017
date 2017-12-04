using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CitySimNetworkService
{
    /// <summary>
    /// <Module>Networking Server Connection</Module>
    /// <Team>Networking Team</Team>
    /// <Description>Socket set up information</Description>
    /// <Author>
    /// <By>Harman Mahal</By>
    /// <ChangeLog>Initial Socket setup</ChangeLog>
    /// <Date>October 21, 2017</Date>
    /// </Author>
    /// </summary>    /// </summary>
    public class SocketState
    {
        public Socket socket = null;
        public const int BufferSize = 1024;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder receivedData = new StringBuilder();
    }
}
