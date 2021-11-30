using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ActionBar", menuName = "ActionBar/Action Bar", order = 1)]
public class ActionBar : ScriptableObject
{
    [SerializeField] private List<Action> actions = new List<Action>();

    public List<Action> Actions { get { return actions; } }
}