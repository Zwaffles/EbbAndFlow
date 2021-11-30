using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTargetting : MonoBehaviour
{
    //placeholder, replace with inherited tower range
    [SerializeField] float towerRange = 5f;

    private GameObject currentlyTargeted;
    private CircleCollider2D rangeCollider;
    private List<GameObject> enemiesWithinRange = new List<GameObject>();


    void Start()
    {
        rangeCollider = GetComponent<CircleCollider2D>();
        rangeCollider.radius = towerRange;
    }

    void Update()
    {
        currentlyTargeted = FindClosestEnemy(enemiesWithinRange);
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

}

/*
// Find the name of the closest enemy

using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour
{
    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
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
}
*/