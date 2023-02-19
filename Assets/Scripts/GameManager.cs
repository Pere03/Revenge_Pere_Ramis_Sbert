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
    private Scene scene;
    public string Paco;
    void Start()
    {
        PausePanel.SetActive(false);
        StatsPanel.SetActive(false);
        Time.timeScale = 1;
        for (int i = 0; i < 3; i++)
        {
            Invoke("SpawnEnemys", 0.01f);
        }
    }

    
    void Update()
    {
        scene = SceneManager.GetActiveScene();

        Paco = scene.name;

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

    public Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-19,18), 1.539f, Random.Range(13,50));
    }

    public void SpawnEnemys()
    {
        if(scene.name == "Floor_1")
        {
            SpawnPosition = RandomSpawnPosition();
            Instantiate(enemyPrefabs[0], SpawnPosition, enemyPrefabs[0].transform.rotation);
        }
    }
}
