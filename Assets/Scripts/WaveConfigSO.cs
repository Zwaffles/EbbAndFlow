using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] List<GameObject> enemies;
    [SerializeField] float enemySpawnInterval = 0.5f;

    public List<GameObject> Enemies { get { return enemies; } }
    public float EnemySpawnInterval { get { return enemySpawnInterval; } }
}
