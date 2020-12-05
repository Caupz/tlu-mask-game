using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    public float hp;
    public float maxhp = 5f;
    public HealthbarBehaviourScript healthBar;
    public bool goToNextSceneOnDeath = false;

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
            if(goToNextSceneOnDeath)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            Destroy(gameObject);
            SoundManagerScript.PlaySound("enemyDeath");
        }
        healthBar.SetHealth(hp, maxhp);
    }
}
