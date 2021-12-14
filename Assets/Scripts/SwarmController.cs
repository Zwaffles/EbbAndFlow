using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwarmController : MonoBehaviour
{
    [SerializeField] GameObject[] swarmLayer;
    [SerializeField] GameObject swarmTimer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Base"))
        {
            foreach(var _swarmLayer in swarmLayer)
            {
                _swarmLayer.SetActive(true);
            }
            swarmTimer.SetActive(true);
            GetComponent<SwarmTimer>().timerIsRunning = true;
        }
    }

    public void SwarmStart()
    {

    }
}

public class SwarmTimer : MonoBehaviour
{
    public float timeRemaining = 10;
    public bool timerIsRunning = false;

    public TextMeshProUGUI timeText;

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
                GetComponent<SwarmController>().SwarmStart();
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}