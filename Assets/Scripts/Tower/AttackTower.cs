using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTower : Tower
{
    TowerTargeting towerTargeting;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float damage = 3f;
    [SerializeField] float splashRadius = 0f;
    [SerializeField] float splashDamage = 0f;
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
        if (cooldown <= 0) //executes if cooldown has reached 0
        {
            for (int i = 0; i < numberOfTargets; i++) //for loop that checks for multiple targets incase its a multi shot tower
            {
                if(towerTargeting.AcquireTarget().Count - 1 < i) { return; }
                if(towerTargeting.AcquireTarget()[i] == null) { return; }
                target = towerTargeting.AcquireTarget()[i].transform;
                Shoot();        
            }
        }
    }

    void Shoot() //tower goes pew pew
    {
        cooldown = fireRate;
        GameObject projectileGO = (GameObject)Instantiate(projectilePrefab, turret.position, turret.rotation);
        Projectile projectile = projectileGO.GetComponent<Projectile>();

        if(projectile != null) //sets projectile parameters
        {
            projectile.SetDamage(damage);
            projectile.SetSplash(splashRadius, splashDamage);
            projectile.Seek(target);
        }
    }
}
