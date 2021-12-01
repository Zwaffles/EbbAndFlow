using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] int health = 50;

   

    WaveSpawner waveSpawner;
   

    private void Awake()
    {
        waveSpawner = FindObjectOfType<WaveSpawner>();        
    }
    public void RemoveEnemyHealth(int damage)
    {
        if(damage < health)
        {
            health -= damage;           
        }         
        else if(damage >= health)
        {
            waveSpawner.currentWaveEnemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
