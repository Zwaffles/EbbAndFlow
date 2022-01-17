using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;


public class PlayerCurrency : MonoBehaviour
{
    [Header("Currency")]
    public int playerNormalCurrency = 50;
    public int playerInfectedCurrency = 0;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI normalCurrencyText;
    [SerializeField] TextMeshProUGUI infectedCurrencyText;

    private List<TowerBuilder> towerBuilders = new List<TowerBuilder>();

    private void Start()
    {
        towerBuilders = FindObjectsOfType<TowerBuilder>().ToList();
        normalCurrencyText.text = playerNormalCurrency.ToString();
        infectedCurrencyText.text = playerInfectedCurrency.ToString();
        TowerBuildCheck();
    }

    private void TowerBuildCheck()
    {
        for (int i = 0; i < towerBuilders.Count; i++)
        {
            /* Enable Button if we can afford */
            if (CanBuy(towerBuilders[i].TowerCost))
            {
                towerBuilders[i].Enable();
            }
            /* Disable Button if we cant afford */
            else
            {
                towerBuilders[i].Disable();
            }
        }
    }

    public void AddPlayerNormalCurrency(int amount)
    {
        playerNormalCurrency += amount;
        normalCurrencyText.text = playerNormalCurrency.ToString();
        GameManager.Instance.ActionBarManager.UpdateButtonStates();
        TowerBuildCheck();
    }

    public void RemovePlayerNormalCurrency(int amount)
    {
        playerNormalCurrency -= amount;
        normalCurrencyText.text = playerNormalCurrency.ToString();
        GameManager.Instance.ActionBarManager.UpdateButtonStates();
        TowerBuildCheck();
    }

    public void AddPlayerInfectedCurrency(int amount)
    {
        playerInfectedCurrency += amount;
        infectedCurrencyText.text = playerInfectedCurrency.ToString();
        GameManager.Instance.ActionBarManager.UpdateButtonStates();
        TowerBuildCheck();
    }

    public void RemovePlayerInfectedCurrency(int amount)
    {
        playerInfectedCurrency -= amount;
        infectedCurrencyText.text = playerInfectedCurrency.ToString();
        GameManager.Instance.ActionBarManager.UpdateButtonStates();
        TowerBuildCheck();
    }

    public bool CanBuy(int cost)
    {
        if (cost <= playerNormalCurrency)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool InfectedCanBuy(int cost)
    {
        if (cost <= playerInfectedCurrency)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
