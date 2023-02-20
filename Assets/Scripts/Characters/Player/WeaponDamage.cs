using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public int maxDamage;
    public int currentDamage;
    //public GameObject bloodParticle;
    public bool wMelee;
    public bool wRanged;
    public Transform Enemy;

    void Start()
    {
        //Enemy = GameObject.FindWithTag("Enemy").transform;
        UpdateDamage(maxDamage);
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
            other.gameObject.GetComponent<HealthManager>().DamageCharacter(currentDamage);

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
        maxDamage = newDamage;
        currentDamage = maxDamage;
    }
}
