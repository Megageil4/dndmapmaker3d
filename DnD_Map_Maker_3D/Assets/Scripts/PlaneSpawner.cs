using System;
using System.Collections;
using System.Text;
using DefaultNamespace;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

public class PlaneSpawner : MonoBehaviour
{
    public int sizeX;
    public int sizeY;
    private Mesh _mesh;
    [FormerlySerializedAs("Vertices")] public Vector3[] vertices;
    private int[] _triangles;
    private Vector2[] _uvs;
    void Start()
    {
        //RegenerateMeshFromStart();
    }

    public void RegenerateMeshFromStart()
    {
        CreateVertices();
        CreateTriangles();
        GenerateUVs();
        ReloadMesh();
    }
    
    public void ReloadMesh()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;
        _mesh.vertices = vertices;
        _mesh.triangles = _triangles;
        GenerateUVs();
        _mesh.uv = _uvs;
        _mesh.RecalculateNormals();
        GetComponent<MeshCollider>().sharedMesh = _mesh;
    }

    private void CreateVertices()
    {
        vertices = new Vector3[(sizeX + 1) * (sizeY + 1)];
        int i = 0;
        for (int y = 0; y <= sizeY; y++)
        {
            for (int x = 0; x <= sizeX; x++)
            {
                vertices[i] = new Vector3(x, 0, y);
                i++;
            }
        }
    }
    
    
    private void CreateTriangles()
    {
        _triangles = new int[sizeX * sizeY * 6];
        int vert = 0; 
        int tri = 0;
        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                _triangles[tri + 0] = vert + 0;
                _triangles[tri + 1] = vert + sizeX + 1;
                _triangles[tri + 2] = vert + 1;
            
                _triangles[tri + 3] = vert + 1;
                _triangles[tri + 4] = vert + sizeX + 1;
                _triangles[tri + 5] = vert + sizeX + 2;

                tri += 6;
                vert++;
            }

            vert++;
        }
    }

    private void GenerateUVs()
    {
        _uvs = new Vector2[vertices.Length];
        int i = 0;
        for (int y = 0; y <= sizeY; y++)
        {
            for (int x = 0; x <= sizeX; x++)
            {
                _uvs[i] = new Vector2(x % 2, y % 2);
                i++;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (vertices != null)
        {
            for (int i = 0; i < vertices.GetLength(0); i++)
            {
                Gizmos.DrawSphere(vertices[i],0.1f);
            }
        }
    }
    public void SetNewMap(MapData map)
    {
        sizeX = map.sizeX;
        sizeY = map.sizeY;
        vertices = new Vector3[(sizeX + 1) * (sizeY + 1)];
        int i = 0;
        foreach (var v in map.Vertices)
        {
            Debug.Log(i);
            vertices[i] = new Vector3(v[0], v[1], v[2]);
            i++;
        }
        _triangles = map.Triangles;
        ReloadMesh();
    }

    public void TestConn(MapData mapData)
    {
        var json = JsonConvert.SerializeObject(mapData);
        StartCoroutine(PostRequest($"http://{DataContainer.ServerIP}:5180/GameObject/MapChange",json));
        StartCoroutine(GetRequest($"http://{DataContainer.ServerIP}:5180/GameObject/GetMap",s => Debug.Log(s)));
    }
    IEnumerator PostRequest(string url, string json, Action<string> finishDelegate = null)
    {
        Debug.Log($"Sending data to {url}");
        using (UnityWebRequest www = new UnityWebRequest(url,"POST"))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log($"Error while Sending: {www.error}");
            }

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Upload complete!");
                if (finishDelegate != null)
                {
                    finishDelegate(www.downloadHandler.text);
                }
            }
        }
    }
    
    IEnumerator GetRequest(string url, Action<string> finishDelegate)
    {
        Debug.Log($"Sending data to {url}");
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log($"Error while receiving: {www.error}");
            }

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Downloaded data!");
                if (finishDelegate != null)
                {
                    finishDelegate(www.downloadHandler.text);
                }
            }
        }
    }
}
