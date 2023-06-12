using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;
    
    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
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
        throw new NotImplementedException();
    }
    
    public void Quit()
    {
        Util.Exit();
    }
}
