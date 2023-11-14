using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip shotHitSound, shotSound, jumpSound, enemyDeathSound, coinsSound, checkpointActivationSound;
    public static AudioSource source1, source2;

    // Start is called before the first frame update
    void Awake()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        source1 = audioSources[0];
        source2 = audioSources[1];
        shotHitSound = Resources.Load<AudioClip>("shotHit");
        shotSound = Resources.Load<AudioClip>("shot");
        jumpSound = Resources.Load<AudioClip>("jump");
        enemyDeathSound = Resources.Load<AudioClip>("enemyDeath");
        coinsSound = Resources.Load<AudioClip>("coins");
        checkpointActivationSound = Resources.Load<AudioClip>("checkpoint");
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "shot":
                source1.PlayOneShot(shotSound);
                break;
            case "shotHit":
                source1.PlayOneShot(shotHitSound);
                break;
            case "jump":
                source1.PlayOneShot(jumpSound);
                break;
            case "enemyDeath":
                source1.PlayOneShot(enemyDeathSound);
                break;
            case "coins":
                source1.PlayOneShot(coinsSound);
                break;
            case "checkpoint":
                source1.PlayOneShot(checkpointActivationSound);
                break;
        }
    }
}
