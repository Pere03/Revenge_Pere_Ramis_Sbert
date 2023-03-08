using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject Controlls;
    public GameObject Achievements;
    public int EnemiesDefeated;
    public int BossDefeated;
    public int DeathCount;
    public TextMeshProUGUI ED;
    public TextMeshProUGUI BD;
    public TextMeshProUGUI DC;
    public GameObject musicMenu;
    void Start()
    {
        musicMenu.SetActive(true);
        Controlls.SetActive(false);
        Achievements.SetActive(false);
        LoadUserOptions();
    }

    // Update is called once per frame
    void Update()
    {
        SaveUserOptions();
    }

    public void Play()
    {
        musicMenu.SetActive(false);
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

    public void OpenAchievements()
    {
        Achievements.SetActive(true);
    }

    public void CloseAchievements()
    {
        Achievements.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void SaveUserOptions()
    {
        DataPersistence.sharedInstance.EnemiesD = EnemiesDefeated;
        DataPersistence.sharedInstance.BossD = BossDefeated;
        DataPersistence.sharedInstance.DeathC = DeathCount;
        DataPersistence.sharedInstance.Data();
    }
    public void LoadUserOptions()
    {
        EnemiesDefeated = PlayerPrefs.GetInt("Enemies_Defeated");
        ED.text = EnemiesDefeated.ToString();
        BossDefeated = PlayerPrefs.GetInt("Boss_Defeated");
        BD.text = BossDefeated.ToString();
        DeathCount = PlayerPrefs.GetInt("Death_Count");
        DC.text = DeathCount.ToString();
    }
}
