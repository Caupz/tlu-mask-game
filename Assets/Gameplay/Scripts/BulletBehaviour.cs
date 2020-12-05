using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    float timeOfDeath;
    Collider2D collideComponent;
    Collider2D playerCollision;

    void Start()
    {
        timeOfDeath = Time.time + 1f;
        collideComponent = gameObject.GetComponent<Collider2D>();
        playerCollision = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
    }

    void Update()
    {
        Physics2D.IgnoreCollision(collideComponent, playerCollision);

        if(Time.time > timeOfDeath)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemys = collision.collider.GetComponents<EnemyHealth>();
        foreach(var enemy in enemys)
        {
            enemy.TakeHit(1f);
        }

        if(collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
