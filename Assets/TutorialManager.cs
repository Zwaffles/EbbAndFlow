using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialCanvas;
    [SerializeField] private PauseManager pauseManager;

    public void CloseTutorialWindow()
    {
        GameManager.Instance.TimeScaleManager.canChangeTimeScale = true; 
        tutorialCanvas.SetActive(false);
        pauseManager.canPause = true;
        Time.timeScale = 1;
        GameManager.Instance.SceneManagement.FadeIn();
        GameManager.Instance.WaveSpawner.SpawnerActive = true;
    }
}