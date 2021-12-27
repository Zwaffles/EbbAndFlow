using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyTowerUpgrades : TowerUpgrades
{
    [SerializeField] private List<EnergyTowerUpgrade> energyTowerUpgrades = new List<EnergyTowerUpgrade>();
    private CurrencyTower currencyTower;

    private void Awake()
    {
        currencyTower = GetComponent<CurrencyTower>();
    }

    public override bool CanUpgrade()
    {
        if (CurrentUpgrade < (energyTowerUpgrades.Count - 1))
        {
            /* Can afford upgrade */
            if (GameManager.Instance.PlayerCurrency.CanBuy(energyTowerUpgrades[CurrentUpgrade + 1].UpgradeCost))
            {
                return true;
            }
            /* Cant afford upgrade */
            else
            {
                return false;
            }
        }
        /* No more upgrades available */
        else
        {
            return false;
        }
    }

    public override void UpgradeTower()
    {
        base.UpgradeTower();
        CurrentUpgrade++;
        currencyTower.CurrencyPerWave += energyTowerUpgrades[CurrentUpgrade].CurrencyPerWave;
        GameManager.Instance.PlayerCurrency.RemovePlayerNormalCurrency(energyTowerUpgrades[CurrentUpgrade].UpgradeCost);
    }
}