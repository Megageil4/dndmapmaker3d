using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using ConnStuff;
using DefaultNamespace;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class WsConn : MonoBehaviour, IDnDConnection
{
   // private WebSocketClient _socketClient;
    public void SendMap(MapData map)
    {
        var json = JsonConvert.SerializeObject(map);
        StartCoroutine(PostRequest($"http://{DataContainer.ServerIP}:5180/DnD/Map", json, "POST"));
    }

    public void AddGameObject(GameObject gameObject)
    {
        JKGameObject jkGameObject = new JKGameObject(gameObject);
        DataContainer.GameObjects.Add(jkGameObject.Guid, gameObject);
        DataContainer.Guids.Add(gameObject,jkGameObject.Guid);
        var json = JsonConvert.SerializeObject(jkGameObject);
        Debug.Log(json);
        StartCoroutine(PostRequest($"http://{DataContainer.ServerIP}:5180/DnD/GameObject", json, "POST"));
    }

    public void ChangeGameObject(Guid guid)
    {
        JKGameObject jkGameObject = new JKGameObject(DataContainer.GameObjects[guid], guid);
        var json = JsonConvert.SerializeObject(jkGameObject);
        StartCoroutine(PostRequest($"http://{DataContainer.ServerIP}:5180/DnD/GameObject", json, "PUT"));
    }

    public List<JKGameObject> GetGameObjects()
    {
        var request = (HttpWebRequest)WebRequest.Create($"http://{DataContainer.ServerIP}:5180/DnD/GameObject");
        request.Method = "GET";
        request.Proxy = null!;
        using var v = new StreamReader(request.GetResponse().GetResponseStream()!).ReadToEndAsync();
        return JsonConvert.DeserializeObject<List<JKGameObject>>(v.Result);
    }

    public bool MapExists()
    {
        var request =
            (HttpWebRequest)WebRequest.Create($"http://{DataContainer.ServerIP}:5180/DnD/Map/Exists");
        request.Method = "GET";
        request.Proxy = null!;
        using var v = new StreamReader(request.GetResponse().GetResponseStream()!).ReadToEndAsync();
        Thread.Sleep(10);
        return v.Result == "true";
    }

    public MapData OnConnectMap()
    {
        var request = (HttpWebRequest)WebRequest.Create($"http://{DataContainer.ServerIP}:5180/DnD/Map");
        request.Method = "GET";
        request.Proxy = null!;
        using var v = new StreamReader(request.GetResponse().GetResponseStream()!).ReadToEndAsync();
        return JsonConvert.DeserializeObject<MapData>(v.Result);
    }

    public void Connect()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    // private IEnumerable<Coroutine> Getter(string url)
    // {
    //     yield return StartCoroutine(GetRequest(url));
    // }

/*    public void Connect()
    {
        _socketClient = new();
        _socketClient.NewMap += (_,_) => OnConnectMap();
        _socketClient.NewGameObject += (_, _) => GetGameObjects();
        _socketClient.NewGuid += (_, y) => DataContainer.ClientId = y.Id;
        _socketClient?.Connect($"ws://{DataContainer.ServerIP}:5020/ws");
    }

    public void Dispose()
    {
        _socketClient?.Disconnect();
    } */
    
    public void TestConn(MapData mapData)
    {
        var json = JsonConvert.SerializeObject(mapData);
        StartCoroutine(PostRequest($"http://{DataContainer.ServerIP}:5180/DnD/Map", json, "POST"));
        // StartCoroutine(GetRequest($"http://{DataContainer.ServerIP}:5180/GameObject/GetMap"));
    }

    IEnumerator PostRequest(string url, string json, string method)
    {
        Debug.Log($"Sending data to {url}");
        using UnityWebRequest www = new UnityWebRequest(url, method);
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

    // IEnumerator GetRequest(string url) //deaktiviert
    // {
    //     // Debug.Log($"Sending data to {url}");
    //     using UnityWebRequest www = UnityWebRequest.Get(url);
    //     yield return www.SendWebRequest();
    //     if (www.result != UnityWebRequest.Result.Success)
    //     {
    //         Debug.Log($"Error while receiving: {www.error}");
    //     }
    //
    //     if (www.result == UnityWebRequest.Result.Success)
    //     {
    //         // Debug.Log("Downloaded data!");
    //         _ergebniss = www.downloadHandler.text;
    //         Debug.Log(_ergebniss);
    //     }
    // }
}
