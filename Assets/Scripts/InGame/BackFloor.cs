using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackFloor : MonoBehaviour
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
            if (scene.name == "Floor_2")
            {
                SceneManager.LoadScene("Floor_1");
            }

            if (scene.name == "Floor_3")
            {
                SceneManager.LoadScene("Floor_2");
            }
        }
    }
}
