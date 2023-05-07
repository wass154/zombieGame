using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyFollow : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 5.0f;
    public float rotateSpeed = 5.0f;


    void Update()
    {
        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        transform.position += direction * moveSpeed * Time.deltaTime;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }
}
