using UnityEngine;

public class Startup : MonoBehaviour
{
    public GameObject MenuController;

    public GameObject MeshSpawner;
    // Start is called before the first frame update
    void Start()
    {
        if (ServerKomm.existsMap())
        {
            MenuController.GetComponent<MenuBarController>().MapFromMash((ServerKomm.MapFromJson(ServerKomm.fetchMap(), MeshSpawner)));
        }
        else
        {
            MenuController.GetComponent<MenuBarController>().MakeGridSizePopUpVisible();
        }
    }
}
