using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileFireball : Projectile
{
    [SerializeField] ParticleSystem _particleSystem;


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
            DetachParticles();
            HitTarget();
            //target.GetComponent<Enemy>().TakeDamage(damage);
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }


    //detaches particle system from projectile
    private void DetachParticles()
    {
        _particleSystem.Play();
        _particleSystem.transform.parent = null;
    }
}
