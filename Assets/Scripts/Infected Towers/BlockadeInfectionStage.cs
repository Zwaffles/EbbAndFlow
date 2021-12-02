using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BlockadeInfectionStage
{
    [SerializeField] string stage = "Stage 1";
    [SerializeField] private int infectionScoreTrigger;
    [SerializeField] private int enemyAmountToSpawn;
    [SerializeField] GameObject enemyToSpawn;


    public int InfectionScoreTrigger { get { return infectionScoreTrigger; } }
    public int EnemyAmountToSpawn { get { return enemyAmountToSpawn; } }

    public GameObject EnemyToSpawn { get { return enemyToSpawn; } }

}
