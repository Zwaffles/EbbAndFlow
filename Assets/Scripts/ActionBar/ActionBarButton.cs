using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionBarButton: MonoBehaviour
{
    [SerializeField] private Image actionIcon;
    [SerializeField] private float activationDelay = 0.1f;
    private MultiImageButton actionButton;

    private Tooltip tooltip;
    private Action action;
    
    private string actionTooltip;

    private void Awake()
    {
        actionButton = GetComponent<MultiImageButton>();
        tooltip = GetComponent<Tooltip>();
    }

    public void UpdateActionButton(Action action, Sprite icon, string tooltip)
    {
        this.action = action;
        actionIcon.sprite = icon;
        actionTooltip = tooltip;
        this.tooltip.UpdateTooltip(tooltip);
        EnableActionButton();
    }

    private void DelayedActivation()
    {
        actionButton.enabled = true;
    }

    public void UseAction()
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

    public void UpdateState()
    {
        if(action != null)
        {
            if (action.Interactable())
            {
                Interactable(true);
            }
            else
            {
                Interactable(false);
            }
        } 
    }

    public void EnableActionButton()
    {
        gameObject.SetActive(true);
        Invoke("DelayedActivation", activationDelay);
    }

    public void DisableActionButton()
    {
        actionButton.enabled = false;
        gameObject.SetActive(false);
    }
}