
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
        ServerIP = "localhost";
    }
    
    public static void CreateConn(IDnDConnection con)
    {
        Conn = con;
    }
}
