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

    public void Settings()
    {
        throw new NotImplementedException();
    }
    
    public void Logout() 
    {
        SceneManager.LoadScene("SampleScene");
    }
    
    public void Quit()
    {
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
