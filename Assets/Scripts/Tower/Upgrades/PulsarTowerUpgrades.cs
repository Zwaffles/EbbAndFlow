using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsarTowerUpgrades : TowerUpgrades
{
    [SerializeField] private List<PulsarTowerUpgrade> pulsarTowerUpgrades = new List<PulsarTowerUpgrade>();
    private AttackTower attackTower;

    private void Awake()
    {
        attackTower = GetComponent<AttackTower>();
    }

    public override bool CanUpgrade()
    {
        if (CurrentUpgrade < (pulsarTowerUpgrades.Count - 1))
        {
            /* Can afford upgrade */
            if (GameManager.Instance.PlayerCurrency.CanBuy(pulsarTowerUpgrades[CurrentUpgrade + 1].UpgradeCost))
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
            GameManager.Instance.PlayerCurrency.RemovePlayerNormalCurrency(pulsarTowerUpgrades[CurrentUpgrade].UpgradeCost);
        }

        /* Tower not upgraded */
        if (CurrentUpgrade < 0)
        {
            attackTower.Damage = attackTower.BaseDamage * (1.0f + GameManager.Instance.UpgradeManager.PermanentPulsarTowerUpgrades.DamageIncrease);
            attackTower.Range = attackTower.BaseRange * (1.0f + GameManager.Instance.UpgradeManager.PermanentPulsarTowerUpgrades.RangeIncrease);
            attackTower.FireRate = attackTower.BaseFireRate * (1.0f + GameManager.Instance.UpgradeManager.PermanentPulsarTowerUpgrades.FireRateIncrease);
            attackTower.SplashRadius = attackTower.BaseSplashRadius;
            attackTower.SplashDamage = attackTower.BaseSplashDamage;
        }
        else
        {
            attackTower.Damage = attackTower.BaseDamage * (1.0f + pulsarTowerUpgrades[CurrentUpgrade].DamageIncrease + GameManager.Instance.UpgradeManager.PermanentPulsarTowerUpgrades.DamageIncrease);
            attackTower.Range = attackTower.BaseRange * (1.0f + pulsarTowerUpgrades[CurrentUpgrade].RangeIncrease + GameManager.Instance.UpgradeManager.PermanentPulsarTowerUpgrades.RangeIncrease);
            attackTower.FireRate = attackTower.BaseFireRate * (1.0f + pulsarTowerUpgrades[CurrentUpgrade].FireRateIncrease + GameManager.Instance.UpgradeManager.PermanentPulsarTowerUpgrades.FireRateIncrease);
            attackTower.SplashRadius = attackTower.BaseSplashRadius * (1.0f + pulsarTowerUpgrades[CurrentUpgrade].SplashRadius);
            attackTower.SplashDamage = attackTower.BaseSplashDamage * (1.0f + pulsarTowerUpgrades[CurrentUpgrade].SplashDamage);
        }

        attackTower.GetComponent<TowerRangeOutline>().UpdateOutline();
    }
}