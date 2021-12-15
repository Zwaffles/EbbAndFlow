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

    private bool activated;

    public void ActivateEndScreen(bool gameOver)
    {

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

            endScreenPanel.SetActive(true);
            activated = true;
        }
    }

    private void UpdateStats()
    {

    }
}
