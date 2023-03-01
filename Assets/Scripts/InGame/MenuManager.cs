using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject Controlls;
    void Start()
    {
        Controlls.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene("Floor_1");
    }

    public void OpenControlls()
    {
        Controlls.SetActive(true);
    }

    public void CloseControlls()
    {
        Controlls.SetActive(false);
    }
}
