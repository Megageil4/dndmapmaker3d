using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class FileListener : MonoBehaviour
{
    // Start is called before the first frame update
    private int _iterator;
    public GameObject menuController;
    private Process _websocetConn;
    private string _tmpPath = Path.GetTempPath() + "/DnD/";
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

    private void OnApplicationQuit()
    {
        _websocetConn.Close();
    }
}