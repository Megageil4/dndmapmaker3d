using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class FileListener : MonoBehaviour
{
    // Start is called before the first frame update
    private int iterator;
    public GameObject menuController;
    private Process _websocetConn;
    void Start()
    {
        _websocetConn = Process.Start(@"..\Int5.DnD3D.WebClient\Int5.DnD3D.WebClient\bin\Debug\net6.0\Int5.DnD3D.WebClient.exe");
        _websocetConn?.CloseMainWindow();
    }

    // Update is called once per frame
    void Update()
    {
        if (File.Exists(Path.GetTempPath() + @$"/{iterator}"))
        {
            var content = File.ReadAllText(Path.GetTempPath() + @$"/{iterator}"); 
            switch (content)
            {
                case "nm":
                    menuController.GetComponent<MenuBarController>().MapFromMapData();
                    break;
                case "ngo": 
                    menuController.GetComponent<MenuBarController>().GameObjectsIntoDict();
                    break;
                default:
                    DataContainer.ClientId = Guid.Parse(content.Substring(1, content.Length - 1));
                    break;
            }
            File.Delete(Path.GetTempPath() + @$"/{iterator}");
            iterator++;
        }
    }

    private void OnApplicationQuit()
    {
        _websocetConn.Close();
    }
}