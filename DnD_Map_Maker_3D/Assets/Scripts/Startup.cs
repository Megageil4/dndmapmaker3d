using DefaultNamespace;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;

public class Startup : MonoBehaviour
{
    [FormerlySerializedAs("MenuController")] public GameObject menuController;

    [FormerlySerializedAs("MeshSpawner")] public GameObject meshSpawner;
    // Start is called before the first frame update

    public void Start()
    {
        if (DataContainer.Conn.MapExists())
        {
            var map = ServerKomm.FetchMap();
            menuController.GetComponent<MenuBarController>().MapFromMapData(JsonConvert.DeserializeObject<MapData>(map));
        }
        else
        {
            menuController.GetComponent<MenuBarController>().MakeGridSizePopUpVisible();
        }
    }
}
