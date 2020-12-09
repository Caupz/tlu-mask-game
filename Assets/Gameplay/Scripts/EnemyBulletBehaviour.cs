using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehaviour : MonoBehaviour
{
    float timeOfDeath;
    private Vector3 target;

    void Start()
    {
        timeOfDeath = Time.time + 1f;
    }

    void Update()
    {
        GameObject[] ignores = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject ignore in ignores)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), ignore.GetComponent<Collider2D>());
        }

        if (Time.time > timeOfDeath)
        {
            //Debug.Log("Vaenlase kuuli aeg sai otsa");
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // TODO et võtaks playerilt elusid maha.
            //Debug.Log("Tabas mängijat");
            Destroy(gameObject);
        }
    }
}
