using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyTower : Tower
{
    [Header("Tower Settings")]
    [SerializeField] int currencyPerWave = 1;

    public int CurrencyPerWave { get { return currencyPerWave; } set { currencyPerWave = value; } }

    public override SelectionInfo GetSelectionInfo()
    {
        for (int i = 0; i < SelectionInfo.StatInfo.Count; i++)
        {
            switch (SelectionInfo.StatInfo[i].Stat)
            {
                case StatInfo.StatType.InfectionScore:
                    SelectionInfo.StatInfo[i].BaseStat = InfectionScore;
                    break;
                default:
                    Debug.Log("No Method for " + SelectionInfo.StatInfo[i].Stat + " implemented!");
                    break;
            }
        }
        return SelectionInfo;
    }

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
