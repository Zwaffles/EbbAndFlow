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
    protected Animator animator;
    [SerializeField] protected bool isInfected;
    [SerializeField] private int infectionScore = 0;

    [SerializeField] GameObject infectionScoreUI;
    [SerializeField] TextMeshProUGUI infectionScoreText;
    [SerializeField] private int scoreRequiredForCorruption = 1;

    public ModifierType GetModifierType()
    {
        return modifierType;
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

    public void InfectTower()
    {
        isInfected = true;
        BuffManager.Instance.AddInfectedTower(this);
        if (GetComponent<Animator>())
        {
            animator = GetComponent<Animator>();
            animator.SetBool("isInfected", true);
        }
        SceneManagement.Instance.AddTowerToList(this);
    }

    public void CleanseTower()
    {
        isInfected = false;
        BuffManager.Instance.RemoveInfectedTower(this);
        animator.SetBool("isInfected", false);
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
