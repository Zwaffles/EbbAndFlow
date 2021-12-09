using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

public class InfectedSpeedModifier
{
    public string stage = "Stage 1";
    [SerializeField] private int infectionScoreTrigger;
    [SerializeField] private int speedModifier;

    public int InfectionScoreTrigger { get { return infectionScoreTrigger; } }
    public int SpeedModifier { get { return speedModifier; } }
}
