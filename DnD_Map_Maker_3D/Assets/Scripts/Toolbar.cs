using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Toolbar : MonoBehaviour
{
    [FormerlySerializedAs("PlaneSpawner")] public PlaneSpawner planeSpawner;
    public static IMapTool MapTool;
    public List<Button> buttons;
    public void OnErhoehen()
    {
        MapTool = new Extrude(planeSpawner);
    }

    public void OnErniedrigen()
    {
        MapTool = new Intrude(planeSpawner);
    }
}
