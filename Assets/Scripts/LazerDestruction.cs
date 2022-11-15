using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerDestruction : MonoBehaviour
{

    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject ammoExplosion;
    [SerializeField] GameObject blastExplosion;
    [SerializeField] AudioSource destroyedPowerUp;
    [SerializeField] AudioSource destroyedAmmo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Vector3 pos = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
            var explosionInstance = Instantiate(explosion, pos, gameObject.transform.rotation);
            gameManager.IncreaseScore();
            Debug.Log("Explosion Created");
            Destroy(explosionInstance, 1);
            Debug.Log("Explosion Destroyed.");
        }

        if (other.gameObject.CompareTag("AmmoResupply"))
        {
            Destroy(other.gameObject);
            destroyedAmmo.Play();
            Vector3 ammoPos = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
            var ammoExplosionInstance = Instantiate(ammoExplosion, ammoPos, gameObject.transform.rotation);
            Debug.Log("AMMO EXPLODED");
            Destroy(ammoExplosionInstance, 1);
        }

        if (other.gameObject.CompareTag("Blast"))
        {
            Destroy(other.gameObject);
            destroyedPowerUp.Play();
            Vector3 blastPos = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
            var blastExplosionInstance = Instantiate(blastExplosion, blastPos, gameObject.transform.rotation);
            Debug.Log("AMMO EXPLODED");
            Destroy(blastExplosionInstance, 1);
        }
    }

   

}
