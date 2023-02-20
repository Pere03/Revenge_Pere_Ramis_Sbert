using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverPoints : MonoBehaviour
{
    [SerializeField] private HealthManager HP;
    [SerializeField] private Player_Abilities MN;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Weapon;
    public int RHealth;
    public int RMana;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Player = GameObject.FindWithTag("Player");
        HP = Player.GetComponent<HealthManager>();
        MN = Player.GetComponent<Player_Abilities>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(HP.currentHealth < HP.maxHealth)
            {
                HP.currentHealth += RHealth;
            }

            if(MN.currentMana < MN.maxMana)
            {
                MN.currentMana += RMana;
            }
        }
    }

}
