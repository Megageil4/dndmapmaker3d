using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectController : MonoBehaviour
{
    public GameObject Prefab { get; set; }
    private GameObject _previousField { get; set; } 

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
}
