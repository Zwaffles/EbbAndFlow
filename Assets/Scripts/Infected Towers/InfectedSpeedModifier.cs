using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InfectedSpeedModifier
{
    public string stage = "Stage 1";
    [SerializeField] private int infectionScoreTrigger;
    [SerializeField] private float speedModifier;
    [SerializeField] private bool globalRange;
    [SerializeField] private bool stackable;
 
    public int InfectionScoreTrigger { get { return infectionScoreTrigger; } }
    public float SpeedModifier { get { return speedModifier; } }
    public bool GlobalRange { get { return globalRange; } }
    public bool Stackable { get { return stackable; } }
}