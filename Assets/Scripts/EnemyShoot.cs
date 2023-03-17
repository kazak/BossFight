using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField]
    GameObject projectile;

    Transform target;

    [SerializeField]
    
    Transform shootPoint;
    
    [SerializeField]
    
    float turnspeed = 2;
    private Animator anim;
    float fireRate = 0.2f;
    private void Start ( )
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }
    private void Update ()
    {
        fireRate -= Time.deltaTime;
        
        Vector3 direction = transform.position - target.position;
        
        
        transform.rotation = Quaternion.Slerp(target.rotation, Quaternion.LookRotation(direction), turnspeed * Time.deltaTime);
        
        if ( fireRate <= 0 )
        {
            fireRate = 2f;
            Shoot();
        }
       
    }
    void Shoot() 
    { 
        Instantiate (projectile,shootPoint.position,shootPoint.rotation);
    }
}

