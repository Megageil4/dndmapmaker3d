using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using DefaultNamespace;
using Newtonsoft.Json;
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

            tellServer();
        
        GridSizePopUp.SetActive(false);
    }
    public void MakeGridSizePopUpVisible()
    {
        GridSizePopUp.SetActive(true);
    }

    private void tellServer()
    {
        var request = (HttpWebRequest)WebRequest.Create($"http://{DataContainer.ServerIP}:5180/GameObject/MapChange");
        var mesh = MeshSpawner.GetComponent<MeshFilter>().mesh;

        MapData mapData = new MapData();
        mapData.Triangles = mesh.triangles;
        mapData.Vertices = new();
        foreach (var vertex in mesh.vertices)
        {
            mapData.Vertices.Add(new []{vertex.x,vertex.y,vertex.z});
        }

        var json = JsonConvert.SerializeObject(mapData);

        var data = Encoding.UTF8.GetBytes(json);

        request.Method = "POST";
        request.ContentType = "application/json";
        request.ContentLength = data.Length;
        request.Proxy = null;

        using (var stream = request.GetRequestStream())
        {
            stream.Write(data, 0, data.Length);
        }
    }
}
