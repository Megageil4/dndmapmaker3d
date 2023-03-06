using System;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Threading;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Task = UnityEditor.VersionControl.Task;
using ThreadPriority = System.Threading.ThreadPriority;

public class Login : MonoBehaviour
{
    // Was ist inF??? -> bessere Namen
    [FormerlySerializedAs("InF")] public TMP_InputField inF;

    public void SubmitName()
    {
        Thread thread = new Thread(() =>
        {
            try
            {
                // Bitte bessere Variablen Namen!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                // Kein v / v1 / t !!!!!!!!
                // Und bitte keinen code mit syntax error pushen, findet unity nicht toll
                Debug.Log("http://" + inF.text + ":5180/GameObject/TestConnection");
                var webRequest = WebRequest.Create("http://" + inF.text + ":5180/GameObject/TestConnection");
                webRequest.Proxy = null;
                var response = webRequest.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream()!);
                var responseString = streamReader.ReadLine();
                // Debug.Log(s);
                if ("Connection erstellt" == responseString)
                {
                    inF.image.color = Color.green;
                    SceneManager.LoadScene("SampleScene");
                }
                else
                {
                    inF.image.color = Color.red;
                }
            }
            catch (Exception ex)
            {
                inF.image.color = Color.red;
            }
        });
        thread.Name = "Conn";
        thread.Priority = ThreadPriority.AboveNormal;
        thread.Start();
    }
}