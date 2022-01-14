using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InfectedDamageModifier 
{
    public string stage = "Stage 1";
    [SerializeField] private int infectionScoreTrigger;
    [SerializeField] private float damageModifierValue;
    [SerializeField] private bool globalRange;
    [SerializeField] private bool stackable;
    
    public int InfectionScoreTrigger { get { return infectionScoreTrigger; } }
    public float DamageModifierValue { get { return damageModifierValue; } set { damageModifierValue = value; } }
    public bool GlobalRange { get { return globalRange; } }
    public bool Stackable { get { return stackable; } }
}