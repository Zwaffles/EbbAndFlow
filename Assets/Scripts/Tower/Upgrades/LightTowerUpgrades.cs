using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTowerUpgrades : TowerUpgrades
{
    [SerializeField] private List<LightTowerUpgrade> lightTowerUpgrades = new List<LightTowerUpgrade>();
    private AttackTower attackTower;

    private void Awake()
    {
        attackTower = GetComponent<AttackTower>();
    }

    public override bool CanUpgrade()
    {
        if (CurrentUpgrade < (lightTowerUpgrades.Count - 1))
        {
            /* Can afford upgrade */
            if (GameManager.Instance.PlayerCurrency.CanBuy(lightTowerUpgrades[CurrentUpgrade + 1].UpgradeCost))
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
            GameManager.Instance.PlayerCurrency.RemovePlayerNormalCurrency(lightTowerUpgrades[CurrentUpgrade].UpgradeCost);
        }

        /* Tower not upgraded */
        if (CurrentUpgrade < 0)
        {
            attackTower.Damage = attackTower.BaseDamage * (1.0f + GameManager.Instance.UpgradeManager.PermanentLightTowerUpgrades.DamageIncrease);
            attackTower.Range = attackTower.BaseRange * (1.0f + GameManager.Instance.UpgradeManager.PermanentLightTowerUpgrades.RangeIncrease);
            attackTower.FireRate = attackTower.BaseFireRate - (attackTower.BaseFireRate * GameManager.Instance.UpgradeManager.PermanentLightTowerUpgrades.FireRateIncrease);
        }
        else
        {
            attackTower.Damage = attackTower.BaseDamage * (1.0f + lightTowerUpgrades[CurrentUpgrade].DamageIncrease + GameManager.Instance.UpgradeManager.PermanentLightTowerUpgrades.DamageIncrease);
            attackTower.Range = attackTower.BaseRange * (1.0f + lightTowerUpgrades[CurrentUpgrade].RangeIncrease + GameManager.Instance.UpgradeManager.PermanentLightTowerUpgrades.RangeIncrease);
            attackTower.FireRate = attackTower.BaseFireRate - (attackTower.BaseFireRate * (lightTowerUpgrades[CurrentUpgrade].FireRateIncrease + GameManager.Instance.UpgradeManager.PermanentLightTowerUpgrades.FireRateIncrease));
        }
     
        attackTower.GetComponent<TowerRangeOutline>().UpdateOutline();
    }
}