using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Movement : MonoBehaviour
{
    private Animator anim;
    public float cooldownTime = 2f;
    private float nextFireTime = 0f;
    public int noOfClicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = 1;

    public AudioSource Audios;
    public AudioClip AuSlash;
    public AudioClip AuKick;

    public bool Attacking;

    public ParticleSystem Fuego_H1;
    public ParticleSystem Fuego_H2;
    public ParticleSystem Fuego_H3;
    public ParticleSystem Fuego_H4;

    [SerializeField] private BoxCollider box;

    public bool Tornado_On;
    public Player_Abilities PAbilty;
    void Start()
    {
        anim = GetComponent<Animator>();
        Audios = GetComponent<AudioSource>();
        PAbilty = GetComponent<Player_Abilities>();
        box.enabled = false;
        //box = GetComponentInChildren<BoxCollider>();
    }

    void Update()
    {
        Tornado_On = PAbilty.Tornado_Abilty;

        if (Tornado_On == false)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.3 && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit1"))
            {
                anim.SetBool("Hit1", false);
            }

            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.3 && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit2"))
            {
                anim.SetBool("Hit2", false);
            }

            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.3 && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit3"))
            {
                anim.SetBool("Hit3", false);
            }

            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.3 && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit4"))
            {
                anim.SetBool("Hit4", false);
                noOfClicks = 0;
            }

            if (Time.time - lastClickedTime > maxComboDelay)
            {
                noOfClicks = 0;
                box.enabled = false;
                Attacking = false;
            }

            if (Time.time > nextFireTime)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    OnClick();
                }
            }
        }
    }

    void OnClick()
    {
        if(Tornado_On == false)
        {
            lastClickedTime = Time.time;
            noOfClicks++;

            if (noOfClicks == 1)
            {
                anim.SetBool("Hit1", true);
                Audios.PlayOneShot(AuSlash);
                box.enabled = true;
                Attacking = true;
                if (anim.GetBool("Hit1") == true )
                {
                    Vector3 offsete = new Vector3(0, 1.2f, 0.5f);
                    var insta = Instantiate(Fuego_H1, transform.position + offsete, Fuego_H1.transform.rotation);
                    insta.Play();
                }
            }
            noOfClicks = Mathf.Clamp(noOfClicks, 0, 4);

            if (noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5 && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit1"))
            {
                anim.SetBool("Hit1", false);
                anim.SetBool("Hit2", true);
                Audios.PlayOneShot(AuSlash);
                box.enabled = true;
                Attacking = true;

                if (anim.GetBool("Hit2") == true)
                {
                    Vector3 offsete = new Vector3(0, 1.2f, 0.5f);
                    var insta = Instantiate(Fuego_H2, transform.position + offsete, Fuego_H1.transform.rotation);
                    insta.Play();
                }
            }

            if (noOfClicks >= 3 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5 && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit2"))
            {
                anim.SetBool("Hit2", false);
                anim.SetBool("Hit3", true);
                Audios.PlayOneShot(AuKick);
                box.enabled = true;
                Attacking = true;
            }

            if (noOfClicks >= 4 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5 && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit3"))
            {
                anim.SetBool("Hit3", false);
                anim.SetBool("Hit4", true);
                Audios.PlayOneShot(AuSlash);
                box.enabled = true;
                Attacking = true;
            }
        }
    }
}
