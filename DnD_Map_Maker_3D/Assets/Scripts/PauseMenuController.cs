using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class to control the behaviour of the pause menu.
/// </summary>
public class PauseMenuController : MonoBehaviour
{
    /// <summary>
    /// The UI element of the pause menu
    /// </summary>
    [SerializeField]
    private GameObject pauseMenu;
    
    /// <summary>
    /// Shows the pause menu
    /// </summary>
    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    /// <summary>
    /// Closes the pause menu
    /// </summary>
    public void Resume()
    {
        pauseMenu.SetActive(false);
    }

    /// <summary>
    /// Opens the settings menu and closes the pause menu
    /// </summary>
    /// <param name="settingsMenu">The UI element of the settings menu</param>
    public void Settings(GameObject settingsMenu)
    {
        settingsMenu.SetActive(!settingsMenu.activeSelf);
        Resume();
    }
    
    /// <summary>
    /// Closes the settings menu and opens the pause menu
    /// </summary>
    /// <param name="settingsMenu">The UI element of the settings menu</param>
    public void CloseSettings(GameObject settingsMenu)
    {
        settingsMenu.SetActive(false);
        ShowPauseMenu();
    }

    /// <summary>
    /// Logs out the user and loads the login scene. The connection should be closed properly by this method. 
    /// </summary>
    public void Logout() 
    {
        DataContainer.Conn.Logout();
        SceneManager.LoadScene("Login");
    }
    
    /// <summary>
    /// Close the application. The connection should be closed properly by this method.
    /// </summary>
    public void Quit()
    {
        DataContainer.Conn.Logout();
        Util.Exit();
    }

    /// <summary>
    /// Update is used to check if the escape key is pressed to show or hide the pause menu
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf)
            {
                Resume();   
            }
            else
            {
                ShowPauseMenu();
            }
        }
    }
}
