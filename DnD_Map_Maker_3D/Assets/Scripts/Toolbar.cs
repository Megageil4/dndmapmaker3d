using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class Toolbar : MonoBehaviour
{
    public PlaneSpawner PlaneSpawner;
    public static IMapTool MapTool;
    public List<Button> Buttons;
    public void onErhoehen()
    {
        MapTool = new Extrude(PlaneSpawner);
        ClearButtons();
        Buttons[0].GetComponent<Image>().color = Color.gray;
    }

    public void onErniedrigen()
    {
        MapTool = new Intrude(PlaneSpawner);
        ClearButtons();
        Buttons[1].GetComponent<Image>().color = Color.gray;
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
