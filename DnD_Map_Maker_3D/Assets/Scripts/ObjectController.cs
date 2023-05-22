using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Class for controlling the object menu on the right side of the screen
/// </summary>
public class ObjectController : MonoBehaviour
{
    /// <summary>
    /// The currently selected prefab
    /// </summary>
    public GameObject Prefab { get; set; }
    /// <summary>
    /// The previously selected tab
    /// </summary>
    public GameObject PreviousField {  get; private set; }
    /// <summary>
    /// List of all available models and their names to make saving and loading with WS possible
    /// </summary>
    public static Dictionary<string,GameObject> ModelTypes { get; set; }
    
    /// <summary>
    /// A struct used to make a pseudo dictionary for the model types used in the inspector
    /// </summary>
    [Serializable]
    public struct ModelType {
        public string name;
        public GameObject model;
    }
    /// <summary>
    /// List of all models set in the inspector with their names. Gets converted to a dictionary in the start method
    /// </summary>
    [FormerlySerializedAs("Models")] public ModelType[] models;

    /// <summary>
    /// Gets called when a prefab is selected in the object menu and writes it in the Prefab property
    /// </summary>
    /// <param name="prefab">The prefab set in the inspector that belongs to a button in the object menu</param>
    public void OnObjectSelected(GameObject prefab)
    {
        Prefab = prefab;
    }

    /// <summary>
    /// Gets called when a tab is selected in the object menu and sets the tab to active and the previous tab to inactive
    /// </summary>
    /// <param name="tab"></param>
    public void OnTabSelected(GameObject tab)
    {
        if (PreviousField != null)
        {
            PreviousField.SetActive(false);   
        }
        tab.SetActive(true);
        PreviousField = tab;
    }

    /// <summary>
    /// Used to fill the dictionary with the models set in the inspector
    /// </summary>
    private void Start()
    {
        ModelTypes = new();
        foreach (var model in models)
        {
            ModelTypes.Add(model.name, model.model);
        }
    }
}
