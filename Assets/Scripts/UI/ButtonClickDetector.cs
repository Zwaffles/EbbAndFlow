using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickDetector : MonoBehaviour
{
    [SerializeField] AudioClip clickSound;

    private Button[] buttons;

    private void Awake()
    {
        buttons = FindObjectsOfType<Button>();
    }

    private void OnEnable()
    {
        foreach(Button button in buttons)
        {
            button.onClick.AddListener(() => buttonCallBack());
        }
    }

    private void buttonCallBack()
    {
        GameManager.Instance.AudioManager.Play(clickSound, true);
    }

    private void OnDisable()
    {
        foreach (Button button in buttons)
        {
            button.onClick.RemoveAllListeners();
        }
    }
}
