using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] List<GameObject> enemies;
    [Header("Spawn Interval")]
    [SerializeField] float enemySpawnInterval = 0.5f;

    [Header("Currency Reward")]
    [SerializeField] int waveNormalCurrencyReward = 50;
    

    public List<GameObject> Enemies { get { return enemies; } }
    public float EnemySpawnInterval { get { return enemySpawnInterval; } }
    public int WaveNormalCurrencyReward { get { return waveNormalCurrencyReward; } }    
}
