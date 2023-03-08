using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyInvoke : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Vector3 SpawnPosition;
    public int EnemiesCount;
    public int NumberEnemiesFloor;
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();

        //This means that depending on which room we are in, a certain number of enemies will spawn us
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

    // Update is called once per frame
    void Update()
    {
        EnemiesCount = FindObjectsOfType<EnemyIA>().Length;
    }

    public Vector3 RandomSpawnPosition1()
    {
        return new Vector3(Random.Range(-19, 18), 1.8f, Random.Range(13, 50));
    }
    public Vector3 RandomSpawnPosition2()
    {
        return new Vector3(Random.Range(-19, 18), 1.8f, Random.Range(50, 84));
    }
    public Vector3 RandomSpawnPosition3()
    {
        return new Vector3(Random.Range(-19, 18), 1.8f, Random.Range(81, 118));
    }


    public void SpawnEnemys()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Floor_1")
        {
            SpawnPosition = RandomSpawnPosition1();
            Instantiate(enemyPrefabs[0], SpawnPosition, enemyPrefabs[0].transform.rotation);
        }
        else if (scene.name == "Floor_2")
        {
            SpawnPosition = RandomSpawnPosition2();
            Instantiate(enemyPrefabs[1], SpawnPosition, enemyPrefabs[1].transform.rotation);
        }
        else if (scene.name == "Floor_3")
        {
            SpawnPosition = RandomSpawnPosition3();
            Instantiate(enemyPrefabs[2], SpawnPosition, enemyPrefabs[2].transform.rotation);
        }
    }
}
