using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sell Tower Action", menuName = "ActionBar/Sell Tower", order = 4)]
public class SellTowerAction : Action
{
    public override void UseAction()
    {
        base.UseAction();
        if (!GameManager.Instance.SelectionManager.CanSellTower()) { return; }
        GameManager.Instance.SelectionManager.SellTower();
    }

    public override bool Interactable()
    {
        if (GameManager.Instance.SelectionManager.CanSellTower())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}