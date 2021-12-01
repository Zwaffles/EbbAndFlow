using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    Transform endPosition;
    WaveSpawner waveSpawner;


    void Update()
    {
        FollowPath();
    }

    public void Initialize(Transform position, WaveSpawner waveSpawner)
    {
        endPosition = position;
        this.waveSpawner = waveSpawner;
    }

    void FollowPath()
    {
        if (endPosition != null)
        {
            Vector3 targetPosition = endPosition.position;
            float delta = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            EndPositionReached();
        }
    }

    void EndPositionReached()
    {
        if (Vector3.Distance(transform.position, endPosition.position) < 0.1f)
        {
            waveSpawner.currentWaveEnemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }

}
