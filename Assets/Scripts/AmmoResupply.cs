using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AmmoResupply : MonoBehaviour
{

    private Fire fireScript;
    private SFXManager sfxManager;
    private PlayerController playerScript;
    [SerializeField] int addedAmmo = 50;


    private void Awake()
    {
        fireScript = GameObject.Find("Player").GetComponent<Fire>();
        sfxManager = GameObject.Find("SFXManager").GetComponent<SFXManager>();
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            sfxManager.PlaySFX("Reload");
            Destroy(gameObject);
            fireScript.UpdateAmmo(addedAmmo);
            playerScript.StartCoroutine("displayAmmoPlayerLabel");
            Debug.Log("Added 50 ammo.");  
        }
    }
}

