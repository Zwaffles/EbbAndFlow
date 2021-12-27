using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PulsarTowerUpgrade
{
    public string upgrade = "Upgrade 1";

    [Header("Upgrades")]
    [SerializeField] private float damageIncrease = 5.0f;
    [SerializeField] private float rangeIncrease = 1.0f;
    [SerializeField] private float fireRateIncrease = 0.2f;
    [SerializeField] private float splashRadius = 0.25f;
    [SerializeField] private float splashDamage = 5.0f;

    [Header("Cost")]
    [SerializeField] private int upgradeCost = 5;

    public float DamageIncrease { get { return damageIncrease; } set { damageIncrease = value; } }
    public float RangeIncrease { get { return rangeIncrease; } set { rangeIncrease = value; } }
    public float FireRateIncrease { get { return fireRateIncrease; } set { fireRateIncrease = value; } }
    public float SplashRadius { get { return splashRadius; } set { splashRadius = value; } }
    public float SplashDamage { get { return splashDamage; } set { splashDamage = value; } }
    public int UpgradeCost { get { return upgradeCost; } set { upgradeCost = value; } }
}