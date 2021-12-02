using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Pathfinding;

public class CheatDetection : MonoBehaviour
{
    public static CheatDetection Instance { get { return instance; } }
    private static CheatDetection instance;

    AIPath path;

    GraphNode node1;
    GraphNode node2;

    private void Awake()
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
        NNConstraint buildingConstraint = new NNConstraint();
        buildingConstraint.graphMask = 0 | 1;

        node1 = AstarPath.active.GetNearest(transform.position, buildingConstraint).node;
        node2 = AstarPath.active.GetNearest(GetComponent<AIDestinationSetter>().target.position, buildingConstraint).node;
    }

    private void Update()
    {
        Debug.Log("is path possible? " + PathUtilities.IsPathPossible(node1, node2));
    }

    public bool CheckForObstacles()
    {
        if (PathUtilities.IsPathPossible(node1, node2))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
