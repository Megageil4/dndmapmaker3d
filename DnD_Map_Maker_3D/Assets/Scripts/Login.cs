using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Login : MonoBehaviour
{
    // Was ist inF??? -> bessere Namen
    [FormerlySerializedAs("InF")] public TMP_InputField inF;

    public void login()
    {
        SubmitName();
    }

    public void SubmitName()
    {
        try
        {
            // Bitte bessere Variablen Namen!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // Kein v / v1 / t !!!!!!!!
            // Und bitte keinen code mit syntax error pushen, findet unity nicht toll
            // doch
            var webRequest = WebRequest.Create("http://" + inF.text + ":5180/GameObject/TestConnection");
            webRequest.Proxy = null;
            // Task<string> d = ;
            var responseString =  connect(webRequest);
            if ("Connection erstellt" == responseString)
            {
                inF.image.color = Color.green;
                DataContainer.ServerIP = inF.text;
                SceneManager.LoadScene("SampleScene");
            }
            else
            {
                inF.image.color = Color.red;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            inF.image.color = Color.red;
        }
    }
    public string /*async Task<string>*/ connect(WebRequest webRequest)
    {
        var response = webRequest.GetResponse();
        StreamReader streamReader = new StreamReader(response.GetResponseStream()!);
        return streamReader.ReadLine();
    }
}