using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIOClient;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

public class SocketHandler : MonoBehaviour
{

    static SocketIO client = new SocketIO("http://localhost:8080/", new SocketIOOptions
    {
        Reconnection = false
    });
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        client.On("message", response =>
        {
            
        });
        client.On("package", response =>
        {
            Debug.Log("Message received: " + response);
            PackageHandler.packageList.Add(response);
        });
    }

    public void ServerConnect()
    {
        if (!client.Connected)
        {
            client.ConnectAsync();
        }
        else
        {
            Debug.Log("You are already connected to the server.");
        }
    }

    public void ServerDisconnect()
    {
        if (client.Connected)
        {
            client.DisconnectAsync();
        }
        else
        {
            Debug.Log("You are not connected to the server.");
        }
    }

    public static async void PackageSend(string message)
    {
        await client.EmitAsync("request", message);
    }
    







}
