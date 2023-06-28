using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Class to control a specific popup window to display errors or other messages
/// </summary>
public class ErrorPopupController : PopupController
{
    [SerializeField]
    private GameObject popup;
    [SerializeField]
    private TMP_Text popupText;
    
    /// <summary>
    /// Shows the popup with the specified text
    /// </summary>
    /// <param name="text">The text the popup should contain</param>
    public override void ShowPopup(string text)
    {
        popupText.text = text;
        popup.SetActive(true);
    }
    
    /// <summary>
    /// Closes the Popup and clears the text.
    /// </summary>
    public override void ClosePopup()
    {
        popupText.text = "";
        popup.SetActive(false);
    }
}
