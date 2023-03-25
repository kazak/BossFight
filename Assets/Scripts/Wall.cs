using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Wall: MonoBehaviour
{
    
    public float Speed = 0.11f;

    private void Update()
    {
        if (transform.position.x >= 10)
        {
            Speed = -Speed;
        }
        if (transform.position.x <= -10)
        {
            Speed = -Speed;
        }
        Vector3 input = new Vector3(1, 0, 0);
        transform.position = transform.position + input * Time.deltaTime * Speed;
    }

    private void FixedUpdate()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {      
        Destroy(GameObject.FindGameObjectWithTag("bullet"));
    }
    
}