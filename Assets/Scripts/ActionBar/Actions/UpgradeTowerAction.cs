using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade Tower Action", menuName = "ActionBar/Upgrade Tower", order = 2)]
public class UpgradeTowerAction : Action
{
    public override void UseAction()
    {
        base.UseAction();
        if (GameManager.Instance.SelectionManager.TowerUpgrades.CanUpgrade())
        {
            GameManager.Instance.SelectionManager.TowerUpgrades.UpgradeTower();
        }
    }

    public override bool Interactable()
    {
        if (GameManager.Instance.SelectionManager.TowerUpgrades.CanUpgrade())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}