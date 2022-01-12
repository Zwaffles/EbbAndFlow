using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public abstract class TowerUpgrade
{
    public string upgrade = "Upgrade 1";

    [Header("Cost")]
    [SerializeField] private int upgradeCost = 5;

    public int UpgradeCost { get { return upgradeCost; } set { upgradeCost = value; } }
}