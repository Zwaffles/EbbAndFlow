using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class LightningTowerUpgrade : TowerUpgrade
{
    [Header("Upgrades")]
    [Range(0.0f, 1.0f)]
    [Tooltip("Increase in percent!")]
    [SerializeField] private float damageIncrease = 0.25f;
    [Range(0.0f, 1.0f)]
    [Tooltip("Increase in percent!")]
    [SerializeField] private float rangeIncrease = 0.25f;
    [Range(0.0f, 1.0f)]
    [Tooltip("Increase in percent!")]
    [SerializeField] private float fireRateIncrease = 0.25f;
    [SerializeField] private int numberOfTargets = 1;

    public float DamageIncrease { get { return damageIncrease; } set { damageIncrease = value; } }
    public float RangeIncrease { get { return rangeIncrease; } set { rangeIncrease = value; } }
    public float FireRateIncrease { get { return fireRateIncrease; } set { fireRateIncrease = value; } }
    public int NumberOfTargets { get { return numberOfTargets; } set { numberOfTargets = value; } }

    public LightningTowerUpgrade()
    {
        UpgradeCost = 0;
        damageIncrease = 0;
        rangeIncrease = 0;
        fireRateIncrease = 0;
        numberOfTargets = 0;
    }
}