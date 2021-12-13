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
    private Image healthBar;
    private WaveSpawner waveSpawner;
    private Animator animator;

    private void Awake()
    {
        waveSpawner = FindObjectOfType<WaveSpawner>();
        animator = GetComponent<Animator>();
    }

    public void Initialize(float healthModifier)
    {
        enemyHealth += healthModifier;
        currentHealth = enemyHealth;
        healthBar = transform.GetChild(0).GetChild(1).GetComponent<Image>();
    }

    public void TakeDamage(float _damage) //is called when a projectile hits an enemy
    {
        currentHealth -= _damage * dmgMultiplier;
        if (currentHealth <= 0)
        {
            animator.SetTrigger("isDead");
        }
        else
        {
            animator.SetTrigger("isHurt");
        }
        healthBar.fillAmount = currentHealth / enemyHealth;
    }

    public void Die()
    {
        currentHealth = 0;
        waveSpawner.currentWaveEnemies.Remove(gameObject);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (GetComponent<AIPath>().reachedEndOfPath)
        {
            waveSpawner.currentWaveEnemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
