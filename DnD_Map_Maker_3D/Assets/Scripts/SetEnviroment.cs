using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEnviroment : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Environment.SetEnvironmentVariable("NOPROXY","10.0.207.3");   
    }
}
