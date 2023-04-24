using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DefaultNamespace;
using DnD_3D.ServerConnection.Default;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace ConnStuff
{
    public class WebserviceCon : IDnDConnection
    {
        public void SendMap(MapData map)
        {

           var request = (HttpWebRequest)WebRequest.Create($"http://{DataContainer.ServerIP}:5180/GameObject/MapChange");
           var json = JsonConvert.SerializeObject(map);
           
           var data = Encoding.UTF8.GetBytes(json); 

           request.Method = "POST";
           request.ContentType = "application/json";
           request.ContentLength = data.Length;
           request.Proxy = null!;

           request.Timeout = 500;
           request.ServicePoint.ConnectionLeaseTimeout = 500;
           request.ServicePoint.MaxIdleTime = 500;

           using var stream = request.GetRequestStream();
           stream.Write(data, 0, data.Length);
           stream.Flush();
           stream.Close(); 
           stream.Dispose();
        }

        
        IEnumerator postRequest(string url, string json)
        {
            var uwr = new UnityWebRequest(url, "POST");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            uwr.SetRequestHeader("Content-Type", "application/json");

            //Send the request then wait here until it returns
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError)
            {
                Debug.Log("Error While Sending: " + uwr.error);
            }
            else
            {
                Debug.Log("Received: " + uwr.downloadHandler.text);
            }
        }
        
        public void AddGameObject(GameObject gameObject)
        {
            var request = (HttpWebRequest)WebRequest.Create($"http://{DataContainer.ServerIP}:5180/GameObject/PostChange");
            JKGameObject jkGameObject = new JKGameObject(gameObject);
            DataContainer.GameObjects.Add(jkGameObject.Guid,gameObject);
            var json = JsonConvert.SerializeObject(jkGameObject);

            var data = Encoding.UTF8.GetBytes(json);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            request.Proxy = null!;

            using var stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);
        }

        public List<JKGameObject> GetGameObjects()
        {
            var request = (HttpWebRequest)WebRequest.Create($"http://{DataContainer.ServerIP}:5180/GameObject/GetAll");
            request.Method = "GET";
            request.Proxy = null!;
            using var re = new StreamReader(request.GetResponse().GetResponseStream()!).ReadToEndAsync();
            return JsonConvert.DeserializeObject<List<JKGameObject>>(re.Result);
        }

        public bool MapExists()
        {
            var request = (HttpWebRequest)WebRequest.Create($"http://{DataContainer.ServerIP}:5180/GameObject/ExistsMap");
            request.Method = "GET";
            request.Proxy = null!;
            using var v = new StreamReader(request.GetResponse().GetResponseStream()!).ReadToEndAsync();
            return v.Result == "true";
        }

        public List<GameObject> OnConnectGO()
        {
            throw new NotImplementedException();
        }

        public MapData OnConnectMap()
        {
            var request = (HttpWebRequest)WebRequest.Create($"http://{DataContainer.ServerIP}:5180/GameObject/GetMap");
            request.Method = "GET";
            request.Proxy = null!;
            using var re = new StreamReader(request.GetResponse().GetResponseStream()!).ReadToEndAsync();
            return JsonConvert.DeserializeObject<MapData>(re.Result);
        }

        public bool Connected()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}