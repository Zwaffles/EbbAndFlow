using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleManager : MonoBehaviour
{
    public void SetTimeScale(float value)
    {
        Time.timeScale = value;
    }
}