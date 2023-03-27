using WebSocketSharp;
using Newtonsoft.Json;
using DnD_3D.ServerConnection.Entity;
using System.Net;

namespace DnD_3D.ServerConnection.Default;

public class WebsocketDnDConnection : IDnDConnection, IDisposable
{
    public event EventHandler<MapEventArgs> GetMap;
    public event EventHandler<GameObjectEventArgs> GetGameObjects;
    public event EventHandler<BoolEventArgs> MapExistsAntwort;
    public string guid;
    private WebSocket ws;
    private bool connected;
    HttpClient client = new HttpClient();
    public WebsocketDnDConnection(string uri)
    {
        client.BaseAddress = new Uri("http://10.0.207.7:5180/GameObject");
        guid = Guid.NewGuid().ToString();
        ws = new(uri);
     
        ws.OnMessage += (sender, e) =>
        {
            Message? message = JsonConvert.DeserializeObject<Message>(e.Data);
            if (message is not null)
            {
                if(message.Guid != guid)
                    switch (message.MessageType)
                    {
                        case "ngo":
                            OnGetGameobjects();
                            break;
                        case "nm":
                            OnGetMap();
                            break;
                        case "me":
                            
                            break;
                        default:
                            break;
                    }
            }
            
        };
        ws.OnOpen += (sender, e) =>
        {
            connected = true;
        };
        ws.Connect();
    }

    public async void AddGameObject(GameObject gameObject)
    {
        //client.GetAsync("/")
        WebRequest




        ws.Send(JsonConvert.SerializeObject(new Message("ngj",guid)));
    }

    public bool Connected()
    {
        return connected;
    }

    public async Task<bool> MapExists()
    {
        var resp = await client.GetAsync("/GetMap");
        var cont = resp.Content;
        string json = await cont.ReadAsStringAsync();
        bool rueck = JsonConvert.DeserializeObject<bool>(json);
        return rueck;
    }

    public void SendMap(Map map)
    {
        ws.Send('2' + JsonConvert.SerializeObject(map));
    }

    protected async virtual void OnGetMap()
    {
        var map = MapQuery().Result;
        this.GetMap?.Invoke(this, new MapEventArgs(map));
    }

    protected async virtual void OnGetGameobjects()
    {
        var go = GOQuery().Result;
        this.GetGameObjects?.Invoke(this, new GameObjectEventArgs(go));
    }

    protected virtual void OnMapAntwort(bool wert)
    {
        this.MapExistsAntwort?.Invoke(this, new(wert));
    }
    public void Dispose()
    {
        ws.Close();
    }

    public List<GameObject> OnConnectGO()
    {
        return GOQuery().Result;   
    }

    public Map OnConnectMap()
    {
        return MapQuery().Result;
    }

    private async Task<Map> MapQuery()
    {
        var resp = await client.GetAsync("/GetMap");
        var cont = resp.Content;
        string json = await cont.ReadAsStringAsync();
        var map = JsonConvert.DeserializeObject<Map>(json);
        return map;
    }

    private async Task<List<GameObject>> GOQuery()
    {
        var resp = await client.GetAsync("/GetAll");
        var cont = resp.Content;
        string json = await cont.ReadAsStringAsync();
        var go = JsonConvert.DeserializeObject<List<GameObject>>(json);
        return go;
    }
}
