﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject[] enemies;
    float randomX;
    Vector2 spawnPos;
    public float spawnRate = 2f;
    float nextSpawn = 0f;
    int randomIdx = 0;
    float selfX = 0f;
    public bool infiniteAmount = true;
    public int enemyCount = 10;
    public int delayFirstSpawn = 0;
    Transform fordon;
     
    // Start is called before the first frame update
    void Start()
    {
        selfX = gameObject.transform.position.x;
        fordon = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void GoToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Destroy(gameObject);
    }

    float previouseSecond = 0;
    // Update is called once per frame
    void Update()
    {
        if(delayFirstSpawn > 0 && Time.time > previouseSecond)
        {
            delayFirstSpawn--;
            previouseSecond = Time.time;
            return;
        }

        if(Vector2.Distance(fordon.position, transform.position) < 15)
        {
            return;
        }

        if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            OnNextSpawnTime();
        }
    }

    void OnNextSpawnTime()
    {
        if (!infiniteAmount)
        {
            enemyCount--;

            if (enemyCount <= 0)
            {
                if(HaveAllEnemiesBeenKilled())
                {
                    GoToNextScene();
                }
            }
        }

        if (enemyCount > 0 || infiniteAmount)
        {
            SpawnRandomEnemy();
        }
    }

    bool HaveAllEnemiesBeenKilled()
    {
        GameObject[] gameObjects = FindObjectsOfType<GameObject>();

        foreach(GameObject gameObject in gameObjects)
        {
            if(gameObject.tag == "EnemySpawner")
            {
                EnemySpawnerScript scriptObj = gameObject.GetComponent<EnemySpawnerScript>();

                if(scriptObj != null && scriptObj.enemyCount > 0)
                {
                    //Debug.Log("ScriptObj: " + scriptObj.enemyCount);
                    return false;
                }
            }
            else if (gameObject.tag == "Enemy")
            {
                //Debug.Log("There is obj with Enemy tag");
                return false;
            }
        }

        //Debug.Log("ALL ENEMIES KILLED");
        return true;
    }

    void SpawnRandomEnemy()
    {
        randomX = Random.Range(-1f, 1f);
        spawnPos = new Vector2(randomX + selfX, transform.position.y);
        Instantiate(PickRandomEnemy(), spawnPos, Quaternion.identity);
    }

    GameObject PickRandomEnemy()
    {
        randomIdx = Random.Range(0, enemies.Length);
        return enemies[randomIdx];
    }
}
