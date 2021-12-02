using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    bool isPaused;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                isPaused = true;
                PauseGame();
            }
            else if (isPaused)
            {
                isPaused = false;
                ResumeGame();
            }
        }
    }

    void PauseGame()
    {
        pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void StartGame()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Exiting Game...");
    }
}
