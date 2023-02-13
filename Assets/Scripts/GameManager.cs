using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject StatsPanel;
    void Start()
    {
        PausePanel.SetActive(false);
        StatsPanel.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (PausePanel.activeInHierarchy == false && Input.GetKeyDown(KeyCode.P))
        {
            Open_Pause();
        }
    }

    public void Open_Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Close_Pause()
    {
        PausePanel.SetActive(false);
        StatsPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Open_Stats()
    {
        StatsPanel.SetActive(true);
    }

    public void Close_Stats()
    {
        StatsPanel.SetActive(false);
    }
}
