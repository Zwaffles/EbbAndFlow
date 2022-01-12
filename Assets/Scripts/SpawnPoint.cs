using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private List<GameObject> cysts = new List<GameObject>();

    public void AddNewSpawn(GameObject cyst) //adds a new cyst to the list of cysts
    {
        cyst.GetComponent<SpriteRenderer>().color = Color.red;
        cysts.Add(cyst);
        GameManager.Instance.CheatDetection.IncreaseCystIndex();
        SetSpawn();
    }

    public void RemoveOldSpawn(GameObject cyst) //removes an old cyst from the list of cysts
    {
        cyst.GetComponent<SpriteRenderer>().color = Color.black;
        cysts.Remove(cyst);
        GameManager.Instance.CheatDetection.DecreaseCystIndex();
        SetSpawn();
    }

    private void SetSpawn() //sets a new spawnpoint at the last index of the list of cysts
    {
        transform.position = cysts[cysts.Count - 1].transform.position;
    }
}
