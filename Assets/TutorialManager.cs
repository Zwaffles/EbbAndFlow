using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialCanvas;
    [SerializeField] private PauseManager pauseManager;

    private void Start()
    {
        Invoke("DelayedTimeStop", 0.01f);
    }

    private void DelayedTimeStop()
    {
        Time.timeScale = 0;
    }

    public void CloseTutorialWindow()
    {
        GameManager.Instance.TimeScaleManager.canChangeTimeScale = true; 
        tutorialCanvas.SetActive(false);
        pauseManager.canPause = true;
        Time.timeScale = 1;
    }
}