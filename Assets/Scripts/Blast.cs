using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour
{
    [SerializeField] SpawnManager spawnManager;
    [SerializeField] GameManager gameManager;
    [SerializeField] SFXManager sfxManager;
    [SerializeField] GameObject blastAquiredText;
    [SerializeField] GameObject lazerField;
    [SerializeField] Animator lazerAnim;
    [SerializeField] AudioSource lazerAudioSource;
    [SerializeField] bool hasLazerField;


    void Update()
    {
        FireBlast();
    }


    void FireBlast()
    {
        if (hasLazerField == true && gameManager.isGameActive)
        {
           if (Input.GetButtonDown("Blast") || Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.Mouse1))
            {
                lazerAudioSource.Play();
                lazerAnim.SetTrigger("lazerActive");
                blastAquiredText.SetActive(false);
                hasLazerField = false;
                Debug.Log("Blast used.");
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Blast"))
        {
            hasLazerField = true;
            sfxManager.PlaySFX("BlastAquired");
            blastAquiredText.SetActive(true);
            Destroy(other.gameObject);
            Debug.Log("Has blast.");
        }
    }

}
