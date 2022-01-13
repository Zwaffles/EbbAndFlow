using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeScaleManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeScaleText;

    public void TogggleTimeScale()
    {
        Time.timeScale = Time.timeScale == 1.0f ? 1.5f : Time.timeScale == 1.5f ? 2.0f : 1.0f;
        timeScaleText.text = "Timescale\n" + Time.timeScale + "x";
    }
}