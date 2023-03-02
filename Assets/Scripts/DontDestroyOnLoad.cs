using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();

        if(scene.name != "Menu")
        {
            if (!Player_Abilities.playerCreated)
            {
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
