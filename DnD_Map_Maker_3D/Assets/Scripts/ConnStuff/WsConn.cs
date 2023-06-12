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

/// <summary>
/// connection to the server
/// </summary>
public class WsConn : MonoBehaviour, IDnDConnection
{
   // private WebSocketClient _socketClient;
   /// <summary>
   /// Sends a map to the server
   /// </summary>
   /// <param name="map">MapData that will be sent to the server</param>
    public void SendMap(MapData map)
    {
        var json = JsonConvert.SerializeObject(map);
        StartCoroutine(PostRequest($"http://{DataContainer.ServerIP}:443/DnD/Map", json, "POST"));
    }

   /// <summary>
   /// Posts a gameobject to the server and DataContainer
   /// </summary>
   /// <param name="gameObject">Object that will be addet</param>
    public void AddGameObject(GameObject gameObject)
    {
        JKGameObject jkGameObject = new JKGameObject(gameObject);
        DataContainer.GameObjects.Add(jkGameObject.Guid, gameObject);
        DataContainer.Guids.Add(gameObject,jkGameObject.Guid);
        var json = JsonConvert.SerializeObject(jkGameObject);
            Debug.Log(json);
        StartCoroutine(PostRequest($"http://{DataContainer.ServerIP}:443/DnD/GameObject", json, "POST"));
    }

   /// <summary>
   /// Puts a gameobject to the server
   /// </summary>
   /// <param name="guid">id of the gameobject that changed</param>
    public void ChangeGameObject(Guid guid)
    {
        JKGameObject jkGameObject = new JKGameObject(DataContainer.GameObjects[guid], guid);
        var json = JsonConvert.SerializeObject(jkGameObject);
        StartCoroutine(PostRequest($"http://{DataContainer.ServerIP}:443/DnD/GameObject", json, "PUT"));
    }

   /// <summary>
   /// Getts all gameobjects from the server
   /// </summary>
   /// <returns>a List of GameObjects</returns>
    public List<JKGameObject> GetGameObjects()
    {
        var request = (HttpWebRequest)WebRequest.Create($"http://{DataContainer.ServerIP}:443/DnD/GameObject");
        request.Method = "GET";
        request.Proxy = null!;
        using var v = new StreamReader(request.GetResponse().GetResponseStream()!).ReadToEndAsync();
        return JsonConvert.DeserializeObject<List<JKGameObject>>(v.Result);
    }

   /// <summary>
   /// Checks if a map exists on the server
   /// </summary>
   /// <returns>true whene a map already exists on the server, else false</returns>
    public bool MapExists()
    {
        var request =
            (HttpWebRequest)WebRequest.Create($"http://{DataContainer.ServerIP}:443/DnD/Map/Exists");
        request.Method = "GET";
        request.Proxy = null!;
        using var v = new StreamReader(request.GetResponse().GetResponseStream()!).ReadToEndAsync();
        Thread.Sleep(10);
        return v.Result == "true";
    }
   /// <summary>
   /// Fetches the map for the first time from the server
   /// </summary>
   /// <returns>mapdata</returns>
   public MapData FetchMap()
   {
       var request = (HttpWebRequest)WebRequest.Create($"http://{DataContainer.ServerIP}:443/DnD/Map");
       request.Method = "GET";
       request.Proxy = null!;
       using var v = new StreamReader(request.GetResponse().GetResponseStream()!).ReadToEndAsync();
       return JsonConvert.DeserializeObject<MapData>(v.Result);
   }

   
   /// <summary>
   /// gets a map from the server
   /// </summary>
   /// <returns>map from the server</returns>
    public MapData OnConnectMap()
    {
        var request = (HttpWebRequest)WebRequest.Create($"http://{DataContainer.ServerIP}:443/DnD/Map");
        request.Method = "GET";
        request.Proxy = null!;
        using var v = new StreamReader(request.GetResponse().GetResponseStream()!).ReadToEndAsync();
        return JsonConvert.DeserializeObject<MapData>(v.Result);
    }

    // private IEnumerable<Coroutine> Getter(string url)
    // {
    //     yield return StartCoroutine(GetRequest(url));
    // }

    public void Connect()
    {
        
    }

    public void TestConn(MapData mapData)
    {
        var json = JsonConvert.SerializeObject(mapData);
        StartCoroutine(PostRequest($"http://{DataContainer.ServerIP}:443/DnD/Map", json, "POST"));
        // StartCoroutine(GetRequest($"http://{DataContainer.ServerIP}:443/GameObject/GetMap"));
    }

    /// <summary>
    /// Sends A WebRequest to a Webserver
    /// </summary>
    /// <param name="url">URL of the Webserver</param>
    /// <param name="json">JSON string that will be sent to the server</param>
    /// <param name="method">Request Type (POST/PUT)</param>
    /// <returns></returns>
    IEnumerator PostRequest(string url, string json, string method)
    {
        // Debug.Log($"Sending data to {url}");
        using UnityWebRequest www = new UnityWebRequest(url, method);
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log($"Error while Sending: {www.error} , {url}");
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
