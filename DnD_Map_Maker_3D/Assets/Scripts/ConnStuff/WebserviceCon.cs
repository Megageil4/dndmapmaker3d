using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace ConnStuff
{
    public class WebserviceCon : MonoBehaviour, IDnDConnection
    {
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
            string str = "";
            StartCoroutine(GetRequest($"http://{DataContainer.ServerIP}:5180/GameObject/GetAll", s => str = s));
            while (str == "") {}
            return JsonConvert.DeserializeObject<List<JKGameObject>>(str);
        }

        public bool MapExists()
        {
            string str = "";
            StartCoroutine(GetRequest($"http://{DataContainer.ServerIP}:5180/GameObject/ExistsMap", s => str = s));
            while (str == "") {}
            return str == "true";
        }

        public List<GameObject> OnConnectGO()
        {
            throw new NotImplementedException();
        }

        public MapData OnConnectMap()
        {
            string str = "";
            StartCoroutine(GetRequest($"http://{DataContainer.ServerIP}:5180/GameObject/GetMap", s => str = s));
            while (str == "") {}
            return JsonConvert.DeserializeObject<MapData>(str);
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
            StartCoroutine(GetRequest($"http://{DataContainer.ServerIP}:5180/GameObject/GetMap", s => Debug.Log(s)));
        }

        IEnumerator PostRequest(string url, string json, Action<string> finishDelegate = null)
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
                if (finishDelegate != null)
                {
                    finishDelegate(www.downloadHandler.text);
                }
            }
        }

        IEnumerator GetRequest(string url, Action<string> finishDelegate)
        {
            Debug.Log($"Sending data to {url}");
            using UnityWebRequest www = UnityWebRequest.Get(url);
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