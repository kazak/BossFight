using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]
    
    float damage = 10f;
    
    Rigidbody rb;

    [SerializeField]
    float speed = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Transform target = GameObject.FindGameObjectWithTag("Player").transform;

        Vector3 direction = target.position;
        
        rb.AddForce(direction * speed * Time.deltaTime);
    }
}
