using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeFloor : MonoBehaviour
{
    public string sceneName = "New scene name here";

    public bool isAutomatic;

    private bool manualEnter;

    public string uuid;

    void Update()
    {
        if (!isAutomatic && !manualEnter)
        {
            manualEnter = Input.GetKeyDown(KeyCode.E);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Teleport(other.name);
    }

    private void OnTriggerStay(Collider other)
    {
       Teleport(other.name);
    }

    public void Teleport(string objName)
    {
        if (objName == "Player")
        {
            if (isAutomatic || (!isAutomatic && manualEnter))
            {
                FindObjectOfType<Player_Abilities>().nextUuid = uuid;
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
