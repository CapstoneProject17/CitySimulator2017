using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Sockets;
using System.IO;
using System.Text;

/// <summary>
/// Name:       CitySimulator 
/// Author:     Gisu Kim A00959494
/// Date:       2017-10-02
/// Updated by: 2017-10-05
/// Updated by: 2017-10-16
/// Updated by: 2017-10-17
/// Updated by: 2017-10-28
/// Updated by: 2017-10-31
/// Updated by: 2017-11-02
/// Updated by: 2017-11-04
/// What the superviosr should know: N/A
/// </summary>

public class Client : MonoBehaviour
{
    public GameObject chatContainer;
    public GameObject messagePrefab;

    public string clientName;

    private bool socketReady = false;
    private TcpClient socket;
    private NetworkStream stream;
    public StringBuilder sb = new StringBuilder();


    public void ConnectToServer()
    {
        if (socketReady)
        {
            return;
        }

        //default host / port values
        string host = "127.0.0.1";
        int port = 6321;

        //overwrite default host / port values, if there is something in thoese boxes
        string h;
        int p;

        h = GameObject.Find("HostInput").GetComponent<InputField>().text;
        if (h != "")
            host = h;
        int.TryParse(GameObject.Find("PortInput").GetComponent<InputField>().text, out p);
        if (p != 0)
        {
            port = p;
        }

        //cresate the socket
        try
        {
            socket = new TcpClient(host, port);
            stream = socket.GetStream();
            socketReady = true;
        }
        catch (Exception e)
        {
            Debug.Log("Socket errror: " + e.Message);
        }
    }

	/// <summary>
	///  implements a reader that reads characters from a byte stream in a particular encoding.
	/// </summary>
    private void Update()
    {
		if (socketReady)
		{
            if (stream.DataAvailable)
            {
                byte[] buffer = new byte[1024];
                int data = stream.Read(buffer, 0, 1024);
                if (data != 0)
                {
                    sb.Append(Encoding.ASCII.GetString(
                        buffer, 0, data));
                    OnIncomingData(sb.ToString());
                }
                stream.Flush();
            }
        }
    }


    private void OnIncomingData(string data)
    {	
//        if (data == "%NAME")
//        {
//            Send("&NAME| " + clientName);
//            return;
//        }

		//detect data and parse into an object
		DataManager.ToObject(data);


        GameObject go = Instantiate(messagePrefab, chatContainer.transform);
        go.GetComponentInChildren<Text>().text = data;
    }

	/// <summary>
	/// Implements a Writer for writing characters to a stream in a particular encoding.
	/// </summary
	/// <param name="data">Json type of data we want to sent to the server </param>
    private void Send(string data)
    {
        if (!socketReady)
        {
            Debug.Log("Socket not ready");
            Console.WriteLine("Socket not ready");
            return;
        }
	
        byte[] dataToSend = Encoding.ASCII.GetBytes(data);
        stream.Write(dataToSend, 0, dataToSend.Length);
        stream.Flush();

        Debug.Log("data sent");
    }

	/// <summary>
	/// Raises the send button event.
	/// </summary>
    public void OnSendBtn()
    {
        string message = GameObject.Find("SendInput").GetComponent<InputField>().text;
        Send(message);
    }

	/// <summary>
	/// Closes the socket.
	/// </summary>
	private void CloseSocket() {
		if (!socketReady)
			return;
		
		stream.Close ();
		socket.Close ();
		socketReady = false;
	}
	/// <summary>
	/// Raises the application quit event.
	/// </summary>
	private void OnApplicationQuit() {
		CloseSocket ();
	}

	/// <summary>
	/// Raises the disable event.
	/// </summary>
	private void OnDisable() {
		CloseSocket ();
	}
}


