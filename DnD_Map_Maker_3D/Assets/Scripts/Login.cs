using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

/// <summary>
/// Class for the login screen
/// </summary>
public class Login : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField serverIp;

    [SerializeField]
    private PopupController popup;

    [SerializeField]
    private TMP_InputField username;
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
            var responseString =  Connect(webRequest); 
            if ("Connection erstellt" == responseString)
            {
                DataContainer.ServerIP = serverIp.text;
                DataContainer.WebserviceConnection = Process.Start(@"..\Int5.DnD3D.WebClient\Int5.DnD3D.WebClient\bin\Debug\net6.0\Int5.DnD3D.WebClient.exe", DataContainer.ServerIP + " " + username.text);
                DataContainer.WebserviceConnection!.StartInfo.CreateNoWindow = true;
                while (!File.Exists(Path.GetTempPath() + @$"/DnD/0"))
                { }
                string content = File.ReadAllText(Path.GetTempPath() + "/DnD/" + 0);
                DataContainer.ClientId = Guid.Parse(content.Substring(1, content.Length - 1));
                File.Delete(Path.GetTempPath() + "/DnD/0");
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