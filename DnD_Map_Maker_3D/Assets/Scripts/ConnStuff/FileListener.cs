using System;
using System.IO;
using System.Net;
using UnityEngine;

public class FileListener : MonoBehaviour
{
    // Start is called before the first frame update
    private int iterator;
    public GameObject menuController;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (File.Exists($"\\{iterator}"))
        {
            switch (File.ReadAllText($"\\{iterator}"))
            {
                case "":
                    menuController.GetComponent<MenuBarController>().MapFromMapData();
                    break;
                case "": 
                    menuController.GetComponent<MenuBarController>().GameObjectsIntoDict();
                    break;
                case "": 
                    DataContainer.ClientId = Guid.Parse("");
                    break;
            }
            File.Delete($"\\{iterator}");
        }
    }
}