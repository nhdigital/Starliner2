using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpwards : MonoBehaviour
{

    [SerializeField] float projectileSpeed = 9.0f;

    void LateUpdate()
    {
        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime , Space.Self);
    }
}
