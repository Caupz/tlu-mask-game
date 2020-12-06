using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip characterHitSound, shootSound, enemyDeathSound, hairThrowSound;
    static AudioSource audioSrc;

    void Start()
    {
        characterHitSound = Resources.Load<AudioClip>("Sounds/SFX/PlayerHit");
        shootSound = Resources.Load<AudioClip>("Sounds/SFX/MaskCannonFire");
        enemyDeathSound = Resources.Load<AudioClip>("Sounds/SFX/DeathSound");
        hairThrowSound = Resources.Load<AudioClip>("Sounds/SFX/AirWoosh");

        audioSrc = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "fire":
                audioSrc.PlayOneShot(shootSound);
                break;
            case "charHit":
                audioSrc.PlayOneShot(characterHitSound);
                break;
            case "enemyDeath":
                audioSrc.PlayOneShot(enemyDeathSound);
                break;
            case "throwHair":
                audioSrc.PlayOneShot(hairThrowSound);
                break;
        }
    }
}
