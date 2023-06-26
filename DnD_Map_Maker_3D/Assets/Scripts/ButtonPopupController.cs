using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class to controll a specific popup window to display errors or other messages
/// </summary>
public class ButtonPopupController : PopupController
{
    [SerializeField]
    private GameObject popup;
    [SerializeField]
    private TMP_Text popupText;
    [SerializeField]
    private TMP_Text buttonText;
    [SerializeField]
    private Button button;
    
    /// <summary>
    /// Shows the popup with the specified text
    /// </summary>
    /// <param name="text">The text the popup should contain</param>
    public override void ShowPopup(string text)
    {
        popupText.text = text;
        buttonText.text = "OK";
        button.onClick.AddListener(ClosePopup);
        popup.SetActive(true);
    }

    public void ShowPopup(string text, string textOnButton, Action buttonAction)
    {
        popupText.text = textOnButton;
        buttonText.text = textOnButton;
        button.onClick.AddListener(delegate { buttonAction(); });
        button.onClick.AddListener(ClosePopup);
        popup.SetActive(true);
    }

    public override void ClosePopup()
    {
        popupText.text = "";
        buttonText.text = "";
        button.onClick.RemoveAllListeners();
        popup.SetActive(false);
    }
}
