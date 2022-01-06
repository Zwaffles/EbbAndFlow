using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PulsarTowerUpgrade: TowerUpgrade
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
    [Range(0.0f, 1.0f)]
    [Tooltip("Increase in percent!")]
    [SerializeField] private float splashRadius = 0.25f;
    [Range(0.0f, 1.0f)]
    [Tooltip("Increase in percent!")]
    [SerializeField] private float splashDamage = 0.25f;

    public float DamageIncrease { get { return damageIncrease; } set { damageIncrease = value; } }
    public float RangeIncrease { get { return rangeIncrease; } set { rangeIncrease = value; } }
    public float FireRateIncrease { get { return fireRateIncrease; } set { fireRateIncrease = value; } }
    public float SplashRadius { get { return splashRadius; } set { splashRadius = value; } }
    public float SplashDamage { get { return splashDamage; } set { splashDamage = value; } }

    public PulsarTowerUpgrade()
    {
        UpgradeCost = 0;
        damageIncrease = 0;
        rangeIncrease = 0;
        fireRateIncrease = 0;
        splashRadius = 0;
        SplashDamage = 0;
    }
}