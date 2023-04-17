using ConnStuff;
using UnityEngine;
using UnityEngine.Serialization;

public class Startup : MonoBehaviour
{
    [FormerlySerializedAs("MenuController")] public GameObject menuController;

    [FormerlySerializedAs("MeshSpawner")] public GameObject meshSpawner;
    // Start is called before the first frame update

    public void Start()
    {
        DataContainer.CreateConn(new WebserviceCon());
        Debug.Log(DataContainer.Conn.MapExists());
        if (DataContainer.Conn.MapExists())
        {
            var map = DataContainer.Conn.OnConnectMap();
            Debug.Log(map);
            menuController.GetComponent<MenuBarController>().MapFromMapData(map);
        }
        else
        {
            menuController.GetComponent<MenuBarController>().MakeGridSizePopUpVisible();
        }
    }
}
