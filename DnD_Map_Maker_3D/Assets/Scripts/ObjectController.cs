using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectController : MonoBehaviour
{
    public GameObject Prefab { get; set; }
    private GameObject _previousField { get; set; }
    public static Dictionary<string,GameObject> ModelTypes { get; set; }
    
    [Serializable]
    public struct ModelType {
        public string name;
        public GameObject model;
    }
    public ModelType[] Models;

    public void onObjectSelected(GameObject prefab)
    {
        Prefab = prefab;
    }

    public void OnTabSelected(GameObject tab)
    {
        if (_previousField != null)
        {
            _previousField.SetActive(false);   
        }
        tab.SetActive(true);
        _previousField = tab;
    }

    private void Start()
    {
        ModelTypes = new();
        foreach (var model in Models)
        {
            ModelTypes.Add(model.name, model.model);
        }
    }
}
