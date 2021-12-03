using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private float damage;

    public float speed = 70f;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        transform.right = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            target.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    private void HitTarget()
    {
        
    }
}
