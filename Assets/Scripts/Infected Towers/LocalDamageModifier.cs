using System.Collections.Generic;
using UnityEngine;

public class LocalDamageModifier : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private InfectedDamageModifier damageModifier;

    public InfectedDamageModifier DamageModifier { get { return damageModifier; } }

    public void UpdateDamageModifier(InfectedDamageModifier infectedDamageModifier)
    {
        this.damageModifier = infectedDamageModifier;
    }
}