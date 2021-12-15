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

    private void Start()
    {
        livesText.text = ("Lives: " + playerLives.ToString());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (playerLives <= 1)
            {
                playerLives = 0;
                GameManager.Instance.EndScreen.ActivateEndScreen(true);
            }
            else
            {
                playerLives -= 1;

                GameManager.Instance.InfectionManager.IncreaseInfectionSpeed();
            }
            GameManager.Instance.WaveSpawner.RemoveEnemy(collision.gameObject);
            Destroy(collision.gameObject);
            livesText.text = ("Lives: " + playerLives.ToString());
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Infection"))
        {
            playerLives = 0;
            GameManager.Instance.EndScreen.ActivateEndScreen(true);
        }
    }
}
