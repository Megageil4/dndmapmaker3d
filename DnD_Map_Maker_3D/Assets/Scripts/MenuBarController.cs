using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
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
        mapData.Vertices = new float[mesh.vertices.GetLength(0),3];
        int i = 0;
        foreach (var vertex in mesh.vertices)
        {
            mapData.Vertices[i,0] = vertex.x;
            mapData.Vertices[i,1] = vertex.y;
            mapData.Vertices[i,2] = vertex.z;
            i++;
        }

        var json = JsonUtility.ToJson(mapData);

        var data = Encoding.UTF8.GetBytes(json);

        request.Method = "POST";
        request.ContentType = "application/json";
        request.ContentLength = data.Length;
        request.Proxy = null;

        using (var stream = request.GetRequestStream())
        {
            stream.Write(data, 0, data.Length);
        }

         var response = (HttpWebResponse)request.GetResponse();

        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
    }
}
