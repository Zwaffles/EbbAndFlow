using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTower : Tower
{
    TowerTargeting towerTargeting;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float damage = 3f;
    public int numberOfTargets = 2;

    private Transform target;

    private void Awake()
    {
        
    }

    void Start()
    {
        turret = this.transform;
        towerTargeting = GetComponent<TowerTargeting>();
    }

    void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0)
        {
            for (int i = 0; i < numberOfTargets; i++)
            {
                if(towerTargeting.AcquireTarget().Count - 1 < i) { return; }
                if(towerTargeting.AcquireTarget()[i] == null) { return; }
                target = towerTargeting.AcquireTarget()[i].transform;
                Shoot();        
            }
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
