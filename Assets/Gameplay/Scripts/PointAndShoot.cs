using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndShoot : MonoBehaviour
{
    public GameObject crosshairs;
    public GameObject gun;
    private Vector3 target;
    public Fordon_move player;
    public GameObject bulletPrefab;
    public float bulletSpeed = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        crosshairs.transform.position = new Vector2(target.x, target.y);

        Vector3 difference = target - gun.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        if (!player.facingRight)
        {
            rotationZ += 180f;
        }

        gun.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        if(Input.GetMouseButtonDown(0))
        {
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            FireBullet(direction, rotationZ);
        }
    }

    void FireBullet(Vector2 direction, float rotationZ)
    {
        SoundManagerScript.PlaySound("fire");
        GameObject b = Instantiate(bulletPrefab) as GameObject;
        b.transform.position = gun.transform.position;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }
}
