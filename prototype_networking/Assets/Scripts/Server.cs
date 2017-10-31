using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.IO;

public class Server : MonoBehaviour {
	//public string server = "127.0.0.1";
	public int PORT = 6321;
	public List<ServerClient> clients;
	public List<ServerClient> disconnnectList;
	private TcpListener server;
	private bool serverStarted;


	// Use this for initialization
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

	private void OnIncomingData(ServerClient c, string data) {
		if (data.Contains ("&NAME")) {
			c.clientName = data.Split ('|') [1];
			Broadcast (c.clientName + " has connected", clients);
			return;
		}
		Broadcast (c.clientName + " : " + data, clients);
	}

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

	private void StartListening() {
		server.BeginAcceptTcpClient (AcceptTcpClient, server);
	}

	private void AcceptTcpClient(IAsyncResult ar) {
		TcpListener listener = (TcpListener)ar.AsyncState;

		clients.Add (new ServerClient (listener.EndAcceptTcpClient (ar)));
		StartListening ();

		//semd a message to everyone, say someone has connected
		Broadcast("%NAME", new List<ServerClient>() {clients[clients.Count - 1]});
	}

	void OnGUI(){
		/*
		if (Network.peerType == NetworkPeerType.Disconnected) {
			if (GUI.Button (new Rect (100, 100, 100, 25), "Start Client")) {
				Network.Connect (server, PORT);
			}

			if (GUI.Button (new Rect (100, 125, 100, 25), "Start Server")) {
				Network.InitializeServer (10, PORT);

			}
		} else {
			if (Network.peerType == NetworkPeerType.Client) {
				GUI.Label (new Rect(100,100,100,25), "Client");

				if (GUI.Button (new Rect (100, 125, 100, 25), "Logout")) {
					Network.Disconnect (250);
				}
			}

			if (Network.peerType == NetworkPeerType.Server) {
				GUI.Label (new Rect(100,100,100,25), "Server");
				GUI.Label (new Rect(100,125,100,25), "Connetions: " + Network.connections.Length);

				if (GUI.Button (new Rect (100, 150, 100, 25), "Logout")) {
					Network.Disconnect (250);
				}
			}
		}
		*/


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

public class ServerClient {
	public TcpClient tcp;
	public string clientName;

	public ServerClient(TcpClient clientScoket) {
		clientName = "Gisu Kim";
		tcp = clientScoket;
	}
}
