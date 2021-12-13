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
    private AIPath path;

    private void Awake()
    {
        path = GetComponent<AIPath>();
    }

    public void Initialize(float healthModifier, float speedModifier)
    {
        enemyHealth += healthModifier;
        currentHealth = enemyHealth;
        path.maxSpeed += speedModifier;
        healthBar = transform.GetChild(0).GetChild(1).GetComponent<Image>();
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
