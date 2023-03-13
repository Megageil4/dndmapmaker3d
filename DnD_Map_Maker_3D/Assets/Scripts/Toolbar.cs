using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class Toolbar : MonoBehaviour
{
    public PlaneSpawner PlaneSpawner;
    public static IMapTool MapTool;
    public List<Button> buttons;
    public void onErhoehen()
    {
        MapTool = new Extrude(PlaneSpawner);
    }

    public void onErniedrigen()
    {
        MapTool = new Intrude(PlaneSpawner);
    }
}
