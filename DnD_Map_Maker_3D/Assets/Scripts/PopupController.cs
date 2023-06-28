using UnityEngine;

public abstract class PopupController : MonoBehaviour
{
    /// <summary>
    /// Shows the popup with the specified text
    /// </summary>
    /// <param name="text">The text the popup should contain</param>
    public abstract void ShowPopup(string text);

    /// <summary>
    /// Closes the Popup.
    /// </summary>
    public abstract void ClosePopup();
}