using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyTower : Tower
{
    [SerializeField] int currencyPerWave = 1;

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
