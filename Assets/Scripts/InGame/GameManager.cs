using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject StatsPanel;
    public GameObject GameOver;
    public string Paco;
    public GameObject Door;
    public GameObject Player;
    public HealthManager HP;
    public Player_Abilities MN;
    public CharacterStats LVL;
    public Vector3 startP;
    void Start()
    {
        startP = Player.transform.position;
        HP = Player.GetComponent<HealthManager>();
        MN = Player.GetComponent<Player_Abilities>();
        LVL = Player.GetComponent<CharacterStats>();
        PausePanel.SetActive(false);
        StatsPanel.SetActive(false);
        GameOver.SetActive(false);
        Time.timeScale = 1;
    }
    void Update()
    {
        

        if (PausePanel.activeInHierarchy == false && Input.GetKeyDown(KeyCode.P))
        {
            Open_Pause();
        } else if (PausePanel.activeInHierarchy == true && Input.GetKeyDown(KeyCode.P))
        {
            Close_Pause();
        }

        if (HP.currentHealth <= 0)
        {
            Player.transform.position = startP;
            GameOverM();
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

    public void ExitMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void GameOverM()
    {
        GameOver.SetActive(true);
        Time.timeScale = 0;
    }

    public void Restart()
    {
        LVL.exp = 0;
        LVL.level = 1;
        HP.currentHealth = HP.maxHealth;
        MN.currentMana = MN.maxMana;
        PausePanel.SetActive(false);
        StatsPanel.SetActive(false);
        GameOver.SetActive(false);
        Time.timeScale = 1;
        Player.transform.position = startP;
    }
}
