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

    public override void UpgradeTower(bool increaseUpgradeIndex = true)
    {
        if (increaseUpgradeIndex)
        {
            CurrentUpgrade++;
            GameManager.Instance.PlayerCurrency.RemovePlayerNormalCurrency(lightningTowerUpgrades[CurrentUpgrade].UpgradeCost);
        }
        
        /* Tower not upgraded */
        if(CurrentUpgrade < 0)
        {
            attackTower.Damage = attackTower.BaseDamage * (1.0f + GameManager.Instance.UpgradeManager.PermanentLightningTowerUpgrades.DamageIncrease);
            attackTower.Range = attackTower.BaseRange * (1.0f + GameManager.Instance.UpgradeManager.PermanentLightningTowerUpgrades.RangeIncrease);
            attackTower.FireRate = attackTower.BaseFireRate * (1.0f + GameManager.Instance.UpgradeManager.PermanentLightningTowerUpgrades.FireRateIncrease);
            attackTower.NumberOfTargets = attackTower.BaseNumberOfTargets;
        }
        else
        {
            attackTower.Damage = attackTower.BaseDamage * (1.0f + lightningTowerUpgrades[CurrentUpgrade].DamageIncrease + GameManager.Instance.UpgradeManager.PermanentLightningTowerUpgrades.DamageIncrease);
            attackTower.Range = attackTower.BaseRange * (1.0f + lightningTowerUpgrades[CurrentUpgrade].RangeIncrease + GameManager.Instance.UpgradeManager.PermanentLightningTowerUpgrades.RangeIncrease);
            attackTower.FireRate = attackTower.BaseFireRate * (1.0f + lightningTowerUpgrades[CurrentUpgrade].FireRateIncrease + GameManager.Instance.UpgradeManager.PermanentLightningTowerUpgrades.FireRateIncrease);
            attackTower.NumberOfTargets = attackTower.BaseNumberOfTargets + lightningTowerUpgrades[CurrentUpgrade].NumberOfTargets;
        }
        
        attackTower.GetComponent<TowerRangeOutline>().UpdateOutline();
    }
}