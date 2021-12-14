using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("Lives")]
    [SerializeField] int playerLives = 50;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI livesText;

    InfectionManager infectionManager;

    private void Start()
    {
        infectionManager = FindObjectOfType<InfectionManager>();
        livesText.text = ("Lives: " + playerLives.ToString());
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (playerLives <= 1)
            {
                Debug.Log("Game Over!");
                playerLives = 0;
            }
            else
            {
                playerLives -= 1;
                infectionManager.IncreaseInfectionSpeed();
            }
            WaveSpawner.Instance.RemoveEnemy(collision.gameObject);
            Destroy(collision.gameObject);
            livesText.text = ("Lives: " + playerLives.ToString());
        }
    }


}
