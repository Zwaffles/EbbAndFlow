using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerCurrency : MonoBehaviour
{
    [Header("Currency")]
    public int playerNormalCurrency = 50;
    public int playerInfectedCurrency = 0;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI normalCurrencyText;
    [SerializeField] TextMeshProUGUI infectedCurrencyText;

    private void Start()
    {
        normalCurrencyText.text = ("Normal Currency: " + playerNormalCurrency.ToString());
        infectedCurrencyText.text = ("Infected Currency: " + playerInfectedCurrency.ToString());
    }

    public void AddPlayerNormalCurrency(int amount)
    {
        playerNormalCurrency += amount;
        normalCurrencyText.text = ("Normal Currency: " + playerNormalCurrency.ToString());
    }

    public void RemovePlayerNormalCurrency(int amount)
    {
        playerNormalCurrency -= amount;
        normalCurrencyText.text = ("Normal Currency: " + playerNormalCurrency.ToString());
    }

    public void AddPlayerInfectedCurrency(int amount)
    {
        playerInfectedCurrency += amount;
        infectedCurrencyText.text = ("Infected Currency: " + playerInfectedCurrency.ToString());
    }

    public void RemovePlayerInfectedCurrency(int amount)
    {
        playerInfectedCurrency -= amount;
        infectedCurrencyText.text = ("Infected Currency: " + playerInfectedCurrency.ToString());
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
