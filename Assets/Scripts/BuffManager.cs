using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BuffManager : MonoBehaviour
{
    [SerializeField] private List<InfectedHealthModifier> infectedHealthModifiers;
    [SerializeField] private List<InfectedSpeedModifier> infectedSpeedModifiers;
    [SerializeField] private List<InfectedCurrencyModifier> infectedCurrencyModifiers;
    [SerializeField] private List<InfectedSpawnModifier> infectedSpawnModifiers;

    private List<Tower> normalTowers = new List<Tower>();
    private List<Tower> healthModifierTowers = new List<Tower>();
    private List<Tower> speedModifierTowers = new List<Tower>();
    private List<Tower> damageModifierTowers = new List<Tower>();
    private List<Tower> currencyModifierTowers = new List<Tower>();

    private bool[] speedModifierStageAdded;

    private float healthModifier;
    private float speedModifier;
    private int currencyModifier;

    public void IncreaseInfectionScore()
    {
        for (int i = 0; i < normalTowers.Count; i++)
        {
            normalTowers[i].IncreaseInfectionScore(1);
        }

        for (int i = 0; i < healthModifierTowers.Count; i++)
        {
            healthModifierTowers[i].IncreaseInfectionScore(1);
        }

        for (int i = 0; i < speedModifierTowers.Count; i++)
        {
            speedModifierTowers[i].IncreaseInfectionScore(1);
        }

        for (int i = 0; i < damageModifierTowers.Count; i++)
        {
            damageModifierTowers[i].IncreaseInfectionScore(1);
        }
        for (int i = 0; i < currencyModifierTowers.Count; i++)
        {
            currencyModifierTowers[i].IncreaseInfectionScore(1);
        }
    }

    public void AddInfectedTower(Tower tower)
    {
        
        switch (tower.GetModifierType())
        {
            case Tower.ModifierType.None:
                normalTowers.Add(tower);
                break;
            case Tower.ModifierType.Health:
                healthModifierTowers.Add(tower);
                break;
            case Tower.ModifierType.Speed:
                speedModifierTowers.Add(tower);
                break;
            case Tower.ModifierType.Damage:
                damageModifierTowers.Add(tower);
                break;
            case Tower.ModifierType.Currency:
                currencyModifierTowers.Add(tower);                
                break;
            default:
                Debug.LogWarning("Tower: ModifierType not found!");
                break;
        }
    }

    public void RemoveInfectedTower(Tower tower)
    {
        switch (tower.GetModifierType())
        {
            case Tower.ModifierType.None:
                break;
            case Tower.ModifierType.Health:
                healthModifierTowers.Remove(tower);
                break;
            case Tower.ModifierType.Speed:
                speedModifierTowers.Remove(tower);
                break;
            case Tower.ModifierType.Damage:
                damageModifierTowers.Remove(tower);
                break;
            case Tower.ModifierType.Currency:
                currencyModifierTowers.Remove(tower);
                break;
            default:
                break;
        }
    }

    #region HealthModifier

    public void CalculateHealthModifier()
    {
        float healthModifierTotal = 0;

        for (int i = 0; i < healthModifierTowers.Count; i++)
        {
            healthModifierTotal += GetTowerHealthModifier(healthModifierTowers[i]);
        }
        healthModifier = healthModifierTotal;
    }

    private float GetTowerHealthModifier(Tower tower)
    {
        float towerHealthModifier = 0f;

        for (int i = 0; i < infectedHealthModifiers.Count; i++)
        {
            if (tower.GetInfectionScore() >= infectedHealthModifiers[i].InfectionScoreTrigger)
            {
                towerHealthModifier = infectedHealthModifiers[i].HealthModifier;
            }
        }
        return towerHealthModifier;
    }

    public float GetHealthModifier()
    {
        return healthModifier;
    }

    #endregion

    #region Currency/Spawn-Modifier

    public int CalculateInfectedCurrencyModifier()
    {
        int currencyModifierTotal = 0;

        for (int i = 0; i < currencyModifierTowers.Count; i++)
        {
            currencyModifierTotal += GetTowerInfectedCurrencyModifier(currencyModifierTowers[i]);
        }
        currencyModifier = currencyModifierTotal;
        return currencyModifierTotal;
    }

    private int GetTowerInfectedCurrencyModifier(Tower tower)
    {
        int towerCurrencyModifier = 0;

        for (int i = 0; i < infectedCurrencyModifiers.Count; i++)
        {
            if (tower.GetInfectionScore() >= infectedCurrencyModifiers[i].InfectionScoreTrigger)
            {
                towerCurrencyModifier = infectedCurrencyModifiers[i].CurrencyModifier;
            }
        }
        return towerCurrencyModifier;
    }

    public void SpawnAdditionalEnemies()
    {
        List<GameObject> enemiesToSpawn = new List<GameObject>();

        for (int i = 0; i < currencyModifierTowers.Count; i++)
        {
            List<GameObject> tempList = new List<GameObject>();

            for (int j = 0; j < infectedSpawnModifiers.Count; j++)
            {
                if (currencyModifierTowers[i].GetInfectionScore() >= infectedSpawnModifiers[j].InfectionScoreTrigger - 1)
                {
                    tempList.Clear();
                    tempList = infectedSpawnModifiers[j].EnemiesToSpawn;
                }
            }
            enemiesToSpawn.AddRange(tempList);
        }

        for (int i = 0; i < enemiesToSpawn.Count; i++)
        {
            GameManager.Instance.WaveSpawner.AddAdditionalEnemy(enemiesToSpawn[i]);
        }
    }

    #endregion

    #region SpeedModifier

    public void CalculateSpeedModifier()
    {
        float speedModifierTotal = 0f;
        speedModifierStageAdded = new bool[infectedSpeedModifiers.Count];

        for (int i = 0; i < speedModifierTowers.Count; i++)
        {
            speedModifierTotal += GetTowerSpeedModifier(speedModifierTowers[i]);
        }
        speedModifier = speedModifierTotal;
    }

    private float GetTowerSpeedModifier(Tower tower)
    {
        float towerSpeedModifier = 0f;

        for (int i = 0; i < infectedSpeedModifiers.Count; i++)
        {
            if (tower.GetInfectionScore() >= infectedSpeedModifiers[i].InfectionScoreTrigger)
            {
                /* Global Slow Effect */
                if (infectedSpeedModifiers[i].GlobalRange)
                {
                    RemoveLocalSpeedModifier(tower);

                    /* If Stackable, always add Modifier */
                    if (infectedSpeedModifiers[i].Stackable)
                    {
                        towerSpeedModifier = infectedSpeedModifiers[i].SpeedModifier;
                        speedModifierStageAdded[i] = true;
                    }
                    /* If not Stackable, check if we have already added Modifier */
                    else if (speedModifierStageAdded[i] == false)
                    {
                        towerSpeedModifier = infectedSpeedModifiers[i].SpeedModifier;
                        speedModifierStageAdded[i] = true;
                    }
                }
                /* Local Slow Effect */
                else
                {
                    AddLocalSpeedModifier(tower);
                    UpdateLocalSpeedModifier(tower, infectedSpeedModifiers[i]);
                }
                
            }
        }
        return towerSpeedModifier;
    }

    private void AddLocalSpeedModifier(Tower tower)
    {
        GameObject rangeGameObject = tower.transform.GetChild(0).gameObject;
        LocalSpeedModifier modifier = rangeGameObject.GetComponent<LocalSpeedModifier>();

        if (modifier == null)
        {
            rangeGameObject.AddComponent<LocalSpeedModifier>();
        }
    }

    private void UpdateLocalSpeedModifier(Tower tower, InfectedSpeedModifier infectedSpeedModifier)
    {
        tower.transform.GetChild(0).gameObject.GetComponent<LocalSpeedModifier>().UpdateSpeedModifier(infectedSpeedModifier);
    }

    private void RemoveLocalSpeedModifier(Tower tower)
    {
        LocalSpeedModifier modifier = tower.transform.GetChild(0).gameObject.GetComponent<LocalSpeedModifier>();
        if (modifier != null)
        {
            Destroy(modifier);
        }
    }

    public InfectedSpeedModifier GetSpeedModifierByInfectionScore(int infectionScore)
    {
        for (int i = infectedSpeedModifiers.Count - 1; i >= 0; i--)
        {
            if (infectionScore >= infectedSpeedModifiers[i].InfectionScoreTrigger)
            {
                return infectedSpeedModifiers[i];
            }
        }
        return null;
    }

    public float GetSpeedModifier()
    {
        return speedModifier;
    }

    #endregion
}