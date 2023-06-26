using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Class for the menu bar at the top of the screen
/// Gets sometimes misused for server communication to make things easier
/// </summary>
public class MenuBarController : MonoBehaviour
{
    /// <summary>
    /// The canvas for the grid size change menu pop up
    /// </summary>
    [FormerlySerializedAs("GridSizePopUp")] public GameObject gridSizePopUp;
    /// <summary>
    /// The input field for the x size of the grid of above mentioned pop up
    /// </summary>
    [FormerlySerializedAs("XSize")] public TMP_InputField xSize;
    /// <summary>
    /// The input field for the Z(y) size of the grid of above mentioned pop up
    /// </summary>
    [FormerlySerializedAs("ZSize")] public TMP_InputField zSize;
    /// <summary>
    /// The mesh spawner used to generate the map
    /// </summary>
    [FormerlySerializedAs("MeshSpawner")] public GameObject meshSpawner;
    
    /// <summary>
    /// Gets called when OK button in the grid size pop up gets clicked.
    /// Used to change the grid size according to the input fields 
    /// </summary>
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
        
        // DataContainer.Conn.SendMap(Util.MeshToMapData(mesh,
        //     meshSpawner.GetComponent<PlaneSpawner>().sizeX,
        //     meshSpawner.GetComponent<PlaneSpawner>().sizeY));
        
        gridSizePopUp.SetActive(false);
    }
    /// <summary>
    /// Gets called when the "Change Grid Size" button gets clicked.
    /// Makes the pop up visible
    /// </summary>
    public void MakeGridSizePopUpVisible()
    {
        gridSizePopUp.SetActive(true);
    }

    /// <summary>
    /// Gets called by the map update cycle.
    /// Makes a new Map from the given map data.
    /// </summary>
    public void MapFromMapData()
    {
        meshSpawner.GetComponent<PlaneSpawner>().SetNewMap(DataContainer.Conn.FetchMap(),false);
    }

    public void FixCamaraRotation(Camera playerCamera)
    {
        // reset the misaligned camera rotation
        var cameraTransform = playerCamera.transform;
        var rotation = cameraTransform.rotation;
        var rotationEulerAngles = rotation.eulerAngles;
        rotationEulerAngles.z = 0;
        rotation.eulerAngles = rotationEulerAngles;
        cameraTransform.rotation = rotation;
    }
    
    /// <summary>
    /// DECPRECATED - Was used to send the map to the server on exit.
    /// Only here for reference
    /// </summary>
    public void OnExit()
    {
        var mesh = meshSpawner.GetComponent<MeshFilter>().mesh;

        MapData mapData = new()
        {
            triangles = mesh.triangles,
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
        Application.Quit();
    }

    /// <summary>
    /// Called by the map update cycle.
    /// Gets the maps from the WS and instantiates the gameobjects
    /// and adds them into the dictionary for managing all gameobjects.
    /// </summary>
    public void GameObjectsIntoDict()
    {
        foreach (var jkGameObject in DataContainer.Conn.GetGameObjects())
        {
            if (jkGameObject == null)
            {
                Debug.Log("jkGameObject is null");
                continue;
            }

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
            
            Debug.Log(jkGameObject.Modeltype);
            Debug.Log("Object GUID :" +jkGameObject.Guid);


            DataContainer.GameObjects[jkGameObject.Guid].transform.GetChild(0).GetChild(0)
                .GetComponent<MeshRenderer>().material.color = new Color(r/255f,g/255f,b/255f);
        }
    }
    // private int _count;
    // private int _count = 0;
    // private void FixedUpdate()
    // {
    //     if (_count >= 50)
    //     {
    //         _count = 0;
    //         GameObjectsIntoDict();
    //         var map = DataContainer.Conn.FetchMap();
    //         MapFromMapData(map);
    //     }
    //     _count++;
    // }
}
