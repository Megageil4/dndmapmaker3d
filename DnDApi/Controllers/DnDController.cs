using Microsoft.AspNetCore.Mvc;

namespace DnDApi.Controllers;
[ApiController]
[Route("[controller]")]
public class DnDController : ControllerBase
{
    
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
    [ActionName("GameObjekt")]
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
    }

    // Postet eine Map welche die alte aMap überschreibt
    [HttpPost]
    [ActionName("Map")]
    [Route("[action]")]
    public void MapChange([FromBody] Map map)
    {
        
        Map = map;
        Console.WriteLine(map);
    }
}