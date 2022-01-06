using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTower : Tower
{ 
    [Header("Projectile Settings")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float damage = 3f;
    [SerializeField] float splashRadius = 0f;
    [SerializeField] float splashDamage = 0f;
    public int numberOfTargets = 2;

    private TowerTargeting towerTargeting;
    private Transform target;

    [SerializeField] private float baseDamage;
    [SerializeField] private float baseRange;
    [SerializeField] private float baseFireRate;
    private float baseSplashRadius;
    private float baseSplashDamage;
    private int baseNumberOfTargets;

    /* Accessors */
    public float Damage { get { return damage; } set { damage = value; } }
    public float Range { get { return towerTargeting.towerRange;} set { towerTargeting.towerRange = value; } }
    public float FireRate { get { return fireRate; } set { fireRate = value; } }
    public float SplashRadius { get { return splashRadius; } set { splashRadius = value; } }
    public float SplashDamage { get { return splashDamage; } set { splashDamage = value; } }
    public int NumberOfTargets { get { return numberOfTargets; } set { numberOfTargets = value; } }

    public float BaseDamage { get { return baseDamage; } set { baseDamage = value; } }
    public float BaseRange { get { return baseRange; } set { baseRange = value; } }
    public float BaseFireRate { get { return baseFireRate; } set { baseFireRate = value; } }
    public float BaseSplashRadius { get { return baseSplashRadius; } set { baseSplashRadius = value; } }
    public float BaseSplashDamage { get { return baseSplashDamage; } set { baseSplashDamage = value; } }
    public int BaseNumberOfTargets { get { return baseNumberOfTargets; } set { baseNumberOfTargets = value; } }

    void Awake()
    {
        towerTargeting = GetComponent<TowerTargeting>();
        baseDamage = damage;
        baseRange = towerTargeting.towerRange;
        baseFireRate = fireRate;
        baseSplashRadius = splashRadius;
        baseSplashDamage = splashDamage;
        baseNumberOfTargets = numberOfTargets;
        turret = this.transform;
    }

    void Update() 
    {
        if(!isInfected)
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
