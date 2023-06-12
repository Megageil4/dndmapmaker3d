using System;
using System.IO;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

/// <summary>
/// Class for the login screen
/// </summary>
public class Login : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField serverIp;

    [SerializeField]
    private PopupController popup;

    
    /// <summary>
    /// Gets called when the user clicks the connect button.
    /// Test the server connection and if it works, loads the next scene
    /// </summary>
    public void ConnectToServer()
    {
        try
        {
            var webRequest = WebRequest.Create("http://" + serverIp.text + ":443/DnD/TestConnection");
            webRequest.Proxy = null;
            var responseString =  Connect(webRequest); //TODO: Change to use method from connection class
            if ("Connection erstellt" == responseString)
            {
                DataContainer.ServerIP = serverIp.text;
                SceneManager.LoadScene("SampleScene");
            }
            else
            {
                popup.ShowPopup("Server responded with invalid message. " +
                                "Please check if the server is running the correct version.");
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            popup.ShowPopup("Server not found. Please check if the server is running and the ip is correct.");
        }
    }
    private string Connect(WebRequest webRequest)
    {
        using var response = webRequest.GetResponse();
        using StreamReader streamReader = new StreamReader(response.GetResponseStream()!);
        return streamReader.ReadLine();
    }
}