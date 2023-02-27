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
    public void SubmitName(string arg0)
    {
        WebResponse v = null;
        try
        {
            v = WebRequest.Create("http://" + arg0 + ":5180/GameObject/TestConnection").GetResponse();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            inF.image.color = Color.red;
        }
        if (v is not null)
        {
            StreamReader r = new StreamReader(v.GetResponseStream());
            var s = r.ReadLine();
            Debug.Log(s);
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
        else
        {
            inF.image.color = Color.red;
        }
    }
}