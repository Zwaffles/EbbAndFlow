using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//incomplete, blame me for now (Jesper)
public class ProjectileLightning : Projectile
{
    [SerializeField] ParticleSystem trailParticleSystem;


    void Update() // updates every frame
    {
        if(target == null) //returns and destroys the projectile if target is null
        {
            DetachParticles();
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
        trailParticleSystem.transform.parent = null;
    }
}
