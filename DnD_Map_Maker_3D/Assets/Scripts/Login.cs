using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;
using System.Collections;

/// <summary>
/// Class for the login screen
/// </summary>
public class Login : MonoBehaviour
{
    [SerializeField] private TMP_InputField serverIp;

    [SerializeField] private PopupController popup;

    [SerializeField] private TMP_InputField username;
    [SerializeField] private ButtonPopupController buttonPopup;

    /// <summary>
    /// Gets called when the user clicks the connect button.
    /// Test the server connection and if it works, loads the next scene
    /// </summary>
    public void ConnectToServer()
    {
        try
        {
            Debug.Log("http://" + serverIp.text + ":443/DnD/TestConnection");
            // sets up the request
            var webRequest = WebRequest.Create("http://" + serverIp.text + ":443/DnD/TestConnection");
            webRequest.Proxy = null;
            var responseString = Connect(webRequest);
            Debug.Log(responseString);
            // if the connection was successful
            if ("Connection erstellt" == responseString)
            {
                // saves the server ip
                DataContainer.ServerIP = serverIp.text;
                // starts the websocket connection
                string arg = DataContainer.ServerIP + " " + username.text;
                DataContainer.WebserviceConnection.StartInfo.Arguments = arg;
// to check if its a build or executen in unity environment, for file path                
#if DEBUG
                DataContainer.WebserviceConnection.StartInfo.FileName =
                    @"..\Int5.DnD3D.WebClient\Int5.DnD3D.WebClient\bin\Debug\net6.0\Int5.DnD3D.WebClient.exe";
#else
                DataContainer.WebserviceConnection.StartInfo.FileName =
                    @".\WebClient\Int5.DnD3D.WebClient.exe";
#endif
                
                DataContainer.WebserviceConnection.Start();

                // waits for the first message from the Socket
                while (!File.Exists(Path.Combine(Path.GetTempPath(), "DnD", "0")))
                {
                }
                
                // reads the message from the socket and formats it
                string content = File.ReadAllText(Path.Combine(Path.GetTempPath(), "DnD", "0"));
                File.Delete(Path.Combine(Path.GetTempPath(), "DnD", "0"));
                content = content.Substring(1, content.Length - 1);

                // if the user does not exist
                if (content == "nu")
                { 
                    // shows a popup to create the user
                    buttonPopup.ShowPopup("User does not exist. Do you wannt to create it?", "Save User",
                        () =>
                        {
                            StartCoroutine(PostRequest($"http://{DataContainer.ServerIP}:443/DnD/User",
                                JsonConvert.SerializeObject(username.text), "POST"));
                            // closes the old socket connection
                            DataContainer.WebserviceConnection.Close();
                            Debug.Log("cmd geschlossen");
                            // sets up the socket connection with the now created user
                            DataContainer.WebserviceConnection.StartInfo.Arguments = arg;
#if DEBUG
                            DataContainer.WebserviceConnection.StartInfo.FileName =
                                @"..\Int5.DnD3D.WebClient\Int5.DnD3D.WebClient\bin\Debug\net6.0\Int5.DnD3D.WebClient.exe";
#else
                DataContainer.WebserviceConnection.StartInfo.FileName =
                    @".\WebClient\Int5.DnD3D.WebClient.exe";
#endif
                            DataContainer.WebserviceConnection.Start();
                            //Change path on build
                            
                            // waits for the initail message from the Socket
                            while (!File.Exists(Path.Combine(Path.GetTempPath(), "DnD", "0"))) 
                            {}

                            // reads the message from the socket and formats it
                            content = File.ReadAllText(Path.Combine(Path.GetTempPath(), "DnD", "0"));
                            File.Delete(Path.Combine(Path.GetTempPath(), "DnD", "0"));
                            content = content.Substring(1, content.Length - 1);
                            
                            // saves the client id
                            DataContainer.ClientId = Guid.Parse(content);

                            // deletes the file and loads the next scene
                            File.Delete(Path.Combine(Path.GetTempPath(), "DnD", "0"));
                            SceneManager.LoadScene("SampleScene");
                        });
                }
                else
                {
                    // saves the client id
                    DataContainer.ClientId = Guid.Parse(content);

                    // deletes the file and loads the next scene
                    File.Delete(Path.Combine(Path.GetTempPath(), "DnD", "0"));
                    SceneManager.LoadScene("SampleScene");
                }
            }
            else
            {
                // when the server could not be found
                popup.ShowPopup("Server responded with invalid message. " +
                                "Please check if the server is running the correct version.");
            }
        }
        catch (IOException e)
        {
            // sometimes a file gets accessed by to programs at the same time
            Debug.Log(e);
        }
        catch (Exception e)
        {
            // if anything goes wrong
            Debug.Log(e.Message);
            popup.ShowPopup("Server not found. Please check if the server is running and the ip is correct.");
        }
    }

    /// <summary>
    /// sends a WebRequest to a Webserver to check if it is up and running and returns the response
    /// </summary>
    /// <param name="webRequest">WebRequest to be sent to the Server</param>
    /// <returns>the answer from the webRequest</returns>
    private string Connect(WebRequest webRequest)
    {
        using var response = webRequest.GetResponse();
        using StreamReader streamReader = new StreamReader(response.GetResponseStream()!);
        return streamReader.ReadLine();
    }

    /// <summary>
    /// Sends A WebRequest to a Webserver
    /// </summary>
    /// <param name="url">URL of the Webserver</param>
    /// <param name="json">JSON string that will be sent to the server</param>
    /// <param name="method">Request Type (POST/PUT)</param>
    /// <returns></returns>
    IEnumerator PostRequest(string url, string json, string method)
    {
        // Debug.Log($"Sending data to {url}");
        using UnityWebRequest www = new UnityWebRequest(url, method);
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log($"Error while Sending: {www.error} , {url}");
        }

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Upload complete!");

        }

    }
}