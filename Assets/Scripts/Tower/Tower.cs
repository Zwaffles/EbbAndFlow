using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public enum ModifierType
    {
        Health, Speed, Damage
    }

    [SerializeField] protected Transform turret;
    [SerializeField] public float fireRate = 1.0f;
    [SerializeField] private ModifierType modifierType;
    public int baseCost = 100;
    public int sellPrice = 75;
    
    protected float cooldown;
    public bool isInfected;
    [SerializeField] private int infectionScore = 0;

   

    public ModifierType GetModifierType()
    {
        return modifierType;
    }

    public void IncreaseInfectionScore(int value)
    {
        infectionScore += value;
    }

    public void DecreaseInfectionScore(int value)
    {
        infectionScore -= value;
    }

    public int GetInfectionScore()
    {
        return infectionScore;
    }

    public void InfectTower()
    {
        isInfected = true;
        BuffManager.Instance.AddInfectedTower(this);
    }

    public void CleanseTower()
    {
        isInfected = false;
        BuffManager.Instance.RemoveInfectedTower(this);
    }
}
