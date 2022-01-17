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


    [Header("Max Allowed Upgrades")]
    public int lightTowerMaxDamage = 5;
    public int lightTowerMaxRange = 5;
    public int lightTowerMaxAttackSpeed = 5;
    [Space]
    public int lightningTowerMaxDamage = 5;
    public int lightningTowerMaxRange = 5;
    public int lightningTowerMaxAttackSpeed = 5;
    [Space]
    public int pulsarTowerMaxDamage = 5;
    public int pulsarTowerMaxRange = 5;
    public int pulsarTowerMaxAttackSpeed = 5;
    [Space]
    public int energyTowerMaxCurrency = 5;

    [Header("Debug Fields")]
    public int lightTowerDamage = 0;
    public int lightTowerRange = 0;
    public int lightTowerAttackSpeed = 0;

    public int lightningTowerDamage = 0;
    public int lightningTowerRange = 0;
    public int lightningTowerAttackSpeed = 0;

    public int pulsarTowerDamage = 0;
    public int pulsarTowerRange = 0;
    public int pulsarTowerAttackSpeed = 0;

    public int energyTowerCurrency = 0;

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

    public bool UpgradeAllowed(Tower.TowerType towerType, PermanentTowerUpgradeAction.Upgrade upgrade)
    {
        switch (upgrade)
        {
            case PermanentTowerUpgradeAction.Upgrade.Damage:
                switch (towerType)
                {
                    case Tower.TowerType.LightTower:
                        if (lightTowerDamage + 1 <= lightTowerMaxDamage) { return true; } else { return false; }
                    case Tower.TowerType.LightningTower:
                        if (lightningTowerDamage + 1 <= lightningTowerMaxDamage) { return true; } else { return false; }
                    case Tower.TowerType.PulsarTower:
                        if (pulsarTowerDamage + 1 <= pulsarTowerMaxDamage) { return true; } else { return false; }
                    default:
                        Debug.Log("UpgradeAllowed(): Case not defined!");
                        return false;
                }
            case PermanentTowerUpgradeAction.Upgrade.Range:
                switch (towerType)
                {
                    case Tower.TowerType.LightTower:
                        if (lightTowerRange + 1 <= lightTowerMaxRange) { return true; } else { return false; }
                    case Tower.TowerType.LightningTower:
                        if (lightningTowerRange + 1 <= lightningTowerMaxRange) { return true; } else { return false; }
                    case Tower.TowerType.PulsarTower:
                        if (pulsarTowerRange + 1 <= pulsarTowerMaxRange) { return true; } else { return false; }
                    default:
                        Debug.Log("UpgradeAllowed(): Case not defined!");
                        return false;
                }
            case PermanentTowerUpgradeAction.Upgrade.Speed:
                switch (towerType)
                {
                    case Tower.TowerType.LightTower:
                        if (lightTowerAttackSpeed + 1 <= lightTowerMaxAttackSpeed) { return true; } else { return false; }
                    case Tower.TowerType.LightningTower:
                        if (lightningTowerAttackSpeed + 1 <= lightningTowerMaxAttackSpeed) { return true; } else { return false; }
                    case Tower.TowerType.PulsarTower:
                        if (pulsarTowerAttackSpeed + 1 <= pulsarTowerMaxAttackSpeed) { return true; } else { return false; }
                    default:
                        Debug.Log("UpgradeAllowed(): Case not defined!");
                        return false;
                }
            case PermanentTowerUpgradeAction.Upgrade.Currency:
                switch (towerType)
                {
                    case Tower.TowerType.EnergyTower:
                        if (energyTowerCurrency + 1 <= energyTowerMaxCurrency) { return true; } else { return false; }

                    default:
                        return false;
                }
            default:
                Debug.Log("UpgradeAllowed(): Case not defined!");
                return false;
        }
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
                energyTowerCurrency++;
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
                lightTowerDamage++;
                permanentLightTowerUpgrades.DamageIncrease += value;
                UpdateTowerUpgrades(towerType);
                break;
            case Tower.TowerType.LightningTower:
                lightningTowerDamage++;
                permanentLightningTowerUpgrades.DamageIncrease += value;
                UpdateTowerUpgrades(towerType);
                break;
            case Tower.TowerType.PulsarTower:
                pulsarTowerDamage++;
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
                lightTowerRange++;
                permanentLightTowerUpgrades.RangeIncrease += value;
                UpdateTowerUpgrades(towerType);
                break;
            case Tower.TowerType.LightningTower:
                lightningTowerRange++;
                permanentLightningTowerUpgrades.RangeIncrease += value;
                UpdateTowerUpgrades(towerType);
                break;
            case Tower.TowerType.PulsarTower:
                pulsarTowerRange++;
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
                lightTowerAttackSpeed++;
                permanentLightTowerUpgrades.FireRateIncrease += value;
                UpdateTowerUpgrades(towerType);
                break;
            case Tower.TowerType.LightningTower:
                lightningTowerAttackSpeed++;
                permanentLightningTowerUpgrades.FireRateIncrease += value;
                UpdateTowerUpgrades(towerType);
                break;
            case Tower.TowerType.PulsarTower:
                pulsarTowerAttackSpeed++;
                permanentPulsarTowerUpgrades.FireRateIncrease += value;
                UpdateTowerUpgrades(towerType);
                break;
            default:
                Debug.LogWarning("Warning: UpgradeSpeed() - TowerType not found!");
                break;
        }
    }
}