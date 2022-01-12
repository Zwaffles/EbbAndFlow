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

    private bool alive = true;

    public bool Alive { get { return alive; } }

    private void Start()
    {
        livesText.text = ("Lives: " + playerLives.ToString());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (alive)
        {
            if (collision.CompareTag("Enemy"))
            {
                /* Game Over */
                if (playerLives <= 1)
                {
                    alive = false; 
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
            /* Game Over */
            if (collision.gameObject.layer == LayerMask.NameToLayer("Infection"))
            {
                alive = false;
                playerLives = 0;
                GameManager.Instance.EndScreen.ActivateEndScreen(true);
            }
        }
    }
}
