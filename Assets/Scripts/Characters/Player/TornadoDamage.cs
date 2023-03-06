using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoDamage : MonoBehaviour
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
        //UpdateTornadoDamage(maxDamage);

        currentDamage = maxDamage;
    }


    void Update()
    {
        if (wRanged == true && wMelee == false)
        {
            //transform.LookAt(Enemy);
        }
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
}
