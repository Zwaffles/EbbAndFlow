using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [Space]
    [SerializeField] private EnergyTowerUpgrade permanentEnergyTowerUpgrades = new EnergyTowerUpgrade();
    [Space]
    [SerializeField] private LightTowerUpgrade permanentLightTowerUpgrades = new LightTowerUpgrade();
    [Space]
    [SerializeField] private LightningTowerUpgrade permanentLightningTowerUpgrades = new LightningTowerUpgrade();
    [Space]
    [SerializeField] private PulsarTowerUpgrade permanentPulsarTowerUpgrades = new PulsarTowerUpgrade();
   
    public EnergyTowerUpgrade PermanentEnergyTowerUpgrades { get { return permanentEnergyTowerUpgrades; } set { permanentEnergyTowerUpgrades = value; } }
    public LightTowerUpgrade PermanentLightTowerUpgrades { get { return permanentLightTowerUpgrades; } set { permanentLightTowerUpgrades = value; } }
    public LightningTowerUpgrade PermanentLightningTowerUpgrades { get { return permanentLightningTowerUpgrades; } set { permanentLightningTowerUpgrades = value; } }
    public PulsarTowerUpgrade PermanentPulsarTowerUpgrades { get { return permanentPulsarTowerUpgrades; } set { permanentPulsarTowerUpgrades = value; } }

    public void UpgradeTowerType(Tower.TowerType towerType, PermanentTowerUpgradeAction.Upgrade upgrade, float value)
    {

        switch (upgrade)
        {
            case PermanentTowerUpgradeAction.Upgrade.Damage:
                UpgradeDamage(towerType, value);
                break;
            case PermanentTowerUpgradeAction.Upgrade.Range:
                UpgradeRange(towerType, value);
                break;
            case PermanentTowerUpgradeAction.Upgrade.Speed:
                UpgradeSpeed(towerType, value);
                break;
            default:
                Debug.LogWarning("Upgrade Type not found!");
                break;
        }
    }

    public void UpgradeDamage(Tower.TowerType towerType, float value)
    {
        switch (towerType)
        {
            case Tower.TowerType.Blockade:
                break;
            case Tower.TowerType.EnergyTower:
                break;
            case Tower.TowerType.LightTower:
                permanentLightTowerUpgrades.DamageIncrease += value;
                break;
            case Tower.TowerType.LightningTower:
                permanentLightningTowerUpgrades.DamageIncrease += value;
                break;
            case Tower.TowerType.PulsarTower:
                permanentPulsarTowerUpgrades.DamageIncrease += value;
                break;
            default:
                break;
        }
    }

    public void UpgradeRange(Tower.TowerType towerType, float value)
    {
        switch (towerType)
        {
            case Tower.TowerType.Blockade:
                break;
            case Tower.TowerType.EnergyTower:
                break;
            case Tower.TowerType.LightTower:
                permanentLightTowerUpgrades.RangeIncrease += value;
                break;
            case Tower.TowerType.LightningTower:
                permanentLightningTowerUpgrades.RangeIncrease += value;
                break;
            case Tower.TowerType.PulsarTower:
                permanentPulsarTowerUpgrades.RangeIncrease += value;
                break;
            default:
                break;
        }
    }

    public void UpgradeSpeed(Tower.TowerType towerType, float value)
    {
        switch (towerType)
        {
            case Tower.TowerType.Blockade:
                break;
            case Tower.TowerType.EnergyTower:
                break;
            case Tower.TowerType.LightTower:
                permanentLightTowerUpgrades.FireRateIncrease += value;
                break;
            case Tower.TowerType.LightningTower:
                permanentLightningTowerUpgrades.FireRateIncrease += value;
                break;
            case Tower.TowerType.PulsarTower:
                permanentPulsarTowerUpgrades.FireRateIncrease += value;
                break;
            default:
                break;
        }
    }

}
