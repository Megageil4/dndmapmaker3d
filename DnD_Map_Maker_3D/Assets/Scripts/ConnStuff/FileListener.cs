using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;
/// <summary>
/// file listener used to listen for files in the temp folder
/// </summary>
public class FileListener : MonoBehaviour
{
    private int _iterator;
    
    /// <summary>
    /// controller used to get map and gameobject data to the server
    /// </summary>
    public GameObject menuController;
    
    /// <summary>
    /// the process of the websocket connection
    /// </summary>
    private Process _websocetConn;
    
    /// <summary>
    /// the current path of the temp folder
    /// </summary>
    private string _tmpPath = Path.GetTempPath() + "/DnD/";
    
    /// <summary>
    /// connects to the server, gets the client id and creates a folder for the client
    /// </summary>
    void Awake()
    {
        _websocetConn = Process.Start(@"..\Int5.DnD3D.WebClient\Int5.DnD3D.WebClient\bin\Debug\net6.0\Int5.DnD3D.WebClient.exe");
        while (!File.Exists(Path.GetTempPath() + @$"/DnD/{_iterator}"))
        { }
        string content = File.ReadAllText(_tmpPath + _iterator);
        DataContainer.ClientId = Guid.Parse(content.Substring(1, content.Length - 1));
        File.Delete(_tmpPath + _iterator);
        _tmpPath += DataContainer.ClientId + "/";
        _iterator++;
    }

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
            }
            File.Delete(_tmpPath + _iterator);
            _iterator++;
        }
    }

    /// <summary>
    /// closes the websocket connection when the application is closed
    /// </summary>
    private void OnApplicationQuit()
    {
        _websocetConn.Close();
    }
}