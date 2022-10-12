using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InfectedHealthModifier
{
    public string stage = "Stage 1";
    [SerializeField] private int infectionScoreTrigger;
    [SerializeField] private float healthModifier;
    
    public int InfectionScoreTrigger { get { return infectionScoreTrigger; } }
    public float HealthModifier { get { return healthModifier; } }
    
}
