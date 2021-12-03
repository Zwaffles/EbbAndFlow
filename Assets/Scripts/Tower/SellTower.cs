using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellTower : MonoBehaviour
{
    [SerializeField] int costToReturn;
    PlayerCurrency playerCurrency;

    public void TowerCurrencyReturn()
    {
        playerCurrency = FindObjectOfType<PlayerCurrency>();
        playerCurrency.AddPlayerNormalCurrency(costToReturn);
    }

    public void DestroyTower()
    {
        BuildingManager.Instance.RemoveTower(this.gameObject);
    }
}
