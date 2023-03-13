using System.Threading.Tasks;
using UnityEngine;

public class Startup : MonoBehaviour
{
    public GameObject MenuController;

    public GameObject MeshSpawner;
    // Start is called before the first frame update
    async Task Start()
    {
        var ex = ServerKomm.ExistsMap();
        if (await ex)
        {
            var map = ServerKomm.FetchMap();
            MenuController.GetComponent<MenuBarController>().MapFromMapData(ServerKomm.MapFromJson(await map, MeshSpawner));
        }
        else
        {
            MenuController.GetComponent<MenuBarController>().MakeGridSizePopUpVisible();
        }
    }
}
