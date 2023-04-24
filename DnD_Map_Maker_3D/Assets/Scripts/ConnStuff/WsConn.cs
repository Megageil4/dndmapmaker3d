using System;
using System.Collections;
using System.Collections.Generic;
using ConnStuff;
using DefaultNamespace;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class WsConn : MonoBehaviour, IDnDConnection
{
    private string _ergebniss;
    public void SendMap(MapData map)
    {
        var json = JsonConvert.SerializeObject(map);
        StartCoroutine(PostRequest($"http://{DataContainer.ServerIP}:5180/GameObject/MapChange", json));
    }

    public void AddGameObject(GameObject gameObject)
    {
        JKGameObject jkGameObject = new JKGameObject(gameObject);
        DataContainer.GameObjects.Add(jkGameObject.Guid, gameObject);
        var json = JsonConvert.SerializeObject(jkGameObject);
        StartCoroutine(PostRequest($"http://{DataContainer.ServerIP}:5180/GameObject/PostChange", json));
    }

    public List<JKGameObject> GetGameObjects()
    {
        Getter($"http://{DataContainer.ServerIP}:5180/GameObject/GetAll");
        return JsonConvert.DeserializeObject<List<JKGameObject>>(_ergebniss);
    }

    public bool MapExists()
    {
        foreach (var coroutine in Getter($"http://{DataContainer.ServerIP}:5180/GameObject/ExistsMap"));
        Debug.Log(_ergebniss);
        return  _ergebniss == "true";
    }
    

    public List<GameObject> OnConnectGO()
    {
        throw new NotImplementedException();
    }

    public MapData OnConnectMap()
    {
        Getter($"http://{DataContainer.ServerIP}:5180/GameObject/GetMap");
        
        return JsonConvert.DeserializeObject<MapData>(_ergebniss);
    }

    private IEnumerable<Coroutine> Getter(string url)
    {
        yield return StartCoroutine(GetRequest(url));
    }

    public bool Connected()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
    
    public void TestConn(MapData mapData)
    {
        var json = JsonConvert.SerializeObject(mapData);
        StartCoroutine(PostRequest($"http://{DataContainer.ServerIP}:5180/GameObject/MapChange", json));
        StartCoroutine(GetRequest($"http://{DataContainer.ServerIP}:5180/GameObject/GetMap"));
    }

    IEnumerator PostRequest(string url, string json)
    {
        Debug.Log($"Sending data to {url}");
        using UnityWebRequest www = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log($"Error while Sending: {www.error}");
        }

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Upload complete!");
            
        }
        
    }

    IEnumerator GetRequest(string url)
    {
        // Debug.Log($"Sending data to {url}");
        using UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log($"Error while receiving: {www.error}");
        }

        if (www.result == UnityWebRequest.Result.Success)
        {
            // Debug.Log("Downloaded data!");
            _ergebniss = www.downloadHandler.text;
            Debug.Log(_ergebniss);
        }
    }
}
