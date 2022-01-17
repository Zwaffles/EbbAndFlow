using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [Header("Win Settings")]
    [TextArea(2, 5)]
    [SerializeField] private string playerWonHeader = "YOU WON!";
    [TextArea(2, 5)]
    [SerializeField] private string playerWonText = "EZClap";

    [Header("Lost Settings")]
    [TextArea(2,5)]
    [SerializeField] private string playerLostHeader = "YOU LOST!";
    [TextArea(2, 5)]
    [SerializeField] private string playerLostText = "LOSER";

    [Header("Setup Fields")]
    [SerializeField] private GameObject endScreenPanel;
    [SerializeField] private TextMeshProUGUI endScreenHeader;
    [SerializeField] private TextMeshProUGUI endScreenText;

    [Header("Statistics")]
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI killsText;
    [SerializeField] private TextMeshProUGUI towersBuiltText;
    [SerializeField] private TextMeshProUGUI progressText;

    private bool activated;

    public void ActivateEndScreen(bool gameOver)
    {
        GameManager.Instance.AudioManager.GetComponent<FMODUnity.StudioEventEmitter>().SetParameter("death", 1f);

        if (!activated)
        {
            /* Player Lost */
            if (gameOver)
            {
                endScreenHeader.text = playerLostHeader;
                endScreenText.text = playerLostText;
            }
            /* Player Won */
            else
            {
                endScreenHeader.text = playerWonHeader;
                endScreenText.text = playerWonText;
            }

            UpdateStats();
            endScreenPanel.SetActive(true);
            activated = true;
        }
    }

    private void UpdateStats()
    {
        timeText.text = FormatTime(GameManager.Instance.StatisticsManager.TimeCounter);
        killsText.text = GameManager.Instance.StatisticsManager.Kills.ToString();
        towersBuiltText.text = GameManager.Instance.StatisticsManager.TowersBuilt.ToString();
        progressText.text = (GameManager.Instance.StatisticsManager.Progress * 100).ToString("F1") + "%";
    }

    private string FormatTime(float time)
    {
        int intTime = (int)time;
        int milliseconds = (int)((time - intTime) * 100);
        int seconds = (int)(time % 60);
        int minutes = (int)(time / 60 % 60);

        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
}
