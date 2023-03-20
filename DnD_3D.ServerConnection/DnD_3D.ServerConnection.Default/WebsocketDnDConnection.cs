using WebSocketSharp;
using Newtonsoft.Json;
using DnD_3D.ServerConnection.Entity;

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
        
        guid = Guid.NewGuid().ToString();
        ws = new(uri);
     
        ws.OnMessage += (sender, e) =>
        {
            Message? message = JsonConvert.DeserializeObject<Message>(e.Data);
            if (message is not null)
            {
                switch (message)
                {
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
        





        ws.Send(JsonConvert.SerializeObject(new Message("ngj",guid)));
    }

    public bool Connected()
    {
        return connected;
    }

    public void MapExists()
    {
        ws.Send("3");
    }

    public void SendMap(Map map)
    {
        ws.Send('2' + JsonConvert.SerializeObject(map));
    }

    protected virtual void OnGetMap()
    {
        //JsonConvert.DeserializeObject<Message>(data);
        //Map? map = JsonConvert.DeserializeObject<Map>(data);
        //this.GetMap?.Invoke(this, new MapEventArgs(map));
    }

    protected virtual void OnGetGameobjects(string data)
    {
        List < GameObject > objects = JsonConvert.DeserializeObject<List<GameObject>>(data);
        this.GetGameObjects?.Invoke(this, new GameObjectEventArgs(objects));
    }

    protected virtual void OnMapAntwort(bool wert)
    {
        this.MapExistsAntwort?.Invoke(this, new(wert));
    }
    public void Dispose()
    {
        ws.Close();
    }
}
