using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName = "New Switch Action Bar Action", menuName = "ActionBar/Switch Action Bar", order = 6)]
public class SwitchActionBarAction : Action
{
    [Header("New Action Bar")]
    [SerializeField] private ActionBar actionBar;

    public ActionBar ActionBar { get { return actionBar; } }

    public override void UseAction()
    {
        base.UseAction();
        GameManager.Instance.ActionBarManager.UpdateActionBar(actionBar);
        FindObjectOfType<EventSystem>().SetSelectedGameObject(null);
    }
}