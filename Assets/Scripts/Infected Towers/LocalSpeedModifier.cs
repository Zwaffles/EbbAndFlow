using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalSpeedModifier : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private InfectedSpeedModifier infectedSpeedModifier;
    [SerializeField] private List<Enemy> modifiedEnemies = new List<Enemy>();

    public void UpdateSpeedModifier(InfectedSpeedModifier infectedSpeedModifier)
    {
        this.infectedSpeedModifier = infectedSpeedModifier;
    }

    private void ApplySpeedModifier(Enemy enemy)
    {
        if (infectedSpeedModifier.Stackable)
        {
            enemy.SpeedDebuff = true;
            enemy.ModifyMoveSpeed(infectedSpeedModifier.SpeedModifier);
        }
        else if (!enemy.SpeedDebuff)
        {
            enemy.SpeedDebuff = true;
            if(infectedSpeedModifier != null)
            {
                modifiedEnemies.Add(enemy);
                enemy.ModifyMoveSpeed(infectedSpeedModifier.SpeedModifier);
            }
        }
    }

    private void RemoveSpeedModifier(Enemy enemy)
    {
        if (infectedSpeedModifier.Stackable)
        {
            enemy.ModifyMoveSpeed(-infectedSpeedModifier.SpeedModifier);
            enemy.SpeedDebuff = false;
        }
        else if(modifiedEnemies.Contains(enemy))
        {
            enemy.ModifyMoveSpeed(-infectedSpeedModifier.SpeedModifier);
            modifiedEnemies.Remove(enemy);
            enemy.SpeedDebuff = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            ApplySpeedModifier(other.gameObject.GetComponent<Enemy>());
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!infectedSpeedModifier.Stackable)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                ApplySpeedModifier(other.gameObject.GetComponent<Enemy>());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            RemoveSpeedModifier(other.gameObject.GetComponent<Enemy>());
        }
    }
}