using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies;
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject ammoResupply;
    [SerializeField] GameObject blastPowerUp;
    [SerializeField] float startDelay = 10.0f;
    [SerializeField] float lowAmmoStartDelay = 15.0f;
    [SerializeField] float highAmmoStartDelay = 25.0f;
    [SerializeField] float lowAmmoSpawnTime = 16.0f;
    [SerializeField] float highAmmoSpawnTime = 27.0f;
    [SerializeField] float lowBlastStartDelay = 17.0f;
    [SerializeField] float highBlastStartDelay = 29.0f;
    [SerializeField] float lowBlastSpawnTime = 35.0f;
    [SerializeField] float highBlastSpawnTime = 50.0f;
    [SerializeField] float spawnTime = 2.0f;
    [SerializeField] float one = 1.0f;
    [SerializeField] float difficultyIncrease = 0.01f;
    [SerializeField] float spawnPointZ = 5.1f;
    [SerializeField] float spawnPointY = 0.2f;
    [SerializeField] float xSpawnRange = 4.3f;



    public void StartSpawning()
    {
        Invoke("SpawnEnemy", startDelay / (one + (gameManager.difficulty * difficultyIncrease)));
        InvokeRepeating("SpawnAmmoResupply",Random.Range(lowAmmoStartDelay, highAmmoStartDelay), Random.Range(lowAmmoSpawnTime, highAmmoSpawnTime));
        InvokeRepeating("SpawnBlastPowerUp", Random.Range(lowBlastStartDelay, highBlastStartDelay), Random.Range(lowBlastSpawnTime, highBlastSpawnTime));
    }

    
    void SpawnEnemy()
    {
        if (gameManager.isGameActive)
        {
            int index = Random.Range(0, enemies.Length);
            float randomSpawnPointX = Random.Range(-xSpawnRange, xSpawnRange);
            Vector3 spawnPoint = new Vector3(randomSpawnPointX, spawnPointY, spawnPointZ);
            Instantiate(enemies[index], spawnPoint, gameObject.transform.rotation);
            Invoke("SpawnEnemy", spawnTime / gameManager.difficulty);
        }
    }


    void SpawnAmmoResupply()
    {
        if (gameManager.isGameActive)
        {
           
            float randomAmmoSpawnPointX = Random.Range(-xSpawnRange, xSpawnRange);
            Vector3 ammoSpawnPoint = new Vector3(randomAmmoSpawnPointX, spawnPointY, spawnPointZ);
            Instantiate(ammoResupply, ammoSpawnPoint, gameObject.transform.rotation);
        }
    }


    void  SpawnBlastPowerUp()
    {
        if (gameManager.isGameActive)
        {
            
            float randomBlastSpawnPointX = Random.Range(-xSpawnRange, xSpawnRange);
            Vector3 blastSpawnPoint = new Vector3(randomBlastSpawnPointX, spawnPointY, spawnPointZ);
            Instantiate(blastPowerUp, blastSpawnPoint, gameObject.transform.rotation);
        }
    }

   
    public void DestroyAllPrefabs()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }

        var ammo = GameObject.FindGameObjectsWithTag("AmmoResupply");
        foreach (var ammunition in ammo)
        {
            Destroy(ammunition);
        }

        var blast = GameObject.FindGameObjectsWithTag("Blast");
        foreach (var powerUp in blast)
        {
            Destroy(powerUp);
        }
    }
}
