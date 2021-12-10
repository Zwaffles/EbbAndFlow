using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

public class InfectedSpeedModifier
{
    public string stage = "Stage 1";
    [SerializeField] private int infectionScoreTrigger;
    [SerializeField] private float increasedSpeedModifier;
    [SerializeField] private float slowSpeedModifier;

    public int InfectionScoreTrigger { get { return infectionScoreTrigger; } }
    public float IncreasedSpeedModifier { get { return increasedSpeedModifier; } }
    public float SlowSpeedModifier { get { return slowSpeedModifier; } }
}
