using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    [SerializeField] private HealthBar healthBar;
    public int expWhenDefeated;
    public static HealthManager sharedInstance;
    public int DE;
    public int DC;
    public int DB;

    private void Awake()
    {
       
    }

    void Start()
    {
        UpdateMaxHealth(maxHealth);
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
    }

    public void DamageCharacter(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            //This means that, if we do damage to an enemy and its life reaches 0 or less, it adds experience to the player and increases the counter of defeated enemies
            if (gameObject.tag.Equals("Enemy"))
            {
                GameObject.Find("Player").GetComponent<CharacterStats>().AddExperience(expWhenDefeated);
                DE++;
                DataPersistence.sharedInstance.EnemiesD += DE;
                DataPersistence.sharedInstance.Data();
                gameObject.SetActive(false);
            }

            //This means that, if we do damage to an boss and its life reaches 0 or less, it adds experience to the player and increases the counter of defeated boss
            if (gameObject.tag.Equals("Boss"))
            {
                 DB++;
                DataPersistence.sharedInstance.BossD += DB;
                DataPersistence.sharedInstance.Data();
                gameObject.SetActive(false);
            }

            //This means that, if the player's life reaches 0 or less, then the death counter increases
            if (gameObject.tag.Equals("Player"))
            {
                DC++;
                DataPersistence.sharedInstance.DeathC += DC;
                DataPersistence.sharedInstance.Data();
            }

        }
    }

    public void UpdateMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth;
    }
}
