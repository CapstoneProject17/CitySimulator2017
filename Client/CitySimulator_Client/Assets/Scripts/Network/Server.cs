using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.IO;

/// <summary>
/// Name:       CitySimulator 
/// Author:     Gisu Kim A00959494
/// Date:       2017-10-02
/// Updated by: 2017-10-07
/// Updated by: 2017-10-11
/// Updated by: 2017-10-12
/// Updated by: 2017-10-27
/// Updated by: 2017-10-30
/// Updated by: 2017-11-03
/// What the superviosr should know: N/A
/// </summary>

public class Server : MonoBehaviour {
	public int PORT = 6321;

	//list of clients connect to the server
	public List<ServerClient> clients;
	//list of clients disconnect to the server
	public List<ServerClient> disconnnectList;
	//tcp server
	private TcpListener server;
	private bool serverStarted;

	/// <summary>
	///  Use this for initialization
	/// </summary>
	void Start () {
		clients = new List<ServerClient> ();
		disconnnectList = new List<ServerClient> ();

		try {
			server = new TcpListener(IPAddress.Any, PORT);
			server.Start();

			StartListening();
			serverStarted = true;

			Debug.Log("Server has been started on port " + PORT.ToString());

		}catch (Exception ex) {
			Debug.Log ("Socket error : " + ex.Message);
		}
	}

	/// <summary>
	///  implements a reader that reads characters from a byte stream in a particular encoding.
	///  update disconnecting users' list
	/// </summary>
	void Update() {
		if (!serverStarted) {
			return;
		}
		foreach (ServerClient c in clients) {
			//is the client still connected?
			if (!isConnected (c.tcp)) {
				c.tcp.Close ();
				disconnnectList.Add (c);
				continue;
			}
			//check for message from the client
			else {
				NetworkStream s = c.tcp.GetStream ();
				if (s.DataAvailable) {
					StreamReader reader = new StreamReader (s, true);
					string data = reader.ReadLine ();

					if (data != null) {
						OnIncomingData (c, data);
					}
				}
			}
		}

		// dealing with disconnected user
		for (int i = 0; i < disconnnectList.Count - 1; i++) {
			Broadcast (disconnnectList[i].clientName + " has disconnected", clients);

			clients.Remove (disconnnectList [i]);
			disconnnectList.RemoveAt (i);
		}
	}

	/// <summary>
	/// parse data into that we need
	/// </summary>
	private void OnIncomingData(ServerClient c, string data) {
		if (data.Contains ("&NAME")) {
			c.clientName = data.Split ('|') [1];
			Broadcast (c.clientName + " has connected", clients);
			return;
		}
		Broadcast (c.clientName + " : " + data, clients);
	}

	/// <summary>
	/// Implements a Writer for writing characters to a stream in a particular encoding.
	/// </summary>
	private void Broadcast(string data, List<ServerClient> cl) {
		foreach (ServerClient c in cl) {
			try {
				StreamWriter writer = new StreamWriter(c.tcp.GetStream());
				writer.WriteLine(data);
				writer.Flush();
			}
			catch (Exception e) {
				Debug.Log ("Writer error : " + e.Message + " to client");
			}
		}
	}

	/// <summary>
	///  check whether or not a client is connected
	/// </summary>
	private bool isConnected(TcpClient c) {
		try {
			if(c != null && c.Client != null && c.Client.Connected ) {
				if(c.Client.Poll(0, SelectMode.SelectRead)) 
					return !(c.Client.Receive(new byte[1], SocketFlags.Peek) == 0);
				return true;
			}else 
				return false;
		}catch {
			return false;
		}
	}

	/// <summary>
	/// Begins an asynchronous operation to accept an incoming connection attempt.
	/// </summary>
	private void StartListening() {
		server.BeginAcceptTcpClient (AcceptTcpClient, server);
	}

	/// <summary>
	/// Asynchronously accepts an incoming connection attempt 
	/// and creates a new TcpClient to handle remote host communication.
	/// </summary>
	private void AcceptTcpClient(IAsyncResult ar) {
		TcpListener listener = (TcpListener)ar.AsyncState;

		clients.Add (new ServerClient (listener.EndAcceptTcpClient (ar)));
		StartListening ();

		//semd a message to everyone, say someone has connected
		Broadcast("%NAME", new List<ServerClient>() {clients[clients.Count - 1]});
	}

	/*
	void TCPServerConnect(string server, int port, string message)  {

		{
			try 
			{
				// Create a TcpClient.
				// Note, for this client to work you need to have a TcpServer 
				// connected to the same address as specified by the server, port
				// combination.

				TcpClient client = new TcpClient(server, port);

				// Translate the passed message into ASCII and store it as a Byte array.
				Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);         

				// Get a client stream for reading and writing.
				//  Stream stream = client.GetStream();

				NetworkStream stream = client.GetStream();

				// Send the message to the connected TcpServer. 
				stream.Write(data, 0, data.Length);

				Console.WriteLine("Sent: {0}", message);         

				// Receive the TcpServer.response.

				// Buffer to store the response bytes.
				data = new Byte[256];

				// String to store the response ASCII representation.
				String responseData = String.Empty;

				// Read the first batch of the TcpServer response bytes.
				Int32 bytes = stream.Read(data, 0, data.Length);
				responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
				Console.WriteLine("Received: {0}", responseData);         

				// Close everything.
				stream.Close();         
				client.Close();         
			} 
			catch (ArgumentNullException e) 
			{
				Console.WriteLine("ArgumentNullException: {0}", e);
			} 
			catch (SocketException e) 
			{
				Console.WriteLine("SocketException: {0}", e);
			}

			Console.WriteLine("\n Press Enter to continue...");
			Console.Read();
		}
	}
	*/
}

/// <summary>
/// client object to the server
/// </summary>
public class ServerClient {
	public TcpClient tcp;
	public string clientName;

	public ServerClient(TcpClient clientScoket) {
		clientName = "Gisu Kim";
		tcp = clientScoket;
	}
}
