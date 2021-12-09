using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InfectedCurrencyModifier
{
    public string stage = "Stage 1";
    [SerializeField] private int infectionScoreTrigger;
    [SerializeField] private int currencyModifier;
    
    public int InfectionScoreTrigger { get { return infectionScoreTrigger; } }
    public int CurrencyModifier { get { return currencyModifier; } }
    
}
