using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

	public string IP = "127.0.0.1";
	public int PORT = 25001;

	void OnGUI(){
		if (Network.peerType == NetworkPeerType.Disconnected) {
			if (GUI.Button (new Rect (100, 100, 100, 25), "Start Client")) {
				Network.Connect (IP, PORT);
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
	}
}
