using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Fire : MonoBehaviour
{
    public GameManager gameManager;
    public int ammunition;
    [SerializeField] GameObject canonBall;
    [SerializeField] Transform canon;
    [SerializeField] TextMeshProUGUI ammunitionLabel;
    [SerializeField] SFXManager sfxManager;
    
  
  
   
    void Start()
    {
        
        ammunition = 100;
        ammunitionLabel.text = "Ammo: " + ammunition.ToString();
    }

    
    void Update()
    {
        if (gameManager.isGameActive)
        {
        
            FireCanon();
        }
    }


    void FireCanon()
    {
            if ((Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) && ammunition > 0)
            {
                Instantiate(canonBall, canon.position, transform.rotation);
                sfxManager.PlaySFX("Rocket");
                ammunition--;
                ammunitionLabel.text = "Ammo: " + ammunition.ToString();
            }   
    }


    public void UpdateAmmo(int ammoIncrease)
    {
        ammunition += ammoIncrease;
        ammunitionLabel.text = "Ammo: " + ammunition.ToString();
    }



}

