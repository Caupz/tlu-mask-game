using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float hp;
    public float maxhp = 5f;
    public HealthbarBehaviourScript healthBar;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxhp;
        healthBar.SetHealth(hp, maxhp);
    }

    public void TakeHit(float damage)
    {
        hp -= damage;
        SoundManagerScript.PlaySound("charHit");

        if (hp <= 0)
        {
            Destroy(gameObject);
            SoundManagerScript.PlaySound("enemyDeath");
        }
        healthBar.SetHealth(hp, maxhp);
    }
}
