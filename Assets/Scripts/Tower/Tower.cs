using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    public enum TowerType
    {
        Blockade, EnergyTower, LightTower, LightningTower, PulsarTower
    }

    public enum ModifierType
    {
        None, Health, Speed, Damage, Currency
    }

    [Header("Tower Selection")]
    [SerializeField] private ActionBar actionBar;
    [SerializeField] private SelectionInfo selectionInfo;

    [Header("Tower Settings")]
    [SerializeField] protected Transform turret;
    [SerializeField] public float fireRate = 1.0f;
    [SerializeField] private TowerType towerType;
    [SerializeField] private ModifierType modifierType;

    [Header("Tower Cost")]
    public int baseCost = 100;
    public int sellPrice = 75;

    [Header("Infection Settings")]
    [SerializeField] GameObject infectionScoreUI;
    [SerializeField] TextMeshProUGUI infectionScoreText;
    [SerializeField] private int scoreRequiredForCorruption = 1;
    [SerializeField] protected bool isInfected;
    [SerializeField] private int infectionScore = 0;
  
    protected float cooldown;
    public float sellTimer = 10f;
    protected Animator animator;

    public ActionBar ActionBar { get { return actionBar; } }
    public SelectionInfo SelectionInfo { get { return selectionInfo; } }
    public int InfectionScore { get { return infectionScore; } }

    private void Start()
    {
        GameManager.Instance.UpgradeManager.AddTower(GetComponent<TowerUpgrades>(), towerType);
    }

    public virtual SelectionInfo GetSelectionInfo()
    {
        for (int i = 0; i < SelectionInfo.StatInfo.Count; i++)
        {
            switch (SelectionInfo.StatInfo[i].Stat)
            {
                case StatInfo.StatType.InfectionScore:
                    SelectionInfo.StatInfo[i].BaseStat = InfectionScore;
                    break;
                default:
                    Debug.Log("No Method for " + SelectionInfo.StatInfo[i].Stat + " implemented!");
                    break;
            }
        }
        return SelectionInfo;
    }

    public ModifierType GetModifierType()
    {
        return modifierType;
    }

    public TowerType GetTowerType()
    {
        return towerType;
    }

    public void IncreaseInfectionScore(int value)
    {
        infectionScore += value;
        if(infectionScore >= scoreRequiredForCorruption)
        {
            animator.SetBool("isCorrupted", true);
        }

    }

    public void DecreaseInfectionScore(int value)
    {
        infectionScore -= value;
    }

    public int GetInfectionScore()
    {
        return infectionScore;
    }

    public void RemoveTower()
    {
        GameManager.Instance.UpgradeManager.RemoveTower(GetComponent<TowerUpgrades>(), towerType);
    }

    public void InfectTower()
    {
        isInfected = true;
        RemoveTower();
        GameManager.Instance.BuffManager.AddInfectedTower(this);
        if (GetComponent<Animator>())
        {
            animator = GetComponent<Animator>();
            animator.SetBool("isInfected", true);
        }
        if (GetComponent<TowerTargeting>())
        {
            GetComponent<TowerTargeting>().enabled = false;
        }
        GameManager.Instance.InfectionManager.AddTowerToList(this);
    }

    public void CleanseTower()
    {
        isInfected = false;
        GameManager.Instance.BuffManager.RemoveInfectedTower(this);
        if (GetComponent<Animator>())
        {
            animator = GetComponent<Animator>();
            animator.SetBool("isInfected", false);
        }
        if (GetComponent<TowerTargeting>())
        {
            GetComponent<TowerTargeting>().enabled = true;
        }
        GameManager.Instance.InfectionManager.RemoveTowerFromList(this);        
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
        if(sellTimer > 0)
        {
            sellTimer -= Time.deltaTime;
        }
    }

    public bool CheckTowerInfected()
    {
        return isInfected;
    }
}
