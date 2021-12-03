using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TowerTargeting : MonoBehaviour
{
    [SerializeField] CircleCollider2D rangeCollider;
    //placeholder, replace with inherited tower range
    [SerializeField] float towerRange = 5f;

    private List<GameObject> currentlyTargeted = new List<GameObject>();
    private List<GameObject> enemiesWithinRange = new List<GameObject>();

    void Start()
    {

    }

    void Update()
    {
        rangeCollider.radius = towerRange;
        currentlyTargeted = FindClosestObjectsInList(enemiesWithinRange, GetComponent<AttackTower>().numberOfTargets);
    }


    public void OnChildTriggerEnter2D(Collider2D other) 
    {
        // Add Enemy to List
        if(other.gameObject.CompareTag("Enemy"))
        {
            enemiesWithinRange.Add(other.gameObject);
        }
    }

    public void OnChildTriggerExit2D(Collider2D other) 
    {
        // Remove Enemy from List
        if(other.gameObject.CompareTag("Enemy"))
        {
            enemiesWithinRange.Remove(other.gameObject);
        }
    }

    // Returns list of GameObjects sorted by distance
    public List<GameObject> FindClosestObjectsInList(List<GameObject> list, int numOfObjects)
    {
        //returns if no object is in list
        if(list.Count == 0)
        {
            return list;
        }



        List<GameObject> removeNullList = new List<GameObject>();
        for(int i = 0; i < list.Count; i++)
        {
            if(list[i] != null) 
            {
                removeNullList.Add(list[i]);
            }
        }
        list = removeNullList;

        //sorts game object list by distance
        Vector3 position = transform.position;
        list = list.OrderBy(go => Vector3.Distance(go.transform.position, position)).ToList<GameObject>();

        //adds sorted objects within numOfObjects limit to sortedObjects
        List<GameObject> sortedObjects = new List<GameObject>();
        for(int i = 0; i < numOfObjects; i++)
        {
            if(i < list.Count) 
            {
                sortedObjects.Add(list[i]);
            }
        }

        return sortedObjects;
    }

    public List<GameObject> AcquireTarget()
    {
        return currentlyTargeted;
    }
}