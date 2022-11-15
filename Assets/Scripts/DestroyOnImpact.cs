using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DestroyOnImpact : MonoBehaviour
{

    GameManager gameManager;
    SFXManager sfxManager;
    [SerializeField] GameObject explosion;
   

    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        sfxManager = GameObject.Find("SFXManager").GetComponent<SFXManager>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(CreateAndDestroyExplosion());
            Destroy(other.gameObject);
            Destroy(gameObject);
            gameManager.IncreaseScore();
        }
        else if (other.gameObject.CompareTag("Barrier"))
        {
            Destroy(gameObject);
            gameManager.GameOver();
        }
        else if (other.gameObject.CompareTag("AmmoResupply"))
        {
            Destroy(other.gameObject);
            StartCoroutine(CreateAndDestroyExplosion());
            sfxManager.PlayFailSFX();
        }
        else if (other.gameObject.CompareTag("Blast"))
        {
            Destroy(other.gameObject);
            StartCoroutine(CreateAndDestroyExplosion());
            sfxManager.PlayFailSFX();
        }
    }


    IEnumerator CreateAndDestroyExplosion()
    {
        Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        var explosionInstance = Instantiate(explosion, pos, gameObject.transform.rotation);
        Debug.Log("Explosion Created");
        Destroy(explosionInstance, 1);
        Debug.Log("Explosion Destroyed.");
        yield return new WaitForSeconds(0);
    }
}
