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
    [Tooltip("Enables pause menu")] [SerializeField] private bool canPause;
    private bool isPaused;

    [Header("Options Menu")]
    [SerializeField] GameObject optionsMenuUI;
    private bool isOptions;
    
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

    void PauseGame() //Pauses game
    {
        pauseMenuUI.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame() //Resumes game
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

    public void QuitGame() //Quits Game :)
    {
        Application.Quit();
        Debug.Log("Exiting Game...");
    } 
}