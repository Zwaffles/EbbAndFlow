using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRange : MonoBehaviour
{
    private TowerTargeting parent;

    void Start()
    {
        parent = transform.parent.GetComponent<TowerTargeting>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        parent.OnChildTriggerEnter2D(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        parent.OnChildTriggerExit2D(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        parent.OnChildTriggerStay2D(other);
    }
}
