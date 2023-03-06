using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraCollision : MonoBehaviour
{
    public GameObject CameraCollider;
    public Vector3 Position;
    void Start()
    {
        CameraCollider = GameObject.FindWithTag("CameraCollider");
    }

    // Update is called once per frame
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Floor_1")
        {
            Position = new Vector3(-0.45f, 11.79f, 31.5f);
            CameraCollider.transform.position = Position;
        }
        else if (scene.name == "Floor_2")
        {
            Position = new Vector3(-0.45f, 11.79f, 66.5f);
            CameraCollider.transform.position = Position;
        }
        else if (scene.name == "Floor_3")
        {
            Position = new Vector3(-0.45f, 11.79f, 100f);
            CameraCollider.transform.position = Position;
        }
        else if (scene.name == "Floor_4")
        {
            Position = new Vector3(-0.45f, 11.79f, 134.47f);
            CameraCollider.transform.position = Position;
        }
        else if (scene.name == "Floor_5")
        {
            Position = new Vector3(-0.45f, 11.79f, 169.57f);
            CameraCollider.transform.position = Position;
        }
    }
}
