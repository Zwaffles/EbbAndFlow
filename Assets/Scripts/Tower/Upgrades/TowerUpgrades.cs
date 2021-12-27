using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class TowerUpgrades : MonoBehaviour
{
    private int currentUpgrade = -1;
    public int CurrentUpgrade { get { return currentUpgrade; } set { currentUpgrade = value; } }


    public virtual bool CanUpgrade()
    {
        return true;
    }


    public virtual void UpgradeTower()
    {
        
    }
}