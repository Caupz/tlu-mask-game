using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public Fordon_move player;
    float timeOfDeath;

    void Start()
    {
        timeOfDeath = Time.time + 1f;
    }

    void Update()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player.GetComponent<Collider2D>());

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
