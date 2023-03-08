using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Abilities : MonoBehaviour
{
    public bool Tornado_Abilty;
    public bool ManaRecharge;
    public bool Spinjitzu;
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
    public float costRocks;

    [SerializeField] private ManaBar mBar;
    [SerializeField] private Attack_Movement atck;
    private Rigidbody Rig;
    public Animator Anim;
    public Transform Enemy;
    public string nextUuid;
    public static bool playerCreated;
    private CharacterStats CS;

    void Start()
    {
        CS = GetComponent<CharacterStats>();
        Anim = GetComponent<Animator>();
        Rig = GetComponent<Rigidbody>();
        atck = GetComponent<Attack_Movement>();

        playerCreated = true;
        Tornado_Abilty = false;
        Tornado.SetActive(false);
        Skeleton.SetActive(true);
        Geometry.SetActive(true);

        mBar.UpdateManaBar(maxMana, currentMana);
        UpdateMaxMana(maxMana);

        ManaRecharge = false;
    }

    void Update()
    {
        if(currentMana > 100)
        {
            currentMana = 100;
        }

        mBar.UpdateManaBar(maxMana, currentMana);

        Player_Atacking = atck.Attacking;

        //Activate the Tornado Ability
        if (Input.GetKeyDown(KeyCode.F) && Tornado_Abilty == false && currentMana > 0 && ManaRecharge == false && Player_Atacking == false && CS.level >= 5)
        {
            Anim.SetTrigger("Spinj1");
        }

        //Desactivate the Tornado Ability
        if (Input.GetKeyDown(KeyCode.F) && Tornado_Abilty == true)
        {
            Tornado_Abilty = false;
            TornadoSpinjitzu();
        }

        //Activate the Earth Attack and spends an amount of mana
        if (Input.GetKeyDown(KeyCode.G) && Tornado_Abilty == false && currentMana > 0 && ManaRecharge == false && Player_Atacking == false)
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

        //This means that if the mana reaches 0 or less, it starts to water itself automatically
        if (currentMana <= 0)
        {
            currentMana = 0;
            Tornado_Abilty = false;
            TornadoSpinjitzu();
            ManaRecharge = true;
        }

        if(Tornado_Abilty == false && ManaRecharge == true)
        {
            currentMana += rechargeMana * Time.deltaTime; 
        }

        //Desactivate mana recharge when the bar is full
        if (ManaRecharge == true && currentMana >= maxMana)
        {
            ManaRecharge = false;
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
