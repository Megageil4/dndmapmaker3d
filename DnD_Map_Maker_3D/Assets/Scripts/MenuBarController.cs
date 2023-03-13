using System;
using System.Net.Http;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class MenuBarController : MonoBehaviour
{
    public GameObject GridSizePopUp;
    public TMP_InputField XSize;
    public TMP_InputField ZSize;
    public GameObject MeshSpawner;
    private static readonly HttpClient client = new HttpClient();
    public void ChangeGridSize()
    {
        if (XSize == null || XSize.text == "" || !int.TryParse(XSize.text, out _) 
            || ZSize == null || ZSize.text == "" || !int.TryParse(ZSize.text, out _))
        {
            return;
        }

        PlaneSpawner _spawner = MeshSpawner.GetComponent<PlaneSpawner>();
        _spawner.sizeX = Convert.ToInt32(XSize.text);
        _spawner.sizeY = Convert.ToInt32(ZSize.text);
        _spawner.RegenerateMeshFromStart();

        ServerKomm.TellServer(MeshSpawner);
        
        GridSizePopUp.SetActive(false);
    }
    public void MakeGridSizePopUpVisible()
    {
        GridSizePopUp.SetActive(true);
    }

    public void MapFromMapData(MapData map)
    {
        MeshSpawner.GetComponent<PlaneSpawner>().setNewMap(map);
    }
}
