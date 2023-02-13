using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoDamage : MonoBehaviour
{
    public int damage;
    public int index;

    public void UpdateDamage(int newDamage)
    {
        damage = newDamage;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<HealthManager>().
            DamageCharacter(damage);
        }
    }
}
