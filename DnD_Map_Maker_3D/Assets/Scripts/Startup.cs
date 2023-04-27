using UnityEngine;
using UnityEngine.Serialization;

public class Startup : MonoBehaviour
{
    [FormerlySerializedAs("MenuController")] public GameObject menuController;

    [FormerlySerializedAs("MeshSpawner")] public GameObject meshSpawner;

    public GameObject Conn;
    // Start is called before the first frame update

    public void Start()
    {
        DataContainer.CreateConn(Conn.GetComponent<WsConn>());
        // Debug.Log(DataContainer.Conn.MapExists());
        if (DataContainer.Conn.MapExists())
        {
            var map = DataContainer.Conn.OnConnectMap();
            Debug.Log(map);
            menuController.GetComponent<MenuBarController>().MapFromMapData(map);
            menuController.GetComponent<MenuBarController>().GameObjectsIntoDict();
        }
        else
        {
            menuController.GetComponent<MenuBarController>().MakeGridSizePopUpVisible();
        }
    }
}
