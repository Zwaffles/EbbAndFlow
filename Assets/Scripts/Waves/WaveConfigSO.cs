using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [Serializable]
    public class EnemyEntry
    {
        public GameObject enemy;
        public int amount;
    }

    [SerializeField] List<EnemyEntry> enemyEntries;

    //[SerializeField] List<GameObject> enemies;

    [Header("Spawn Interval")]
    [SerializeField] float enemySpawnInterval = 0.5f;

    [Header("Currency Reward")]
    [SerializeField] int waveNormalCurrencyReward = 50;

    public List<EnemyEntry> EnemyEntries { get { return enemyEntries; } }
    //public List<GameObject> Enemies { get { return enemies; } }
    public float EnemySpawnInterval { get { return enemySpawnInterval; } }
    public int WaveNormalCurrencyReward { get { return waveNormalCurrencyReward; } }    
}
