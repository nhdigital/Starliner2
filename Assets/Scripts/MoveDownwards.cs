using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDownwards : MonoBehaviour
{ 
    [SerializeField] float speed;

    void FixedUpdate()
    {
        MoveDownwardsEnemy();
    }

    void MoveDownwardsEnemy()
    {
        transform.Translate(Vector3.back * Time.deltaTime * speed , Space.World);
    }
}
