using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EnergyTowerUpgrade
{
    public string upgrade = "Upgrade 1";

    [Header("Upgrades")]
    [SerializeField] private int currencyPerWave = 1;

    [Header("Cost")]
    [SerializeField] private int upgradeCost = 1;

    public int CurrencyPerWave { get { return currencyPerWave; } set { currencyPerWave = value; } }
    public int UpgradeCost { get { return upgradeCost; } set { upgradeCost = value; } }
}