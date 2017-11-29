using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using NLog;

namespace CitySimNetworkService
{
    /// <summary>
    /// <Module>Networking Server Connection</Module>
    /// <Team>Networking Team</Team>
    /// <Description>Responsible for handling incoming connections and passing the data to appropriate handlers.</Description>
    /// <BasedOn>https://docs.microsoft.com/en-us/dotnet/framework/network-programming/asynchronous-server-socket-example</BasedOn>
    /// <Author>
    /// <By>Harman Mahal</By>
    /// <ChangeLog>Modified the handler to add request handlers</ChangeLog>
    /// <Date>November 04, 2017</Date>
    /// </Author>
    /// </summary>
    public class AsyncServer
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private RequestHandler requestHandler;
        public ManualResetEvent allDone = new ManualResetEvent(false);

        /// <summary>
        /// Specifies main request handler.
        /// </summary>
        /// 
        /// <param name="_requestHandler">
        /// Request handler to set.
        /// </param>
        public AsyncServer(RequestHandler _requestHandler)
        {
            requestHandler = _requestHandler;
        }

        /// <summary>
        /// Opens connection to server after initializations.
        /// </summary>
        public void StartListening()
        {
            byte[] bytes = new Byte[1024];
#pragma warning disable CS0618 // Type or member is obsolete
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
#pragma warning restore CS0618 // Type or member is obsolete
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            //TODO: The port should be specified in App.Config
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 13456);

			//create socket for to listen for clients with specified port
            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(ipEndPoint);
                listener.Listen(100);

                while (true)
                {
                    allDone.Reset();
                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
                    allDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                logger.Error(e);
            }
        }

        /// <summary>
        /// Accepts callback with the status of an asynchronous operation.
        /// </summary>
        /// 
        /// <param name="ar"> 
        /// Represents the status of an asynchronous operation. 
        /// </param>
        private void AcceptCallback(IAsyncResult ar)
        {
            allDone.Set();

            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            SocketState state = new SocketState
            {
                socket = handler
            };

            handler.BeginReceive(state.buffer, 0, SocketState.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }

        /// <summary>
        /// Executes a read with the status of an asynchronous operation.
        /// </summary>
        /// 
        /// <param name="ar">
        /// Represents the status of an asynchronous operation.
        /// </param>
        private void ReadCallback(IAsyncResult ar)
        {
            string content = String.Empty;
            SocketState state = (SocketState)ar.AsyncState;
            Socket handler = state.socket;

			//read bytes from socket
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
				//convert to string format
                state.receivedData.Append(Encoding.UTF8.GetString(state.buffer, 0, bytesRead));
                content = state.receivedData.ToString();
                if (content.IndexOf("<EOF>") > -1)
                {
                    string response = requestHandler.ParseRequest(content); //convert to JSON format
                    Send(handler, response);
                }

            }
        }


        /// <summary>
        /// Sends encoded data by handler.
        /// </summary>
        /// 
        /// <param name="handler">
        /// A socket.
        /// </param>
        /// 
        /// <param name="data">
        /// Data.
        /// </param>
        private void Send(Socket handler, string data)
        {
            byte[] encodedData = Encoding.UTF8.GetBytes(data);
            handler.BeginSend(encodedData, 0, encodedData.Length, 0, new AsyncCallback(SendCallback), handler);
        }

        /// <summary>
        /// Sends callback with the status of an asynchronous operation.
        /// </summary>
        /// 
        /// <param name="ar">
        /// Represents the status of an asynchronous operation.
        /// </param>
        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception e)
            {
                logger.Error(e);
            }
        }
    }
}
