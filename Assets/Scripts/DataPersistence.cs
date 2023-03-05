using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistence : MonoBehaviour
{
    public static DataPersistence sharedInstance;
    public int EnemiesD;
    public int BossD;
    public int DeathC;
    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
            DontDestroyOnLoad(sharedInstance);
        }
        else
        {
            Destroy(this);
        }
    }

    public void Data()
    {
        PlayerPrefs.SetInt("Enemies_Defeated", EnemiesD);
        PlayerPrefs.SetInt("Boss_Defeated", BossD);
        PlayerPrefs.SetInt("Death_Count", DeathC);
    }
}
