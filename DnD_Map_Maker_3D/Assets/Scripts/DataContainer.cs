using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataContainer
{
    public static string ServerIP { get; set; }

    static DataContainer()
    {
        ServerIP = "10.0.207.3";
    }
}
