using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Permanent Tower Upgrade Action", menuName = "ActionBar/Permanent Tower Upgrade", order = 3)]
public class PermanentTowerUpgradeAction : Action
{
    public enum Type { BlockadeTower, LightTower, EnergyTower, LightningTower, PulsarTower }
    public enum Upgrade { Damage, Range, Speed }

    [SerializeField] private Type towerType;
    [SerializeField] private Upgrade upgradeType;

    public Type TowerType { get { return towerType; } }
    public Upgrade UpgradeType { get { return upgradeType; } }

    public override void UseAction()
    {
        base.UseAction();
    }
}