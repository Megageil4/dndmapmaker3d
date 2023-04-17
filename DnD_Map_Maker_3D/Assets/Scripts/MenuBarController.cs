using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class MenuBarController : MonoBehaviour
{
    [FormerlySerializedAs("GridSizePopUp")] public GameObject gridSizePopUp;
    [FormerlySerializedAs("XSize")] public TMP_InputField xSize;
    [FormerlySerializedAs("ZSize")] public TMP_InputField zSize;
    [FormerlySerializedAs("MeshSpawner")] public GameObject meshSpawner;
    public void ChangeGridSize()
    {
        if (xSize == null || xSize.text == "" || !int.TryParse(xSize.text, out _) 
            || zSize == null || zSize.text == "" || !int.TryParse(zSize.text, out _))
        {
            return;
        }

        PlaneSpawner spawner = meshSpawner.GetComponent<PlaneSpawner>();
        spawner.sizeX = Convert.ToInt32(xSize.text);
        spawner.sizeY = Convert.ToInt32(zSize.text);
        spawner.RegenerateMeshFromStart();

        var mesh = meshSpawner.GetComponent<MeshFilter>().mesh;

        MapData mapData = new()
        {
            Triangles = mesh.triangles,
            Vertices = new()
        };
        foreach (var vertex in mesh.vertices)
        {
            mapData.Vertices.Add(new []{vertex.x,vertex.y,vertex.z});
        }

        mapData.sizeX = meshSpawner.GetComponent<PlaneSpawner>().sizeX;
        mapData.sizeY = meshSpawner.GetComponent<PlaneSpawner>().sizeY;
        DataContainer.Conn.SendMap(mapData);
        
        gridSizePopUp.SetActive(false);
    }
    public void MakeGridSizePopUpVisible()
    {
        gridSizePopUp.SetActive(true);
    }

    public void MapFromMapData(MapData map)
    {
        meshSpawner.GetComponent<PlaneSpawner>().SetNewMap(map);
    }

    public void OnExit()
    {
        var mesh = meshSpawner.GetComponent<MeshFilter>().mesh;

        MapData mapData = new()
        {
            Triangles = mesh.triangles,
            Vertices = new()
        };
        foreach (var vertex in mesh.vertices)
        {
            mapData.Vertices.Add(new []{vertex.x,vertex.y,vertex.z});
        }

        mapData.sizeX = meshSpawner.GetComponent<PlaneSpawner>().sizeX;
        mapData.sizeY = meshSpawner.GetComponent<PlaneSpawner>().sizeY;
        DataContainer.Conn.SendMap(mapData);
        // ServerKomm.TellServer(meshSpawner);
        DataContainer.Conn.Dispose();
        Application.Quit();
    }
}
