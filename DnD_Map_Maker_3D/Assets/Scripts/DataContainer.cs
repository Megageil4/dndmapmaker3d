using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataContainer
{
    public static string ServerIP { get; set; }
    public static IDnDConnection Conn { get; private set; }
    public static Dictionary<Guid, GameObject> GameObjects;
    public static Dictionary<GameObject, Guid> Guids;

    static DataContainer()
    {
        GameObjects = new();
        Guids = new();
        ServerIP = "10.0.207.3";
    }
    
    public static void CreateConn(IDnDConnection con)
    {
        Conn = con;
    }
}
