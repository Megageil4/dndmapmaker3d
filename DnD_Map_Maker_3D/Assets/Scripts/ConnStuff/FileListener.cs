using System.IO;
using DefaultNamespace;
using UnityEngine;
using Debug = UnityEngine.Debug;
/// <summary>
/// file listener used to listen for files in the temp folder
/// </summary>
public class FileListener : MonoBehaviour
{
    private int _iterator = 1;
    
    /// <summary>
    /// controller used to get map and gameobject data to the server
    /// </summary>
    [SerializeField]
    private GameObject menuController;
    
    [SerializeField]
    private GameObject connController;

    /// <summary>
    /// the current path of the temp folder
    /// </summary>
    private readonly string _tmpPath = Path.GetTempPath() + "/DnD/" + DataContainer.ClientId + "/";
    

    // Update is called once per frame
    /// <summary>
    /// checks if a file exists and if it does, it reads the content and does something with it
    /// </summary>
    void Update()
    {
        if (File.Exists(_tmpPath + _iterator))
        {
            Debug.Log("File found!");
            switch (File.ReadAllText(_tmpPath + _iterator))
            {
                case "nm":
                    menuController.GetComponent<MenuBarController>().MapFromMapData();
                    break;
                case "ngo": 
                    menuController.GetComponent<MenuBarController>().GameObjectsIntoDict();
                    break;
                case "np":
                    menuController.GetComponent<CurrentUserController>().UpdateUsers(connController.GetComponent<WsConn>().GetUsers());
                    break;
            }
            File.Delete(_tmpPath + _iterator);
            _iterator++;
        }
    }
}