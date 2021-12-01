using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TowerTargeting : MonoBehaviour
{
    //placeholder, replace with inherited tower range
    [SerializeField] float towerRange = 5f;
    [SerializeField] int numberOfTargets = 2;

    private List<GameObject> currentlyTargeted = new List<GameObject>();
    private CircleCollider2D rangeCollider;
    private List<GameObject> enemiesWithinRange = new List<GameObject>();

    void Start()
    {
        rangeCollider = GetComponent<CircleCollider2D>();
        
        //currentlyTargeted = new GameObject[numberOfTargets];
    }

    void Update()
    {
        rangeCollider.radius = towerRange;
        currentlyTargeted.Clear();
        currentlyTargeted.AddRange(FindClosestEnemies(enemiesWithinRange, numberOfTargets));
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        // Add Enemy to ArrayList
        if(other.gameObject.CompareTag("Enemy"))
        {
            enemiesWithinRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        // Remove Enemy from ArrayList
        if(other.gameObject.CompareTag("Enemy"))
        {
            enemiesWithinRange.Remove(other.gameObject);
        }
    }

    // Returns list of GameObjects sorted by distance
    public List<GameObject> FindClosestEnemies(List<GameObject> list, int numOfObjects)
    {
        List<GameObject> goList = list;

        //returns if no object is in list
        if(goList.Count == 0)
        {
            return goList;
        }

        //sorts game object list by distance
        Vector3 position = transform.position;
        goList = goList.OrderBy(go => Vector3.Distance(go.transform.position, position)).ToList<GameObject>();

        //adds sorted objects within numOfObjects limit to sortedObjects
        List<GameObject> sortedObjects = new List<GameObject>();
        for(int i = 0; i < numOfObjects; i++)
        {
            if(i < goList.Count) 
            {
                sortedObjects.Add(goList[i]);
            }
        }

        return sortedObjects;
    }
}

/*
// Returns closest GameObject in ArrayList
    public GameObject FindClosestEnemy(List<GameObject> list)
    {
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in list)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
    */
