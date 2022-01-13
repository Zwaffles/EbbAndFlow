using System.Collections.Generic;
using UnityEngine;

public class LocalDamageModifier : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private InfectedDamageModifier infectedDamageModifier;
    [SerializeField] private List<Enemy> modifiedEnemies = new List<Enemy>();

    public bool Buff;
    public bool Debuff;

    public void UpdateDamageModifier(InfectedDamageModifier infectedDamageModifier)
    {
        this.infectedDamageModifier = infectedDamageModifier;
    }

    private void ApplyDamageModifier(Enemy enemy)
    {
        /* If stackable always add DamageModifier */
        if (infectedDamageModifier.Stackable)
        {
            modifiedEnemies.Add(enemy);
            /* Damage Buff (Damage Reduced) */
            if(infectedDamageModifier.DamageModifier > 0)
            {
                ApplyBuff(enemy);
            }
            /* Damage Debuff (Damage Increased) */
            else
            {
                ApplyDebuff(enemy);
            }
        }
        /* If NOT Stackable*/
        else
        {
            /* Damage Buff (Damage Reduced) */
            if (infectedDamageModifier.DamageModifier > 0)
            {
                /* Apply Buff if none is applied */
                if (!enemy.DamageBuff)
                {
                    ApplyBuff(enemy);
                    enemy.DamageModifierTower = this;
                    modifiedEnemies.Add(enemy);
                }
                /* Update Buff if its more effective */
                else if(enemy.DamageBuffMultiplier > 1.0f + infectedDamageModifier.DamageModifier)
                {
                    enemy.UpdateDamageModifierTower(this);
                    ApplyBuff(enemy);
                }
            }
            /* Damage Debuff (Damage Increased) */
            else
            {
                /* Apply Buff if none is applied */
                if (!enemy.DamageDebuff)
                {
                    ApplyDebuff(enemy);
                    enemy.DamageModifierTower = this;
                    modifiedEnemies.Add(enemy);
                }
                /* Update Debuff if its more effective */
                else if (enemy.DamageDebuffMultiplier > 1.0f + infectedDamageModifier.DamageModifier)
                {
                    enemy.UpdateDamageModifierTower(this);
                    ApplyDebuff(enemy);
                }
            }
        }
    }

    public void ApplyBuff(Enemy enemy)
    {
        Buff = true;
        enemy.DamageBuff = true;
        enemy.DamageBuffMultiplier += infectedDamageModifier.DamageModifier;
    }

    public void ApplyDebuff(Enemy enemy)
    {
        Debuff = true;
        enemy.DamageDebuff = true;
        enemy.DamageDebuffMultiplier += infectedDamageModifier.DamageModifier;
    }

    public void RemoveBuff(Enemy enemy)
    {
        Buff = false;
        enemy.DamageBuffMultiplier -= infectedDamageModifier.DamageModifier;
        enemy.DamageBuff = true;
    }

    public void RemoveDebuff(Enemy enemy)
    {
        Debuff = false;
        enemy.DamageDebuffMultiplier -= infectedDamageModifier.DamageModifier;
        enemy.DamageDebuff = true;
    }

    private void RemoveDamageModifier(Enemy enemy)
    {
        if (modifiedEnemies.Contains(enemy))
        {
            /* Damage Buff (Damage Reduced) */
            if (infectedDamageModifier.DamageModifier > 0)
            {
                RemoveBuff(enemy);
            }
            /* Damage Debuff (Damage Increased) */
            else
            {
                RemoveDebuff(enemy);
            }
        }
        modifiedEnemies.Remove(enemy);
    }

    public void AddModifiedEnemy(Enemy enemy)
    {
        modifiedEnemies.Add(enemy);
    }

    public void RemoveModifiedEnemy(Enemy enemy)
    {
        modifiedEnemies.Remove(enemy);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            ApplyDamageModifier(other.gameObject.GetComponent<Enemy>());
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!infectedDamageModifier.Stackable)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                ApplyDamageModifier(other.gameObject.GetComponent<Enemy>());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            RemoveDamageModifier(other.gameObject.GetComponent<Enemy>());
        }
    }
}