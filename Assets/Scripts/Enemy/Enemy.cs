using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float enemyHealth = 10f;
    [SerializeField] private float currentHealth;
    [SerializeField] private float dmgMultiplier = 1f;
    [SerializeField] private float minMoveSpeed = 0.25f;
    [SerializeField] private float moveSpeed = 3.0f;

    private Image healthBar;
    private AIPath path;
    private bool speedDebuff;
    private float globalSpeedModifier;

    public bool SpeedDebuff { get; set; }
    public float MoveSpeed { get; }

    private void Awake()
    {
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
        currentHealth -= _damage * dmgMultiplier;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            WaveSpawner.Instance.RemoveEnemy(gameObject);
            Destroy(gameObject);
        }
        healthBar.fillAmount = currentHealth / enemyHealth;
    }

    private void Update()
    {
        if (GetComponent<AIPath>().reachedEndOfPath)
        {
            WaveSpawner.Instance.RemoveEnemy(gameObject);
            Destroy(gameObject);
        }
    }
}