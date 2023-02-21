using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextFloor : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerStay(Collider other)
    {
        Scene scene = SceneManager.GetActiveScene();

        if (other.gameObject.CompareTag("Player"))
        {
            if(scene.name == "Floor_1")
            {
                SceneManager.LoadScene("Floor_2");
            }

            if (scene.name == "Floor_2")
            {
                SceneManager.LoadScene("Floor_3");
            }
        }
    }
}
