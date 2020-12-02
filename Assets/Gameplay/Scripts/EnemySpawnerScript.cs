using System.Collections;
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
     
    // Start is called before the first frame update
    void Start()
    {
        selfX = gameObject.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextSpawn)
        {
            if(!infiniteAmount)
            {
                enemyCount--;

                if(enemyCount <= 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    Destroy(gameObject);
                }
            }

            nextSpawn = Time.time + spawnRate;
            randomX = Random.Range(-1f, 1f);
            spawnPos = new Vector2(randomX + selfX, transform.position.y);

            Instantiate(PickRandomEnemy(), spawnPos, Quaternion.identity);
        }
    }

    GameObject PickRandomEnemy()
    {
        randomIdx = Random.Range(0, enemies.Length);
        return enemies[randomIdx];
    }
}
