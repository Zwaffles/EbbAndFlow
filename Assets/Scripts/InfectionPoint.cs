using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InfectionPoint
{
    public int index;
    public Vector3 position;
    public Vector3 startPosition;
    public Vector3 targetPosition;

    public InfectionPoint(int index, Vector3 position)
    {
        this.index = index;
        this.position = position;
        startPosition = position;
        targetPosition = position;
    }

    public bool TargetReached()
    {
        if(position == targetPosition)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}