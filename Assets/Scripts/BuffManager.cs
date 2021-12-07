using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public static BuffManager Instance { get { return instance; } }
    private static BuffManager instance;
    private float healthModifier;

    [SerializeField] List<InfectedHealthModifier> infectedHealthModifiers;

    List<Tower> healthModifierTowers;
    List<Tower> speedModifierTowers;
    List<Tower> damageModifierTowers;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
    }

    public void AddInfectedTower(Tower tower)
    {
        switch (tower.GetModifierType())
        {
            case Tower.ModifierType.Health:
                healthModifierTowers.Add(tower);
                break;
            case Tower.ModifierType.Speed:
                speedModifierTowers.Add(tower);
                break;
            case Tower.ModifierType.Damage:
                damageModifierTowers.Add(tower);
                break;
            default:
                break;
        }
    }

    public void RemoveInfectedTower(Tower tower)
    {
        switch (tower.GetModifierType())
        {
            case Tower.ModifierType.Health:
                healthModifierTowers.Remove(tower);
                break;
            case Tower.ModifierType.Speed:
                speedModifierTowers.Remove(tower);
                break;
            case Tower.ModifierType.Damage:
                damageModifierTowers.Remove(tower);
                break;
            default:
                break;
        }
    }

    void CalculateHealthModifier()
    {
        for (int i = 0; i < healthModifierTowers.Count; i++)
        {
            IncreaseHealthModifier(GetTowerHealthModifier(healthModifierTowers[i]));
        }
    }

    float GetTowerHealthModifier(Tower tower)
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

    public void IncreaseInfectionScore()
    {
        for (int i = 0; i < healthModifierTowers.Count; i++)
        {
            healthModifierTowers[i].IncreaseInfectionScore(1);
        }

        for (int j = 0; j < speedModifierTowers.Count; j++)
        {
            speedModifierTowers[j].IncreaseInfectionScore(1);
        }

        for (int k = 0; k < damageModifierTowers.Count; k++)
        {
            damageModifierTowers[k].IncreaseInfectionScore(1);
        }
    }

    public float GetHealthModifier()
    {
        return healthModifier;
    }

    public void IncreaseHealthModifier(float value)
    {
        healthModifier += value;
    }

    public void DecreaseHealthModifier(float value)
    {
        healthModifier -= value;
    }
  
}
