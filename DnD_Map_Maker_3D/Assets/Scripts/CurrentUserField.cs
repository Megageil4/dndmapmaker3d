using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Class to attach to the UI prefab for a user field.
/// Is used by the CurrentUserController to set the username
/// </summary>
public class CurrentUserField : MonoBehaviour
{
    [SerializeField]
    private TMP_Text username;
    
    public void SetUsername(string uName)
    {
        username.SetText(uName);
    }
}
