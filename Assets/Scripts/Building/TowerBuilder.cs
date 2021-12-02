using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    public GameObject towerPrefab;

    public void PlaceTower()
    {
        BuildingManager.Instance.PlaceTower(towerPrefab);
    }
}