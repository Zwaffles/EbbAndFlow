using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("Lives")]
    [SerializeField] int playerLives = 50;

    [Header("On Lives Lost")]
    [Tooltip("Set % value (1 = 100%)")] [Range(0, 10)] [SerializeField] float infectionSpeedIncrease;
    [SerializeField] int infectionSpeedChangeDuration;
    


    InfectionManager infectionManager;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI livesText;

    private void Start()
    {
        infectionManager = FindObjectOfType<InfectionManager>();       
        livesText.text = ("Lives: " + playerLives.ToString());
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (playerLives < 2)
            {
                Debug.Log("Game Over!");
                playerLives = 0;
            }
            else
            {
                playerLives -= 1;
                infectionManager.ChangeInfectionSpeed(infectionSpeedChangeDuration, infectionSpeedIncrease);
            }
            livesText.text = ("Lives: " + playerLives.ToString());
        }
    }

   
}
