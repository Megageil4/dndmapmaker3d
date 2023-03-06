using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Toolbar : MonoBehaviour
{
    public PlaneSpawner PlaneSpawner;
    public static IMapTool MapTool;
    public void onErhoehen()
    {
        MapTool = new Extrude(PlaneSpawner);
    }

    public void onErniedrigen()
    {
        MapTool = new Intrude(PlaneSpawner);
    }
}
