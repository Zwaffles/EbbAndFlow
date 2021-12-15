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
    private SceneManager sceneManagement;
    private BuffManager buffManager;

    private PlayerCurrency playerCurrency;
    private CheatDetection cheatDetection;
    private WaveSpawner waveSpawner;
    private EndScreen endScreen;

    public SelectionManager SelectionManager { get { return selectionManager; } }
    public InfectionManager InfectionManager { get { return infectionManager; } }
    public BuildingManager BuildingManager { get { return buildingManager; } }
    public SceneManager SceneManagement { get { return sceneManagement; } }
    public BuffManager BuffManager { get { return buffManager; } }

    public PlayerCurrency PlayerCurrency { get { return playerCurrency; } }
    public CheatDetection CheatDetection { get { return cheatDetection; } }
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
        selectionManager = FindObjectOfType<SelectionManager>();
        infectionManager = FindObjectOfType<InfectionManager>();
        buildingManager = FindObjectOfType<BuildingManager>();
        sceneManagement = FindObjectOfType<SceneManager>();
        buffManager = FindObjectOfType<BuffManager>();

        playerCurrency = FindObjectOfType<PlayerCurrency>();
        cheatDetection = FindObjectOfType<CheatDetection>();
        waveSpawner = FindObjectOfType<WaveSpawner>();
        endScreen = FindObjectOfType<EndScreen>();
    }
}