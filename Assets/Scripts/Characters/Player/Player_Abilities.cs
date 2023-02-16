using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Abilities : MonoBehaviour
{
    public bool Tornado_Abilty;
    public bool ManaRecharge;
    public bool Spinjitzu;
    public bool Shuriken_Attack;
    public bool Shuriken_M_Attack;
    public bool Player_Atacking;
    public GameObject Tornado;
    public GameObject Skeleton;
    public GameObject Geometry;
    public GameObject Shuriken_Obj;
    public ParticleSystem Bombastic;
    public float maxMana = 100;
    public float currentMana;
    public float costMana;
    public float rechargeMana;
    public float costShuriken;
    public float costRocks;

    [SerializeField] private ManaBar mBar;
    [SerializeField] private Attack_Movement atck;
    private Rigidbody Rig;
    public Animator Anim;

    public Transform Enemy;

    void Start()
    {
        Anim = GetComponent<Animator>();
        Rig = GetComponent<Rigidbody>();
        atck = GetComponent<Attack_Movement>();
        Tornado_Abilty = false;
        Tornado.SetActive(false);
        Skeleton.SetActive(true);
        Geometry.SetActive(true);

        mBar.UpdateManaBar(maxMana, currentMana);
        UpdateMaxMana(maxMana);

        ManaRecharge = false;

        Enemy = GameObject.FindWithTag("Enemy").transform;
    }

    // Update is called once per frame
    void Update()
    {

        mBar.UpdateManaBar(maxMana, currentMana);

        Player_Atacking = atck.Attacking;

        //Tornado Ability
        if (Input.GetKeyDown(KeyCode.F) && Tornado_Abilty == false && currentMana > 0 && ManaRecharge == false && Player_Atacking == false)
        {
            //Anim.SetBool("Spinjitzu",true);
            Anim.SetTrigger("Spinj1");
        }

        if (Input.GetKeyDown(KeyCode.F) && Tornado_Abilty == true)
        {
            //Anim.SetBool("Spinjitzu", false);
            Tornado_Abilty = false;
            TornadoSpinjitzu();
        }

        if(Input.GetKeyDown(KeyCode.G) && Tornado_Abilty == false && currentMana > 0 && ManaRecharge == false && Player_Atacking == false)
        {
            Vector3 offsete = new Vector3(0, 0, 1.5f);
            var insta = Instantiate(Bombastic, transform.position + offsete, transform.rotation);
            insta.Play();

            currentMana -= costRocks;
        }

        if (Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && Anim.GetCurrentAnimatorStateInfo(0).IsName("Spinjitzu"))
        {
            Tornado_Abilty = true;
            TornadoSpinjitzu();
        }

        if (Tornado_Abilty == true)
        {
            currentMana -= costMana * Time.deltaTime;
        }

        if (currentMana <= 0)
        {
            Tornado_Abilty = false;
            TornadoSpinjitzu();
            ManaRecharge = true;
        }

        if(Tornado_Abilty == false && ManaRecharge == true)
        {
            currentMana += rechargeMana * Time.deltaTime; 
        }

        //Mana Recharge
        if (ManaRecharge == true && currentMana >= maxMana)
        {
            ManaRecharge = false;
        }


        //Shuriken Normal Attack
        if (Input.GetKeyDown(KeyCode.R) && ManaRecharge == false && Tornado_Abilty == false && Player_Atacking == false)
        {
            Shuriken_Attack = true;
            ShurikenAttack();
        }
        else
        {
            Shuriken_Attack = false;
            ShurikenAttack();
        }

        if(Shuriken_Attack == true)
        {
            currentMana -= costShuriken;
        }

        //Multiple Shuriken Attack
        if (Input.GetKeyDown(KeyCode.R) && ManaRecharge == false && Tornado_Abilty == true && Player_Atacking == false)
        {
            Shuriken_M_Attack = true;
            ShurikenMultipleAttack();
        }

    }

    public void TornadoSpinjitzu()
    {
        if (Tornado_Abilty == true)
        {
            Tornado.SetActive(true);
            Skeleton.SetActive(false);
            Geometry.SetActive(false);
        }
        else if (Tornado_Abilty == false)
        {
            Tornado.SetActive(false);
            Skeleton.SetActive(true);
            Geometry.SetActive(true);
        }
    }

    public void ShurikenAttack()
    {
        if(Shuriken_Attack == true)
        {
            Rigidbody rb = Instantiate(Shuriken_Obj, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 20f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            //transform.LookAt(Enemy);
        }
    }

    public void ShurikenMultipleAttack()
    {
        if(Shuriken_M_Attack == true)
        {
            Rigidbody rb1 = Instantiate(Shuriken_Obj, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb1.AddForce(transform.forward * 20f, ForceMode.Impulse);
            rb1.AddForce(transform.up * 8f, ForceMode.Impulse);

            Rigidbody rb2 = Instantiate(Shuriken_Obj, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb2.AddForce(-transform.forward * 20f, ForceMode.Impulse);
            rb2.AddForce(transform.up * 8f, ForceMode.Impulse);

            Rigidbody rb3 = Instantiate(Shuriken_Obj, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb3.AddForce(transform.right * 20f, ForceMode.Impulse);
            rb3.AddForce(transform.up * 8f, ForceMode.Impulse);

            Rigidbody rb4 = Instantiate(Shuriken_Obj, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb4.AddForce(-transform.right * 20f, ForceMode.Impulse);
            rb4.AddForce(transform.up * 8f, ForceMode.Impulse);
        }
    }

    public void UpdateMaxMana(float newMaxMana)
    {
        maxMana = newMaxMana;
        currentMana = maxMana;
    }

    public void ManaRecharging()
    {
        ManaRecharge = true;

        if(ManaRecharge == true && currentMana <= 0)
        {
            currentMana += costMana * Time.deltaTime; ;
        }
    }
}
