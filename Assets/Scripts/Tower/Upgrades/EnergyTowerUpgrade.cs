using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EnergyTowerUpgrade: TowerUpgrade
{
    [Header("Upgrades")]
    [SerializeField] private int currencyPerWave = 1;

    public int CurrencyPerWave { get { return currencyPerWave; } set { currencyPerWave = value; } }   

    public EnergyTowerUpgrade()
    {
        UpgradeCost = 0;
        currencyPerWave = 0;
    }
}