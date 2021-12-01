using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveSpawner : MonoBehaviour
{    
    [SerializeField] List<WaveConfigSO> waves;
    [SerializeField] float timeBetweenWaves = 15f;

    [Header("Path")]
    [SerializeField] Transform startPosition;
    [SerializeField] Transform endPosition;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI currentWaveText;
    [SerializeField] TextMeshProUGUI waveTimerText;
    [SerializeField] Button skipWaveButton;

    [HideInInspector] public List<GameObject> currentWaveEnemies;

    float waveSpawnCounter;
    int waveIndex = -1;
    bool spawning;
    bool spawnerActive = true;

    Coroutine spawnWaveCoroutine = null;

    void Update()
    {       
        SpawnWaves(); 
        currentWaveText.text = ("Wave: " + (waveIndex + 1) + "/5"); //Sets current wave              
    }

    void SpawnWaves()
    {
        if (spawnerActive && !spawning && currentWaveEnemies.Count == 0)
        {
            if (waveSpawnCounter <= 0)
            {
                waveTimerText.gameObject.SetActive(false); //Hides Timer
                skipWaveButton.gameObject.SetActive(false); //Hides Skip Button
                spawning = true;
                spawnWaveCoroutine = StartCoroutine(SpawnNextWave());
            }
            else
            {
                waveTimerText.gameObject.SetActive(true);
                skipWaveButton.gameObject.SetActive(true);
                waveSpawnCounter -= Time.deltaTime;
                waveTimerText.text = ("Next Wave: " + (waveSpawnCounter.ToString("F0")));
            }
        }
    }

    IEnumerator SpawnNextWave()
    {        
        waveIndex++;
        FinalWaveCheck();
        if (waveIndex > waves.Count - 1)
        {
            yield break;
        }

        foreach (GameObject enemy in GetCurrentWave().Enemies)
        {
            GameObject enemyInstance = Instantiate(enemy, startPosition.position, Quaternion.identity);
            enemyInstance.GetComponent<Pathfinder>().Initialize(endPosition, this);
            currentWaveEnemies.Add(enemyInstance);
            yield return new WaitForSeconds(GetCurrentWave().EnemySpawnInterval);
        }
        waveSpawnCounter = timeBetweenWaves;
        spawning = false;
    }

    void FinalWaveCheck()
    {
        if (waveIndex == waves.Count - 1)
        {
            spawnerActive = false;
            StopCoroutine(spawnWaveCoroutine);
            Debug.Log("Final Wave!");
        }
    }

    public void NextWave()
    {
        if (!spawning)
        {
            waveSpawnCounter = 0;
            waveTimerText.text = ("Next Wave: " + waveSpawnCounter.ToString("F0"));
        }
    }

    private WaveConfigSO GetCurrentWave()
    {
        return waves[waveIndex];
    }
}