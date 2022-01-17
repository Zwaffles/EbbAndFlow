using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneManager : MonoBehaviour
{
    [Header("Fade In Settings")]
    [Range(0.1f, 5.0f)]
    [SerializeField] private float fadeInSpeed = 1.0f;
    [SerializeField] private Color fadeInColor = new Color(0.1f, 0.1f, 0.1f, 1);

    [Header("Fade Out Settings")]
    [Range(0.1f, 5.0f)]
    [SerializeField] private float fadeOutSpeed = 1.0f;
    [SerializeField] private Color fadeOutColor = new Color(0.1f, 0.1f, 0.1f, 1);

    [Header("Setup Fields")]
    [SerializeField] private Image fadeBackground;
    public FMOD.Studio.Bus MasterBus;

    private bool fadingIn;
    private bool fadingOut;

    private Coroutine fadeInCoroutine = null;
    private Coroutine fadeOutCoroutine = null;

    private void Start()
    {
        MasterBus = FMODUnity.RuntimeManager.GetBus("Bus:/");
        fadeBackground = GetComponentInChildren<Image>();
        fadeInCoroutine = StartCoroutine(FadeIn());
    }

    public void FadeLoadScene(int index)
    {
        Time.timeScale = 1;

        if (!fadingOut)
        {
            StopCoroutine(fadeInCoroutine);
            fadeInCoroutine = StartCoroutine(FadeOut(index));
        }
    }

    private IEnumerator FadeIn()
    {
        fadingIn = true;
        fadeBackground.color = fadeInColor;
        while (fadeBackground.color.a != 0)
        {
            Color newColor = fadeBackground.color;
            newColor.a -= Time.deltaTime * fadeInSpeed;
            fadeBackground.color = newColor;
            yield return null;
        }
        fadeInCoroutine = null;
        fadingOut = false;
    }

    private IEnumerator FadeOut(int index)
    {
        fadingOut = true;
        Color newColor = fadeOutColor;
        newColor.a = 0;
        fadeBackground.color = newColor;

        while(fadeBackground.color.a < 1)
        {
            newColor.a += Time.deltaTime * fadeOutSpeed;
            fadeBackground.color = newColor;
            yield return null;
        }

        fadeInCoroutine = null;

        MasterBus.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }

    public void LoadScene(int index)
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }

    public void QuitGame()
    {
        Debug.Log("Quiting Game..."); 
        Application.Quit();
    } 
}