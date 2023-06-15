using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

/// <summary>
/// Class that contains all the data that needs to be accessed from multiple scripts
/// </summary>
public class DataContainer
{
    /// <summary>
    /// the process of the websocket connection
    /// </summary>
    public static Process WebsocetConn;
    /// <summary>
    /// The server ip that is used to connect to the server
    /// </summary>
    public static string ServerIP { get; set; }
    /// <summary>
    /// The connection object that is used to communicate with the server
    /// </summary>
    public static IDnDConnection Conn { get; private set; }
    public static Dictionary<Guid, GameObject> GameObjects;
    public static Dictionary<GameObject, Guid> Guids;
    private static Guid _clientID;

    public static Guid ClientId
    {
        get => _clientID;
        set { _clientID = value; Debug.Log(_clientID); }
    }

    static DataContainer()
    {
        GameObjects = new();
        Guids = new();
        ServerIP = "10.0.207.3";
    }
    
    public static void CreateConn(IDnDConnection con)
    {
        Conn = con;
        Conn.Connect();
    }
}