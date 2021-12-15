using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return instance; } }
    private static GameManager instance;

    private SelectionManager selectionManager;
    private InfectionManager infectionManager; 
    private BuildingManager buildingManager;
    private SceneManagement sceneManagement;
    private BuffManager buffManager;
    private PlayerCurrency playerCurrency;
    private CheatDetection cheatDetection;
    private WaveSpawner waveSpawner;

    public SelectionManager SelectionManager { get; }
    public InfectionManager InfectionManager { get; }
    public BuildingManager BuildingManager { get; }
    public SceneManagement SceneManagement { get; }
    public BuffManager BuffManager { get; }

    public PlayerCurrency PlayerCurrency { get; }
    public CheatDetection CheatDetection { get; }
    public WaveSpawner WaveSpawner { get; }

    void Awake()
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

        /* Managers lmao */
        selectionManager = FindObjectOfType<SelectionManager>();
        infectionManager = FindObjectOfType<InfectionManager>();
        buildingManager = FindObjectOfType<BuildingManager>();
        sceneManagement = FindObjectOfType<SceneManagement>();
        buffManager = FindObjectOfType<BuffManager>();

        playerCurrency = FindObjectOfType<PlayerCurrency>();
        cheatDetection = FindObjectOfType<CheatDetection>();
        waveSpawner = FindObjectOfType<WaveSpawner>();
    }

    public bool CanBuy(int cost)
    {
        if(cost <= GetComponent<PlayerCurrency>().playerNormalCurrency)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
