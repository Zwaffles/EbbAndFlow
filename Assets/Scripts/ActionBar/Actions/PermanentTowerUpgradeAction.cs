using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Permanent Tower Upgrade Action", menuName = "ActionBar/Permanent Tower Upgrade", order = 3)]
public class PermanentTowerUpgradeAction : Action
{
    public enum Upgrade { Damage, Range, Speed }

    [SerializeField] private Tower.TowerType towerType;
    [SerializeField] private Upgrade upgradeType;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float percentageValue = 0.1f;
    [SerializeField] private int upgradeCost = 10;

    public Tower.TowerType TowerType { get { return towerType; } }
    public Upgrade UpgradeType { get { return upgradeType; } }

    public override void UseAction()
    {
        base.UseAction();
        GameManager.Instance.UpgradeManager.UpgradeTowerType(Tower.TowerType.LightningTower, upgradeType, percentageValue);
    }

    public override bool Interactable()
    {
        if (GameManager.Instance.PlayerCurrency)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}