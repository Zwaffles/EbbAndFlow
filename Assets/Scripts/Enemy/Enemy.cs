using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float enemyHealth = 10f;
    private float currentHealth;
    [SerializeField] private float dmgMultiplier = 1f;
    private Image healthBar;

    void Start()
    {
        currentHealth = enemyHealth;
        healthBar = transform.GetChild(0).GetChild(1).GetComponent<Image>();
    }

    public void TakeDamage(float _damage) //is called when a projectile hits an enemy
    {
        currentHealth -= _damage * dmgMultiplier;
        if(currentHealth <= 0) 
        {
            currentHealth = 0;
            Destroy(gameObject); 
        }
        healthBar.fillAmount = currentHealth / enemyHealth;
    }

}
