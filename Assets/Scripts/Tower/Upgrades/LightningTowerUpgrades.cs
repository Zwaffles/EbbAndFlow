using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTowerUpgrades : TowerUpgrades
{
    [SerializeField] private List<LightningTowerUpgrade> lightningTowerUpgrades = new List<LightningTowerUpgrade>();
    private AttackTower attackTower;

    private void Awake()
    {
        attackTower = GetComponent<AttackTower>();
    }

    public override bool CanUpgrade()
    {
        if (CurrentUpgrade < (lightningTowerUpgrades.Count - 1))
        {
            /* Can afford upgrade */
            if (GameManager.Instance.PlayerCurrency.CanBuy(lightningTowerUpgrades[CurrentUpgrade + 1].UpgradeCost))
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
        attackTower.Damage += lightningTowerUpgrades[CurrentUpgrade].DamageIncrease;
        attackTower.Range += lightningTowerUpgrades[CurrentUpgrade].RangeIncrease;
        attackTower.FireRate += lightningTowerUpgrades[CurrentUpgrade].FireRateIncrease;
        attackTower.NumberOfTargets += lightningTowerUpgrades[CurrentUpgrade].NumberOfTargets;
        attackTower.GetComponent<TowerRangeOutline>().UpdateOutline();
        GameManager.Instance.PlayerCurrency.RemovePlayerNormalCurrency(lightningTowerUpgrades[CurrentUpgrade].UpgradeCost);
    }
}