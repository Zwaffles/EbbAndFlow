using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBarManager : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private ActionBar defaultActionBar;
    [SerializeField] private ActionBar currentActionBar;
    [SerializeField] private List<ActionBarButton> actionBarButtons = new List<ActionBarButton>();
    

    private void Start()
    {
        UpdateActionBar(defaultActionBar);
    }

    public void DefaultActionBar()
    {
        UpdateActionBar(defaultActionBar);
    }

    public void UpdateActionBar(ActionBar actionBar)
    {
        /* Disable ActionBar Buttons */
        for (int i = 0; i < actionBarButtons.Count; i++)
        {
            actionBarButtons[i].DisableActionButton();
        }

        /* Assign new Action Bar */
        currentActionBar = actionBar;

        /* Loop through ActionBar Actions  */
        for (int i = 0; i < currentActionBar.Actions.Count; i++)
        {
            /* Assign new Action to button */
            Action newAction = actionBar.Actions[i];
            actionBarButtons[newAction.ActionBarOrder].UpdateActionButton(newAction, newAction.ActionIcon, newAction.ActionTooltip);
        }
        UpdateButtonStates();
    }

    public void UpdateButtonStates()
    {
        for (int i = 0; i < actionBarButtons.Count; i++)
        {
            actionBarButtons[i].UpdateState();
        }
    }
}