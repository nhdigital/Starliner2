using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
   
    void Update()
    {
        DestroyCanonBall();
        DestroyEnemy();
    }


    void DestroyCanonBall()
    {
        if (transform.position.z > 7.0f)
        {
            Destroy(gameObject);
        }
    }


    void DestroyEnemy()
    {
        if (transform.position.z < -4f)
        {
            Destroy(gameObject);
        }

    }
}
