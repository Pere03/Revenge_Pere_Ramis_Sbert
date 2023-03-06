using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

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
    public int DC;
    public int ED;
    public int BD;

    public TextMeshProUGUI HP_P;
    public Image HP_I;
    public TextMeshProUGUI MN_P;
    public Image MN_I;
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
        LoadUserOptions();
    }
    void Update()
    {
        HP_P.text = HP_I.fillAmount*100 + "%";
        float Value = MN.currentMana;
        MN_P.text = string.Format("{0:#}%", Value);

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
            for (int i = 0; i < 1; i++)
            {
                GameOverM();
            }
        }
    }
    public void LoadUserOptions()
    {
        ED = PlayerPrefs.GetInt("Enemies_Defeated");
        BD = PlayerPrefs.GetInt("Boss_Defeated");
        DC = PlayerPrefs.GetInt("Death_Count");
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
        DC++;
        DataPersistence.sharedInstance.DeathC += DC;
        DataPersistence.sharedInstance.Data();
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

    public void SaveUserOptions()
    {
        DataPersistence.sharedInstance.EnemiesD = ED;
        DataPersistence.sharedInstance.BossD = BD;
        DataPersistence.sharedInstance.DeathC = DC;
        DataPersistence.sharedInstance.Data();
    }
}
