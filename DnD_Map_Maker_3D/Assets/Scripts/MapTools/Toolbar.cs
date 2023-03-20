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
    public ObjectController Controller; //TODO: add to mehtods
    
    public void OnSelect(Camera playerCamera)
    {
        MapTool = new Select(playerCamera);
        ClearButtons();
        Buttons[0].GetComponent<Image>().color = Color.gray;
    }

    public void onErhoehen()
    {
        MapTool = new Extrude(PlaneSpawner);
        ClearButtons();
        Buttons[1].GetComponent<Image>().color = Color.gray;
    }

    public void onErniedrigen()
    {
        MapTool = new Intrude(PlaneSpawner);
        ClearButtons();
        Buttons[2].GetComponent<Image>().color = Color.gray;
    }

    public void onPlace()
    {
        MapTool = new Place(Controller);
        ClearButtons();
        Buttons[3].GetComponent<Image>().color = Color.gray;
    }
    
    private void ClearButtons()
    {
        for (var index = 0; index < Buttons.Count; index++)
        {
            var button = Buttons[index];
            button.GetComponent<Image>().color = Color.white;
        }
    } 
}
