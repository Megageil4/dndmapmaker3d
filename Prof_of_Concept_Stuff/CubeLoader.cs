using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Color = System.Drawing.Color;

public class CubeLoader : MonoBehaviour
{
    public Cube cubePreset;
    private Cube _lastCubePreset;
    // Start is called before the first frame update
    void Start()
    {
        if (cubePreset)
        {
            transform.position = new Vector3(cubePreset.PosX, cubePreset.PosY, cubePreset.PosZ);
            transform.localScale = new Vector3(cubePreset.SizeX, cubePreset.SizeY, cubePreset.SizeZ);
            Color color = Color.FromName(cubePreset.Color);
            Material material = new Material(Shader.Find("Standard"));
            material.color = new UnityEngine.Color(color.R, color.G, color.B);
            GetComponent<Renderer>().sharedMaterial = material;
            _lastCubePreset = cubePreset;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(cubePreset != _lastCubePreset)
            Start();
    }
    
}
