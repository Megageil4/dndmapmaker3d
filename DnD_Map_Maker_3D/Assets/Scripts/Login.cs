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
    /// <summary>
    /// 
    /// </summary>
    [FormerlySerializedAs("inF")] [FormerlySerializedAs("InF")] public TMP_InputField serverIp;

    
    /// <summary>
    /// Gets called when the user clicks the connect button.
    /// Test the server connection and if it works, loads the next scene
    /// </summary>
    public void ConnectToServer()
    {
        try
        {
            var webRequest = WebRequest.Create("http://" + serverIp.text + ":5180/DnD/TestConnection");
            webRequest.Proxy = null;
            // Task<string> d = ;
            var responseString =  Connect(webRequest);
            if ("Connection erstellt" == responseString)
            {
                serverIp.image.color = Color.green;
                DataContainer.ServerIP = serverIp.text;
                SceneManager.LoadScene("SampleScene");
            }
            else
            {
                serverIp.image.color = Color.red;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            serverIp.image.color = Color.red;
        }
    }
    public string Connect(WebRequest webRequest)
    {
        using (var response = webRequest.GetResponse())
        {
            using (StreamReader streamReader = new StreamReader(response.GetResponseStream()!))
            {
                return streamReader.ReadLine();   
            }
        }
    }
}