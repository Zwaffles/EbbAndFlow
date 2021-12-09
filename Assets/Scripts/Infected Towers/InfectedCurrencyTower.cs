using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class InfectedCurrencyTower : MonoBehaviour
//{    
//    [SerializeField] int infectionScore = 0;
//    [SerializeField] int maxInfectionScore = 10;   
//    [SerializeField] List<EnemySpawner> infectionStages = new List<EnemySpawner>();
//    EnemySpawner currentInfectionStage;
//    WaveSpawner waveSpawner;
//    bool hasAddedEnemies;



//    public void IncreaseEnemiesInWave() //Adds infection score and enemies
//    {
        
//        if (infectionScore < maxInfectionScore)
//        {
//            infectionScore += 1;
//        }

//        if (!hasAddedEnemies)
//        {
//            hasAddedEnemies = true;
//            UpdateInfectionStage();
//            AddEnemy();
//        }
//    }

//    void AddEnemy() //Adds enemies to list in wavespawner
//    {
//        waveSpawner = FindObjectOfType<WaveSpawner>();
//        if (currentInfectionStage != null)
//        {
//            for (int i = 0; i < currentInfectionStage.EnemyAmountToSpawn; i++)
//            {
//                waveSpawner.AddAdditionalEnemy(currentInfectionStage.EnemyToSpawn);
//            }
//        }
//        hasAddedEnemies = false;
//        Debug.Log(hasAddedEnemies);
//    }

//    void UpdateInfectionStage() //Updates current infection stage
//    {
//        foreach (EnemySpawner infectionStage in infectionStages)
//        {
//            if (infectionScore >= infectionStage.InfectionScoreTrigger)
//            {
//                currentInfectionStage = infectionStage;
//            }
//        }
//    }
//}
