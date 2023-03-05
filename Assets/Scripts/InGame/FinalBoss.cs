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

    // Update is called once per frame
    void Update()
    {
        Boss = FindObjectsOfType<EnemyIA>().Length;

        if(Boss <= 0)
        {
            SceneManager.LoadScene("Win");
        }
    }
}
