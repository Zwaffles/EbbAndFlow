using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float enemyHealth;
    [SerializeField] private float dmgReduction = 1f;

    void Start()
    {
        
    }

    void TakeDamage(float _damage) //is called when a projectile hits an enemy
    {
        enemyHealth -= _damage * dmgReduction;
        if(enemyHealth <= 0) { Destroy(gameObject); } //placeholder, in the future enemy death will probably be delegated into a new method
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
