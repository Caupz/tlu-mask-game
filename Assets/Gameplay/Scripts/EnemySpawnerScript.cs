using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject enemy;
    float randomX;
    Vector2 spawnPos;
    public float spawnRate = 2f;
    float nextSpawn = 0f;
     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            randomX = Random.Range(-1f, 1f);
            spawnPos = new Vector2(randomX, transform.position.y);
            Instantiate(enemy, spawnPos, Quaternion.identity);
        }
    }
}
