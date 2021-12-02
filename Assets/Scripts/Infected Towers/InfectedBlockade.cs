using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedBlockade : MonoBehaviour
{    
    [SerializeField] int infectionScore = 0;
    [SerializeField] int maxInfectionScore = 10;   
    [SerializeField] List<BlockadeInfectionStage> infectionStages = new List<BlockadeInfectionStage>();
    BlockadeInfectionStage currentInfectionStage;
    WaveSpawner waveSpawner;
    bool hasAddedEnemies;



    public void IncreaseEnemiesInWave()
    {
        if (infectionScore < maxInfectionScore)
        {
            infectionScore += 1;
        }

        if (!hasAddedEnemies)
        {
            hasAddedEnemies = true;
            UpdateInfectionStage();
            AddEnemy();
        }
    }

    void AddEnemy()
    {
        waveSpawner = FindObjectOfType<WaveSpawner>();
        if (currentInfectionStage != null)
        {
            for (int i = 0; i < currentInfectionStage.EnemyAmountToSpawn; i++)
            {
                waveSpawner.AddAdditionalEnemy(currentInfectionStage.EnemyToSpawn);
            }
        }
        hasAddedEnemies = false;
        Debug.Log(hasAddedEnemies);
    }

    void UpdateInfectionStage()
    {
        foreach (BlockadeInfectionStage infectionStage in infectionStages)
        {
            if (infectionScore >= infectionStage.InfectionScoreTrigger)
            {
                currentInfectionStage = infectionStage;
            }
        }
    }
}
