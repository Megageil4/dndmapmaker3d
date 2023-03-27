using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectController : MonoBehaviour
{
    public GameObject Prefab { get; set; }

    public void onObjectSelected(GameObject prefab)
    {
        Prefab = prefab;
    }
}
