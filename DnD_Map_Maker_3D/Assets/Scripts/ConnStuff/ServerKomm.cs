using System.IO;
using System.Net;
using System.Text;
using DefaultNamespace;
using Newtonsoft.Json;
using UnityEngine;

public static class ServerKomm
{
    public static void TellServer(GameObject meshSpawner)
    {
        var request = (HttpWebRequest)WebRequest.Create($"http://{DataContainer.ServerIP}:5180/GameObject/MapChange");
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

        var json = JsonConvert.SerializeObject(mapData);

        var data = Encoding.UTF8.GetBytes(json);

        request.Method = "POST";
        request.ContentType = "application/json";
        request.ContentLength = data.Length;
        request.Proxy = null!;

        using var stream = request.GetRequestStream();
        stream.Write(data, 0, data.Length);
    }

    public static string FetchMap()
    {
        var request = (HttpWebRequest)WebRequest.Create($"http://{DataContainer.ServerIP}:5180/GameObject/GetMap");
        request.Method = "GET";
        request.Proxy = null!;
        using var re = new StreamReader(request.GetResponse().GetResponseStream()!).ReadToEndAsync();
        return re.Result;
    }

    public static bool ExistsMap()
    {
        var request = (HttpWebRequest)WebRequest.Create($"http://{DataContainer.ServerIP}:5180/GameObject/ExistsMap");
        request.Method = "GET";
        request.Proxy = null!;
        using var v = new StreamReader(request.GetResponse().GetResponseStream()!).ReadToEndAsync();
        return v.Result == "true";
    }
}
