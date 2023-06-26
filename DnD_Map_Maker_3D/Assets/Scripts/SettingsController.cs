using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
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
            options.Add(resolution.width + " x " + resolution.height);
            if (resolution.width == Screen.currentResolution.width &&
                resolution.height == Screen.currentResolution.height)
                currentResolutionIndex = Array.IndexOf(resolutions, resolution);
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
