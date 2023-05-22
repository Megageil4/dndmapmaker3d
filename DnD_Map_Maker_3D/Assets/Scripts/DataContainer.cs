using System;
using System.Collections.Generic;
using UnityEngine;

public static class DataContainer
{
    public static string ServerIP { get; set; }
    public static IDnDConnection Conn { get; private set; }
    public static Dictionary<Guid, GameObject> GameObjects;
    private static Guid _clientID;

    public static Guid ClientId
    {
        get => _clientID;
        set { _clientID = value; Debug.Log(_clientID); }
    }

    static DataContainer()
    {
        GameObjects = new();
        ServerIP = "10.0.207.7";
    }
    
    public static void CreateConn(IDnDConnection con)
    {
        Conn = con;
        Conn.Connect();
    }
}