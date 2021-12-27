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

    public override void UpgradeTower()
    {
        base.UpgradeTower();

        CurrentUpgrade++;
        attackTower.Damage += pulsarTowerUpgrades[CurrentUpgrade].DamageIncrease;
        attackTower.Range += pulsarTowerUpgrades[CurrentUpgrade].RangeIncrease;
        attackTower.FireRate += pulsarTowerUpgrades[CurrentUpgrade].FireRateIncrease;
        attackTower.SplashRadius += pulsarTowerUpgrades[CurrentUpgrade].SplashRadius;
        attackTower.SplashDamage += pulsarTowerUpgrades[CurrentUpgrade].SplashDamage;
        attackTower.GetComponent<TowerRangeOutline>().UpdateOutline();
        GameManager.Instance.PlayerCurrency.RemovePlayerNormalCurrency(pulsarTowerUpgrades[CurrentUpgrade].UpgradeCost);
    }
}