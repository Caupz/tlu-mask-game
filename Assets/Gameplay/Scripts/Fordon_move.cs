using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fordon_move : MonoBehaviour {
    public Animator animator;
	Rigidbody2D body;
    AudioSource audioSrc;

    float horizontal;
	float vertical;
	float moveLimiter = 0.7f;

	public float walkSpeed;
	public float energy;
	public float decreaseSpeed;
	public float sprintSpeed;
    public float maxHealth = 100f;
    public float regenAfterSecs = 3.0f;
    public float regenRate = 50f;
    public float enemyNearDamage = 0.15f;
    float health;
    public HealthbarBehaviourScript healthBar;

    public bool facingRight = true;
    public bool isMoving = false;
	
	void Start ()
	{
		body = GetComponent<Rigidbody2D>();
        health = maxHealth;
        healthBar.SetHealth(health, maxHealth);
        audioSrc = GetComponent<AudioSource>();
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

        if(health < 100f && Time.time > (lastCollison + regenAfterSecs))
        {
            RegenHealth();
        }

        if(health > 0)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                if (Vector2.Distance(transform.position, enemy.transform.position) < 1.0f)
                {
                    health -= enemyNearDamage;
                    healthBar.SetHealth(health, maxHealth);
                    lastCollison = Time.time;

                    if (health < 0)
                    {
                        OnPlayerDeath();
                    }
                }
            }
        }

        if (body.velocity.x != 0 || body.velocity.y != 0) 
        {
            isMoving = true;
        }
        else
            isMoving = false;

        if (isMoving)
        {
            if (!audioSrc.isPlaying)
                audioSrc.Play();
        }
        else
            audioSrc.Stop();
    }

    void RegenHealth()
    {
        health += (Time.deltaTime * regenRate);

        if(health > maxHealth)
        {
            health = maxHealth;
        }

        healthBar.SetHealth(health, maxHealth);
    }

	void FixedUpdate()
	{
		if (horizontal != 0 && vertical != 0) // Check for diagonal movement
		{
			// limit movement speed diagonally, so you move at 70% speed
			horizontal *= moveLimiter;
			vertical *= moveLimiter;
		}

        float speed = horizontal * walkSpeed;
        float vspeed = vertical * walkSpeed;
        float totalSpeed = Mathf.Abs(speed + vspeed);
		body.velocity = new Vector2(speed, vspeed);
        animator.SetFloat("Speed", totalSpeed);

    }
    
	void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    float lastCollison = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (health > 0 && collision.gameObject.tag == "Enemy")
        {
            lastCollison = Time.time;
            health -= 1.0f;
            healthBar.SetHealth(health, maxHealth);
        }
        else if(health > 0 && collision.gameObject.tag == "EnemyBullet")
        {
            lastCollison = Time.time;
            health -= 4.0f;
            healthBar.SetHealth(health, maxHealth);
        }

        if(health < 0)
        {
            OnPlayerDeath();
        }
    }

    private void OnPlayerDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //Debug.Log("OnPlayerDeath");
    }
}