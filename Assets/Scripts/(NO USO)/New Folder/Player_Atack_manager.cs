using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Atack_manager : MonoBehaviour
{
    public Animator animPlayer;

    public Transform pivotWeapon;

    public BoxCollider colliderWeapon;

    public GameObject objWeapon;

    void Start()
    {
        animPlayer = GetComponent<Animator>();

        objWeapon = pivotWeapon.GetChild(0).gameObject;

        colliderWeapon = objWeapon.GetComponent<BoxCollider>();

        colliderWeapon.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Attack();
        }
    }

    private void Attack()
    {
        animPlayer.SetBool("IsAttacking", true);
    }


    public void AttackStart()
    {
        colliderWeapon.enabled = true;
    }

    public void AttackEnd()
    {
        colliderWeapon.enabled = false;
    }
}
