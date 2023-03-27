using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyShoot : MonoBehaviour
{
    public AudioSource AudioShoot;
    [SerializeField]
    GameObject projectile;

    Transform target;

    [SerializeField]
    
    Transform shootPoint;
    
    [SerializeField]
    
    float turnspeed = 2;

    [SerializeField]
    Slider slider;

    float fireRate = 5f;

    
    BossController bossController = new BossController();
    private void Start ( )
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
       
        
    }
    private void Update ()
    {
        
        float distance = Vector3.Distance(transform.position, target.position);
        fireRate -= Time.deltaTime;
        
        Vector3 direction = transform.position - target.position;
        
        
        transform.rotation = Quaternion.Slerp(target.rotation, Quaternion.LookRotation(direction), turnspeed * Time.deltaTime);


        //Debug.Log(distance);
        
        if (distance > 5 && distance < 50 && slider.value >0 )
        {
            if (fireRate <= 0)
            {
                fireRate = 6f;
                Shoot();
                AudioShoot.Play();
            }

        }
    }

    void Shoot() 
    {   
        
        Instantiate (projectile,shootPoint.position,shootPoint.rotation);
        
    }
}

