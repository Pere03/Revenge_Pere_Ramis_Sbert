using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalBoss : MonoBehaviour
{
    public int Boss;
    public GameObject paco;
    void Start()
    {
        
    }

  
    void Update()
    {
        Boss = FindObjectsOfType<EnemyIA>().Length;
        //When the boss is killed, it will send us to the Win scene.
        if (Boss <= 0)
        {
            SceneManager.LoadScene("Win");
        }
    }
}
