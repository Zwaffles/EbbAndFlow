using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return instance; } }
    private static GameManager instance;

    void Awake()
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

    public bool CanBuy(int cost)
    {
        if(cost <= GetComponent<PlayerCurrency>().playerNormalCurrency)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
