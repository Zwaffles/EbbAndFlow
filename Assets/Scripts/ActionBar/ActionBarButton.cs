using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionBarButton: MonoBehaviour
{
    private Action action;
    private Button actionButton;
    private Image actionIcon;
    private string actionTooltip;

    private void Awake()
    {
        actionButton = GetComponent<Button>();
        actionIcon = GetComponentInChildren<Image>();
    }

    public void InitializeActionButton(Action action, Sprite icon, string tooltip)
    {
        this.action = action;
        actionIcon.sprite = icon;
        actionTooltip = tooltip;
    }

    public void OnClick()
    {
        action.UseAction();
    }

    public string GetTooltip()
    {
        return actionTooltip;
    }

    public void Interactable(bool state)
    {
        actionButton.interactable = state;
    }

    public void EnableActionButton()
    {
        gameObject.SetActive(true);
    }

    public void DisableActionButton()
    {
        gameObject.SetActive(false);
    }
}