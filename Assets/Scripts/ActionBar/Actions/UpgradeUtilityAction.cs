using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade Utility Action", menuName = "ActionBar/Upgrade Utility", order = 5)]
public class UpgradeUtilityAction : Action
{
    public override void UseAction()
    {
        base.UseAction();
        Debug.Log("Utility Upgraded! (placeholder since we have no Utility upgrades implemented)");
    }
}