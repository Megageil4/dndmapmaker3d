using FinalTest.ConnectionManager;
using FinalTest.Model;
using Int5.DnD3D.WebApi.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;
namespace FinalTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DnDController : ControllerBase
    {
        // Handelt alle WebSocketConnections
        public static WebSocketConnectionManager _connectionManager { get; } = new();
        public static DatabaseManager _databaseManager { get; } = new(@"Data Source=user.db");
        // Vorest nur eine Map
        public static Map? Map { get; set; }
        // List an GameObjects 
        public static List<GameObject> GameObjects = new List<GameObject>();
        private readonly ILogger<DnDController> _logger;
        public DnDController(ILogger<DnDController> logger)
        {
            _logger = logger;
        }
        /*[Route("/ws")]
        public async Task Get([FromQuery ] string username)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                if (_databaseManager.UserExists(username))
                {
                    using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                    await Echo(webSocket, username);
                }
                else
                {
                    throw new Exception("bad username");
                }
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }
        static async Task Echo(WebSocket webSocket,string username)
        {
            var buffer = new byte[1024 * 4];
            Guid guid = _connectionManager.AddUser(new Int5.DnD3D.WebApi.Model.User(username,webSocket));
            await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(guid + "")), WebSocketMessageType.Text, 0, CancellationToken.None);
            Console.WriteLine("Neuer Client " + guid);
            while (webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Console.WriteLine(message);
            }
        }*/
        [HttpGet]
        [Route("[action]")]
        public string TestConnection()
        {
            return "Connection erstellt";
        }
        [HttpGet]
        [ActionName("Map/Exists")]
        [Route("[action]")]
        public bool ExistsMap()
        {
            return Map is not null;
        }
        // getet Alle Objekte auf der Map
        [HttpGet]
        [ActionName("GameObject")]
        [Route("[action]")]
        public IEnumerable<GameObject> GetAll()
        {
            Console.WriteLine("GetAll " + GameObjects.Count);
            return GameObjects;
        }
        [HttpGet]
        [ActionName("Map")]
        [Route("[action]")]
        public Map GetMap()
        {
            return Map ?? throw new Exception("ERROR: keine Map vorhanden");
        }
        // Postet ein GameObjekt in den Cache
        [HttpPost]
        [ActionName("GameObject")]
        [Route("[action]")]
        public void PostChange([FromBody] GameObject gameObject)
        {
            if (_connectionManager.Connections.Keys.Contains(gameObject.ClientId))
            {
                GameObjects.Add(gameObject);
                // Informiert alle Connections darüber dass ein neues GameObject geposted wurde
                _connectionManager.MessageAll("ngo", gameObject.ClientId + "");
                Console.WriteLine("Post Change");
            }
            else
            {
                throw new Exception("Keine gültige Id");
            }
        }
        [HttpPut]
        [ActionName("GameObject")]
        [Route("[action]")]
        public void PutGameObjekt([FromBody] GameObject gameObject)
        {
            if (_connectionManager.Connections.Keys.Contains(gameObject.ClientId))
            {
                int old = 
                GameObjects.FindIndex(g => g.Guid == gameObject.Guid);
                if (old == -1)
                {
                    GameObjects.RemoveAt(old);
                    GameObjects.Add(gameObject);
                    
                }
                _connectionManager.MessageAll("ngo", gameObject.ClientId + "");


            }
            else
            {
                throw new Exception("Keine gültige Id");
            }
        }
        // Postet eine Map welche die alte aMap überschreibt
        [HttpPost]
        [ActionName("Map")]
        [Route("[action]")]
        public void MapChange([FromBody] Map map)
        {
            if (_connectionManager.Connections.Keys.Contains(map.ClientId))
            {
                Console.WriteLine("Map geändert");
                Map = map;
                // Informiert alle Connections dass neue Map Geposted wurde
                _connectionManager.MessageAll("nm", map.ClientId + "");
            }
            else
            {
                throw new Exception("Keine gültige Id");
            }
        }
        [HttpGet]
        [ActionName("User")]
        [Route("[action]")]
        public List<string> UserGet()
        {
            var users
            = new List<string>();
            foreach (var sessionKey in _connectionManager.Connections.Keys)
            {
                if (_connectionManager.Connections[sessionKey].WebSocket.State == WebSocketState.Open)
                {
                    users.Add(_connectionManager.Connections[sessionKey].Username);
                }
                else
                {
                    _connectionManager.KillConnection(sessionKey);
                }
            }
            return users;
        }

        [HttpPost]
        [ActionName("User")]
        [Route("[action]")]
        public string RegisterUser([FromBody] string username)
        {
            Console.WriteLine("In Register User");
            _databaseManager.AddUser(username);
            return "User added";
        }

    }
}