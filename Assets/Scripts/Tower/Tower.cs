using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    public enum ModifierType
    {
        Health, Speed, Damage, Currency
    }

    [SerializeField] protected Transform turret;
    [SerializeField] public float fireRate = 1.0f;
    [SerializeField] private ModifierType modifierType;
    public int baseCost = 100;
    public int sellPrice = 75;

    protected float cooldown;
    [SerializeField] protected bool isInfected;
    [SerializeField] private int infectionScore = 0;

    [SerializeField] GameObject infectionScoreUI;
    [SerializeField] TextMeshProUGUI infectionScoreText;

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
        SceneManagement.Instance.AddTowerToList(this);
    }

    public void CleanseTower()
    {
        isInfected = false;
        BuffManager.Instance.RemoveInfectedTower(this);        
    }

    public void ShowInfectionScore()
    {
        if (isInfected)
        {           
            infectionScoreUI.SetActive(true);
        }             
    }

    public void HideInfectionScore()
    {
        infectionScoreUI.SetActive(false);
    }

    private void LateUpdate()
    {
        infectionScoreText.text = infectionScore.ToString();
    }
}
