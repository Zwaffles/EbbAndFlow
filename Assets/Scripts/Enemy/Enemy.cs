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

    [SerializeField] private AudioClip deathSound;
    
    private Image healthBar;
    private AIPath path;
    private Animator animator;
    private bool speedDebuff;
    private float globalSpeedModifier;

    public bool SpeedDebuff { get; set; }
    public float MoveSpeed { get; }
    public float DamageMultiplier { get { return damageMultiplier; } set { damageMultiplier = value; } }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        path = GetComponent<AIPath>();
        path.maxSpeed = moveSpeed;
    }

    public void Initialize(float healthModifier, float speedModifier)
    {
        
        enemyHealth += healthModifier;
        currentHealth = enemyHealth;
        globalSpeedModifier = speedModifier;
        path.maxSpeed = moveSpeed + globalSpeedModifier;
        healthBar = transform.GetChild(0).GetChild(1).GetComponent<Image>();
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
            GameManager.Instance.AudioManager.Play(deathSound, true);
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

    //public void Die()
    //{
    //    currentHealth = 0;
    //    GameManager.Instance.WaveSpawner.RemoveEnemy(gameObject);
    //    Destroy(gameObject);
    //}

    //private void Update()
    //{
    //    if (GetComponent<AIPath>().reachedEndOfPath)
    //    {
    //        WaveSpawner.Instance.RemoveEnemy(gameObject);
    //        Destroy(gameObject);
    //    }
    //}
}
