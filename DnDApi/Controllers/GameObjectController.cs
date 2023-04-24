using Microsoft.AspNetCore.Mvc;

namespace DnDApi.Controllers;
[ApiController]
[Route("[controller]")]
public class GameObjectController : ControllerBase
{
    
    // Vorest nur eine Map
    public static Map? Map { get; set; }
    // List an GameObjects 
    public static List<GameObject> GameObjects { get; set; }
    private readonly ILogger<GameObjectController> _logger;

    public GameObjectController(ILogger<GameObjectController> logger)
    {
        _logger = logger;
        GameObjects = new();

     
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