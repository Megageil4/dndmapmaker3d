
using System;
using System.Collections.Generic;
using UnityEngine;

public class DataContainer
{
    public static string ServerIP { get; set; }
    public static IDnDConnection Conn { get; private set; }
    public static Dictionary<Guid, GameObject> GameObjects;

    static DataContainer()
    {
        GameObjects = new();
        ServerIP = "10.0.207.3";
    }
    
    public static void CreateConn(IDnDConnection con)
    {
        Conn = con;
    }
}
