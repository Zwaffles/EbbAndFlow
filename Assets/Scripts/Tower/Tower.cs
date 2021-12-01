using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] protected Transform turret;
    [SerializeField] private int baseCost = 100;
    [SerializeField] public float fireRate = 1.0f;
    protected float cooldown;
}
