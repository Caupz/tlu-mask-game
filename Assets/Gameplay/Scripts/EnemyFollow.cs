using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Animator animator;
    public float speed;
    public Transform target;
    public float stoppingDist = 3;
    public bool isFacingRight = false;
    bool prevIsFacingRight = false;
    public bool canJump = false;
    public float jumpRate;
    public float ZigZagChangingInterval = 3f;

    float jumpCooldown = 0;
    float prevSec = 0;
    bool moveUp = false;
    bool collidedWithOthers = false;
    bool ZigZagUp = false;
    float ZigZagChange;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        jumpCooldown = Time.time + jumpRate;
        moveUp = Random.Range(0, 2) == 1 ? true : false;
        ZigZagChange = ZigZagChangingInterval;
    }

    void SetJumping(bool isJumping)
    {
        if (animator != null)
        {
            animator.SetBool("Jumping", isJumping);
        }
    }

    void OnSecondUpdate()
    {
        jumpCooldown--;

        if (Time.time >= jumpCooldown && canJump)
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
        SetJumping(true);
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
            SetJumping(false);
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

        ZigZagChange -= Time.deltaTime;

        if(ZigZagChange <= 0)
        {
            ZigZagChange = ZigZagChangingInterval;
            ZigZagUp = !ZigZagUp;
        }
    }

    float prevX = 0;
    float speedMult = 1f;

    void Walk()
    {
        if (Vector2.Distance(transform.position, target.position) > stoppingDist)
        {
            Vector3 targetPos = new Vector3(target.position.x, target.position.y, target.position.z);
            
            if(collidedWithOthers)
            {
                collidedWithOthers = false;
                targetPos.x = prevX;
                speedMult = 3f;

                if (moveUp)
                {
                    targetPos.y += 100.0f;
                }
                else
                {
                    targetPos.y -= 100.0f;
                }
            }
            else
            {
                prevX = transform.position.x;
                
                if (ZigZagUp)
                {
                    targetPos.y += 1f;
                }
                else
                {
                    targetPos.y -= 1f;
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * speedMult * Time.deltaTime);
            speedMult = 1f;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // TODO et võtaks playerilt elusid maha.
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            collidedWithOthers = true;
        }
    }
}
