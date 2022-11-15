using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{

    
    private float horizontalSpeed = 10.0f;
    private float xBoundary = 4.2f;
    [SerializeField] GameManager gameManager;
    [SerializeField] float horizontal;
    [SerializeField] float vertical;
    [SerializeField] AudioSource audioSource;  
    [SerializeField] GameObject ammoPlayerLabel;

 

    void FixedUpdate()
    {
        MovePlayer();
        RestrictBoundaries();
    }

    
    void MovePlayer()
    {
        if (gameManager.isGameActive)
        {
            audioSource.Play();
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            transform.Translate(Vector3.right * horizontal * Time.deltaTime * horizontalSpeed, Space.World);
        }
    }


    void RestrictBoundaries()
    {
        if (transform.position.x < -xBoundary)
        {
            transform.position = new Vector3(-xBoundary, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > 4.2f)
        {
            transform.position = new Vector3(xBoundary, transform.position.y, transform.position.z);
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            gameManager.GameOver();
        }
    }


    public IEnumerator displayAmmoPlayerLabel()
    {
        ammoPlayerLabel.SetActive(true);
        Debug.Log("Text Active.");
        yield return new WaitForSeconds(2);
        ammoPlayerLabel.SetActive(false);
        Debug.Log("Text inactive.");
    }



}
