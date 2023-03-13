using DefaultNamespace;
using Newtonsoft.Json;
using UnityEngine;

public class Startup : MonoBehaviour
{
    public GameObject MenuController;

    public GameObject MeshSpawner;
    // Start is called before the first frame update

    public void Start()
    {
        var ex = ServerKomm.ExistsMap();
        if (ex)
        {
            var map = ServerKomm.FetchMap();
            MenuController.GetComponent<MenuBarController>().MapFromMapData(JsonConvert.DeserializeObject<MapData>(map));
        }
        else
        {
            MenuController.GetComponent<MenuBarController>().MakeGridSizePopUpVisible();
        }
    }
}
