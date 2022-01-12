using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarAnchor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, -transform.root.rotation.z);
    }
}
