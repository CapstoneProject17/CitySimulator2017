using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Sockets;
using System.IO;
using System.Text;

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

        //overwrite default host / port valies, if there is something in thoese boxes
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
        if (data == "%NAME")
        {
            Send("&NAME| " + clientName);
            return;
        }

        GameObject go = Instantiate(messagePrefab, chatContainer.transform);
        go.GetComponentInChildren<Text>().text = data;
    }

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

    public void OnSendBtn()
    {
        string message = GameObject.Find("SendInput").GetComponent<InputField>().text;
        Send(message);
    }
}


