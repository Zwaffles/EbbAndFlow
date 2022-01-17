using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;

    private bool isPaused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void PauseUnpause()
    {
        /* UnPause */
        if (isPaused)
        {
            /* Backtrack thing */
            if (optionsMenu.activeSelf)
            {
                ToggleOptionsMenu();
                TogglePauseMenu();
            }
            /* UnPause */
            else
            {
                Time.timeScale = 1;
                isPaused = false;
                pauseMenuPanel.SetActive(false);
            }
        }
        /* Pause */
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            pauseMenuPanel.SetActive(true);
        }
    }

    public void TogglePauseMenu()
    {
        /* Deactivate */
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
        }
        /* Activate */
        else
        {
            pauseMenu.SetActive(true);
        }
    }

    public void ToggleOptionsMenu()
    {
        /* Deactivate */
        if (optionsMenu.activeSelf)
        {
            optionsMenu.SetActive(false);
        }
        /* Activate */
        else
        {
            pauseMenu.SetActive(true);
        }
    }
}