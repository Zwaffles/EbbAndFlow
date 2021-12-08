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

    private void Awake()
    {
        waveSpawner = FindObjectOfType<WaveSpawner>();
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
            currentHealth = 0;
            waveSpawner.currentWaveEnemies.Remove(gameObject);
            Destroy(gameObject);
        }
        healthBar.fillAmount = currentHealth / enemyHealth;
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
