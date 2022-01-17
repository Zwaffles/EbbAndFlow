using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class TimeScaleManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeScaleText;
    private MultiImageButton selectedButton;

    [Header("Time Scale Buttons")]
    [SerializeField] private GameObject timeScaleButtonOne;
    [SerializeField] private GameObject timeScaleButtonTwo;
    [SerializeField] private GameObject timeScaleButtonFive;

    private void Start()
    {
        SelectButton(timeScaleButtonOne);
        SetTimeScale(1.0f);
    }

    private void Update()
    {
        TimeScaleHotkeys();
    }

    private void TimeScaleHotkeys()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SelectButton(timeScaleButtonOne);
            SetTimeScale(1.0f);
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            SelectButton(timeScaleButtonTwo);
            SetTimeScale(2.0f);
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            SelectButton(timeScaleButtonFive);
            SetTimeScale(5.0f);
        }
    }

    public void TogggleTimeScale()
    {
        Time.timeScale = Time.timeScale == 1.0f ? 1.5f : Time.timeScale == 1.5f ? 2.0f : 1.0f;
        timeScaleText.text = "Timescale\n" + Time.timeScale + "x";
    }

    public void SelectButton(GameObject button)
    {
        if(selectedButton != null)
        {
            selectedButton.interactable = true;
        }

        selectedButton = button.GetComponent<MultiImageButton>();
        selectedButton.interactable = false;
    }

    public void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }
}