using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get { return instance; } }
    private static BuildingManager instance;

    private GameObject towerToPlace;
    private bool placingTower;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
    }

    private void Update()
    {
        PlaceTower();
    }

    private void PlaceTower()
    {
        if (placingTower)
        {
            if(towerToPlace != null)
            {
                FollowCursor();
                BuildTower();
            }
        }
    }

    private void FollowCursor()
    {
        /* TODO: Snap to Grid */
        towerToPlace.transform.position = Utilities.GetMouseWorldPosition();
    }

    private void BuildTower()
    {
        /* TODO: CAN-Build check */
        /* Change tower opacity/color if you cant */
    }
}