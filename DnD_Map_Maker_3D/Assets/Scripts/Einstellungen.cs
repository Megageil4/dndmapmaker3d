using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Einstellungen : MonoBehaviour
{
    public TMP_Dropdown grafik;
    [SerializeField] public Toggle MotionBlur;

    public GameObject self;
    // Start is called before the first frame update
    void Start()
    {
        self.SetActive(false);
        grafik.ClearOptions();
        grafik.AddOptions(new List<string>() {"Niedrig","Mittel","Hoch","Ultra"});
    }

    public void onEinstellugen()
    {
        self.SetActive(true);
    }

    public void onSave()
    {
        self.SetActive(false);
    }
}
