using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade Tower Action", menuName = "ActionBar/Upgrade Tower", order = 2)]
public class UpgradeNormalUtilityAction : Action
{
    public override void UseAction()
    {
        Debug.Log("Normal income upgraded!");
        GameManager.Instance.WaveSpawner.GlobalCurrencyUpgrade();
    }
}