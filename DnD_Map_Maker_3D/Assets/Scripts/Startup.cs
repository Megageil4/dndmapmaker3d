using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Class for the startup of the program and the connection to the server
/// </summary>
public class Startup : MonoBehaviour
{
    /// <summary>
    /// Controller used to send map and gameobject data to the server
    /// </summary>
    [FormerlySerializedAs("MenuController")] public GameObject menuController;

    /// <summary>
    /// The object containing the connection to the server
    /// </summary>
    [FormerlySerializedAs("Conn")] public GameObject connection;
    // Start is called before the first frame update

    public void Start()
    {
        DataContainer.CreateConn(connection.GetComponent<WsConn>());
        // Debug.Log(DataContainer.Conn.MapExists());
        if (DataContainer.Conn.MapExists())
        {
            menuController.GetComponent<MenuBarController>().MapFromMapData();
            menuController.GetComponent<MenuBarController>().GameObjectsIntoDict();
        }
        else
        {
            menuController.GetComponent<MenuBarController>().MakeGridSizePopUpVisible();
        }

        Debug.Log("start " + DataContainer.ClientId);
    }
}
