using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class to control a specific popup window to display errors or other messages.
/// This version of the popup contains an extra button that can be defined.
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
    /// Shows the button popup with the specified text.
    /// The default button displays the text "OK" and closes the popup
    /// </summary>
    /// <param name="text">The text the popup should contain</param>
    public override void ShowPopup(string text)
    {
        popupText.text = text;
        buttonText.text = "OK";
        button.onClick.AddListener(ClosePopup);
        popup.SetActive(true);
    }

    /// <summary>
    /// Shows the popup with the specified text, button text and action
    /// </summary>
    /// <param name="text">The text the popup should contain</param>
    /// <param name="textOnButton">The text of the button</param>
    /// <param name="buttonAction">The action that should be done when the button is clicked</param>
    public void ShowPopup(string text, string textOnButton, Action buttonAction)
    {
        popupText.text = textOnButton;
        buttonText.text = textOnButton;
        button.onClick.AddListener(delegate { buttonAction(); });
        button.onClick.AddListener(ClosePopup);
        popup.SetActive(true);
    }

    /// <summary>
    /// Closes the Popup and clears the text and button as well as the action
    /// </summary>
    public override void ClosePopup()
    {
        popupText.text = "";
        buttonText.text = "";
        button.onClick.RemoveAllListeners();
        popup.SetActive(false);
    }
}
