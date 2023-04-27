using Microsoft.AspNetCore.Mvc;

namespace DnDApi.Controllers;
[ApiController]
[Route("[controller]")]
public class GameObjectController : ControllerBase
{
    
    // Vorest nur eine Map
    public static Map? Map { get; set; }
    // List an GameObjects 
    public static List<GameObject> GameObjects = new List<GameObject>();
    private readonly ILogger<GameObjectController> _logger;

    public GameObjectController(ILogger<GameObjectController> logger)
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
    [Route("[action]")]
    public bool ExistsMap()
    {
        return Map is not null;
    }
    // getet Alle Objekte auf der Map
    [HttpGet]
    [Route("[action]")]
    public IEnumerable<GameObject> GetAll()
    {
        Console.WriteLine(GameObjects.Count);
        return GameObjects;
    }

    [HttpGet]
    [Route("[action]")]
    public Map GetMap()
    {
        
        return Map ?? throw new Exception("ERROR: keine Map vorhanden");
    }
    
    // Postet ein GameObjekt in den Cache
    [HttpPost]
    [Route("[action]")]
    public void PostChange([FromBody] GameObject gameObject)
    {
        GameObjects.Add(gameObject);
        Console.WriteLine(gameObject);
        Console.WriteLine(GameObjects.Count);
    }

    // Postet eine Map welche die alte aMap überschreibt
    [HttpPost]
    [Route("[action]")]
    public void MapChange([FromBody] Map map)
    {
        
        Map = map;
        Console.WriteLine(map);
    }
}