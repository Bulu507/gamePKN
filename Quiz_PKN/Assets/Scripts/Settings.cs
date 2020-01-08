using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    Text persenTeks;

    // Start is called before the first frame update
    void Start()
    {
        persenTeks = GameObject.Find("Vol_percent").GetComponent<Text>();
    }

    // teks untuk persen
    public void Volume(float value)
    {
        persenTeks.text = Mathf.RoundToInt(value * 100) + " %";
    }
}
