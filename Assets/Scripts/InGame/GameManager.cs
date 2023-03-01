using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject StatsPanel;
    public GameObject[] enemyPrefabs;
    public Vector3 SpawnPosition;
    public string Paco;
    public GameObject Door;
    public int EnemiesCount;
    public int NumberEnemiesFloor;
    void Start()
    {
        Door = GameObject.FindWithTag("Door_block");
        //PausePanel = GameObject.FindWithTag("Pause_Panel");
        //StatsPanel = GameObject.FindWithTag("Stats_Panel");
        //Door.SetActive(true);
        PausePanel.SetActive(false);
        StatsPanel.SetActive(false);
        Time.timeScale = 1;

        Scene scene = SceneManager.GetActiveScene();
        Paco = scene.name;

        if (scene.name == "Floor_1")
        {
            NumberEnemiesFloor = 3;
        }
        else if (scene.name == "Floor_2")
        {
            NumberEnemiesFloor = 5;
        }
        else if (scene.name == "Floor_3")
        {
            NumberEnemiesFloor = 7;
        }

        for (int i = 0; i < NumberEnemiesFloor; i++)
        {
            Invoke("SpawnEnemys", 0.01f);
        }

    }
    void Update()
    {
        EnemiesCount = FindObjectsOfType<EnemyIA>().Length;
        //Door = GameObject.FindWithTag("Door_block");

        if (PausePanel.activeInHierarchy == false && Input.GetKeyDown(KeyCode.P))
        {
            Open_Pause();
        }

        if(EnemiesCount > 0)
        {
            Door.SetActive(true);
        }
        else
        {
            Door.SetActive(false);
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

    public Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-19,18), 1.8f, Random.Range(13,50));
    }

    public void SpawnEnemys()
    {
        Scene scene = SceneManager.GetActiveScene();
        SpawnPosition = RandomSpawnPosition();
        if(scene.name == "Floor_1")
        {
            Instantiate(enemyPrefabs[0], SpawnPosition, enemyPrefabs[0].transform.rotation);
        }
        else if (scene.name == "Floor_2")
        {
            Instantiate(enemyPrefabs[1], SpawnPosition, enemyPrefabs[0].transform.rotation);
        }
    }
}
