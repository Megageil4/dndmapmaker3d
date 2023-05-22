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
        // spawner.TestConn(mapData);
        
        DataContainer.Conn.SendMap(Util.MeshToMapData(mesh,
            meshSpawner.GetComponent<PlaneSpawner>().sizeX,
            meshSpawner.GetComponent<PlaneSpawner>().sizeY));
        
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

    public void GameObjectsIntoDict()
    {
        foreach (var jkGameObject in DataContainer.Conn.GetGameObjects())
        {
            if (!DataContainer.GameObjects.ContainsKey(jkGameObject.Guid))
            {
                GameObject newObject = Instantiate(ObjectController.ModelTypes[jkGameObject.Modeltype]);
                DataContainer.GameObjects.Add(jkGameObject.Guid, newObject);
                DataContainer.Guids.Add(newObject,jkGameObject.Guid);
            }
            DataContainer.GameObjects[jkGameObject.Guid].transform.position = new Vector3(jkGameObject.pos3[0], jkGameObject.pos3[1], jkGameObject.pos3[2]);
            DataContainer.GameObjects[jkGameObject.Guid].transform.Rotate(new Vector3(jkGameObject.rot3[0], jkGameObject.rot3[1], jkGameObject.rot3[2]));
            DataContainer.GameObjects[jkGameObject.Guid].transform.localScale = new Vector3(jkGameObject.scale3[0], jkGameObject.scale3[1], jkGameObject.scale3[2]);
            
            int r = Convert.ToInt32(jkGameObject.Color.Substring(0,2),16);
            int g = Convert.ToInt32(jkGameObject.Color.Substring(2,2),16);
            int b = Convert.ToInt32(jkGameObject.Color.Substring(4,2),16);
            
            DataContainer.GameObjects[jkGameObject.Guid].transform.GetChild(0).GetChild(0)
                .GetComponent<MeshRenderer>().material.color = new Color(r/255f,g/255f,b/255f);
        }
    }

    private int _count = 0;
    private void FixedUpdate()
    {
        if (_count >= 15)
        {
            _count = 0;
            GameObjectsIntoDict();
            var map = DataContainer.Conn.OnConnectMap();
            MapFromMapData(map);
        }
        _count++;
    }
}
