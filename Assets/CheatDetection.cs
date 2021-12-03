using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Pathfinding;
using UnityEngine.Tilemaps;

public class CheatDetection : MonoBehaviour
{
    public static CheatDetection Instance { get { return instance; } }
    private static CheatDetection instance;

    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform goalPoint;

    //GraphNode node1;
    //GraphNode node2;
    GraphMask mask1;
    //GraphMask mask2;

    NNConstraint buildingConstraint = new NNConstraint();

    //GraphUpdateObject guo = new GraphUpdateObject();

    //Seeker seeker;

    //private float remainingDistance;

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

        //seeker = GetComponent<Seeker>();

        mask1 = GraphMask.FromGraphName("Detection Graph");

        buildingConstraint = NNConstraint.Default;
        buildingConstraint.graphMask = mask1;

        //var guo = new GraphUpdateObject(FindObjectOfType<TilemapCollider2D>().bounds);

        //node1 = AstarPath.active.GetNearest(transform.position, buildingConstraint).node;
        //node2 = AstarPath.active.GetNearest(GetComponent<AIDestinationSetter>().target.position, buildingConstraint).node;
    }

//    private void Start()
//    {
        
//    }

//    void OnPathCalculated(Path path)
//    {

//        if (path.error)
//        {
//            Debug.Log("ahhh!!");
//            return;
//        }

//        float pathLength = path.GetTotalLength();
//}

//    private void Update()
//    {
//        GetComponent<AIPath>().SearchPath();
//        Debug.Log(GraphUpdateUtilities.UpdateGraphsNoBlock(guo, node1, node2, false));
//    }

    public bool CheckForObstacles()
    {
        var guo = new GraphUpdateObject(GetComponent<Collider2D>().bounds);
        var spawnPointNode = AstarPath.active.GetNearest(spawnPoint.position, buildingConstraint).node;
        var goalNode = AstarPath.active.GetNearest(goalPoint.position, buildingConstraint).node;

        if(GraphUpdateUtilities.UpdateGraphsNoBlock(guo, spawnPointNode, goalNode, false))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
