using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade Tower Action", menuName = "ActionBar/Upgrade Tower", order = 2)]
public class UpgradeInfectedUtilityAction : Action
{
    public override void UseAction()
    {
        Debug.Log("Infected income upgraded!");
        GameManager.Instance.WaveSpawner.GlobalInfectedCurrencyUpgrade();
    }
}