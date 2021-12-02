using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Pathfinding;

public class WaveSpawner : MonoBehaviour
{
    //Waves
    [SerializeField] List<WaveConfigSO> waves;
    [SerializeField] float timeBetweenWaves = 15f;
    [HideInInspector] public List<GameObject> currentWaveEnemies;
    Coroutine spawnWaveCoroutine = null;
    float waveSpawnCounter = 60f;
    int waveIndex = -1;
    bool spawning;
    bool spawnerActive = true;

    [Header("Path")]
    [SerializeField] Transform startPosition;
    [SerializeField] Transform endPosition;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI currentWaveText;
    [SerializeField] TextMeshProUGUI waveTimerText;
    [SerializeField] Button skipWaveButton;

    //Currency
    bool hasRecievedCurrency;
    PlayerCurrency playerCurrency;

    EnemyHealth enemyHealth;


    private void Awake()
    {
        playerCurrency = FindObjectOfType<PlayerCurrency>();
    }

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
                waveSpawnCounter -= Time.deltaTime; //Starts countdown to next wave
                waveTimerText.text = ("Next Wave: " + (waveSpawnCounter.ToString("F0")));

                if (!hasRecievedCurrency && waveIndex >= 0) //Adds currency at the start of each "Build Phase"
                {
                    hasRecievedCurrency = true;
                    playerCurrency.AddPlayerNormalCurrency(GetCurrentWave().WaveNormalCurrencyReward);
                }
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
            enemyInstance.GetComponent<AIDestinationSetter>().target = endPosition;
            currentWaveEnemies.Add(enemyInstance);
            yield return new WaitForSeconds(GetCurrentWave().EnemySpawnInterval);
        }
        hasRecievedCurrency = false;
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