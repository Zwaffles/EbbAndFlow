using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionCyst : MonoBehaviour
{
    private SpriteMask infectionSpriteMask;

    private void Start()
    {
        infectionSpriteMask = GetComponentInChildren<SpriteMask>();
    }
}