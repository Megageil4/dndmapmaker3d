using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;
    
    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
    }

    public void Settings(GameObject settingsMenu)
    {
        settingsMenu.SetActive(!settingsMenu.activeSelf);
        Resume();
    }
    
    public void CloseSettings(GameObject settingsMenu)
    {
        settingsMenu.SetActive(false);
    }

    public void Logout() 
    {
        DataContainer.Conn.Logout();
        SceneManager.LoadScene("Login");
    }
    
    public void Quit()
    {
        DataContainer.Conn.Logout();
        Util.Exit();
    }

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
