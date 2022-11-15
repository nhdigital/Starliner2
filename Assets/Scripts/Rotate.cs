using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    [SerializeField] private Vector3 rotation;
    [SerializeField] float speed;
   
    void FixedUpdate()
    {
        transform.Rotate(rotation * Time.deltaTime * speed);
    }
}
