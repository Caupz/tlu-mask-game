using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PointAndShoot : MonoBehaviour
{
    public GameObject crosshairs;
    public GameObject gun;
    private Vector3 target;
    public Fordon_move player;
    public GameObject bulletPrefab;
    public float bulletSpeed = 30.0f;
    public bool inAccurate = false;

    float randomX;
    float randomY;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        Vector3 bullettarget = new Vector3(target.x, target.y, target.z);

        if (inAccurate)
        {
            randomX = Random.Range(0f, 1.5f);
            randomY = Random.Range(0f, 1.5f);
            int randomOption = Random.Range(0, 4);

            switch (randomOption)
            {
                case 0:
                    {
                        bullettarget.x -= randomX;
                        bullettarget.y -= randomY;
                        break;
                    }
                case 1:
                    {
                        bullettarget.x += randomX;
                        bullettarget.y += randomY;
                        break;
                    }
                case 2:
                    {
                        bullettarget.x -= randomX;
                        bullettarget.y += randomY;
                        break;
                    }
                default:
                    {
                        bullettarget.x += randomX;
                        bullettarget.y -= randomY;
                        break;
                    }
            }
        }

        crosshairs.transform.position = new Vector2(target.x, target.y);

        Vector3 difference = target - gun.transform.position;
        Vector3 bulletDifference = bullettarget - gun.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        float rotationZBullet = Mathf.Atan2(bulletDifference.y, bulletDifference.x) * Mathf.Rad2Deg;

        if (!player.facingRight)
        {
            rotationZ += 180f;
            rotationZBullet += 180f;
        }

        gun.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        if(Input.GetMouseButtonDown(0))
        {
            float distance = bulletDifference.magnitude;
            Vector2 direction = bulletDifference / distance;
            direction.Normalize();
            crosshairs.transform.position = new Vector2(bullettarget.x, bullettarget.y);
            gun.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZBullet);
            FireBullet(direction, rotationZBullet);
        }
    }

    void FireBullet(Vector2 direction, float rotationZ)
    {
        SoundManagerScript.PlaySound("fire");
        GameObject b = Instantiate(bulletPrefab) as GameObject;
        Collider2D[] collideComponents = player.GetComponents<Collider2D>();

        foreach (Collider2D collideComponent in collideComponents)
        {
            Physics2D.IgnoreCollision(b.GetComponent<Collider2D>(), collideComponent);
        }
        
        b.transform.position = gun.transform.position;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }
}
