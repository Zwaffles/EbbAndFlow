using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModifier : MonoBehaviour
{
    [SerializeField] private float damageIncreaseMultiplier = 1;
    [SerializeField] private float damageDecreaseMultiplier = 1;

    [Header("Clamp Values")]
    [SerializeField] private float minDamageMultiplier = -0.1f;
    [SerializeField] private float maxDamageMultiplier = 5.0f;

    private Enemy enemy;
    private LocalDamageModifier activeDamageIncreaseModifier;
    private LocalDamageModifier activeDamageDecreaseModifier;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    private void ApplyDamageModifier(LocalDamageModifier localDamageModifier)
    {
        /* Stackable */
        if (localDamageModifier.DamageModifier.Stackable)
        {
            /* Damage Increase Modifier */
            if(localDamageModifier.DamageModifier.DamageModifierValue > 0)
            {
                damageIncreaseMultiplier += localDamageModifier.DamageModifier.DamageModifierValue;
            }
            /* Damage Decrease Modifier */
            else
            {
                damageDecreaseMultiplier += localDamageModifier.DamageModifier.DamageModifierValue;
            }
        }
        /* Not stackable */
        else
        {
            /* Damage Increase Modifier */
            if (localDamageModifier.DamageModifier.DamageModifierValue > 0)
            {
                /* No Active DamageIncreaseModifier */
                if (activeDamageIncreaseModifier ==  null)
                {
                    activeDamageIncreaseModifier = localDamageModifier;
                    damageIncreaseMultiplier += activeDamageIncreaseModifier.DamageModifier.DamageModifierValue;
                }
                /*  Active DamageIncreaseModifier */
                else
                {
                    /* Update DamageModifier ifs more effective */
                    if (localDamageModifier.DamageModifier.DamageModifierValue <= activeDamageIncreaseModifier.DamageModifier.DamageModifierValue)
                    {
                        damageIncreaseMultiplier -= activeDamageIncreaseModifier.DamageModifier.DamageModifierValue;
                        activeDamageIncreaseModifier = localDamageModifier;
                        damageIncreaseMultiplier += activeDamageIncreaseModifier.DamageModifier.DamageModifierValue;
                    }
                }
            }
            /* Damage Decrease Modifier */
            else
            {
                /* No Active DamageDecreaseModifier */
                if (activeDamageDecreaseModifier == null)
                {
                    activeDamageDecreaseModifier = localDamageModifier;
                    damageDecreaseMultiplier += activeDamageDecreaseModifier.DamageModifier.DamageModifierValue;
                }
                /* Active DamageDecreaseModifier */
                else
                {
                    /* Update DamageModifier ifs more effective */
                    if (localDamageModifier.DamageModifier.DamageModifierValue <= activeDamageDecreaseModifier.DamageModifier.DamageModifierValue)
                    {
                        damageDecreaseMultiplier -= activeDamageDecreaseModifier.DamageModifier.DamageModifierValue;
                        activeDamageDecreaseModifier = localDamageModifier;
                        damageDecreaseMultiplier += activeDamageDecreaseModifier.DamageModifier.DamageModifierValue;
                    }
                }
            }
        }
        UpdateDamageMultiplier();
    }

    private void RemoveDamageModifier(InfectedDamageModifier damageModifier)
    {
        /* Stackable */
        if (damageModifier.Stackable)
        {
            /* Damage Increase Modifier */
            if (damageModifier.DamageModifierValue > 0)
            {
                damageIncreaseMultiplier -= damageModifier.DamageModifierValue;
            }
            /* Damage Decrease Modifier */
            else
            {
                damageDecreaseMultiplier -= damageModifier.DamageModifierValue;
            }
        }
        /* Not stackable */
        else
        {
            if(activeDamageIncreaseModifier != null)
            {
                /* Damage Increase Modifier */
                if (damageModifier == activeDamageIncreaseModifier.DamageModifier)
                {
                    damageIncreaseMultiplier -= damageModifier.DamageModifierValue;
                    activeDamageIncreaseModifier = null;
                }
            }
            if(activeDamageDecreaseModifier != null)
            {
                /* Damage Decrease Modifier */
                if (damageModifier == activeDamageDecreaseModifier.DamageModifier)
                {
                    damageDecreaseMultiplier -= damageModifier.DamageModifierValue;
                    activeDamageDecreaseModifier = null;
                }
            }
        }
        UpdateDamageMultiplier();
    }

    private void UpdateDamageMultiplier()
    {
        float multiplier = damageIncreaseMultiplier * damageDecreaseMultiplier;
       enemy.DamageMultiplier = Mathf.Clamp(multiplier, minDamageMultiplier, maxDamageMultiplier);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("TowerRange"))
        {
            LocalDamageModifier localDamageModifier = other.gameObject.GetComponent<LocalDamageModifier>();
            if(localDamageModifier != null)
            {
                ApplyDamageModifier(localDamageModifier);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("TowerRange"))
        {
            if (activeDamageIncreaseModifier == null || activeDamageDecreaseModifier == null)
            {
                LocalDamageModifier localDamageModifier = other.gameObject.GetComponent<LocalDamageModifier>();
                if (localDamageModifier != null)
                {
                    ApplyDamageModifier(localDamageModifier);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        LocalDamageModifier localDamageModifier = other.gameObject.GetComponent<LocalDamageModifier>();
        if (localDamageModifier != null)
        {
            RemoveDamageModifier(localDamageModifier.DamageModifier);
        }
    }
}