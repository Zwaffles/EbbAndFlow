using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InfectedSpawnModifier
{
    public string stage = "Stage 1";
    [SerializeField] private int infectionScoreTrigger;
    [SerializeField] private List<GameObject> enemiesToSpawn = new List<GameObject>();

    public int InfectionScoreTrigger { get { return infectionScoreTrigger; } }
    public List<GameObject> EnemiesToSpawn { get { return enemiesToSpawn; } }

}
