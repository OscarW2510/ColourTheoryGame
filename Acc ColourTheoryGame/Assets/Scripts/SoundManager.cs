using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip jumpSound, deathSound, runningSound, fireballSound;
    static AudioSource audioSrc;
    void Start()
    {
        jumpSound = Resources.Load<AudioClip>("jump");
        runningSound = Resources.Load<AudioClip>("running");
        deathSound = Resources.Load<AudioClip>("death");
        fireballSound = Resources.Load<AudioClip>("fireball");


        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "jump":
                audioSrc.PlayOneShot(jumpSound);
                break;
            case "death":
                audioSrc.PlayOneShot(deathSound);
                break;
            case "running":
                audioSrc.PlayOneShot(runningSound);
                break;
            case "fireball":
                audioSrc.PlayOneShot(fireballSound);
                break;
        }
    }
}
