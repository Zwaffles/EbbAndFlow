using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    [SerializeField] private CameraBounds2D bounds;
    [SerializeField] private float transitionDuration = 600f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Vector2 maxXPositions;
    private bool targetReached;

    void Awake()
    {
        bounds.Initialize(GetComponent<Camera>());
        startPosition = transform.position;
        maxXPositions = bounds.maxXlimit;
        targetPosition = new Vector3(bounds.GetEdgePosition().x, transform.position.y, -10);
        StartCoroutine(Transition());
    }

    private IEnumerator Transition()
    {
        float t = 0.0f;
        while(t < 1.0f)
        {
            t += Time.deltaTime * (1 / transitionDuration);
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            UpdateProgress();
            yield return 0;
        }
        OnTargetReached();
    }

    void Update()
    {
        //MoveCamera();
    }

    private void MoveCamera()
    {
        if (!targetReached && GameManager.Instance.PlayerHealth.Alive)
        {
            Vector3 currentPosition = transform.position;
            Vector3 target = new Vector3(Mathf.Clamp(transform.position.x + 1, maxXPositions.x, maxXPositions.y), transform.position.y, transform.position.z);
            //timeLerped += Time.deltaTime;
            //transform.position = Vector3.Lerp(startPosition, targetPosition, timeLerped / timeToLerp);

            if (Vector3.Distance(currentPosition, targetPosition) <= 0.01f)
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