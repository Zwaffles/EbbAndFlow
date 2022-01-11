using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sell Tower Action", menuName = "ActionBar/Sell Tower", order = 4)]
public class SellTowerAction : Action
{
    public override void UseAction()
    {
        base.UseAction();
        GameManager.Instance.SelectionManager.SellTower();
    }
}