using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float enemyHealth = 10f;
    [SerializeField] private float currentHealth;
    [SerializeField] private float damageMultiplier = 1.0f;
    [SerializeField] private float minMoveSpeed = 0.25f;
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private SelectionInfo selectionInfo;

    private SpriteRenderer selectionOutline;
    private Image healthBar;
    private Animator animator;
    private AIPath path;

    private bool speedDebuff;
    private float baseHealth;
    private float baseMoveSpeed;
    private float globalSpeedModifier;
    
    public bool SpeedDebuff { get; set; }
    public float MoveSpeed { get; }
    public float DamageMultiplier { get { return damageMultiplier; } set { damageMultiplier = value; } }
    public SpriteRenderer SelectionOutline { get { return selectionOutline; } set { selectionOutline = value; } }

    private void Awake()
    {
        selectionOutline = transform.GetChild(1).GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        path = GetComponent<AIPath>();
        path.maxSpeed = moveSpeed;
        baseMoveSpeed = moveSpeed;
        baseHealth = enemyHealth;
    }

    public void Initialize(float healthModifier, float speedModifier)
    {
        enemyHealth += healthModifier;
        currentHealth = enemyHealth;
        globalSpeedModifier = speedModifier;
        path.maxSpeed = moveSpeed + globalSpeedModifier;
        healthBar = transform.GetChild(0).GetChild(1).GetComponent<Image>();
    }

    public SelectionInfo GetSelectionInfo()
    {
        for (int i = 0; i < selectionInfo.StatInfo.Count; i++)
        {
            switch (selectionInfo.StatInfo[i].Stat)
            {
                case StatInfo.StatType.MovementSpeed:
                    selectionInfo.StatInfo[i].BaseStat = baseMoveSpeed;
                    selectionInfo.StatInfo[i].CurrentStat = moveSpeed + globalSpeedModifier;
                    break;
                case StatInfo.StatType.Health:
                    selectionInfo.StatInfo[i].BaseStat = baseHealth;
                    Debug.Log("Current Stat: " + currentHealth);
                    selectionInfo.StatInfo[i].CurrentStat = currentHealth;
                    break;
                case StatInfo.StatType.Armor:
                    selectionInfo.StatInfo[i].BaseStat = 1.0f;
                    selectionInfo.StatInfo[i].CurrentStat = damageMultiplier * GameManager.Instance.BuffManager.GetGlobalDamageModifier();
                    break;
                default:
                    Debug.Log("No Method for " + selectionInfo.StatInfo[i].Stat + " implemented!");
                    break;
            }
        }
        return selectionInfo;
    }

    public void ModifyMoveSpeed(float value)
    {
        path.maxSpeed += value;
        path.maxSpeed = Mathf.Clamp((path.maxSpeed + value), minMoveSpeed, Mathf.Infinity);
    }

    public void ResetSpeedModifiers()
    {
        path.maxSpeed = moveSpeed + globalSpeedModifier;
    }

    public void TakeDamage(float _damage) //is called when a projectile hits an enemy
    {
        currentHealth -= _damage * damageMultiplier * GameManager.Instance.BuffManager.GetGlobalDamageModifier();
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GameManager.Instance.WaveSpawner.RemoveEnemy(gameObject);
            GameManager.Instance.StatisticsManager.IncreaseKillCount();
            Destroy(gameObject);
        }
        else
        {
            animator.SetTrigger("isHurt");
        }
        if(healthBar != null) 
        {
            healthBar.fillAmount = currentHealth / enemyHealth;
        }
    }

    public void Die()
    {
        currentHealth = 0;
        GameManager.Instance.WaveSpawner.RemoveEnemy(gameObject);
        Destroy(gameObject);
    }

    //private void Update()
    //{
    //    if (GetComponent<AIPath>().reachedEndOfPath)
    //    {
    //        WaveSpawner.Instance.RemoveEnemy(gameObject);
    //        Destroy(gameObject);
    //    }
    //}
}
