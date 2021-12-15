using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CheatDetection : MonoBehaviour
{
    [SerializeField] List<CheatDetector> cheatDetectors = new List<CheatDetector>();
    private int currentCystIndex = -1;

    public void IncreaseCystIndex()
    {
        currentCystIndex++;
    }

    public void DecreaseCystIndex()
    {
        currentCystIndex--;
    }

    public bool CheckForObstacles(GameObject buildMarker)
    {
        Collider2D buildMarkerCollider = buildMarker.GetComponent<CompositeCollider2D>();

        bool isPathValid;

        if(currentCystIndex == 0)
        {
            Debug.Log("checking for index: " + currentCystIndex + " & " + (currentCystIndex + 1));
            isPathValid = cheatDetectors[0].CheckForObstacles(buildMarkerCollider) ? cheatDetectors[1].CheckForObstacles(buildMarkerCollider) ? true : false : false;
            return isPathValid;
        }
        else if(currentCystIndex == cheatDetectors.Count - 1)
        {
            Debug.Log("checking for index: " + (currentCystIndex - 1) + " & " + currentCystIndex);
            isPathValid = cheatDetectors[cheatDetectors.Count - 2].CheckForObstacles(buildMarkerCollider) ? cheatDetectors[cheatDetectors.Count - 1].CheckForObstacles(buildMarkerCollider) ? true : false : false;
            return isPathValid;
        }
        else
        {
            Debug.Log("checking for index: " + (currentCystIndex - 1) + ", " + currentCystIndex + " & " + (currentCystIndex + 1));
            isPathValid = cheatDetectors[currentCystIndex - 1].CheckForObstacles(buildMarkerCollider) ? cheatDetectors[currentCystIndex].CheckForObstacles(buildMarkerCollider) ? cheatDetectors[currentCystIndex + 1].CheckForObstacles(buildMarkerCollider) ? true : false : false : false;
            return isPathValid;
        }
    }
}