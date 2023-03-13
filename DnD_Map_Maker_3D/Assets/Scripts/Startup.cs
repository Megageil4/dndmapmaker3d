using System.Threading.Tasks;
using DefaultNamespace;
using Newtonsoft.Json;
using UnityEngine;

public class Startup : MonoBehaviour
{
    public GameObject MenuController;

    public GameObject MeshSpawner;
    // Start is called before the first frame update
    async Task Start()
    {
        Debug.Log("gestartet");
        var ex = ServerKomm.ExistsMap();
        if (await ex)
        {
            var map = ServerKomm.FetchMap();
            MenuController.GetComponent<MenuBarController>().MapFromMapData(JsonConvert.DeserializeObject<MapData>(await map));
        }
        else
        {
            MenuController.GetComponent<MenuBarController>().MakeGridSizePopUpVisible();
        }
    }
}
