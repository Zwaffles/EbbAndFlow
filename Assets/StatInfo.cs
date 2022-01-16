using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class StatInfo
{
    public enum StatType
    {
        Damage, AttackSpeed, Range, Currency, InfectionScore, MovementSpeed, Health, Armor
    }

    [SerializeField] private StatType stat;
    [SerializeField] private Sprite statIcon;

    private float baseStat;
    private float currentStat;

    public StatType Stat { get { return stat; } }
    public Sprite StatIcon { get { return statIcon; } }
    public float BaseStat { get { return baseStat; } set { baseStat = value; } }
    public float CurrentStat { get { return currentStat; } set { currentStat = value; } }
}