using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLightning : MonoBehaviour
{
    private Transform target;
    private float damage;
    private float splashRadius;
    private float splashDamage;

    private bool hitTarget;

    public ParticleSystem trailParticleSystem;
    public float speed = 70f;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }

    public void SetSplash(float _splashRadius, float _splashDamage)
    {
        splashRadius = _splashRadius;
        splashDamage = _splashDamage;
    }

    void Start()
    {
        
    }

    void Update() // updates every frame
    {
        if(target == null) //returns and destroys the projectile if target is null
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        transform.right = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame) //executes logic if projectile has reached destination
        {
            HitTarget();
            //target.GetComponent<Enemy>().TakeDamage(damage);
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    private void HitTarget() //logic for hitting target
    {
        if (splashRadius > 0) //check if tower has splash damage
        {
            target.GetComponent<Enemy>()?.TakeDamage(damage - splashDamage);
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, splashRadius);
            foreach (Collider2D hitCollider in hitColliders)
            {
                hitCollider.GetComponent<Enemy>()?.TakeDamage(splashDamage);
            }
            Destroy(gameObject);
        }
        else
        {
            target.GetComponent<Enemy>()?.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    //detaches particle system from projectile
    private void DetachParticles()
    {
        trailParticleSystem.transform.parent = null;
    }
}
