using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to control the different submenus of the start menu
/// </summary>
public class StartMenuController : MonoBehaviour
{
    /// <summary>
    /// A list of all possible submenus. Used to disable all menus before enabling the new one
    /// </summary>
    [SerializeField]
    private List<GameObject> menus;

    public StartMenuController()
    {
        menus = new List<GameObject>();
    }

    /// <summary>
    /// Changes the current menu to the given one
    /// </summary>
    /// <param name="menu">The menu it should change to</param>
    public void ChangeMenu(GameObject menu)
    {
        DisableAllMenus();
        menu.SetActive(true);
    }

    /// <summary>
    /// Closes the application. Can be used to close connections if needed
    /// </summary>
    public void Exit()
    {
        DisableAllMenus();
        Application.Quit();
    }
    
    /// <summary>
    /// Disables all menus
    /// </summary>
    private void DisableAllMenus()
    {
        foreach (var menu in menus)
        {
            menu.SetActive(false);
        }
    }

    public void GotoGitlab()
    {
        Application.OpenURL("https://gl.edvschule-plattling.de/bfs2021fi/jkaufman/dndmapmaker3d");
    }
}
