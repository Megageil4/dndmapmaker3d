using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DefaultNamespace;
using DnD_3D.ServerConnection.Default;
using Newtonsoft.Json;
using UnityEngine;

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

            using var stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);
        }

        public void AddGameObject(GameObject gameObject)
        {
            var request = (HttpWebRequest)WebRequest.Create($"http://{DataContainer.ServerIP}:5180/GameObject/MapChange");
            var json = JsonConvert.SerializeObject(new JK_GameObject(gameObject));

            var data = Encoding.UTF8.GetBytes(json);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            request.Proxy = null!;

            using var stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);
        }

        public List<JK_GameObject> GetGameObjects()
        {
            var request = (HttpWebRequest)WebRequest.Create($"http://{DataContainer.ServerIP}:5180/GameObject/GetAll");
            request.Method = "GET";
            request.Proxy = null!;
            using var re = new StreamReader(request.GetResponse().GetResponseStream()!).ReadToEndAsync();
            return JsonConvert.DeserializeObject<List<JK_GameObject>>(re.Result);
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