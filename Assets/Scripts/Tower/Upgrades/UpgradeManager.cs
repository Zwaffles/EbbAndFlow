using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [Header("Debug Fields")]
    [Space]
    [SerializeField] private EnergyTowerUpgrade permanentEnergyTowerUpgrades = new EnergyTowerUpgrade();
    [Space]
    [SerializeField] private LightTowerUpgrade permanentLightTowerUpgrades = new LightTowerUpgrade();
    [Space]
    [SerializeField] private LightningTowerUpgrade permanentLightningTowerUpgrades = new LightningTowerUpgrade();
    [Space]
    [SerializeField] private PulsarTowerUpgrade permanentPulsarTowerUpgrades = new PulsarTowerUpgrade();

    [SerializeField] private List<EnergyTowerUpgrades> energyTowers = new List<EnergyTowerUpgrades>();
    [SerializeField] private List<LightTowerUpgrades> lightTowers = new List<LightTowerUpgrades>();
    [SerializeField] private List<LightningTowerUpgrades> lightningTowers = new List<LightningTowerUpgrades>();
    [SerializeField] private List<PulsarTowerUpgrades> pulsarTowers = new List<PulsarTowerUpgrades>();

    public EnergyTowerUpgrade PermanentEnergyTowerUpgrades { get { return permanentEnergyTowerUpgrades; } set { permanentEnergyTowerUpgrades = value; } }
    public LightTowerUpgrade PermanentLightTowerUpgrades { get { return permanentLightTowerUpgrades; } set { permanentLightTowerUpgrades = value; } }
    public LightningTowerUpgrade PermanentLightningTowerUpgrades { get { return permanentLightningTowerUpgrades; } set { permanentLightningTowerUpgrades = value; } }
    public PulsarTowerUpgrade PermanentPulsarTowerUpgrades { get { return permanentPulsarTowerUpgrades; } set { permanentPulsarTowerUpgrades = value; } }

    private void Awake()
    {
        permanentEnergyTowerUpgrades.CurrencyPerWave = 0;

        permanentLightningTowerUpgrades.DamageIncrease = 0;
        permanentLightningTowerUpgrades.RangeIncrease = 0;
        permanentLightningTowerUpgrades.FireRateIncrease = 0;

        permanentLightTowerUpgrades.DamageIncrease = 0;
        permanentLightTowerUpgrades.RangeIncrease = 0;
        permanentLightTowerUpgrades.FireRateIncrease = 0;

        permanentPulsarTowerUpgrades.DamageIncrease = 0;
        permanentPulsarTowerUpgrades.RangeIncrease = 0;
        permanentPulsarTowerUpgrades.FireRateIncrease = 0;
    }

    public void AddTower(TowerUpgrades towerUpgrades, Tower.TowerType towerType)
    {
        switch (towerType)
        {
            case Tower.TowerType.Blockade:
                break;
            case Tower.TowerType.EnergyTower:
                towerUpgrades.UpgradeTower(false);
                energyTowers.Add((EnergyTowerUpgrades)towerUpgrades);
                break;
            case Tower.TowerType.LightTower:
                towerUpgrades.UpgradeTower(false);
                lightTowers.Add((LightTowerUpgrades)towerUpgrades);
                break;
            case Tower.TowerType.LightningTower:
                towerUpgrades.UpgradeTower(false);
                lightningTowers.Add((LightningTowerUpgrades)towerUpgrades);
                break;
            case Tower.TowerType.PulsarTower:
                towerUpgrades.UpgradeTower(false);
                pulsarTowers.Add((PulsarTowerUpgrades)towerUpgrades);
                break;
            default:
                break;
        }
    }

    public void RemoveTower(TowerUpgrades towerUpgrades, Tower.TowerType towerType)
    {
        switch (towerType)
        {
            case Tower.TowerType.Blockade:
                break;
            case Tower.TowerType.EnergyTower:
                energyTowers.Remove((EnergyTowerUpgrades)towerUpgrades);
                break;
            case Tower.TowerType.LightTower:
                lightTowers.Remove((LightTowerUpgrades)towerUpgrades);
                break;
            case Tower.TowerType.LightningTower:
                lightningTowers.Remove((LightningTowerUpgrades)towerUpgrades);
                break;
            case Tower.TowerType.PulsarTower:
                pulsarTowers.Remove((PulsarTowerUpgrades)towerUpgrades);
                break;
            default:
                break;
        }
    }

    public void UpdateTowerUpgrades(Tower.TowerType towerType)
    {
        switch (towerType)
        {
            case Tower.TowerType.Blockade:
                break;
            case Tower.TowerType.EnergyTower:
                for (int i = 0; i < energyTowers.Count; i++)
                {
                    energyTowers[i].UpgradeTower(false);
                }
                break;
            case Tower.TowerType.LightTower:
                for (int i = 0; i < lightTowers.Count; i++)
                {
                    lightTowers[i].UpgradeTower(false);
                }
                break;
            case Tower.TowerType.LightningTower:
                for (int i = 0; i < lightningTowers.Count; i++)
                {
                    lightningTowers[i].UpgradeTower(false);
                }
                break;
            case Tower.TowerType.PulsarTower:
                for (int i = 0; i < pulsarTowers.Count; i++)
                {
                    pulsarTowers[i].UpgradeTower(false);
                }
                break;
            default:
                break;
        }
    }

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
            case PermanentTowerUpgradeAction.Upgrade.Currency:
                UpgradeCurrency(towerType, value);
                break;
            default:
                Debug.LogWarning("Warning: UpgradeTowerType() - Upgrade not found!");
                break;
        }
    }

    public void UpgradeCurrency(Tower.TowerType towerType, float value)
    {
        switch (towerType)
        {
            case Tower.TowerType.EnergyTower:
                permanentEnergyTowerUpgrades.CurrencyPerWave += Mathf.RoundToInt(value);

                break;
            default:
                Debug.LogWarning("Warning: UpgradeCurrency() - TowerType not found!");
                break;
        }
    }

    public void UpgradeDamage(Tower.TowerType towerType, float value)
    {
        switch (towerType)
        {
            case Tower.TowerType.LightTower:
                permanentLightTowerUpgrades.DamageIncrease += value;
                UpdateTowerUpgrades(towerType);
                break;
            case Tower.TowerType.LightningTower:
                permanentLightningTowerUpgrades.DamageIncrease += value;
                UpdateTowerUpgrades(towerType);
                break;
            case Tower.TowerType.PulsarTower:
                permanentPulsarTowerUpgrades.DamageIncrease += value;
                UpdateTowerUpgrades(towerType);
                break;
            default:
                Debug.LogWarning("Warning: UpgradeDamage() - TowerType not found!");
                break;
        }
    }

    public void UpgradeRange(Tower.TowerType towerType, float value)
    {
        switch (towerType)
        {
            case Tower.TowerType.LightTower:
                permanentLightTowerUpgrades.RangeIncrease += value;
                UpdateTowerUpgrades(towerType);
                break;
            case Tower.TowerType.LightningTower:
                permanentLightningTowerUpgrades.RangeIncrease += value;
                UpdateTowerUpgrades(towerType);
                break;
            case Tower.TowerType.PulsarTower:
                permanentPulsarTowerUpgrades.RangeIncrease += value;
                UpdateTowerUpgrades(towerType);
                break;
            default:
                Debug.LogWarning("Warning: UpgradeRange() - TowerType not found!");
                break;
        }
    }

    public void UpgradeSpeed(Tower.TowerType towerType, float value)
    {
        switch (towerType)
        {
            case Tower.TowerType.LightTower:
                permanentLightTowerUpgrades.FireRateIncrease += value;
                UpdateTowerUpgrades(towerType);
                break;
            case Tower.TowerType.LightningTower:
                permanentLightningTowerUpgrades.FireRateIncrease += value;
                UpdateTowerUpgrades(towerType);
                break;
            case Tower.TowerType.PulsarTower:
                permanentPulsarTowerUpgrades.FireRateIncrease += value;
                UpdateTowerUpgrades(towerType);
                break;
            default:
                Debug.LogWarning("Warning: UpgradeSpeed() - TowerType not found!");
                break;
        }
    }
}