using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemys = collision.collider.GetComponents<EnemyHealth>();
        foreach(var enemy in enemys)
        {
            enemy.TakeHit(1f);
        }

        Destroy(gameObject);
    }
}
