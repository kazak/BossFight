using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Wall: MonoBehaviour
{
    public float StartPos;
    public float EndPos;
    public float Speed = 0.11f;
    //≈сли true - по оси X 
    public bool AxisXZ; 
    public AudioSource AudioSource;

    private void Update()
    {   if (AxisXZ == true) //движение по оси X
        {
            if (transform.position.x >= StartPos)
            {
                Speed = -Speed;
            }
            if (transform.position.x <= EndPos)
            {
                Speed = -Speed;
            }
            Vector3 input = new Vector3(1, 0, 0);
            transform.position = transform.position + input * Time.deltaTime * Speed;
        }
        if (AxisXZ == false) //  движение по оси Z
        {
            if (transform.position.z >= StartPos)
            {
                Speed = -Speed;
            }
            if (transform.position.z <= EndPos)
            {
                Speed = -Speed;
            }
            Vector3 input = new Vector3(0, 0, 1);
            transform.position = transform.position + input * Time.deltaTime * Speed;
        }   
    }

    private void FixedUpdate()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {      
        Destroy(GameObject.FindGameObjectWithTag("bullet"));
        AudioSource.Play();
    }
    
}