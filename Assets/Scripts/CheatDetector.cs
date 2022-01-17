using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CheatDetector : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform goalPoint;

    private NNConstraint buildingConstraint = new NNConstraint();
    private GraphMask mask1;

    private void Start()
    {
        mask1 = GraphMask.FromGraphName("Detection Graph");
        buildingConstraint = NNConstraint.Default;
        buildingConstraint.graphMask = mask1;
    }

    public bool CheckForObstacles(Collider2D buildMarkerCollider)
    {
        GraphUpdateObject graphUpdateObject = new GraphUpdateObject(buildMarkerCollider.bounds);

        GraphNode spawnNode = AstarPath.active.GetNearest(spawnPoint.position, buildingConstraint).node;
        GraphNode goalNode = AstarPath.active.GetNearest(goalPoint.position, buildingConstraint).node;

        /* Updates the graph while checking for blocking elements, if nothing blocks it returns true */
        if (GraphUpdateUtilities.UpdateGraphsNoBlock(graphUpdateObject, spawnNode, goalNode, false))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
