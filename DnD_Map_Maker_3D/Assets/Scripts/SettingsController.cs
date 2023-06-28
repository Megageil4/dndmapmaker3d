using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Class to control behaviour of the settings menu
/// </summary>
public class SettingsController : MonoBehaviour
{
    /// <summary>
    /// List of all possible resolutions the monitor supports
    /// </summary>
    Resolution[] resolutions;
    [SerializeField]
    private TMP_Dropdown resolutionDropdown;
    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        foreach (var resolution in resolutions)
        {
            options.Add(resolution.width + " x " + resolution.height + " " + resolution.refreshRate + "Hz");
            if (resolution.width == Screen.currentResolution.width &&
                resolution.height == Screen.currentResolution.height)
                currentResolutionIndex = Array.IndexOf(resolutions, resolution);
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    /// <summary>
    /// Sets the quality of the game to the given index
    /// </summary>
    /// <param name="qualityIndex">The index corresponding to the graphic setting in project settings</param>
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    
    /// <summary>
    /// Sets the application to fullscreen or windowed mode
    /// </summary>
    /// <param name="isFullscreen">The state of fullscreen</param>
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    
    /// <summary>
    /// Sets the resolution of the game to the given index
    /// </summary>
    /// <param name="resolutionIndex">The index of the resolution in the list</param>
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
