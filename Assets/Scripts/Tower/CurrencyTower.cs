using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyTower : Tower
{
    [Header("Tower Settings")]
    [SerializeField] int currencyPerWave = 1;

    public int CurrencyPerWave { get { return currencyPerWave; } set { currencyPerWave = value; } }

    public int GetTowerCurrencyPerWave()
    {
        if(!isInfected)
        {
            return currencyPerWave;
        }
        else
        {
            return 0;
        }
    }

}
