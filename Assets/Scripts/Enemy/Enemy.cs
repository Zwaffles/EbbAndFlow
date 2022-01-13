using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float enemyHealth = 10f;
    [SerializeField] private float currentHealth;
    [SerializeField] private float damageMultiplier = 1.0f;
    [SerializeField] private float minMoveSpeed = 0.25f;
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float minDamageMultiplier = 0.1f;
    [SerializeField] private float maxDamageMultiplier = 4.0f;

    private LocalDamageModifier damageModifierTower;
    private Image healthBar;
    private AIPath path;
    private Animator animator;
    private bool speedDebuff;
    private bool damageBuff;
    private bool damageDebuff; 
    private float globalSpeedModifier;

    [SerializeField] private float damageBuffMultiplier = 1.0f;
    [SerializeField] private float damageDebuffMultiplier = 1.0f;

    [Header("Debug")]
    [SerializeField] private List<InfectedDamageModifier> damageModifiers = new List<InfectedDamageModifier>();

    public LocalDamageModifier DamageModifierTower { get { return damageModifierTower; } set { damageModifierTower = value; } }
    public bool SpeedDebuff { get; set; }
    public bool DamageBuff { get { return damageBuff; } set { damageBuff = value; } }
    public bool DamageDebuff { get { return damageDebuff; } set { damageDebuff = value; } }
    public float DamageBuffMultiplier { get { return damageBuffMultiplier; } set { damageBuffMultiplier = value; } }
    public float DamageDebuffMultiplier { get { return damageDebuffMultiplier; } set { damageDebuffMultiplier = value; } }
    public float MoveSpeed { get; }
    public float DamageMultiplier { get { return damageMultiplier; } set { damageMultiplier = value; } }
    public List<InfectedDamageModifier> DamageModifiers { get { return damageModifiers; } set { damageModifiers = value; } }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        path = GetComponent<AIPath>();
        path.maxSpeed = moveSpeed;
    }

    public void Initialize(float healthModifier, float speedModifier)
    {
        
        enemyHealth += healthModifier;
        currentHealth = enemyHealth;
        globalSpeedModifier = speedModifier;
        path.maxSpeed = moveSpeed + globalSpeedModifier;
        healthBar = transform.GetChild(0).GetChild(1).GetComponent<Image>();
    }

    public void ModifyMoveSpeed(float value)
    {
        path.maxSpeed += value;
        path.maxSpeed = Mathf.Clamp((path.maxSpeed + value), minMoveSpeed, Mathf.Infinity);
    }

    public void ResetSpeedModifiers()
    {
        path.maxSpeed = moveSpeed + globalSpeedModifier;
    }

    public void UpdateDamageModifierTower(LocalDamageModifier damageModifierTower)
    {
        if(this.damageModifierTower != null)
        {
            this.damageModifierTower.RemoveModifiedEnemy(this);

            /* Buff Modifier */
            if (this.damageModifierTower.Buff)
            {
                this.damageModifierTower.RemoveBuff(this);
            }
            /* Debuff Modifier */
            else if(this.damageModifierTower.Debuff)
            {
                this.damageModifierTower.RemoveDebuff(this);
            }
        }

        /* Update Tower */
        this.damageModifierTower = damageModifierTower;
        this.damageModifierTower.AddModifiedEnemy(this);
    }

    public void TakeDamage(float _damage) //is called when a projectile hits an enemy
    {
        damageDebuffMultiplier = Mathf.Clamp(damageDebuffMultiplier, minDamageMultiplier, 1.0f);
        damageBuffMultiplier = Mathf.Clamp(damageBuffMultiplier, 1.0f, maxDamageMultiplier);
        currentHealth -= _damage * (damageMultiplier + damageBuffMultiplier + damageDebuffMultiplier);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GameManager.Instance.WaveSpawner.RemoveEnemy(gameObject);
            GameManager.Instance.StatisticsManager.IncreaseKillCount();
            Destroy(gameObject);
        }
        else
        {
            animator.SetTrigger("isHurt");
        }
        if(healthBar != null) 
        {
            healthBar.fillAmount = currentHealth / enemyHealth;
        }
    }

    public void Die()
    {
        currentHealth = 0;
        GameManager.Instance.WaveSpawner.RemoveEnemy(gameObject);
        Destroy(gameObject);
    }

    //private void Update()
    //{
    //    if (GetComponent<AIPath>().reachedEndOfPath)
    //    {
    //        WaveSpawner.Instance.RemoveEnemy(gameObject);
    //        Destroy(gameObject);
    //    }
    //}
}
