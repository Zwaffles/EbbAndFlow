using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTower : Tower
{
    TowerTargetingZweifel towerTargeting;
    [SerializeField] GameObject projectilePrefab;

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
        if (cooldown <= 0)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        cooldown = fireRate;
        Instantiate(projectilePrefab, turret.position, turret.rotation);
    }
}
