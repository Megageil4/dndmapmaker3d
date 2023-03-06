using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour
{
    public GameObject MenuController;
    // Start is called before the first frame update
    void Start()
    {
        if (ServerKomm.existsMap())
        {
            ServerKomm.fetchMap();
        }
        else
        {
            MenuController.GetComponent<MenuBarController>().MakeGridSizePopUpVisible();
        }
    }
}
