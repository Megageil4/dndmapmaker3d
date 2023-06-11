using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// A custom input validator for the ip address input field
/// </summary>
[Serializable]
// Add a menu item to the TextMeshPro menu to create a new IPValidator
[CreateAssetMenu(fileName = "InputValidator - IP Address.asset", menuName = "TextMeshPro/Input Validators/IP Adress", order = 100)]
public class IPValidator : TMP_InputValidator
{
    /// <summary>
    /// Gets called when the user enters a character in the input field and validates it
    /// </summary>
    /// <param name="text">The text in the textbox</param>
    /// <param name="pos">The current position of the cursor in the text box</param>
    /// <param name="ch">The typed character</param>
    /// <returns>Returns the typed character if it is valid and a null character if not</returns>
    public override char Validate(ref string text, ref int pos, char ch)
    {
        //Check if the character is part of an ip address structur (0-9 or .)
        //and if it is appand it to the string at the current position and increase the position
        if ((char.IsDigit(ch) || ch == '.') && pos < 15)
        {
            text += ch;
            pos++;
            return ch;
        }
        //if the character is not part of an ip address structur, return a null character
        return '\0';
    }
}
