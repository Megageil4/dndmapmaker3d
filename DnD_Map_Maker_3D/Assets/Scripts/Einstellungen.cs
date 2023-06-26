using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// Class should have been used to change the settings of the program
/// but was not implemented for now due to time constraints.
/// May be implemented in the future
/// </summary>
public class Einstellungen : MonoBehaviour
{
    public TMP_Dropdown grafik;
    [FormerlySerializedAs("MotionBlur")] [SerializeField] public Toggle motionBlur;

    public GameObject self;
    // Start is called before the first frame update
    void Start()
    {
        self.SetActive(false);
        grafik.ClearOptions();
        grafik.AddOptions(new List<string>() {"Niedrig","Mittel","Hoch","Ultra"});
    }

    public void OnEinstellugen()
    {
        self.SetActive(true);
    }

    public void OnSave()
    {
        self.SetActive(false);
    }
}
