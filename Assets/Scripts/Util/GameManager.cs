using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return instance; } }
    private static GameManager instance;

    private StatisticsManager statisticsManager; 
    private TimeScaleManager timeScaleManager;
    private SelectionManager selectionManager;
    private InfectionManager infectionManager;
    private ActionBarManager actionBarManager;
    private BuildingManager buildingManager;
    private TooltipManager tooltipManager;
    private SceneManager sceneManagement;
    private BuffManager buffManager;

    private PlayerCurrency playerCurrency;
    private CheatDetection cheatDetection;
    private PlayerHealth playerHealth;
    private WaveSpawner waveSpawner;
    private EndScreen endScreen;

    public StatisticsManager StatisticsManager { get { return statisticsManager; } }
    public TimeScaleManager TimeScaleManager {  get { return timeScaleManager; } }
    public SelectionManager SelectionManager { get { return selectionManager; } }
    public InfectionManager InfectionManager { get { return infectionManager; } }
    public ActionBarManager ActionBarManager { get { return actionBarManager; } }
    public BuildingManager BuildingManager { get { return buildingManager; } }
    public TooltipManager TooltipManager { get { return tooltipManager; } }
    public SceneManager SceneManagement { get { return sceneManagement; } }
    public BuffManager BuffManager { get { return buffManager; } }

    public PlayerCurrency PlayerCurrency { get { return playerCurrency; } }
    public CheatDetection CheatDetection { get { return cheatDetection; } }
    public PlayerHealth PlayerHealth { get { return playerHealth; } }
    public WaveSpawner WaveSpawner { get { return waveSpawner; } }
    public EndScreen EndScreen { get { return endScreen; } }

    void Awake()
    {
        /* Replace Old Instance */
        if (Instance != null && Instance != this)
        {
            Destroy(instance);
            DontDestroyOnLoad(this);
            instance = this;
        }
        /* Create new Instance */
        else
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
        UpdateReferences();
    }

    private void UpdateReferences()
    {
        /* Managers lmao */
        statisticsManager = FindObjectOfType<StatisticsManager>();
        timeScaleManager = FindObjectOfType<TimeScaleManager>();
        selectionManager = FindObjectOfType<SelectionManager>();
        infectionManager = FindObjectOfType<InfectionManager>();
        actionBarManager = FindObjectOfType<ActionBarManager>();
        buildingManager = FindObjectOfType<BuildingManager>();
        tooltipManager = FindObjectOfType<TooltipManager>();
        sceneManagement = FindObjectOfType<SceneManager>();
        buffManager = FindObjectOfType<BuffManager>();

        playerCurrency = FindObjectOfType<PlayerCurrency>();
        cheatDetection = FindObjectOfType<CheatDetection>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        waveSpawner = FindObjectOfType<WaveSpawner>();
        endScreen = FindObjectOfType<EndScreen>();
    }
}