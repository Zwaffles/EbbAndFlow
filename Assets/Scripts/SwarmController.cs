using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwarmController : MonoBehaviour
{
    [SerializeField] GameObject[] swarmLayer;
    [SerializeField] GameObject swarmTimer;
    private TextMeshProUGUI timeText;

    public float timeRemaining = 0;
    public bool timerIsRunning = false;

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
                SwarmStart();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Base"))
        {
            Debug.Log("Base found!");
            foreach(var _swarmLayer in swarmLayer)
            {
                _swarmLayer.SetActive(true);
            }
            swarmTimer.SetActive(true);
            timerIsRunning = true;
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void SwarmStart()
    {
        Debug.Log("the swarm is on!");
    }
}