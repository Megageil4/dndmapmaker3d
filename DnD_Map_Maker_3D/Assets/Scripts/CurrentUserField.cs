using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentUserField : MonoBehaviour
{
    [SerializeField]
    private TMP_Text username;
    
    public void SetUsername(string uName)
    {
        username.SetText(uName);
    }
}
