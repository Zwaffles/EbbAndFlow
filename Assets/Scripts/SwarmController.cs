using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwarmController : MonoBehaviour
{
    [SerializeField] GameObject[] swarmLayer;
    [SerializeField] GameObject swarmTimer;
    [SerializeField] WaveConfigSO swarmWave;
    private TextMeshProUGUI timeText;

    public float timeRemaining = 0;
    public bool timerIsRunning = false;

    private bool swarming = false;

    private void Start()
    {
        timeText = swarmTimer.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                DisplayTime(timeRemaining);
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                DisplayTime(timeRemaining);
                timerIsRunning = false;
                swarming = true;
                StartSwarm();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //swarm begins AS SOON as base touches the collider, MEANING it starts even when in build phase... watch out!
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Base"))
        {
            Debug.Log("Base found!");
            if(swarmLayer.Length > 0)
            {
                foreach (var _swarmLayer in swarmLayer)
                {
                    _swarmLayer.SetActive(true);
                }
            }
            swarmTimer.SetActive(true);
            timerIsRunning = true;
        }
    }

    void DisplayTime(float timeToDisplay) //displays the time remaining until the swarm arrives, is called when the base collides with a Swarm Point Trigger
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void StartSwarm()
    {
        Debug.Log("Swarm Incoming!");
        swarmTimer.SetActive(false);
        GameManager.Instance.WaveSpawner.NextWave();
        GameManager.Instance.WaveSpawner.SetSwarmActive(swarming, swarmWave.EnemySpawnInterval, swarmWave.EnemyEntries, this);
    }

    public void EndSwarm()
    {
        if(swarmLayer.Length > 0)
        {
            foreach (var _swarmLayer in swarmLayer)
            {
                _swarmLayer.SetActive(false);
            }
        }
        Destroy(gameObject);
    }
}