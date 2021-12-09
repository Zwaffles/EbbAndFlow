using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneManagement : MonoBehaviour
{
    [Header("Pause Menu")]
    [SerializeField] GameObject pauseMenuUI;
    [Tooltip("Enables pause menu")] [SerializeField] bool canPause;
    private bool isPaused;

    [Header("Options Menu")]
    [SerializeField] GameObject optionsMenuUI;
    private bool isOptions;

    [Header("Infection Stats")]
    [SerializeField] GameObject infectionStatsUI;
    private bool infectionStatsShown;




    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (canPause)
            {
                if (!isPaused)
                {
                    isPaused = true;
                    PauseGame();
                }
                else if (isPaused && !isOptions)
                {
                    isPaused = false;
                    ResumeGame();
                }
            }
            if (isOptions)
            {
                optionsMenuUI.gameObject.SetActive(false);
                isOptions = false;
                if (canPause)
                {
                    pauseMenuUI.gameObject.SetActive(true);
                }
            }
        }
    }

    void PauseGame()
    {
        pauseMenuUI.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuUI.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void LoadScene(int index) //Usable for start or restart
    {
        SceneManager.LoadScene(index);
    }

    public void OptionsMenu() //Enables options menu
    {
        pauseMenuUI.gameObject.SetActive(false);
        isOptions = true;
        optionsMenuUI.gameObject.SetActive(true);
    }

    public void Back() //Used for back button
    {
        if (canPause)
        {
            pauseMenuUI.gameObject.SetActive(true);
            optionsMenuUI.gameObject.SetActive(false);
        }
        else
        {
            optionsMenuUI.gameObject.SetActive(false);
        }

        if (isOptions)
        {
            isOptions = false;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Exiting Game...");
    }

    public void ToggleInfectionStats() //Shows/hides Speed %, Health %, Extra enemies #, tower infection score #
    {
        if (!infectionStatsShown)
        {
            //GetComponents<Tower>().ShowInfectionScore();
            infectionStatsUI.SetActive(true);
            infectionStatsShown = true;
        }
        else if (infectionStatsShown)
        {
            //GetComponents<Tower>().HideInfectionScore();
            infectionStatsUI.SetActive(false);
            infectionStatsShown = false;
        }
    }
}


