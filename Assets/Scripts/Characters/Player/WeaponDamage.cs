using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public int damage;
    //public GameObject bloodParticle;
    public int index;
    public bool wMelee;
    public bool wRanged;
    public Transform Enemy;

    void Start()
    {
        Enemy = GameObject.FindWithTag("Enemy").transform;
    }

    
    void Update()
    {
        if(wRanged == true && wMelee == false)
        {
            //transform.LookAt(Enemy);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<HealthManager>().DamageCharacter(damage);

            /*
            if (bloodParticle != null && hitPoint != null)
            {
                Instantiate(bloodParticle, hitPoint.transform.position, hitPoint.transform.rotation);
            }
            */
        }
    }

    public void UpdateDamage(int newDamage)
    {
        damage = newDamage;
    }
}
