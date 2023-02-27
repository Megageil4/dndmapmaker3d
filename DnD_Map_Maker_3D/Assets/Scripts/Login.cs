using System;
using System.IO;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Login : MonoBehaviour
{
    [FormerlySerializedAs("InF")] public TMP_InputField inF;

    public void SubmitName()
    {
        try
        {
            Debug.Log("http://" + inF.text + ":5180/GameObject/TestConnection");
            WebRequest webRequest = WebRequest.Create("http://" + inF.text + ":5180/GameObject/TestConnection");
            StreamReader r = new StreamReader(webRequest.GetResponse().GetResponseStream()!);
            var s = r.ReadLine();
            // Debug.Log(s);
            if ("Connection erstellt" == s)
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
            Debug.Log(ex);
            inF.image.color = Color.red;
        }
    }
}