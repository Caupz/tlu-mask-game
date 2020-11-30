using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed;
    public Transform target;
    public float stoppingDist = 3;
    bool isFacingRight = false;
    bool prevIsFacingRight = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, target.position) > stoppingDist)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            if(transform.position.x > target.position.x)
            {
                isFacingRight = false;
            }
            else
            {
                isFacingRight = true;
            }

            if(prevIsFacingRight != isFacingRight)
            {
                Flip();
            }

            prevIsFacingRight = isFacingRight;
        }
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
