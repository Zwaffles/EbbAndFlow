using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockadeTowerUpgrade : TowerUpgrades
{
    public override bool CanUpgrade()
    {
        return base.CanUpgrade();
    }

    public override void UpgradeTower(bool increaseUpgradeIndex = true)
    {
        base.UpgradeTower();

        if (increaseUpgradeIndex)
        {
            CurrentUpgrade++;
        }
    }
}