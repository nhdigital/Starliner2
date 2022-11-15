using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource explosionAudioSource;
    [SerializeField] AudioSource levelUpSource;
    [SerializeField] AudioSource reloadSource;
    [SerializeField] AudioSource failedSource;
    [SerializeField] AudioClip rocketSFX;
    [SerializeField] AudioClip levelUpSFX;
    [SerializeField] AudioClip reloadSFX;
    [SerializeField] AudioClip blastAquiredSFX;
    
    

    public void PlaySFX(string sfxToPlay)
    {
        if (sfxToPlay == "Rocket")
        {
            audioSource.clip = rocketSFX;
        }

        if (sfxToPlay == "Reload")
        {
            audioSource.clip = reloadSFX;
        }

        if (sfxToPlay == "BlastAquired")
        {
            audioSource.clip = blastAquiredSFX;
        }
       
        audioSource.Play();
    }


    public void PlayExplosionSFX()
    {
        explosionAudioSource.Play();
    }


    public void PlayLevelUpSFX()
    {
        levelUpSource.Play();
    }

    public void PlayFailSFX()
    {
        failedSource.Play();
    }
   
}
