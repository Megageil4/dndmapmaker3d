using System;
using System.Net.Http;
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
    private static readonly HttpClient client = new HttpClient();
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

        ServerKomm.TellServer(meshSpawner);
        
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

    public void onExit()
    {
        ServerKomm.TellServer(meshSpawner);
        Application.Quit();
    }
}
