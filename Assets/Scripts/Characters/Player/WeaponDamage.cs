using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public int maxDamage;
    public int currentDamage;
    public bool wMelee;
    public bool wRanged;
    public Transform Enemy;

    void Start()
    {
        UpdateDamage(maxDamage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<HealthManager>().DamageCharacter(currentDamage);
        }

        if (other.gameObject.CompareTag("Boss"))
        {
            other.gameObject.GetComponent<HealthManager>().DamageCharacter(currentDamage);
        }
    }

    public void UpdateDamage(int newDamage)
    {
        maxDamage = newDamage;
        currentDamage = maxDamage;
    }
}
