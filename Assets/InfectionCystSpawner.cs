using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class InfectionCystSpawner : MonoBehaviour
{
    [SerializeField] Transform infectionCystHolder;
    [SerializeField] PolygonCollider2D trigger;
    PolygonCollider2D collider;

    NNConstraint cystConstraint = new NNConstraint();
    GraphMask mask1;
    NNConstraint validationConstraint = new NNConstraint();
    GraphMask mask2;

    void Start()
    {
        mask1 = GraphMask.FromGraphName("Cyst Graph");

        cystConstraint = NNConstraint.Default;

        cystConstraint.graphMask = mask1;

        cystConstraint.constrainWalkability = true;
        cystConstraint.walkable = false;

        mask2 = GraphMask.FromGraphName("Enemy Graph");

        validationConstraint = NNConstraint.Default;

        validationConstraint.graphMask = mask2;

        validationConstraint.constrainWalkability = true;
        validationConstraint.walkable = true;

        //cystConstraint.constrainTags = true;
        //cystConstraint.tags = 0;
    }

    internal void SpawnCyst() //spawns new cyst on new wave 
    {
        if(collider != null)
        {
            Destroy(collider);
        }
        collider = InfectionManager.Instance.gameObject.AddComponent<PolygonCollider2D>();
        collider.points = trigger.points;

        var graphToScan = AstarPath.active.graphs[2] as GridGraph;
        AstarPath.active.Scan(graphToScan);

        var info = AstarPath.active.GetNearest(infectionCystHolder.position, cystConstraint);
        var correction = AstarPath.active.GetNearest(info.position, validationConstraint);

        var node = correction.node;

        int x = (int)((node.position.x / 1000) + 8.5f);
        if(CheatDetection.Instance.CheckForObstacles(graphToScan.GetNode(x, 5)))
        {
            transform.position = (Vector3)node.position;
        }
        else
        {
            GraphNode newNode = node;
            for (int i = 0; i < graphToScan.depth; i++)
            {

                if (CheatDetection.Instance.CheckForObstacles(graphToScan.GetNode(x, i)))
                {
                    newNode = graphToScan.GetNode(x, i);
                    Debug.Log(i);
                }
            }

            transform.position = (Vector3)newNode.position;
        }

    }
}
