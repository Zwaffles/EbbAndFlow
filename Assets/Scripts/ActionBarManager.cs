using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBarManager : MonoBehaviour
{
    public static ActionBarManager Instance { get { return instance; } }
    private static ActionBarManager instance;

    [Header("Debug")]
    [SerializeField] private List<ActionBarButton> actionBarButtons = new List<ActionBarButton>();
    [SerializeField] private ActionBar currentActionBar;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
        
    }

    private void InitializeActionBar()
    {
        ActionBarButton[] buttons = GetComponentsInChildren<ActionBarButton>();
    }

    private void Start()
    {
        SetCurrentActionBar(currentActionBar);
    }

    public void SetCurrentActionBar(ActionBar actionBar)
    {
        /* Assign new Action Bar */
        currentActionBar = actionBar;

        /* Loop through all Button GameObjects */
        for (int i = 0, j = 0; i < actionBarButtons.Count; i++)
        {
            /* Check if we still have new Action to assign */
            if(j < actionBar.Actions.Count)
            {
                /* Assign new Action to button */
                Action newAction = actionBar.Actions[j];
                actionBarButtons[i].EnableActionButton(); ;
                actionBarButtons[i].InitializeActionButton(newAction, newAction.ActionIcon, newAction.ActionTooltip);
                j++;
            }
            /* Disable Button */
            else
            {
                actionBarButtons[i].DisableActionButton(); ;
            }
        }
    }
}