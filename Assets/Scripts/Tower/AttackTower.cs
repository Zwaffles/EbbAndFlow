using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTower : Tower
{
    TowerTargetingZweifel towerTargeting;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float damage = 3f;

    private Transform target;

    private void Awake()
    {
        
    }

    void Start()
    {
        turret = this.transform;
        towerTargeting = GetComponent<TowerTargetingZweifel>();
    }

    void Update()
    {
        cooldown -= Time.deltaTime;

        if (towerTargeting.AcquireTarget() == null) { return; }
        target = towerTargeting.AcquireTarget().transform;
        if (cooldown <= 0)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        cooldown = fireRate;
        GameObject projectileGO = (GameObject)Instantiate(projectilePrefab, turret.position, turret.rotation);
        Projectile projectile = projectileGO.GetComponent<Projectile>();

        if(projectile != null)
        {
            projectile.SetDamage(damage);
            projectile.Seek(target);
        }
    }
}
