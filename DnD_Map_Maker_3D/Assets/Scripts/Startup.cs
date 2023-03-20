using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

public class Startup : MonoBehaviour
{
    [FormerlySerializedAs("MenuController")] public GameObject menuController;

    [FormerlySerializedAs("MeshSpawner")] public GameObject meshSpawner;
    // Start is called before the first frame update

    public async void Start()
    {
        DataContainer.Conn.GetMap += (_,e) => menuController.GetComponent<MenuBarController>().MapFromMapData(e.Map);
        if (await DataContainer.Conn.MapExists())
        {
            var map = DataContainer.Conn.OnConnectMap();
            menuController.GetComponent<MenuBarController>().MapFromMapData(map);
        }
        else
        {
            menuController.GetComponent<MenuBarController>().MakeGridSizePopUpVisible();
        }
    }
}
