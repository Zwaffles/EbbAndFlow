using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CheatDetection : MonoBehaviour
{
    public static CheatDetection Instance { get { return instance; } }
    private static CheatDetection instance;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform goalPoint;

    private NNConstraint buildingConstraint = new NNConstraint(); 
    private GraphMask mask1;

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
    }

    private void Start()
    {
        mask1 = GraphMask.FromGraphName("Detection Graph");
        buildingConstraint = NNConstraint.Default;
        buildingConstraint.graphMask = mask1;
    }

    public bool CheckForObstacles(GameObject buildMarker)
    {
        Collider2D buildMarkerCollider = buildMarker.GetComponent<CompositeCollider2D>();
        GraphUpdateObject graphUpdateObject = new GraphUpdateObject(buildMarkerCollider.bounds);

        //GraphUpdateShape collider = new GraphUpdateShape()
        //graphUpdateObject.shape

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