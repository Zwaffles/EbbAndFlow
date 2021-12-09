using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Pathfinding;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner Instance { get { return instance; } }
    private static WaveSpawner instance;

    //Waves
    [SerializeField] List<WaveConfigSO> waves;
    [SerializeField] float timeBetweenWaves = 15f;
    [HideInInspector] public List<GameObject> currentWaveEnemies;
    private List<GameObject> additionalEnemies = new List<GameObject>();
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
    PlayerCurrency playerCurrency;

    //End of wave actions
    bool endWaveActionsMade;

    //Infection towers
    //List<InfectedCurrencyTower> infectedBlockades = new List<InfectedCurrencyTower>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
        playerCurrency = FindObjectOfType<PlayerCurrency>();
    }

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
            Debug.Log("Modifying enemy HP by : " + BuffManager.Instance.GetHealthModifier());
            enemyInstance.GetComponent<Enemy>().Initialize(BuffManager.Instance.GetHealthModifier());
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

        playerCurrency.AddPlayerNormalCurrency((GetCurrentWave().WaveNormalCurrencyReward) + waveCurrencyAmount);
        playerCurrency.AddPlayerInfectedCurrency(BuffManager.Instance.CalculateInfectedCurrencyModifier());
        BuffManager.Instance.SpawnAdditionalEnemies();
        BuffManager.Instance.IncreaseInfectionScore();
        BuffManager.Instance.CalculateHealthModifier();
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