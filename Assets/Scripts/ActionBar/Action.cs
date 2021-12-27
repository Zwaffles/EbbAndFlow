using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action: ScriptableObject
{
    [Header("Action Bar Order")]
    [Range(0, 5)]
    [SerializeField] private int actionBarOrder = 1;

    [Header("Action Bar Icon")]
    [SerializeField] private Sprite actionIcon;

    [Header("Tooltip")]
    [TextArea(5, 10)]
    [SerializeField] private string actionTooltip = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";

    public int ActionBarOrder { get { return actionBarOrder; } }
    public Sprite ActionIcon { get { return actionIcon; } }
    public string ActionTooltip { get { return actionTooltip; } }

    public virtual void UseAction()
    {

    }

    public virtual bool Interactable()
    {
        return true;
    }
}