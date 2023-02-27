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
    [FormerlySerializedAs("InF")] public TMP_InputField inF;

    public void SubmitName()
    {
        Thread t = new Thread(() =>
        {
            try
            {
                Debug.Log("http://" + inF.text + ":5180/GameObject/TestConnection");
                var v = WebRequest.Create("http://" + inF.text + ":5180/GameObject/TestConnection").Proxy = null;
                var v1 = v.GetResponse();
                StreamReader r = new StreamReader(v.GetResponseStream()!);
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
                inF.image.color = Color.red;
            }
        });
        t.Name = "Conn";
        t.Priority = ThreadPriority.AboveNormal;
        t.Start();
    }
}