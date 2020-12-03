using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fordon_move : MonoBehaviour {
	
	Rigidbody2D body;

	float horizontal;
	float vertical;
	float moveLimiter = 0.7f;

	public float walkSpeed;
	public float energy;
	public float decreaseSpeed;
	public float sprintSpeed;

	public bool facingRight = true;
	
	void Start ()
	{
		body = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
		vertical = Input.GetAxisRaw("Vertical"); // -1 is down
		
		if(Input.GetKey(KeyCode.LeftShift) && energy > 0){
			// Reduce energy by decreaseSpeed per second
			energy -= decreaseSpeed * Time.deltaTime;

			// if needed avoid negative value
			energy = Mathf.Max(0, energy);

			// double the move distance
			walkSpeed = sprintSpeed;
		} else {
			walkSpeed = 5.0f;
		}

		
		if(facingRight == false && horizontal > 0)
        {
            Flip();
        }
        else if(facingRight == true && horizontal < 0)
        {
            Flip();
        }
	}

	void FixedUpdate()
	{
		if (horizontal != 0 && vertical != 0) // Check for diagonal movement
		{
			// limit movement speed diagonally, so you move at 70% speed
			horizontal *= moveLimiter;
			vertical *= moveLimiter;
		} 

		body.velocity = new Vector2(horizontal * walkSpeed, vertical * walkSpeed);
	}
    
	void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

    }

    void OnCollisionEnter2D(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}