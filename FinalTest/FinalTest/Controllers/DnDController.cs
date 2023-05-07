using FinalTest.ConnectionManager;
using FinalTest.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace FinalTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DnDController : ControllerBase
    {
        // Handelt alle WebSocketConnections
        public static WebSocketConnectionManager _connectionManager { get; } = new();
        // Vorest nur eine Map
        public static Map? Map { get; set; }
        // List an GameObjects 
        public static List<GameObject> GameObjects = new List<GameObject>();
        private readonly ILogger<DnDController> _logger;

        public DnDController(ILogger<DnDController> logger)
        {
            _logger = logger;
        }
       


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
            Console.WriteLine(GameObjects.Count);
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
            GameObjects.Add(gameObject);
            Console.WriteLine(gameObject);
            Console.WriteLine(GameObjects.Count);
            // Informiert alle Connections darüber dass ein neues GameObject geposted wurde
            _connectionManager.anAlle("ngo",gameObject.ClientId+"");
        }
        [HttpPut]
        [ActionName("GameObject")]
        [Route("[action]")]
        public void PutGameObjekt([FromBody] GameObject gameObject)
        {
            var old = GameObjects.Find(g => g.Guid == gameObject.Guid);
            if (old != null)
            {
                GameObjects.Remove(old);
                GameObjects.Add(gameObject);
                Console.WriteLine(gameObject);
                Console.WriteLine(GameObjects.Count);
            }
            // Informiert alle Connections dass ein GameObject geändert wurde
            _connectionManager.anAlle("ngo",gameObject.ClientId+"");
        }

        // Postet eine Map welche die alte aMap überschreibt
        [HttpPost]
        [ActionName("Map")]
        [Route("[action]")]
        public void MapChange([FromBody] Map map)
        {

            Map = map;
            // Informiert alle Connections dass neue Map Geposted wurde
            _connectionManager.anAlle("nm",map.ClientId+"");
        }
    }

}
