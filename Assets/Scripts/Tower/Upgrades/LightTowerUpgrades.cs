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

    public override void UpgradeTower()
    {
        base.UpgradeTower();

        CurrentUpgrade++;
        attackTower.Damage *= (1.0f + lightTowerUpgrades[CurrentUpgrade].DamageIncrease);
        attackTower.Range *= (1.0f + lightTowerUpgrades[CurrentUpgrade].RangeIncrease);
        attackTower.FireRate *= (1.0f + lightTowerUpgrades[CurrentUpgrade].FireRateIncrease);
        attackTower.GetComponent<TowerRangeOutline>().UpdateOutline();
        GameManager.Instance.PlayerCurrency.RemovePlayerNormalCurrency(lightTowerUpgrades[CurrentUpgrade].UpgradeCost);
    }
}