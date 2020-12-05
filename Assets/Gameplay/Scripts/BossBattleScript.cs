using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleScript : MonoBehaviour
{
    public float bulletSpeed = 20f;
    public bool burstFire = false;
    public float projectileRate = 3f;
    public GameObject bulletPrefab;

    float nextProjectile = 0;
    float previousSecond = 0;
    EnemyFollow enemyFollow;
    Vector3 target;
    GameObject player;
    Collider2D playerCollision;
    
    void Start()
    {
        nextProjectile = projectileRate;
        previousSecond = Time.time + 1;
        enemyFollow = gameObject.GetComponent<EnemyFollow>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollision = player.GetComponent<Collider2D>();
    }
    
    void Update()
    {
        if(Time.time > previousSecond)
        {
            nextProjectile--;
            previousSecond = Time.time + 1;

            if(nextProjectile <= 0)
            {
                target = playerCollision.transform.position;

                Vector3 difference = target - transform.position;
                float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

                if (!enemyFollow.isFacingRight)
                {
                    rotationZ += 180f;
                }

                float distance = difference.magnitude;
                Vector2 direction = difference / distance;
                direction.Normalize();
                FireBullet(direction, rotationZ);

                nextProjectile = projectileRate;
            }
        }
    }

    void FireBullet(Vector2 direction, float rotationZ)
    {
        SoundManagerScript.PlaySound("fire");
        GameObject b = Instantiate(bulletPrefab) as GameObject;
        Physics2D.IgnoreCollision(b.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        b.transform.position = transform.position;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }
}
