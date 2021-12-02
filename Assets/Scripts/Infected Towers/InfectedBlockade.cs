using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedBlockade : MonoBehaviour
{
    [SerializeField] List<GameObject> enemies;
    WaveSpawner waveSpawner;    
    bool hasAddedEnemies;
    public int infectionScore = 4;    
      
    public void IncreaseEnemiesInWave()
    {        
        if (!hasAddedEnemies && infectionScore > 0)
        {
            hasAddedEnemies = true;
            AddEnemy();
        }
        infectionScore -= 1;
    }

    void AddEnemy()
    {
        waveSpawner = FindObjectOfType<WaveSpawner>();
        foreach (GameObject enemy in enemies)
        {
            //waveSpawner.currentWaveEnemies.Add(enemy);
            waveSpawner.AddAdditionalEnemy(enemy);
        }
        hasAddedEnemies = false;
        Debug.Log(hasAddedEnemies);
    }
}
