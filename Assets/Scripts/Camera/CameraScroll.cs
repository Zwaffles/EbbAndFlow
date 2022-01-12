using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    [SerializeField] private CameraBounds2D bounds;
    [SerializeField] private float cameraSpeed = 10f;

    private Vector2 startPosition;
    private Vector2 targetPosition;
    private Vector2 maxXPositions;
    private bool targetReached;

    void Awake()
    {
        bounds.Initialize(GetComponent<Camera>());
        startPosition = transform.position;
        maxXPositions = bounds.maxXlimit;
        targetPosition = bounds.GetEdgePosition();
    }

    void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        if (!targetReached && GameManager.Instance.PlayerHealth.Alive)
        {
            Vector3 currentPosition = transform.position;
            Vector3 target = new Vector3(Mathf.Clamp(transform.position.x + 1, maxXPositions.x, maxXPositions.y), transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(currentPosition, target, Time.deltaTime * cameraSpeed);
            UpdateProgress();

            if (Vector3.Distance(currentPosition, target) <= 0.01f)
            {
                targetReached = true;
                OnTargetReached();
            }
        }
    }

    private void OnTargetReached()
    {
        GameManager.Instance.EndScreen.ActivateEndScreen(false);
    }

    private void UpdateProgress()
    {
        float progressPercentage = 1 - Vector2.Distance(transform.position, targetPosition) / Vector2.Distance(startPosition, targetPosition);
        GameManager.Instance.StatisticsManager.SetProgress(progressPercentage);
    }
}