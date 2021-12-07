using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    [SerializeField] CameraBounds2D bounds;
    [SerializeField] float cameraSpeed = 10f;
    Vector2 maxXPositions;

    void Awake()
    {
        bounds.Initialize(GetComponent<Camera>());
        maxXPositions = bounds.maxXlimit;
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = new Vector3(Mathf.Clamp(transform.position.x + 1, maxXPositions.x, maxXPositions.y), transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(currentPosition, targetPosition, Time.deltaTime * cameraSpeed);
    }
}

