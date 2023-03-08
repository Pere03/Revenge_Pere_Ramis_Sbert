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
    public GameObject Camera;
    public EnemyIA[] enemiesS;
    public string Paco;
    public GameObject Door;
    public GameObject Player;
    private HealthManager HP;
    private Player_Abilities MN;
    private CharacterStats LVL;
    public Vector3 startP;
    private int DC;
    private int ED;
    private int BD;
    public GameObject AS;
    public GameObject FB;
    public TextMeshProUGUI HP_P;
    public Image HP_I;
    public TextMeshProUGUI MN_P;
    public TextMeshProUGUI FloorT;
    public GameObject CameraCollider;

    public GameObject canvas;
    void Start()
    {
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
        enemiesS = FindObjectsOfType<EnemyIA>();
        foreach (EnemyIA enemie in enemiesS)
        {
            enemie.enabled = true;
        }

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
           GameOverM();
        }

        //This means that depending on which room we are in, it will show us the name of the room on the screen
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Floor_1")
        {
            AS.SetActive(true);
            FB.SetActive(false);
            FloorT.text = "Floor 1 (Easy)";
            Player.SetActive(true);
            canvas.SetActive(true);
            Camera.SetActive(true);
        }
        else if (scene.name == "Floor_2")
        {
            FloorT.text = "Floor 2 (Medium)";
            Player.SetActive(true);
        }
        else if (scene.name == "Floor_3")
        {
            FloorT.text = "Floor 3 (Hard)";
            Player.SetActive(true);
        }
        else if (scene.name == "Floor_4")
        {
            FloorT.text = "Rest Floor";
            Player.SetActive(true);
        }
        else if (scene.name == "Floor_5")
        {
            FloorT.text = "Final Boss";
            Player.SetActive(true);
            AS.SetActive(false);
            FB.SetActive(true);
        }
        else if (scene.name == "Menu")
        {
            Player.SetActive(false);
            canvas.SetActive(false);
            Camera.SetActive(false);
            AS.SetActive(false);
            FB.SetActive(false);
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
        AS.SetActive(false);
        FB.SetActive(false);
        Time.timeScale = 1;
        Player.transform.position = new Vector3(0, 0.349f, 15.3f);
        Player.transform.rotation = Quaternion.Euler(0, 0, 0);
        LVL.exp = 0;
        LVL.level = 1;
        HP.maxHealth = 200;
        HP.currentHealth = HP.maxHealth;
        MN.maxMana = 100;
        MN.currentMana = MN.maxMana;
        
        GameOver.SetActive(false);
        PausePanel.SetActive(false);
        SceneManager.LoadScene("Menu");
        Player.SetActive(false);
    }

    public void GameOverM()
    {
        GameOver.SetActive(true);
        //Player.SetActive(false);
        Time.timeScale = 0;
    }

    public void Restart()
    {
        
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
