using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Toolbar : MonoBehaviour
{
    public PlaneSpawner PlaneSpawner;
    public static IMapTool MapTool;
    public List<Button> Buttons;
    public ObjectController Controller;
    public GameObject ModelContainer;
    
    public void OnSelect(Camera playerCamera)
    {
        ClearButtons();
        MapTool = new Select(playerCamera);
        Buttons[0].GetComponent<Image>().color = Color.gray;
    }

    public void onErhoehen()
    {
        ClearButtons();
        MapTool = new Extrude(PlaneSpawner);
        Buttons[1].GetComponent<Image>().color = Color.gray;
    }

    public void onErniedrigen()
    {
        ClearButtons();
        MapTool = new Intrude(PlaneSpawner);
        Buttons[2].GetComponent<Image>().color = Color.gray;
    }

    public void onPlace()
    {
        ClearButtons();
        MapTool = new Place(Controller);
        Buttons[3].GetComponent<Image>().color = Color.gray;
        ModelContainer.SetActive(true);
    }
    
    private void ClearButtons()
    {
        if (MapTool is Select select)
        {
            if (select.Selected != null)
            {
                select.Selected.transform.GetChild(1).transform.gameObject.SetActive(false);       
            }
        }
        for (var index = 0; index < Buttons.Count; index++)
        {
            var button = Buttons[index];
            button.GetComponent<Image>().color = Color.white;
        }

        if (ModelContainer != null)
        {
            ModelContainer.SetActive(false);
        }
    } 
}
