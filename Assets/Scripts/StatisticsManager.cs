using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsManager : MonoBehaviour
{
    [SerializeField] private float progress;
    [SerializeField] private float timeCounter;

    [SerializeField] private int kills;
    [SerializeField] private int towersBuilt;
    

    public float Progress { get { return progress; } }
    public float TimeCounter { get { return timeCounter; } }
    
    public int Kills { get { return kills; } }
    public int TowersBuilt { get { return towersBuilt; } }
    

    private void Update()
    {
        timeCounter += Time.deltaTime;
    }

    public void IncreaseKillCount()
    {
        kills++;
    }

    public void IncreaseTowersBuiltCount()
    {
        towersBuilt++;
    }

    public void DecreaseTowersBuiltCount()
    {
        towersBuilt--;
    }

   public void SetProgress(float value)
    {
        progress = value;
    }
}