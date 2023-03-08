using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamage : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private GameObject Img;
    public int damage = 10;
    public Animator Img_Anim;

    private void Update()
    {
        Img = GameObject.FindGameObjectWithTag("Player_Img");
        Img_Anim = Img.GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision other)
    {
        //This means that when he enters with the "player", he will do damage.
        if (other.gameObject.name.Equals("Player"))
        {
            other.gameObject.GetComponent<HealthManager>().DamageCharacter(damage);
            Img_Anim.SetTrigger("Img_Damage");
        }
    }
}
