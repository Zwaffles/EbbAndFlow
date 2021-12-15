using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    [Tooltip("The 'Preview' Animation Controller that will be visible when placing the tower.")]
    [SerializeField] private RuntimeAnimatorController towerPreviewAC;
    [Tooltip("The 'Actual' Tower GameObject that will be placed on the Grid.")]
    [SerializeField] private GameObject towerPrefab;

    private Tower tower;
    private int towerCost;

    public RuntimeAnimatorController TowerPreviewAC { get { return towerPreviewAC; } }
    public GameObject TowerPrefab { get { return towerPrefab; } }
    public int TowerCost { get { return towerCost; } }

    private void Awake()
    {
        tower = towerPrefab.GetComponent<Tower>();
        towerCost = tower.baseCost;
    }

    public void PlaceTower()
    {
        /* Can afford Tower */
        if (GameManager.Instance.PlayerCurrency.CanBuy(towerCost))
        {
            GameManager.Instance.BuildingManager.PlaceTower(this);
        }
        /* Cant afford Tower */
        else
        {
            Debug.Log("Cant afford Tower!");
        } 
    }
}