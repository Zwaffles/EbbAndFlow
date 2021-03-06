using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Pathfinding;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfigSO> waves;
    [SerializeField] private float timeBetweenWaves = 15f;

    [Header("Path")]
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI currentWaveText;
    [SerializeField] private TextMeshProUGUI waveTimerText;
    [SerializeField] private Button skipWaveButton;

    private List<GameObject> currentWaveEnemies = new List<GameObject>();
    private List<GameObject> additionalEnemies = new List<GameObject>();

    private Coroutine spawnWaveCoroutine = null;
    private float waveSpawnCounter = 60f;
    private int waveIndex = -1;
    private bool spawning;
    private bool spawnerActive = true;
    private bool endWaveActionsMade;

    void Update()
    {
        SpawnWaves();
        currentWaveText.text = ("Wave: " + (waveIndex + 1) + "/" + waves.Count.ToString());
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

                if (!endWaveActionsMade && waveIndex >= 0) //Does end wave actions
                {
                    OnWaveEnd();
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

        List<GameObject> tempEnemyWave = new List<GameObject>();

        foreach (GameObject enemy in GetCurrentWave().Enemies)
        {
            tempEnemyWave.Add(enemy);
        }


        foreach (GameObject additionalEnemy in additionalEnemies)
        {
            tempEnemyWave.Add(additionalEnemy);
        }

        foreach (GameObject enemy in tempEnemyWave)
        {
            GameObject enemyInstance = Instantiate(enemy, startPosition.position, Quaternion.identity);
            enemyInstance.GetComponent<AIDestinationSetter>().target = endPosition;
            currentWaveEnemies.Add(enemyInstance);

            GameManager.Instance.BuffManager.CalculateHealthModifier();
            GameManager.Instance.BuffManager.CalculateSpeedModifier();
            enemyInstance.GetComponent<Enemy>().Initialize(GameManager.Instance.BuffManager.GetHealthModifier(), GameManager.Instance.BuffManager.GetSpeedModifier());

            yield return new WaitForSeconds(GetCurrentWave().EnemySpawnInterval);
        }

        endWaveActionsMade = false;
        waveSpawnCounter = timeBetweenWaves;
        spawning = false;
        additionalEnemies = new List<GameObject>();
    }

    public void AddAdditionalEnemy(GameObject enemy)
    {
        additionalEnemies.Add(enemy);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        currentWaveEnemies.Remove(enemy.gameObject);
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

    void OnWaveEnd()
    {
        endWaveActionsMade = true;
       
        //adds currency amount of all towers into waveCurrencyAmount
        List<CurrencyTower> CurrencyTowers = FindObjectsOfType<CurrencyTower>().ToList();
        int waveCurrencyAmount = 0;

        foreach (CurrencyTower tower in CurrencyTowers)
        {
            waveCurrencyAmount += tower.GetTowerCurrencyPerWave();
        }

        GameManager.Instance.PlayerCurrency.AddPlayerNormalCurrency((GetCurrentWave().WaveNormalCurrencyReward + waveCurrencyAmount));
        GameManager.Instance.PlayerCurrency.AddPlayerInfectedCurrency(GameManager.Instance.BuffManager.CalculateInfectedCurrencyModifier());

        GameManager.Instance.BuffManager.SpawnAdditionalEnemies();
        GameManager.Instance.BuffManager.IncreaseInfectionScore();
        GameManager.Instance.BuffManager.CalculateHealthModifier();
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