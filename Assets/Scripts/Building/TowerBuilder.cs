using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{

    [SerializeField] private GameObject towerPrefab;

    public void PlaceTower()
    {
        BuildingManager.Instance.PlaceTower(towerPrefab);
    }
}