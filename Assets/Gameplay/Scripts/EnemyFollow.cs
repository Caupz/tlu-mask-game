using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed;
    public Transform target;
    public float stoppingDist = 3;
    public bool isFacingRight = false;
    bool prevIsFacingRight = false;
    public bool canJump = false;
    public float jumpRate;

    float jumpCooldown = 0;
    float prevSec = 0;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        jumpCooldown = Time.time + jumpRate;
    }

    void OnSecondUpdate()
    {
        jumpCooldown--;

        if (jumpCooldown <= 0 && canJump)
        {
            jumpCooldown = Time.time + jumpRate;
            Jump();
        }
    }

    bool jumping = false;
    float jumpingDuration = 1f;

    void Jump()
    {
        jumping = true;
        jumpingDuration = 0.5f;
    }

    void ProcessJumping(Vector3 pos)
    {
        if(jumpingDuration > 0)
        {
            jumpingDuration -= Time.deltaTime;
        }

        if (jumpingDuration < 0.15f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, (speed * 4.5f) * Time.deltaTime);
        }
        else if (jumpingDuration < 0.25f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, (speed * 2.5f) * Time.deltaTime);
        }
        else if (jumpingDuration < 0.35f)
        {
            Vector3 targetPos = new Vector3(target.position.x, target.position.y, target.position.z);
            targetPos.y += 2.5f;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, (speed * 6.0f) * Time.deltaTime);
        }

        if (jumpingDuration <= 0)
        {
            jumping = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > prevSec)
        {
            prevSec = Time.time + 1;
            OnSecondUpdate();
        }

        if(jumping)
        {
            ProcessJumping(transform.position);
        }
        else
        {
            Walk();
        }

        
    }

    void Walk()
    {
        if (Vector2.Distance(transform.position, target.position) > stoppingDist)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            if (transform.position.x > target.position.x)
            {
                isFacingRight = false;
            }
            else
            {
                isFacingRight = true;
            }

            if (prevIsFacingRight != isFacingRight)
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
