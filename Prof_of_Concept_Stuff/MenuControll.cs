using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuControll : MonoBehaviour
{
    public GameObject cubePrefab;
    public Text idField;
    
    public void OnClick()
    {
        HttpWebRequest request = (HttpWebRequest) WebRequest.Create($"http://localhost:8080/DnD/Cubes/{idField.text}");
        HttpWebResponse response = (HttpWebResponse) request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        var cube = GetCube(json);
        SpawnCube(cube);
    }

    public void ChessBoard()
    {
        HttpWebRequest request = (HttpWebRequest) WebRequest.Create($"http://localhost:8080/DnD/ChessBoard");
        HttpWebResponse response = (HttpWebResponse) request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        JArray jArray = JArray.Parse(json);
        foreach (var jToken in jArray)
        {
            var cube = GetCube(jToken.ToString());
            SpawnCube(cube);
        }
    }
    
    private static Cube GetCube(string json)
    {
        JObject jObject = JObject.Parse(json);
        int posx = jObject.GetValue("posx").Value<int>();
        int posy = jObject.GetValue("posy").Value<int>();
        int posz = jObject.GetValue("posz").Value<int>();
        int sizex = jObject.GetValue("scaleX").Value<int>();
        int sizey = jObject.GetValue("scaleY").Value<int>();
        int sizez = jObject.GetValue("scaleZ").Value<int>();
        string color = jObject.GetValue("color").Value<string>();
        Cube cube = ScriptableObject.CreateInstance<Cube>();
        cube.PosX = posx;
        cube.PosY = posy;
        cube.PosZ = posz;
        cube.SizeX = sizex;
        cube.SizeY = sizey;
        cube.SizeZ = sizez;
        cube.Color = color;
        return cube;
    }

    public void SpawnCube(Cube cube)
    {
        GameObject newCube = Instantiate(cubePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        newCube.GetComponent<CubeLoader>().cubePreset = cube;
    }
}
