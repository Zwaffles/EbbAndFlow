using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] protected Transform turret;
    [SerializeField] public float fireRate = 1.0f;
    public int baseCost = 100;
    public int sellPrice = 75;
    protected float cooldown;
}
